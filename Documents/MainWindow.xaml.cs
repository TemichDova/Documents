using Documents.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Documents
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    //docCon.Type_docs.Load();
    //Type_doc_vh.ItemsSource = docCon.Type_docs.ToList();
    // this.DataContext = docCon.Type_docs.ToList();
    public partial class MainWindow : Window
    {

        private static int save_doc = -1;
        private static Object objDoc;
        public static Object AppDoc;
        DocContext docCon;
        ObservableCollection<Form_doc> FormContent;
        ObservableCollection<Type_doc> TypeContent;
        ObservableCollection<Characteristic_doc> CharacteristicContent;
        ObservableCollection<Form_pr> Form_Prs;
        ObservableCollection<Razdel> Razdel_doc;
        public MainWindow(List<Authorization_BD> login_author)//
        {
            InitializeComponent();

            docCon = new DocContext();
            docCon.Doc_vh.Load(); // загружаем данные входящих
            docCon.Doc_iz.Load(); // загружаем данные изходящих
            docCon.appeal_Gr.Load();
            docCon.prikaze_1.Load();
            docCon.prikaze_2.Load();
            docCon.prikaze_3.Load();
            DocGridPr.ItemsSource = docCon.prikaze_1.Local.ToBindingList();
            DocGridVH.ItemsSource = docCon.Doc_vh.Local.ToBindingList(); // Добавляем в DataGrid входящие
            DocGridIZ.ItemsSource = docCon.Doc_iz.Local.ToBindingList(); // Добавляем в DataGrid изходящие
            PrimGrid.ItemsSource = docCon.appeal_Gr.Local.ToBindingList();
            FormContent = new ObservableCollection<Form_doc>();
            TypeContent = new ObservableCollection<Type_doc>();
            CharacteristicContent = new ObservableCollection<Characteristic_doc>();
            Form_Prs = new ObservableCollection<Form_pr>();
            Razdel_doc = new ObservableCollection<Razdel>();
            foreach (Form_doc a in docCon.Form_Docs) FormContent.Add(a);            
            foreach (Type_doc t in docCon.Type_docs) TypeContent.Add(t);
            foreach (Characteristic_doc c in docCon.Character_doc) CharacteristicContent.Add(c);
            foreach (Form_pr f in docCon.from_Pr) Form_Prs.Add(f);
            foreach (Razdel r in docCon.Razdels) Razdel_doc.Add(r);
            Form_doc_vh.ItemsSource = FormContent; 
            Type_doc_vh.ItemsSource = TypeContent;
            Id_form_doc_iz_combobox.ItemsSource = FormContent;
            Id_type_doc_iz_combobox.ItemsSource = TypeContent;
            Id_characteristic_doc_iz_combobox.ItemsSource = CharacteristicContent;
            Form_prCombobox.ItemsSource = Form_Prs;
            Secrch_prCombobo.ItemsSource = Form_Prs;
            SerchComboBoxVH.ItemsSource = FormContent;
            Razel_docComboBox.ItemsSource = Razdel_doc;
            Secrch_prCombobo.SelectedIndex = 0;
            List<string> griph_pr = new List<string>() { "Н/С","ДСП" };
            GriphPr.ItemsSource = griph_pr;
            //var kkk = new ObservableCollection<string> { "iPhone 6S Plus", "Nexus 6P", "Galaxy S7 Edge" };
            
            if (login_author != null)
            {
                if (login_author[0].Id_user == 1)//admin
                {
                    Reg_doc_vh.Visibility = Visibility.Visible;
                    Del_doc_vh.Visibility = Visibility.Visible;
                    Save_doc_vh_and_New.Visibility = Visibility.Visible;
                    Reg_doc_iz.Visibility = Visibility.Visible;
                    Del_doc_iz.Visibility = Visibility.Visible;
                    Save_doc_iz_and_New.Visibility = Visibility.Visible;
                    Del_doc_Pr.Visibility = Visibility.Visible;
                    SavePrAndNew.Visibility = Visibility.Visible;
                    Reg_Pr.Visibility = Visibility.Visible;
                    Reg_doc_vh.IsEnabled = true;
                    Del_doc_vh.IsEnabled = true;
                    Save_doc_vh_and_New.IsEnabled = true;
                    Reg_doc_iz.IsEnabled = true;
                    Del_doc_iz.IsEnabled = true;
                    Save_doc_iz_and_New.IsEnabled = true;
                    Del_doc_Pr.IsEnabled = true;
                    SavePrAndNew.IsEnabled = true;
                    Reg_Pr.IsEnabled = true;
                }
                if (login_author[0].Id_user == 2)//user
                {
                    /*
                    Reg_doc_vh   
                    Del_doc_vh
                    Save_doc_vh_and_New
                    Reg_doc_iz
                    Del_doc_iz
                    Save_doc_iz_and_New
                    Del_doc_Pr
                    SavePrAndNew
                    Reg_Pr*/
                    
                    Reg_doc_vh.Visibility = Visibility.Hidden;                    
                    Del_doc_vh.Visibility = Visibility.Hidden;
                    Save_doc_vh_and_New.Visibility = Visibility.Hidden;
                    Reg_doc_iz.Visibility = Visibility.Hidden;
                    Del_doc_iz.Visibility = Visibility.Hidden;
                    Save_doc_iz_and_New.Visibility = Visibility.Hidden; 
                    Del_doc_Pr.Visibility = Visibility.Hidden;
                    SavePrAndNew.Visibility = Visibility.Hidden;
                    Reg_Pr.Visibility = Visibility.Hidden;
                    Reg_doc_vh.IsEnabled = false;
                    Del_doc_vh.IsEnabled = false;
                    Save_doc_vh_and_New.IsEnabled = false;
                    Reg_doc_iz.IsEnabled = false;
                    Del_doc_iz.IsEnabled = false;
                    Save_doc_iz_and_New.IsEnabled = false;
                    Del_doc_Pr.IsEnabled = false;
                    SavePrAndNew.IsEnabled = false;
                    Reg_Pr.IsEnabled = false;
                }
            }
        }



        private void Reg_doc_vh_Click(object sender, RoutedEventArgs e)
        {
            // AddCol.Text= Reg_doc_vh_grid.Visibility.ToString();
            save_doc = 0;
            Save_doc_vh_and_New.Visibility = Visibility.Visible;
            Del_doc_vh.Visibility = Visibility.Hidden;
            Reg_doc_vh_grid.Visibility = Visibility.Visible;
            Serch_doc_vh_grid.Visibility = Visibility.Hidden;
        }

        private void Reg_doc_iz_Click(object sender, RoutedEventArgs e)
        {
            save_doc = 0;
            Save_doc_iz_and_New.Visibility = Visibility.Visible;
            Del_doc_iz.Visibility = Visibility.Hidden;
            Serch_doc_iz_grid.Visibility = Visibility.Hidden;
            Reg_doc_iz_grid.Visibility = Visibility.Visible;
        }
        private void win_vh_Click(object sender, RoutedEventArgs e)
        {
            objDoc = null;
            save_doc = -1;
            Reg_doc_vh_grid.Visibility = Visibility.Hidden;
            Reg_doc_iz_grid.Visibility = Visibility.Hidden;
            Serch_doc_iz_grid.Visibility = Visibility.Hidden;
            Prikazi.Visibility = Visibility.Hidden;
            Obr_gr.Visibility = Visibility.Hidden;
            Serch_pr_grid.Visibility = Visibility.Hidden;
            Serch_doc_vh_grid.Visibility = Visibility.Visible;
        }

        private void win_iz_Click(object sender, RoutedEventArgs e)
        {
            objDoc = null;
            save_doc = -1;
            Reg_doc_vh_grid.Visibility = Visibility.Hidden;
            Reg_doc_iz_grid.Visibility = Visibility.Hidden;            
            Serch_doc_vh_grid.Visibility = Visibility.Hidden;
            Prikazi.Visibility = Visibility.Hidden;

            Serch_pr_grid.Visibility = Visibility.Hidden;
            Obr_gr.Visibility = Visibility.Hidden;
            Serch_doc_iz_grid.Visibility = Visibility.Visible;
        }

        private void Bool_post_data_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            Post_data_textbox.Text = null;
            Post_data_textbox.IsEnabled = false;
        }
        private void Bool_post_data_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            //System.Security.Cryptography.SHA1
            Post_data_textbox.IsEnabled = true;
        }

        private void Bool_sign_doc_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            Sign_doc_textbox.Text = null;
            Sign_doc_textbox.IsEnabled = false;
        }
        
        
        private void Bool_sign_doc_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Sign_doc_textbox.IsEnabled = true;
        }

        private void Save_doc_vh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Documents_vh doc_vh = new Documents_vh {
                Reg_num = Int32.Parse(Reg_num_textbox.Text),
                Griph = Griph_textbox.Text,
                Data_reg = Date_reg_textbox.SelectedDate,
                Data_doc_el = Date_doc_el_textbox.SelectedDate,
                Post_num = Int32.Parse(Post_num_textbox.Text),
                Post_data = Post_data_textbox.SelectedDate,
                Bool_post_data = Bool_post_data_checkbox.IsChecked,
                Data_isp = Data_ips_textbox.SelectedDate,
                Copies_num = Int32.Parse(Copies_num_textbox.Text),
                Sheets_doc_el = Int32.Parse(Sheets_doc_el_textbox.Text),
                Sheets_doc = Int32.Parse(Sheets_doc_textbox.Text),
                Sheets_apps_doc = Int32.Parse(Sheets_apps_doc_textbox.Text),
                Sheets_notes_doc = Int32.Parse(Sheets_notes_doc_textbox.Text),             
                Sign_doc = Sign_doc_textbox.Text,
                Bool_sign_doc = Bool_sign_doc_checkbox.IsChecked,
                From_doc = From_doc_textbox.Text,
                Adr_doc = Adr_doc_textbox.Text,
                Summary_doc = Summary_doc_textbox.Text,
                Comment_doc = Comment_doc_textbox.Text,
                Id_form_doc = (int?)Form_doc_vh.SelectedValue, // == null ? null : (int?)Form_doc_vh.SelectedValue,
                Id_type_doc = (int?)Type_doc_vh.SelectedValue,// == null ? null : (int?)Type_doc_vh.SelectedValue,
            };

            if (save_doc ==0)
            {
                docCon.Doc_vh.Add(doc_vh);
                docCon.SaveChanges();
            }
            if (save_doc==1)
            {
                 Documents_vh docvh = (Documents_vh)objDoc;
               // MessageBox.Show(docvh.Adr_doc);
                 var createDocVH = docCon.Doc_vh.Find(docvh.Id_doc_vh);
                 if (createDocVH != null)
                 {
                    createDocVH.Reg_num = Int32.Parse(Reg_num_textbox.Text);
                    createDocVH.Griph = Griph_textbox.Text;
                    createDocVH.Data_reg = Date_reg_textbox.SelectedDate;
                    createDocVH.Data_doc_el = Date_doc_el_textbox.SelectedDate;
                    createDocVH.Post_num = Int32.Parse(Post_num_textbox.Text);
                    createDocVH.Post_data = Post_data_textbox.SelectedDate;
                    createDocVH.Bool_post_data = Bool_post_data_checkbox.IsChecked;
                    createDocVH.Data_isp = Data_ips_textbox.SelectedDate;
                    createDocVH.Copies_num = Int32.Parse(Copies_num_textbox.Text);
                    createDocVH.Sheets_doc_el = Int32.Parse(Sheets_doc_el_textbox.Text);
                    createDocVH.Sheets_doc = Int32.Parse(Sheets_doc_textbox.Text);
                    createDocVH.Sheets_apps_doc = Int32.Parse(Sheets_apps_doc_textbox.Text);
                    createDocVH.Sheets_notes_doc = Int32.Parse(Sheets_notes_doc_textbox.Text);
                    createDocVH.Sign_doc = Sign_doc_textbox.Text;
                    createDocVH.Bool_sign_doc = Bool_sign_doc_checkbox.IsChecked;
                    createDocVH.From_doc = From_doc_textbox.Text;
                    createDocVH.Adr_doc = Adr_doc_textbox.Text;
                    createDocVH.Summary_doc = Summary_doc_textbox.Text;
                    createDocVH.Comment_doc = Comment_doc_textbox.Text;
                    createDocVH.Id_form_doc = (int?)Form_doc_vh.SelectedValue;
                    createDocVH.Id_type_doc = (int?)Type_doc_vh.SelectedValue;
                    docCon.Entry(createDocVH).State = EntityState.Modified;
                    docCon.SaveChanges();

                    //docCon.Doc_vh.Load(); // загружаем данные входящих
                    DocGridVH.Items.Refresh(); // Добавляем в DataGrid входящие
                }
            }

            save_doc = -1;
            Prikazi.Visibility = Visibility.Hidden;
            Obr_gr.Visibility = Visibility.Hidden;
            Reg_doc_vh_grid.Visibility = Visibility.Hidden;
            Reg_doc_iz_grid.Visibility = Visibility.Hidden;
            Serch_doc_iz_grid.Visibility = Visibility.Hidden;
            Serch_doc_vh_grid.Visibility = Visibility.Visible;
            NullBoxVH();
            }
            catch (Exception errorShow)
            {
                MessageBox.Show(errorShow.Message);
            }

        }

        private void Save_doc_iz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Documents_iz doc_iz = new Documents_iz
                {

                    Reg_num = Int32.Parse(Reg_num_iz_textbox.Text),
                    Copies_num = Int32.Parse(Copies_num_iz_textbox.Text),
                    Data_reg = Data_reg_iz_DatePicker.SelectedDate,
                    Sheets_doc_el = Int32.Parse(Sheets_apps_doc_iz_textbox.Text),
                    Sheets_doc = Int32.Parse(Sheets_doc_iz_textbox.Text),
                    // Link_doc = Link_doc_iz_textbox.Text,
                    Sheets_notes_doc = Int32.Parse(Sheets_notes_doc_iz_textbox.Text),
                    Sheets_apps_doc = Int32.Parse(Sheets_apps_doc_iz_textbox.Text),
                    Comment_doc = Comment_doc_iz_textbox.Text,
                    Summary_doc = Summary_doc_iz_textbox.Text,
                    Griph = Griph_iz_textbox.Text,
                    Post_num_isp = Int32.Parse(Post_num_iz_textbox.Text),
                    Post_num = Int32.Parse(Post_num_isp_iz_textbox.Text),
                    Isp_num_doc = Int32.Parse(Isp_num_doc_iz_textbox.Text),
                    Post_data = Post_data_iz_datepick.SelectedDate,
                    Bool_el_doc = Bool_el_doc_iz_checkbox.IsChecked,
                    Shtrih_kod = Shtrih_kod_iz_textbox.Text,
                    FIO_podpis = FIO_podpis_iz_textbox.Text,
                    Org_podpis = Org_podpis_iz_textbox.Text,
                    FIO_ispoln = FIO_ispoln_iz_textbox.Text,
                    Org_ispoln = Org_ispoln_iz_textbox.Text,
                    Id_razdel = (int?)Razel_docComboBox.SelectedValue,
                    Id_form_doc = (int?)Id_form_doc_iz_combobox.SelectedValue,
                    Id_characteristic_doc = (int?)Id_characteristic_doc_iz_combobox.SelectedValue,
                    Id_type_doc = (int?)Id_type_doc_iz_combobox.SelectedValue,
                };
                if (save_doc == 0)
                {
                    docCon.Doc_iz.Add(doc_iz);
                    docCon.SaveChanges();
                }
                else
                {
                    Documents_iz doc = (Documents_iz)objDoc;
                    var createDocIZ = docCon.Doc_iz.Find(doc.Id_doc_iz);
                    if (createDocIZ != null)
                    {
                        createDocIZ.Reg_num = Int32.Parse(Reg_num_iz_textbox.Text);
                        createDocIZ.Copies_num = Int32.Parse(Copies_num_iz_textbox.Text);
                        createDocIZ.Data_reg = Data_reg_iz_DatePicker.SelectedDate;
                        createDocIZ.Sheets_doc_el = Int32.Parse(Sheets_apps_doc_iz_textbox.Text);
                        createDocIZ.Sheets_doc = Int32.Parse(Sheets_doc_iz_textbox.Text);
                        // Link_doc = Link_doc_iz_textbox.Text,
                        createDocIZ.Sheets_notes_doc = Int32.Parse(Sheets_notes_doc_iz_textbox.Text);
                        createDocIZ.Sheets_apps_doc = Int32.Parse(Sheets_apps_doc_iz_textbox.Text);
                        createDocIZ.Comment_doc = Comment_doc_iz_textbox.Text;
                        createDocIZ.Summary_doc = Summary_doc_iz_textbox.Text;
                        createDocIZ.Griph = Griph_iz_textbox.Text;
                        createDocIZ.Post_num_isp = Int32.Parse(Post_num_iz_textbox.Text);
                        createDocIZ.Post_num = Int32.Parse(Post_num_isp_iz_textbox.Text);
                        createDocIZ.Isp_num_doc = Int32.Parse(Isp_num_doc_iz_textbox.Text);
                        createDocIZ.Post_data = Post_data_iz_datepick.SelectedDate;
                        createDocIZ.Bool_el_doc = Bool_el_doc_iz_checkbox.IsChecked;
                        createDocIZ.Shtrih_kod = Shtrih_kod_iz_textbox.Text;
                        createDocIZ.FIO_podpis = FIO_podpis_iz_textbox.Text;
                        createDocIZ.Org_podpis = Org_podpis_iz_textbox.Text;
                        createDocIZ.FIO_ispoln = FIO_ispoln_iz_textbox.Text;
                        createDocIZ.Org_ispoln = Org_ispoln_iz_textbox.Text;
                        createDocIZ.Id_razdel = (int?)Razel_docComboBox.SelectedValue;
                        createDocIZ.Id_form_doc = (int?)Id_form_doc_iz_combobox.SelectedValue;
                        createDocIZ.Id_characteristic_doc = (int?)Id_characteristic_doc_iz_combobox.SelectedValue;
                        createDocIZ.Id_type_doc = (int?)Id_type_doc_iz_combobox.SelectedValue;
                        docCon.Entry(createDocIZ).State = EntityState.Modified;
                        docCon.SaveChanges();

                        //docCon.Doc_vh.Load(); // загружаем данные входящих
                        DocGridIZ.Items.Refresh(); // Добавляем в DataGrid входящие
                    }
                }
                save_doc = -1;

                Prikazi.Visibility = Visibility.Hidden;
                Obr_gr.Visibility = Visibility.Hidden;
                Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                Serch_doc_iz_grid.Visibility = Visibility.Visible;
                NullBoxIZ();
            } catch(Exception errorShow)
            {
                MessageBox.Show(errorShow.Message);
            }


            


        }

        private void ExitWin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }


        void ChangeBackground(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("asd");
           // MessageBox.Show(sender.ToString());
            try
            {
                if (sender != null)
                {
                    DataGrid grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        //This is the code which helps to show the data when the row is double clicked.
                        DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                        //DataRowView dr = (DataRowView)dgr.Item;
                        Documents_vh doc = (Documents_vh)grid.CurrentItem;
                       
                        Reg_num_textbox.Text = doc.Reg_num.ToString();
                        Griph_textbox.Text = doc.Griph.ToString();
                        Date_reg_textbox.SelectedDate = doc.Data_reg;
                        Date_doc_el_textbox.SelectedDate = doc.Data_doc_el;
                        Post_num_textbox.Text = doc.Post_num.ToString();
                        Post_data_textbox.SelectedDate = doc.Post_data;
                        Bool_post_data_checkbox.IsChecked= doc.Bool_post_data;
                        Data_ips_textbox.SelectedDate = doc.Data_isp;
                        Copies_num_textbox.Text = doc.Copies_num.ToString();
                        Sheets_doc_el_textbox.Text = doc.Sheets_doc_el.ToString();
                        Sheets_doc_textbox.Text = doc.Sheets_doc.ToString();
                        Sheets_apps_doc_textbox.Text= doc.Sheets_apps_doc.ToString();
                        Sheets_notes_doc_textbox.Text= doc.Sheets_notes_doc.ToString();
                        Sign_doc_textbox.Text= doc.Sign_doc;
                        Bool_sign_doc_checkbox.IsChecked = doc.Bool_sign_doc;
                        From_doc_textbox.Text= doc.From_doc;
                        Adr_doc_textbox.Text= doc.Adr_doc;
                        Summary_doc_textbox.Text= doc.Summary_doc;
                        Comment_doc_textbox.Text= doc.Comment_doc;
                        Type_doc_vh.SelectedValue = doc.Id_type_doc;
                        Form_doc_vh.SelectedValue = doc.Id_form_doc;
                        objDoc = doc;
                        save_doc = 1;

                        Save_doc_vh_and_New.Visibility = Visibility.Hidden;
                        Del_doc_vh.Visibility = Visibility.Visible;
                        Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                        Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                        Prikazi.Visibility = Visibility.Hidden;
                        Obr_gr.Visibility = Visibility.Hidden;
                        Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                        Reg_doc_vh_grid.Visibility = Visibility.Visible;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void NullBoxVH()
        {
            /*
             Reg_num_textbox
             Griph_textbox
             Date_reg_textbox
             Date_doc_el_textbox
             Post_num_textbox
             Post_data_textbox
             Bool_post_data_checkbox
             Form_doc_vh
             Type_doc_vh
             Data_ips_textbox
             Copies_num_textbox
             Sheets_doc_el_textbox
             Sheets_doc_textbox
             Sheets_apps_doc_textbox
             Sheets_notes_doc_textbox
             Link_doc_textbox
             Sign_doc_textbox
             Bool_sign_doc_checkbox
             From_doc_textbox
             Adr_doc_textbox
             Summary_doc_textbox
             Comment_doc_textbox
             Form_doc_vh
             Type_doc_vh
             */
            Reg_num_textbox.Clear();
             Griph_textbox.Clear();
            Date_reg_textbox.SelectedDate = null;
            Date_doc_el_textbox.SelectedDate = null;
            Post_num_textbox.Clear();
            Post_data_textbox.SelectedDate = null;
            Bool_post_data_checkbox.IsChecked = false;
            Data_ips_textbox.SelectedDate = null;
            Copies_num_textbox.Clear();
            Sheets_doc_el_textbox.Clear();
            Sheets_doc_textbox.Clear();
            Sheets_apps_doc_textbox.Clear();
            Sheets_notes_doc_textbox.Clear();
            Sign_doc_textbox.Clear();
            Bool_sign_doc_checkbox.IsChecked = false;
            From_doc_textbox.Clear();
            Adr_doc_textbox.Clear();
            Summary_doc_textbox.Clear();
            Comment_doc_textbox.Clear();
            Form_doc_vh.SelectedIndex = -1;
            Type_doc_vh.SelectedIndex = -1;
        }
        private void NullBoxIZ()
        {
            /*
             Reg_num_iz_textbox
             Data_reg_iz_DatePicker
             Griph_iz_textbox
             Id_form_doc_iz_combobox
             Id_characteristic_doc_iz_combobox
             Id_type_doc_iz_combobox
             Bool_el_doc_iz_checkbox
             Shtrih_kod_iz_textbox
             Post_num_iz_textbox
             Isp_num_doc_iz_textbox
             Post_data_iz_datepick
             Post_num_isp_iz_textbox
             Copies_num_iz_textbox
             Sheets_doc_iz_textbox
             Sheets_apps_doc_iz_textbox
             Sheets_notes_doc_iz_textbox
             Link_doc_iz_textbox
             Summary_doc_iz_textbox
             Comment_doc_iz_textbox
             Org_podpis_iz_textbox
             FIO_podpis_iz_textbox
             FIO_ispoln_iz_textbox
             Org_ispoln_iz_textbox
             */
            Reg_num_iz_textbox.Clear();
             Data_reg_iz_DatePicker.SelectedDate = null;
            Griph_iz_textbox.Clear();
            Id_form_doc_iz_combobox.SelectedIndex = -1;
            Id_characteristic_doc_iz_combobox.SelectedIndex = -1;
            Id_type_doc_iz_combobox.SelectedIndex = -1;
            Bool_el_doc_iz_checkbox.IsChecked = false;
            Shtrih_kod_iz_textbox.Clear();
            Post_num_iz_textbox.Clear();
            Isp_num_doc_iz_textbox.Clear();
            Post_data_iz_datepick.SelectedDate = null;
            Post_num_isp_iz_textbox.Clear();
            Copies_num_iz_textbox.Clear();
            Sheets_doc_iz_textbox.Clear();
            Sheets_apps_doc_iz_textbox.Clear();
            Sheets_notes_doc_iz_textbox.Clear();
            //Link_doc_iz_textbox.Clear();
            Summary_doc_iz_textbox.Clear();
            Comment_doc_iz_textbox.Clear();
            Org_podpis_iz_textbox.Clear();
            FIO_podpis_iz_textbox.Clear();
            FIO_ispoln_iz_textbox.Clear();
            Org_ispoln_iz_textbox.Clear();
        }
        private void NullBoxPr()
        {
            Reg_numPr.Clear();
            Date_regPr.SelectedDate = null;
            Count_ekzPr.Clear();
            Ekz_numPr.Clear();
            Count_sheetsPr.Clear();
            Count_prilPr.Clear();
            CommentPr.Clear();
            SummaryPr.Clear();
            Fio_podPr.Clear();
            Fio_ispPr.Clear();
            Form_prCombobox.SelectedIndex = -1;
        }


        private void Win_pr_Click(object sender, RoutedEventArgs e)
        {
            objDoc = null;
            save_doc = -1;
            Reg_doc_vh_grid.Visibility = Visibility.Hidden;
            Reg_doc_iz_grid.Visibility = Visibility.Hidden;
            Serch_doc_vh_grid.Visibility = Visibility.Hidden;
            Serch_doc_iz_grid.Visibility = Visibility.Hidden;
            Obr_gr.Visibility = Visibility.Hidden;
            Prikazi.Visibility = Visibility.Hidden;
            Serch_pr_grid.Visibility = Visibility.Visible;
        }

        private void Win_obr_Click(object sender, RoutedEventArgs e)
        {
            objDoc = null;
            save_doc = -1;
            Reg_doc_vh_grid.Visibility = Visibility.Hidden;
            Reg_doc_iz_grid.Visibility = Visibility.Hidden;
            Serch_doc_vh_grid.Visibility = Visibility.Hidden;
            Serch_doc_iz_grid.Visibility = Visibility.Hidden;
            Prikazi.Visibility = Visibility.Hidden;
            Serch_pr_grid.Visibility = Visibility.Hidden;
            Obr_gr.Visibility = Visibility.Visible;
        }

        private void Creaty_doc_vh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                objDoc = DocGridVH.SelectedItem;
                Documents_vh doc = (Documents_vh)objDoc;

                Reg_num_textbox.Text = doc.Reg_num.ToString();
                Griph_textbox.Text = doc.Griph.ToString();
                Date_reg_textbox.SelectedDate = doc.Data_reg;
                Date_doc_el_textbox.SelectedDate = doc.Data_doc_el;
                Post_num_textbox.Text = doc.Post_num.ToString();
                Post_data_textbox.SelectedDate = doc.Post_data;
                Bool_post_data_checkbox.IsChecked = doc.Bool_post_data;
                Data_ips_textbox.SelectedDate = doc.Data_isp;
                Copies_num_textbox.Text = doc.Copies_num.ToString();
                Sheets_doc_el_textbox.Text = doc.Sheets_doc_el.ToString();
                Sheets_doc_textbox.Text = doc.Sheets_doc.ToString();
                Sheets_apps_doc_textbox.Text = doc.Sheets_apps_doc.ToString();
                Sheets_notes_doc_textbox.Text = doc.Sheets_notes_doc.ToString();
                Sign_doc_textbox.Text = doc.Sign_doc;
                Bool_sign_doc_checkbox.IsChecked = doc.Bool_sign_doc;
                From_doc_textbox.Text = doc.From_doc;
                Adr_doc_textbox.Text = doc.Adr_doc;
                Summary_doc_textbox.Text = doc.Summary_doc;
                Comment_doc_textbox.Text = doc.Comment_doc;
                save_doc = 1;

                Save_doc_vh_and_New.Visibility = Visibility.Hidden;
                Del_doc_vh.Visibility = Visibility.Visible;
                Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                Prikazi.Visibility = Visibility.Hidden;
                Obr_gr.Visibility = Visibility.Hidden;
                Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                Reg_doc_vh_grid.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        void SelectDocIz(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    DataGrid grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        //This is the code which helps to show the data when the row is double clicked.
                       
                        //DataRowView dr = (DataRowView)dgr.Item;
                        Documents_iz doc = (Documents_iz)DocGridIZ.SelectedItem;
                        
                        Reg_num_iz_textbox.Text = doc.Reg_num.ToString();
                        Copies_num_iz_textbox.Text = doc.Copies_num.ToString();
                         Data_reg_iz_DatePicker.SelectedDate = doc.Data_reg;
                        Sheets_apps_doc_iz_textbox.Text = doc.Sheets_doc_el.ToString();
                        Sheets_doc_iz_textbox.Text = doc.Sheets_doc.ToString();

                        Sheets_notes_doc_iz_textbox.Text = doc.Sheets_notes_doc.ToString();
                        Sheets_apps_doc_iz_textbox.Text = doc.Sheets_apps_doc.ToString();
                        Comment_doc_iz_textbox.Text = doc.Comment_doc.ToString();
                        Summary_doc_iz_textbox.Text = doc.Summary_doc.ToString();
                        Griph_iz_textbox.Text = doc.Griph.ToString();
                        Post_num_iz_textbox.Text = doc.Post_num_isp.ToString();
                        Post_num_isp_iz_textbox.Text = doc.Post_num.ToString();
                        Isp_num_doc_iz_textbox.Text = doc.Isp_num_doc.ToString();
                        Post_data_iz_datepick.SelectedDate = doc.Post_data;
                        Bool_el_doc_iz_checkbox.IsChecked = doc.Bool_el_doc;
                        Shtrih_kod_iz_textbox.Text = doc.Shtrih_kod.ToString();
                        FIO_podpis_iz_textbox.Text = doc.FIO_podpis.ToString();
                        Org_podpis_iz_textbox.Text = doc.Org_podpis.ToString();
                        FIO_ispoln_iz_textbox.Text = doc.FIO_ispoln.ToString();
                        Org_ispoln_iz_textbox.Text = doc.Org_ispoln.ToString();
                        Razel_docComboBox.SelectedValue = doc.Id_razdel;
                        Id_form_doc_iz_combobox.SelectedValue = doc.Id_form_doc;
                        Id_characteristic_doc_iz_combobox.SelectedValue = doc.Id_characteristic_doc;
                        Id_type_doc_iz_combobox.SelectedValue = doc.Id_type_doc;

                        Del_doc_iz.Visibility = Visibility.Visible;
                        Save_doc_iz_and_New.Visibility = Visibility.Hidden;

                        objDoc = doc;
                        save_doc = 1;                                            
                        Prikazi.Visibility = Visibility.Hidden;
                        Obr_gr.Visibility = Visibility.Hidden;
                        Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                        Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                        Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                        Reg_doc_iz_grid.Visibility = Visibility.Visible;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void NewObr_Click(object sender, RoutedEventArgs e)
        {
            Appeal_gr ap = null;
            RegObr regObr = new RegObr(ap);
            if (regObr.ShowDialog() == true)
            {
                    docCon.appeal_Gr.Add((Appeal_gr)AppDoc);
                    docCon.SaveChanges();

                PrimGrid.ItemsSource = docCon.appeal_Gr.Local;
            }
        }

        private void SerchObr_Click(object sender, RoutedEventArgs e)
        {
           
            if (DolObr.Text.Length >= 1)
            {
                if (isCheckObr.IsChecked == true)
                {
                   var serchObr = docCon.appeal_Gr.Local.Where(gr => gr.FIO_prin.StartsWith(DolObr.Text)).Where(b => b.Bool_pr == isCheckObr.IsChecked);
                    PrimGrid.ItemsSource = serchObr;
                    //PrimGrid.ItemsSource;
                }
                else
                {
                    var serchObr = docCon.appeal_Gr.Local.Where(gr => gr.FIO_prin.StartsWith(DolObr.Text));
                  
                    List<Appeal_gr> appeal_Grs = new List<Appeal_gr>();
                    foreach (Appeal_gr ap in serchObr)
                    {
                        if (ap.Bool_pr == false)
                        {
                            appeal_Grs.Add(ap);
                        }
                        else
                        {
                            appeal_Grs.Add(ap);
                        }
                    }
                    PrimGrid.ItemsSource = appeal_Grs;
                }
            }
            else
            {
                if (isCheckObr.IsChecked == true)
                {
                    var serchObr = docCon.appeal_Gr.Local.Where(b => b.Bool_pr == isCheckObr.IsChecked);
                    PrimGrid.ItemsSource = serchObr;
                    //PrimGrid.ItemsSource;
                }
                else
                {
                    var serchObr = docCon.appeal_Gr.Local.ToBindingList();
                    PrimGrid.ItemsSource = serchObr;
                }

            }

        }

        private void SavePr_Click(object sender, RoutedEventArgs e)
        {

             switch (Form_prCombobox.SelectedValue)
             {
                 case 1:
                     try{
                         Prikaze_1 pr_1 = new Prikaze_1
                         {
                             Reg_num = Int32.Parse(Reg_numPr.Text),
                             Date_reg = Date_regPr.SelectedDate,
                             Griph = GriphPr.SelectedIndex,
                             Count_ekz = Int32.Parse(Count_ekzPr.Text),
                             Ekz_num = Int32.Parse(Ekz_numPr.Text),
                             Count_sheets = Int32.Parse(Count_sheetsPr.Text),
                             Count_pril = Int32.Parse(Count_prilPr.Text),
                             Comment = CommentPr.Text,
                             Summary = SummaryPr.Text,
                             Fio_pod = Fio_podPr.Text,
                             Fio_isp = Fio_ispPr.Text,
                             Id_form_prikaz = Form_prCombobox.SelectedIndex,
                         };
                        if (save_doc == 0)
                        {
                            docCon.prikaze_1.Add(pr_1);
                            docCon.SaveChanges();
                        }
                        else
                        {
                            Prikaze_1 doc = (Prikaze_1)objDoc;
                            var createDocIZ = docCon.prikaze_1.Find(doc.Id_prikaz);
                            if (createDocIZ != null)
                            {
                                createDocIZ.Reg_num = Int32.Parse(Reg_numPr.Text);
                                createDocIZ.Date_reg = Date_regPr.SelectedDate;
                                createDocIZ.Griph = GriphPr.SelectedIndex;
                                createDocIZ.Count_ekz = Int32.Parse(Count_ekzPr.Text);
                                createDocIZ.Ekz_num = Int32.Parse(Ekz_numPr.Text);
                                createDocIZ.Count_sheets = Int32.Parse(Count_sheetsPr.Text);
                                createDocIZ.Count_pril = Int32.Parse(Count_prilPr.Text);
                                createDocIZ.Comment = CommentPr.Text;
                                createDocIZ.Summary = SummaryPr.Text;
                                createDocIZ.Fio_pod = Fio_podPr.Text;
                                createDocIZ.Fio_isp = Fio_ispPr.Text;
                                createDocIZ.Id_form_prikaz = Form_prCombobox.SelectedIndex;
                                docCon.Entry(createDocIZ).State = EntityState.Modified;
                                docCon.SaveChanges();

                                //DocGridPr.ItemsSource = docCon.prikaze_1.Local;
                                //docCon.Doc_vh.Load(); // загружаем данные входящих
                                DocGridPr.Items.Refresh(); // Добавляем в DataGrid входящие
                            }

                        }
                        save_doc = -1;
                        Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                        Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                        Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                        Obr_gr.Visibility = Visibility.Hidden;
                        Prikazi.Visibility = Visibility.Hidden;
                        Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                        Serch_pr_grid.Visibility = Visibility.Visible;
                        NullBoxPr();
                    }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message.ToString());
                     }

                     break;
                 case 2:
                     try
                     {
                         Prikaze_2 pr_2 = new Prikaze_2
                         {
                             Reg_num = Int32.Parse(Reg_numPr.Text),
                             Date_reg = Date_regPr.SelectedDate,
                             Griph = GriphPr.SelectedIndex,
                             Count_ekz = Int32.Parse(Count_ekzPr.Text),
                             Ekz_num = Int32.Parse(Ekz_numPr.Text),
                             Count_sheets = Int32.Parse(Count_sheetsPr.Text),
                             Count_pril = Int32.Parse(Count_prilPr.Text),
                             Comment = CommentPr.Text,
                             Summary = SummaryPr.Text,
                             Fio_pod = Fio_podPr.Text,
                             Fio_isp = Fio_ispPr.Text,
                             Id_form_prikaz = Form_prCombobox.SelectedIndex,

                         };
                        if (save_doc == 0)
                        {
                            docCon.prikaze_2.Add(pr_2);
                            docCon.SaveChanges();
                        }
                        else
                        {
                            Prikaze_2 doc = (Prikaze_2)objDoc;
                            var createDocIZ = docCon.prikaze_2.Find(doc.Id_prikaz);
                            if (createDocIZ != null)
                            {
                                createDocIZ.Reg_num = Int32.Parse(Reg_numPr.Text);
                                createDocIZ.Date_reg = Date_regPr.SelectedDate;
                                createDocIZ.Griph = GriphPr.SelectedIndex;
                                createDocIZ.Count_ekz = Int32.Parse(Count_ekzPr.Text);
                                createDocIZ.Ekz_num = Int32.Parse(Ekz_numPr.Text);
                                createDocIZ.Count_sheets = Int32.Parse(Count_sheetsPr.Text);
                                createDocIZ.Count_pril = Int32.Parse(Count_prilPr.Text);
                                createDocIZ.Comment = CommentPr.Text;
                                createDocIZ.Summary = SummaryPr.Text;
                                createDocIZ.Fio_pod = Fio_podPr.Text;
                                createDocIZ.Fio_isp = Fio_ispPr.Text;
                                createDocIZ.Id_form_prikaz = Form_prCombobox.SelectedIndex;
                                docCon.Entry(createDocIZ).State = EntityState.Modified;
                                docCon.SaveChanges();
                               // DocGridPr.ItemsSource = docCon.prikaze_2.Local;
                                //docCon.Doc_vh.Load(); // загружаем данные входящих
                                DocGridPr.Items.Refresh(); // Добавляем в DataGrid входящие
                            }

                        }
                        save_doc = -1;
                        NullBoxPr();
                        Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                        Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                        Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                        Obr_gr.Visibility = Visibility.Hidden;
                        Prikazi.Visibility = Visibility.Hidden;
                        Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                        Serch_pr_grid.Visibility = Visibility.Visible;
                    }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message.ToString());
                     }

                     break;
                 case 3:
                     try
                     {
                         Prikaze_3 pr_3 = new Prikaze_3
                         {
                             Reg_num = Int32.Parse(Reg_numPr.Text),
                             Date_reg = Date_regPr.SelectedDate,
                             Griph = GriphPr.SelectedIndex,
                             Count_ekz = Int32.Parse(Count_ekzPr.Text),
                             Ekz_num = Int32.Parse(Ekz_numPr.Text),
                             Count_sheets = Int32.Parse(Count_sheetsPr.Text),
                             Count_pril = Int32.Parse(Count_prilPr.Text),
                             Comment = CommentPr.Text,
                             Summary = SummaryPr.Text,
                             Fio_pod = Fio_podPr.Text,
                             Fio_isp = Fio_ispPr.Text,
                             Id_form_prikaz = Form_prCombobox.SelectedIndex,
                         };
                        if (save_doc == 0)
                        {
                            docCon.prikaze_3.Add(pr_3);
                            docCon.SaveChanges();
                        }
                        else
                        {
                            Prikaze_3 doc = (Prikaze_3)objDoc;
                            var createDocIZ = docCon.prikaze_3.Find(doc.Id_prikaz);
                            if (createDocIZ != null)
                            {
                                createDocIZ.Reg_num = Int32.Parse(Reg_numPr.Text);
                                createDocIZ.Date_reg = Date_regPr.SelectedDate;
                                createDocIZ.Griph = GriphPr.SelectedIndex;
                                createDocIZ.Count_ekz = Int32.Parse(Count_ekzPr.Text);
                                createDocIZ.Ekz_num = Int32.Parse(Ekz_numPr.Text);
                                createDocIZ.Count_sheets = Int32.Parse(Count_sheetsPr.Text);
                                createDocIZ.Count_pril = Int32.Parse(Count_prilPr.Text);
                                createDocIZ.Comment = CommentPr.Text;
                                createDocIZ.Summary = SummaryPr.Text;
                                createDocIZ.Fio_pod = Fio_podPr.Text;
                                createDocIZ.Fio_isp = Fio_ispPr.Text;
                                createDocIZ.Id_form_prikaz = Form_prCombobox.SelectedIndex;
                                docCon.Entry(createDocIZ).State = EntityState.Modified;
                                docCon.SaveChanges();
                               // DocGridPr.ItemsSource = docCon.prikaze_3.Local;
                                //docCon.Doc_vh.Load(); // загружаем данные входящих
                                DocGridPr.Items.Refresh(); // Добавляем в DataGrid входящие
                            }

                        }
                        save_doc = -1;
                        NullBoxPr();
                        Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                        Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                        Serch_doc_vh_grid.Visibility = Visibility.Hidden;                        
                        Obr_gr.Visibility = Visibility.Hidden;
                        Prikazi.Visibility = Visibility.Hidden;
                        Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                        Serch_pr_grid.Visibility = Visibility.Visible;
                    }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message.ToString());
                     }

                     break;
                 default:
                     break;
             }


        }

        private void SavePrAndNew_Click(object sender, RoutedEventArgs e)
        {
            switch (Form_prCombobox.SelectedValue)
            {
                case 1:
                    try
                    {
                        Prikaze_1 pr_1 = new Prikaze_1
                        {
                            Reg_num = Int32.Parse(Reg_numPr.Text),
                            Date_reg = Date_regPr.SelectedDate,
                            Griph = GriphPr.SelectedIndex,
                            Count_ekz = Int32.Parse(Count_ekzPr.Text),
                            Ekz_num = Int32.Parse(Ekz_numPr.Text),
                            Count_sheets = Int32.Parse(Count_sheetsPr.Text),
                            Count_pril = Int32.Parse(Count_prilPr.Text),
                            Comment = CommentPr.Text,
                            Summary = SummaryPr.Text,
                            Fio_pod = Fio_podPr.Text,
                            Fio_isp = Fio_ispPr.Text,
                            Id_form_prikaz = Form_prCombobox.SelectedIndex,
                        };
                        docCon.prikaze_1.Add(pr_1);
                        docCon.SaveChanges();

                        NullBoxPr();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                case 2:
                    try
                    {
                        Prikaze_2 pr_2 = new Prikaze_2
                        {
                            Reg_num = Int32.Parse(Reg_numPr.Text),
                            Date_reg = Date_regPr.SelectedDate,
                            Griph = GriphPr.SelectedIndex,
                            Count_ekz = Int32.Parse(Count_ekzPr.Text),
                            Ekz_num = Int32.Parse(Ekz_numPr.Text),
                            Count_sheets = Int32.Parse(Count_sheetsPr.Text),
                            Count_pril = Int32.Parse(Count_prilPr.Text),
                            Comment = CommentPr.Text,
                            Summary = SummaryPr.Text,
                            Fio_pod = Fio_podPr.Text,
                            Fio_isp = Fio_ispPr.Text,
                            Id_form_prikaz = Form_prCombobox.SelectedIndex,
                        };
                        docCon.prikaze_2.Add(pr_2);
                        docCon.SaveChanges();
                        NullBoxPr();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                case 3:
                    try
                    {
                        Prikaze_3 pr_3 = new Prikaze_3
                        {
                            Reg_num = Int32.Parse(Reg_numPr.Text),
                            Date_reg = Date_regPr.SelectedDate,
                            Griph = GriphPr.SelectedIndex,
                            Count_ekz = Int32.Parse(Count_ekzPr.Text),
                            Ekz_num = Int32.Parse(Ekz_numPr.Text),
                            Count_sheets = Int32.Parse(Count_sheetsPr.Text),
                            Count_pril = Int32.Parse(Count_prilPr.Text),
                            Comment = CommentPr.Text,
                            Summary = SummaryPr.Text,
                            Fio_pod = Fio_podPr.Text,
                            Fio_isp = Fio_ispPr.Text,
                            Id_form_prikaz = Form_prCombobox.SelectedIndex,
                        };
                        docCon.prikaze_3.Add(pr_3);
                        docCon.SaveChanges();
                        NullBoxPr();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                default:
                    break;
            }
        }

        private void Serch_prGrid_Click(object sender, RoutedEventArgs e)
        {
            switch (Secrch_prCombobo.SelectedValue)
            {
                case 1:
                    try
                    {
                        if (SerchRegPr.Text.Length >= 1 && SerchFIOPr.Text.Length >=1)
                        {
                            var serch_pr = docCon.prikaze_1.Local.Where(rg => rg.Reg_num.ToString().StartsWith(SerchRegPr.Text)).Where(f=>f.Fio_pod.StartsWith(SerchFIOPr.Text));
                            DocGridPr.ItemsSource = serch_pr;
                        }
                        else
                        {
                            if (SerchRegPr.Text.Length >= 1)
                            {
                                var serch_pr = docCon.prikaze_1.Local.Where(rg => rg.Reg_num.ToString().StartsWith(SerchRegPr.Text));
                                DocGridPr.ItemsSource = serch_pr;
                            }
                            else
                            {
                                if (SerchFIOPr.Text.Length >= 1)
                                {
                                    var serch_pr = docCon.prikaze_1.Local.Where(f => f.Fio_pod.StartsWith(SerchFIOPr.Text));
                                    DocGridPr.ItemsSource = serch_pr;
                                }
                                else
                                {
                                    var serch_pr = docCon.prikaze_1.Local;
                                    DocGridPr.ItemsSource = serch_pr;
                                }
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                case 2:
                    try
                    {
                        if (SerchRegPr.Text.Length >= 1 && SerchFIOPr.Text.Length >= 1)
                        {
                            var serch_pr = docCon.prikaze_2.Local.Where(rg => rg.Reg_num.ToString().StartsWith(SerchRegPr.Text)).Where(f => f.Fio_pod.StartsWith(SerchFIOPr.Text));
                            DocGridPr.ItemsSource = serch_pr;
                        }
                        else
                        {
                            if (SerchRegPr.Text.Length >= 1)
                            {
                                var serch_pr = docCon.prikaze_2.Local.Where(rg => rg.Reg_num.ToString().StartsWith(SerchRegPr.Text));
                                DocGridPr.ItemsSource = serch_pr;
                            }
                            else
                            {
                                if (SerchFIOPr.Text.Length >= 1)
                                {
                                    var serch_pr = docCon.prikaze_2.Local.Where(f => f.Fio_pod.StartsWith(SerchFIOPr.Text));
                                    DocGridPr.ItemsSource = serch_pr;
                                }
                                else
                                {
                                    var serch_pr = docCon.prikaze_2.Local;
                                    DocGridPr.ItemsSource = serch_pr;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                case 3:
                    try
                    {
                        if (SerchRegPr.Text.Length >= 1 && SerchFIOPr.Text.Length >= 1)
                        {
                            var serch_pr = docCon.prikaze_3.Local.Where(rg => rg.Reg_num.ToString().StartsWith(SerchRegPr.Text)).Where(f => f.Fio_pod.StartsWith(SerchFIOPr.Text));
                            DocGridPr.ItemsSource = serch_pr;
                        }
                        else
                        {
                            if (SerchRegPr.Text.Length >= 1)
                            {
                                var serch_pr = docCon.prikaze_3.Local.Where(rg => rg.Reg_num.ToString().StartsWith(SerchRegPr.Text));
                                DocGridPr.ItemsSource = serch_pr;
                            }
                            else
                            {
                                if (SerchFIOPr.Text.Length >= 1)
                                {
                                    var serch_pr = docCon.prikaze_3.Local.Where(f => f.Fio_pod.StartsWith(SerchFIOPr.Text));
                                    DocGridPr.ItemsSource = serch_pr;
                                }
                                else
                                {
                                    var serch_pr = docCon.prikaze_3.Local;
                                    DocGridPr.ItemsSource = serch_pr;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                default:
                    break;
            }
        }


        private void ClearSerchPr_Click(object sender, RoutedEventArgs e)
        {
            switch (Secrch_prCombobo.SelectedValue)
            {
                case 1:
                    try
                    {
                        SerchFIOPr.Clear();
                        SerchRegPr.Clear();
                        var serch_pr = docCon.prikaze_1.Local;
                        DocGridPr.ItemsSource = serch_pr;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                case 2:
                    try
                    {
                        SerchFIOPr.Clear();
                        SerchRegPr.Clear();
                        var serch_pr = docCon.prikaze_2.Local;
                        DocGridPr.ItemsSource = serch_pr;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                case 3:
                    try
                    {
                        SerchFIOPr.Clear();
                        SerchRegPr.Clear();
                        var serch_pr = docCon.prikaze_3.Local;
                        DocGridPr.ItemsSource = serch_pr;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                default:
                    break;
            }

        }

        private void Reg_Pr_Click(object sender, RoutedEventArgs e)
        {
            Del_doc_Pr.Visibility = Visibility.Hidden;
            SavePrAndNew.Visibility = Visibility.Visible;

            save_doc = 0;
            Reg_doc_vh_grid.Visibility = Visibility.Hidden;
            Reg_doc_iz_grid.Visibility = Visibility.Hidden;
            Serch_doc_vh_grid.Visibility = Visibility.Hidden;
            Serch_pr_grid.Visibility = Visibility.Hidden;
            Obr_gr.Visibility = Visibility.Hidden;
            Serch_doc_iz_grid.Visibility = Visibility.Hidden;
            Prikazi.Visibility = Visibility.Visible;

        }
        void SelectPr(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                {

                    DataGrid grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        //This is the code which helps to show the data when the row is double clicked.

                        //DataRowView dr = (DataRowView)dgr.Item;



                        switch (Secrch_prCombobo.SelectedValue)
                        {
                            case 1:
                                try
                                {
                                    Prikaze_1 doc = (Prikaze_1)DocGridPr.SelectedItem;
                                    Reg_numPr.Text = doc.Reg_num.ToString();
                                    Date_regPr.SelectedDate = doc.Date_reg;
                                    GriphPr.SelectedIndex = (int?)doc.Griph == null ? -1 : (int)doc.Griph;
                                    Count_ekzPr.Text = doc.Count_ekz.ToString();
                                    Ekz_numPr.Text = doc.Ekz_num.ToString();
                                    Count_sheetsPr.Text = doc.Count_sheets.ToString();
                                    Count_prilPr.Text = doc.Count_pril.ToString();
                                    CommentPr.Text = doc.Comment;
                                    SummaryPr.Text = doc.Summary;
                                    Fio_podPr.Text = doc.Fio_pod;
                                    Fio_ispPr.Text = doc.Fio_isp;
                                    Form_prCombobox.SelectedIndex = doc.Id_form_prikaz;
                                    objDoc = doc;
                                    Del_doc_Pr.Visibility = Visibility.Visible;
                                    SavePrAndNew.Visibility = Visibility.Hidden;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message.ToString());
                                }

                                break;
                            case 2:
                                try
                                {
                                    Prikaze_2 doc = (Prikaze_2)DocGridPr.SelectedItem;
                                    Reg_numPr.Text = doc.Reg_num.ToString();
                                    Date_regPr.SelectedDate = doc.Date_reg;
                                    GriphPr.SelectedIndex = (int?)doc.Griph == null ? -1 : (int)doc.Griph;
                                    Count_ekzPr.Text = doc.Count_ekz.ToString();
                                    Ekz_numPr.Text = doc.Ekz_num.ToString();
                                    Count_sheetsPr.Text = doc.Count_sheets.ToString();
                                    Count_prilPr.Text = doc.Count_pril.ToString();
                                    CommentPr.Text = doc.Comment;
                                    SummaryPr.Text = doc.Summary;
                                    Fio_podPr.Text = doc.Fio_pod;
                                    Fio_ispPr.Text = doc.Fio_isp;
                                    Form_prCombobox.SelectedIndex = doc.Id_form_prikaz;
                                    objDoc = doc;
                                    Del_doc_Pr.Visibility = Visibility.Visible;
                                    SavePrAndNew.Visibility = Visibility.Hidden;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message.ToString());
                                }

                                break;
                            case 3:
                                try
                                {
                                    Prikaze_3 doc = (Prikaze_3)DocGridPr.SelectedItem;
                                    Reg_numPr.Text = doc.Reg_num.ToString();
                                    Date_regPr.SelectedDate = doc.Date_reg;
                                    GriphPr.SelectedIndex = (int?)doc.Griph == null ? -1 : (int)doc.Griph;
                                    Count_ekzPr.Text = doc.Count_ekz.ToString();
                                    Ekz_numPr.Text = doc.Ekz_num.ToString();
                                    Count_sheetsPr.Text = doc.Count_sheets.ToString();
                                    Count_prilPr.Text = doc.Count_pril.ToString();
                                    CommentPr.Text = doc.Comment;
                                    SummaryPr.Text = doc.Summary;
                                    Fio_podPr.Text = doc.Fio_pod;
                                    Fio_ispPr.Text = doc.Fio_isp;
                                    Form_prCombobox.SelectedIndex = doc.Id_form_prikaz;
                                    objDoc = doc;
                                    Del_doc_Pr.Visibility = Visibility.Visible;
                                    SavePrAndNew.Visibility = Visibility.Hidden;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message.ToString());
                                }

                                break;
                            default:
                                break;
                        }
                        save_doc = 1;

                        Prikazi.Visibility = Visibility.Visible;
                        Serch_pr_grid.Visibility = Visibility.Hidden;
                        Obr_gr.Visibility = Visibility.Hidden;
                        Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                        Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                        Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                        Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void SerchVh_Click(object sender, RoutedEventArgs e)
        {
            if (mainSerchBox.Text != "Введите ключевые слова")
            {
                if (docCon.Doc_vh.Local.Where(org => org.From_doc.StartsWith(mainSerchBox.Text)).ToList().Count != 0)
                {
                    var serch_vh = docCon.Doc_vh.Local.Where(org => org.From_doc.StartsWith(mainSerchBox.Text));                    
                    DocGridVH.ItemsSource = serch_vh;
                }
                if (docCon.Doc_vh.Local.Where(sing => sing.Sign_doc.StartsWith(mainSerchBox.Text)).ToList().Count != 0)
                {
                    var serch_vh = docCon.Doc_vh.Local.Where(sing => sing.Sign_doc.StartsWith(mainSerchBox.Text));                    
                    DocGridVH.ItemsSource = serch_vh;
                }
                if (docCon.Doc_vh.Local.Where(date_1 => date_1.Data_reg.ToString().StartsWith(mainSerchBox.Text)).ToList().Count != 0)
                {
                    var serch_vh = docCon.Doc_vh.Local.Where(date_1 => date_1.Data_reg.ToString().StartsWith(mainSerchBox.Text));
                    DocGridVH.ItemsSource = serch_vh;
                }
                if (docCon.Doc_vh.Local.Where(date_2 => date_2.Data_isp.ToString().StartsWith(mainSerchBox.Text)).ToList().Count != 0)
                {
                    var serch_vh = docCon.Doc_vh.Local.Where(date_2 => date_2.Data_isp.ToString().StartsWith(mainSerchBox.Text));
                    DocGridVH.ItemsSource = serch_vh;
                }
            }
            else
            {
                
                //SerchComboBoxVH
                //num_vhSerch
                //reg_vhSerch
                if (num_vhSerch.Text.Length >= 1 && reg_vhSerch.Text.Length >= 1)
                {
                    if (SerchComboBoxVH.SelectedIndex!=-1)
                    {
                       
                        var serch_vh = docCon.Doc_vh.Local.Where(rg => rg.Reg_num.ToString().StartsWith(reg_vhSerch.Text)).Where(f => f.Post_num.ToString().StartsWith(num_vhSerch.Text)).Where(form =>form.Id_form_doc == (int)SerchComboBoxVH.SelectedValue);
                        DocGridVH.ItemsSource = serch_vh;
                    }
                    else
                    {
                        
                        var serch_vh = docCon.Doc_vh.Local.Where(rg => rg.Reg_num.ToString().StartsWith(reg_vhSerch.Text)).Where(f => f.Post_num.ToString().StartsWith(num_vhSerch.Text));
                        DocGridVH.ItemsSource = serch_vh;
                    }

                }
                else
                {
                    if(num_vhSerch.Text.Length >= 1)
                    {
                        if (SerchComboBoxVH.SelectedIndex != -1)
                        {
                            var serch_vh = docCon.Doc_vh.Local.Where(f => f.Post_num.ToString().StartsWith(num_vhSerch.Text)).Where(form => form.Id_form_doc == (int)SerchComboBoxVH.SelectedValue);
                            DocGridVH.ItemsSource = serch_vh;
                        }
                        else
                        {
                            var serch_vh = docCon.Doc_vh.Local.Where(f => f.Post_num.ToString().StartsWith(num_vhSerch.Text));
                            DocGridVH.ItemsSource = serch_vh;
                        }
                    }
                    if (reg_vhSerch.Text.Length >= 1)
                    {
                        if (SerchComboBoxVH.SelectedIndex != -1)
                        {

                            var serch_vh = docCon.Doc_vh.Local.Where(rg => rg.Reg_num.ToString().StartsWith(reg_vhSerch.Text)).Where(form => form.Id_form_doc == (int)SerchComboBoxVH.SelectedValue);
                            DocGridVH.ItemsSource = serch_vh;
                        }
                        else
                        {

                            var serch_vh = docCon.Doc_vh.Local.Where(rg => rg.Reg_num.ToString().StartsWith(reg_vhSerch.Text));
                            DocGridVH.ItemsSource = serch_vh;
                        }
                    }
                    if (SerchComboBoxVH.SelectedIndex != -1)
                    {

                        var serch_vh = docCon.Doc_vh.Local.Where(form => form.Id_form_doc == (int)SerchComboBoxVH.SelectedValue);
                        DocGridVH.ItemsSource = serch_vh;
                    }

                }
                
            }


        }

        private void clearSerchVH_Click(object sender, RoutedEventArgs e)
        {
            mainSerchBox.Text = "Введите ключевые слова";
            SerchComboBoxVH.SelectedIndex = -1;
            num_vhSerch.Clear();
            reg_vhSerch.Clear();
            DocGridVH.ItemsSource = docCon.Doc_vh.Local;
        }

        private void Del_doc_vh_Click(object sender, RoutedEventArgs e)
        {
            //objDoc
            Documents_vh doc_vh = (Documents_vh)objDoc;
            if (docCon.Doc_vh.Find(doc_vh.Id_doc_vh) != null)
            {
                docCon.Doc_vh.Remove(doc_vh);
                docCon.SaveChanges();
                save_doc = -1;
                Prikazi.Visibility = Visibility.Hidden;
                Obr_gr.Visibility = Visibility.Hidden;
                Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                Serch_doc_vh_grid.Visibility = Visibility.Visible;
                NullBoxVH();

            }
            
        }

        private void SerchIz_Click(object sender, RoutedEventArgs e)
        {
            //Fio_izSerch
            //isp_izSerch
            //reg_izSerch
            if (mainSerchIzBox.Text != "Введите ключевые слова")
            {
                if (docCon.Doc_iz.Local.Where(org => org.FIO_ispoln.StartsWith(mainSerchIzBox.Text)).ToList().Count != 0)
                {
                    var serch_iz = docCon.Doc_iz.Local.Where(org => org.FIO_ispoln.StartsWith(mainSerchIzBox.Text));
                    DocGridIZ.ItemsSource = serch_iz;
                }
                if (docCon.Doc_iz.Local.Where(sing => sing.Org_ispoln.StartsWith(mainSerchIzBox.Text)).ToList().Count != 0)
                {
                    var serch_iz = docCon.Doc_iz.Local.Where(sing => sing.Org_ispoln.StartsWith(mainSerchIzBox.Text));
                    DocGridIZ.ItemsSource = serch_iz;
                }
                if (docCon.Doc_iz.Local.Where(date_1 => date_1.Data_reg.ToString().StartsWith(mainSerchIzBox.Text)).ToList().Count != 0)
                {
                    var serch_iz = docCon.Doc_iz.Local.Where(date_1 => date_1.Data_reg.ToString().StartsWith(mainSerchIzBox.Text));
                    DocGridIZ.ItemsSource = serch_iz;
                }
            
            }
            else
            {
                //Fio_izSerch
                //isp_izSerch
                //reg_izSerch

                if (reg_izSerch.Text.Length >= 1 && isp_izSerch.Text.Length >= 1)
                {
                    var serch_iz = docCon.Doc_iz.Local.Where(rg => rg.Reg_num.ToString().StartsWith(reg_izSerch.Text)).Where(f => f.Isp_num_doc.ToString().StartsWith(isp_izSerch.Text));
                    DocGridIZ.ItemsSource = serch_iz;                  

                }
                else
                {
                    if (reg_izSerch.Text.Length >= 1)
                    {

                        var serch_iz = docCon.Doc_iz.Local.Where(rg => rg.Reg_num.ToString().StartsWith(reg_izSerch.Text));
                        DocGridIZ.ItemsSource = serch_iz;

                    }
                    if (isp_izSerch.Text.Length >= 1)
                    {


                        var serch_iz = docCon.Doc_iz.Local.Where(f => f.Isp_num_doc.ToString().StartsWith(isp_izSerch.Text));
                        DocGridIZ.ItemsSource = serch_iz;

                    }


                }

            }
        }

        private void clearSerchIz_Click(object sender, RoutedEventArgs e)
        {
            mainSerchIzBox.Text = "Введите ключевые слова";
            isp_izSerch.Clear();
            reg_izSerch.Clear();
            DocGridIZ.ItemsSource = docCon.Doc_iz.Local;

        }

        private void Save_doc_iz_and_New_Click(object sender, RoutedEventArgs e)
        {
            try { 
            Documents_iz doc_iz = new Documents_iz
            {

                Reg_num = Int32.Parse(Reg_num_iz_textbox.Text),
                Copies_num = Int32.Parse(Copies_num_iz_textbox.Text),
                Data_reg = Data_reg_iz_DatePicker.SelectedDate,
                Sheets_doc_el = Int32.Parse(Sheets_apps_doc_iz_textbox.Text),
                Sheets_doc = Int32.Parse(Sheets_doc_iz_textbox.Text),
                // Link_doc = Link_doc_iz_textbox.Text,
                Sheets_notes_doc = Int32.Parse(Sheets_notes_doc_iz_textbox.Text),
                Sheets_apps_doc = Int32.Parse(Sheets_apps_doc_iz_textbox.Text),
                Comment_doc = Comment_doc_iz_textbox.Text,
                Summary_doc = Summary_doc_iz_textbox.Text,
                Griph = Griph_iz_textbox.Text,
                Post_num_isp = Int32.Parse(Post_num_iz_textbox.Text),
                Post_num = Int32.Parse(Post_num_isp_iz_textbox.Text),
                Isp_num_doc = Int32.Parse(Isp_num_doc_iz_textbox.Text),
                Post_data = Post_data_iz_datepick.SelectedDate,
                Bool_el_doc = Bool_el_doc_iz_checkbox.IsChecked,
                Shtrih_kod = Shtrih_kod_iz_textbox.Text,
                FIO_podpis = FIO_podpis_iz_textbox.Text,
                Org_podpis = Org_podpis_iz_textbox.Text,
                FIO_ispoln = FIO_ispoln_iz_textbox.Text,
                Org_ispoln = Org_ispoln_iz_textbox.Text,
                Id_razdel = (int?)Razel_docComboBox.SelectedValue,
                Id_form_doc = (int?)Id_form_doc_iz_combobox.SelectedValue,
                Id_characteristic_doc = (int?)Id_characteristic_doc_iz_combobox.SelectedValue,
                Id_type_doc = (int?)Id_type_doc_iz_combobox.SelectedValue,
            };
            if (save_doc == 0)
            {
                docCon.Doc_iz.Add(doc_iz);
                docCon.SaveChanges();
                NullBoxIZ();
            }
                 } catch(Exception errorShow)
            {
                MessageBox.Show(errorShow.Message);
            }

        }

private void Del_doc_iz_Click(object sender, RoutedEventArgs e)
        {
            Documents_iz doc_iz = (Documents_iz)objDoc;
            if (docCon.Doc_iz.Find(doc_iz.Id_doc_iz) != null)
            {
                docCon.Doc_iz.Remove(doc_iz);
                docCon.SaveChanges();
                save_doc = -1;

                Prikazi.Visibility = Visibility.Hidden;
                Obr_gr.Visibility = Visibility.Hidden;
                Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                Serch_doc_iz_grid.Visibility = Visibility.Visible;
                NullBoxIZ();

            }
        }

        private void Creaty_doc_iz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                objDoc = DocGridIZ.SelectedItem;
                Documents_iz doc = (Documents_iz)objDoc;

                

                Reg_num_iz_textbox.Text = doc.Reg_num.ToString();
                Copies_num_iz_textbox.Text = doc.Copies_num.ToString();
                Data_reg_iz_DatePicker.SelectedDate = doc.Data_reg;
                Sheets_apps_doc_iz_textbox.Text = doc.Sheets_doc_el.ToString();
                Sheets_doc_iz_textbox.Text = doc.Sheets_doc.ToString();

                Sheets_notes_doc_iz_textbox.Text = doc.Sheets_notes_doc.ToString();
                Sheets_apps_doc_iz_textbox.Text = doc.Sheets_apps_doc.ToString();
                Comment_doc_iz_textbox.Text = doc.Comment_doc.ToString();
                Summary_doc_iz_textbox.Text = doc.Summary_doc.ToString();
                Griph_iz_textbox.Text = doc.Griph.ToString();
                Post_num_iz_textbox.Text = doc.Post_num_isp.ToString();
                Post_num_isp_iz_textbox.Text = doc.Post_num.ToString();
                Isp_num_doc_iz_textbox.Text = doc.Isp_num_doc.ToString();
                Post_data_iz_datepick.SelectedDate = doc.Post_data;
                Bool_el_doc_iz_checkbox.IsChecked = doc.Bool_el_doc;
                Shtrih_kod_iz_textbox.Text = doc.Shtrih_kod.ToString();
                FIO_podpis_iz_textbox.Text = doc.FIO_podpis.ToString();
                Org_podpis_iz_textbox.Text = doc.Org_podpis.ToString();
                FIO_ispoln_iz_textbox.Text = doc.FIO_ispoln.ToString();
                Org_ispoln_iz_textbox.Text = doc.Org_ispoln.ToString();
                Razel_docComboBox.SelectedValue = doc.Id_razdel;
                Id_form_doc_iz_combobox.SelectedValue = doc.Id_form_doc;
                Id_characteristic_doc_iz_combobox.SelectedValue = doc.Id_characteristic_doc;
                Id_type_doc_iz_combobox.SelectedValue = doc.Id_type_doc;


                save_doc = 1;
                Del_doc_iz.Visibility = Visibility.Visible;
                Save_doc_iz_and_New.Visibility = Visibility.Hidden;
                Prikazi.Visibility = Visibility.Hidden;
                Obr_gr.Visibility = Visibility.Hidden;
                Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                Reg_doc_iz_grid.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void Del_doc_Pr_Click(object sender, RoutedEventArgs e)
        {
           


            switch (Secrch_prCombobo.SelectedValue)
            {
                case 1:
                    try
                    {
                        Prikaze_1 doc = (Prikaze_1)DocGridPr.SelectedItem;
                        if (docCon.prikaze_1.Find(doc.Id_prikaz) != null)
                        {
                            docCon.prikaze_1.Remove(doc);
                            docCon.SaveChanges();

                            
                            Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                            Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                            Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                            Obr_gr.Visibility = Visibility.Hidden;
                            Prikazi.Visibility = Visibility.Hidden;
                            Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                            Serch_pr_grid.Visibility = Visibility.Visible;
                            NullBoxIZ();

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                case 2:
                    try
                    {
                        Prikaze_2 doc = (Prikaze_2)DocGridPr.SelectedItem;
                        if (docCon.prikaze_2.Find(doc.Id_prikaz) != null)
                        {
                            docCon.prikaze_2.Remove(doc);
                            docCon.SaveChanges();


                            Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                            Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                            Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                            Obr_gr.Visibility = Visibility.Hidden;
                            Prikazi.Visibility = Visibility.Hidden;
                            Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                            Serch_pr_grid.Visibility = Visibility.Visible;
                            NullBoxIZ();

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                case 3:
                    try
                    {
                        Prikaze_3 doc = (Prikaze_3)DocGridPr.SelectedItem;
                        if (docCon.prikaze_3.Find(doc.Id_prikaz) != null)
                        {
                            docCon.prikaze_3.Remove(doc);
                            docCon.SaveChanges();


                            Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                            Reg_doc_iz_grid.Visibility = Visibility.Hidden;
                            Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                            Obr_gr.Visibility = Visibility.Hidden;
                            Prikazi.Visibility = Visibility.Hidden;
                            Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                            Serch_pr_grid.Visibility = Visibility.Visible;
                            NullBoxIZ();

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    break;
                default:
                    break;
            }
        }

        private void Creaty_Prikaz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                        switch (Secrch_prCombobo.SelectedValue)
                        {
                            case 1:
                                try
                                {

                                    Prikaze_1 doc = (Prikaze_1)DocGridPr.SelectedItem;
                                    Reg_numPr.Text = doc.Reg_num.ToString();
                                    Date_regPr.SelectedDate = doc.Date_reg;
                                    GriphPr.SelectedIndex = (int?)doc.Griph == null ? -1 : (int)doc.Griph;
                                    Count_ekzPr.Text = doc.Count_ekz.ToString();
                                    Ekz_numPr.Text = doc.Ekz_num.ToString();
                                    Count_sheetsPr.Text = doc.Count_sheets.ToString();
                                    Count_prilPr.Text = doc.Count_pril.ToString();
                                    CommentPr.Text = doc.Comment;
                                    SummaryPr.Text = doc.Summary;
                                    Fio_podPr.Text = doc.Fio_pod;
                                    Fio_ispPr.Text = doc.Fio_isp;
                                    Form_prCombobox.SelectedIndex = doc.Id_form_prikaz;
                                    objDoc = doc;
                                    Del_doc_Pr.Visibility = Visibility.Visible;
                                    SavePrAndNew.Visibility = Visibility.Hidden;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message.ToString());
                                }

                                break;
                            case 2:
                                try
                                {
                                    Prikaze_2 doc = (Prikaze_2)DocGridPr.SelectedItem;
                                    Reg_numPr.Text = doc.Reg_num.ToString();
                                    Date_regPr.SelectedDate = doc.Date_reg;
                                    GriphPr.SelectedIndex = (int?)doc.Griph == null ? -1 : (int)doc.Griph;
                                    Count_ekzPr.Text = doc.Count_ekz.ToString();
                                    Ekz_numPr.Text = doc.Ekz_num.ToString();
                                    Count_sheetsPr.Text = doc.Count_sheets.ToString();
                                    Count_prilPr.Text = doc.Count_pril.ToString();
                                    CommentPr.Text = doc.Comment;
                                    SummaryPr.Text = doc.Summary;
                                    Fio_podPr.Text = doc.Fio_pod;
                                    Fio_ispPr.Text = doc.Fio_isp;
                                    Form_prCombobox.SelectedIndex = doc.Id_form_prikaz;
                                    objDoc = doc;
                                    Del_doc_Pr.Visibility = Visibility.Visible;
                                    SavePrAndNew.Visibility = Visibility.Hidden;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message.ToString());
                                }

                                break;
                            case 3:
                                try
                                {
                                    Prikaze_3 doc = (Prikaze_3)DocGridPr.SelectedItem;
                                    Reg_numPr.Text = doc.Reg_num.ToString();
                                    Date_regPr.SelectedDate = doc.Date_reg;
                                    GriphPr.SelectedIndex = (int?)doc.Griph == null ? -1 : (int)doc.Griph;
                                    Count_ekzPr.Text = doc.Count_ekz.ToString();
                                    Ekz_numPr.Text = doc.Ekz_num.ToString();
                                    Count_sheetsPr.Text = doc.Count_sheets.ToString();
                                    Count_prilPr.Text = doc.Count_pril.ToString();
                                    CommentPr.Text = doc.Comment;
                                    SummaryPr.Text = doc.Summary;
                                    Fio_podPr.Text = doc.Fio_pod;
                                    Fio_ispPr.Text = doc.Fio_isp;
                                    Form_prCombobox.SelectedIndex = doc.Id_form_prikaz;
                                    objDoc = doc;
                                    Del_doc_Pr.Visibility = Visibility.Visible;
                                    SavePrAndNew.Visibility = Visibility.Hidden;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message.ToString());
                                }

                                break;
                            default:
                                break;
                        }
                        save_doc = 1;

                        Prikazi.Visibility = Visibility.Visible;
                        Serch_pr_grid.Visibility = Visibility.Hidden;
                        Obr_gr.Visibility = Visibility.Hidden;
                        Serch_doc_vh_grid.Visibility = Visibility.Hidden;
                        Reg_doc_vh_grid.Visibility = Visibility.Hidden;
                        Serch_doc_iz_grid.Visibility = Visibility.Hidden;
                        Reg_doc_iz_grid.Visibility = Visibility.Hidden;                         

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ObrGrid(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    DataGrid grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        //This is the code which helps to show the data when the row is double clicked.
                        DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                        //DataRowView dr = (DataRowView)dgr.Item;
                        Appeal_gr doc = (Appeal_gr)grid.CurrentItem;
                        RegObr regObr = new RegObr(doc) ;                        
                        
                        if (regObr.ShowDialog() == true)
                        {
                            Appeal_gr aaa = (Appeal_gr)AppDoc;
                            Appeal_gr aasda = docCon.appeal_Gr.Find(aaa.Id_appeal_gr);
                            aasda.Date_pr = aaa.Date_pr;
                            aasda.Time_pr_s = aaa.Time_pr_s;
                            aasda.Time_pr_do = aaa.Time_pr_do;
                            aasda.FIO_gr = aaa.FIO_gr;
                            aasda.FIO_prin = aaa.FIO_prin;
                            aasda.Bool_pr = aaa.Bool_pr;
                            /*
                             
                             
                            Appeal_gr _appeal_Gr = new Appeal_gr
                            {
                                Id_appeal_gr = aaa.Id_appeal_gr,
                                Date_pr = aaa.Date_pr,
                                Time_pr_s = aaa.Time_pr_s,
                                Time_pr_do = aaa.Time_pr_do,
                                FIO_gr = aaa.FIO_gr,
                                FIO_prin = aaa.FIO_prin,
                                Bool_pr = aaa.Bool_pr,
                            };*/
                            docCon.Entry(aasda).State = EntityState.Modified;
                            docCon.SaveChanges();
                            PrimGrid.Items.Refresh();
                            //PrimGrid.ItemsSource = docCon.appeal_Gr.Local;
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Save_doc_vh_and_New_Click(object sender, RoutedEventArgs e)
        {
            try { 
            Documents_vh doc_vh = new Documents_vh
            {
                Reg_num = Int32.Parse(Reg_num_textbox.Text),
                Griph = Griph_textbox.Text,
                Data_reg = Date_reg_textbox.SelectedDate,
                Data_doc_el = Date_doc_el_textbox.SelectedDate,
                Post_num = Int32.Parse(Post_num_textbox.Text),
                Post_data = Post_data_textbox.SelectedDate,
                Bool_post_data = Bool_post_data_checkbox.IsChecked,
                Data_isp = Data_ips_textbox.SelectedDate,
                Copies_num = Int32.Parse(Copies_num_textbox.Text),
                Sheets_doc_el = Int32.Parse(Sheets_doc_el_textbox.Text),
                Sheets_doc = Int32.Parse(Sheets_doc_textbox.Text),
                Sheets_apps_doc = Int32.Parse(Sheets_apps_doc_textbox.Text),
                Sheets_notes_doc = Int32.Parse(Sheets_notes_doc_textbox.Text),
                Sign_doc = Sign_doc_textbox.Text,
                Bool_sign_doc = Bool_sign_doc_checkbox.IsChecked,
                From_doc = From_doc_textbox.Text,
                Adr_doc = Adr_doc_textbox.Text,
                Summary_doc = Summary_doc_textbox.Text,
                Comment_doc = Comment_doc_textbox.Text,
                Id_form_doc = (int?)Form_doc_vh.SelectedValue, // == null ? null : (int?)Form_doc_vh.SelectedValue,
                Id_type_doc = (int?)Type_doc_vh.SelectedValue,// == null ? null : (int?)Type_doc_vh.SelectedValue,
            };

            if (save_doc == 0)
            {
                docCon.Doc_vh.Add(doc_vh);
                docCon.SaveChanges();
                NullBoxVH();
            }
             } catch(Exception errorShow)
            {
                MessageBox.Show(errorShow.Message);
            }

}
    }
}

