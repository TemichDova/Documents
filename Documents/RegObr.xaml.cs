using Documents.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Documents
{
    /// <summary>
    /// Логика взаимодействия для RegObr.xaml
    /// </summary>
    public partial class RegObr : Window
    {
        public Appeal_gr appeal_Gr { get; set; }
        DocContext docCon { get; set; }
        private static int save_app = -1;
        public RegObr(Appeal_gr _appeal_Gr)
        {
            InitializeComponent();
           
            
           
            
            docCon = new DocContext();
            //
            appeal_Gr = _appeal_Gr;
            if (appeal_Gr != null)
            {
                save_app = 1;
                Date_prBox.SelectedDate = _appeal_Gr.Date_pr;
                Time_pr_sBox.Text = _appeal_Gr.Time_pr_s.ToString();
                Time_pr_doBox.Text = _appeal_Gr.Time_pr_do.ToString();
                FIO_grBox.Text = _appeal_Gr.FIO_gr;
                FIO_prinBox.Text = _appeal_Gr.FIO_prin;
                BoolCheck.IsChecked = _appeal_Gr.Bool_pr;
            }
            else
            {
                save_app = 0;
            }
        }

        private void Save_obr_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (save_app == 0)
                {
                    Appeal_gr _appeal_Gr = new Appeal_gr
                    {
                        
                        Date_pr = Date_prBox.SelectedDate,
                        Time_pr_s = TimeSpan.Parse(Time_pr_sBox.Text),
                        Time_pr_do = TimeSpan.Parse(Time_pr_doBox.Text),
                        FIO_gr = FIO_grBox.Text,
                        FIO_prin = FIO_prinBox.Text,
                        Bool_pr = BoolCheck.IsChecked,
                    };
                    MainWindow.AppDoc = _appeal_Gr;
               //     docCon.appeal_Gr.Add(_appeal_Gr);
                //    docCon.SaveChanges();
                    Date_prBox = null;
                    Time_pr_doBox.Clear();
                    Time_pr_sBox.Clear();
                    FIO_grBox.Clear();
                    FIO_prinBox.Clear();
                    BoolCheck = null;
                    this.DialogResult = true;
                    //this.Close();
                }
                if (save_app == 1)
                {
                    Appeal_gr _appeal_Gr = new Appeal_gr
                    {
                        Id_appeal_gr = appeal_Gr.Id_appeal_gr,
                        Date_pr = Date_prBox.SelectedDate,
                        Time_pr_s = TimeSpan.Parse(Time_pr_sBox.Text),
                        Time_pr_do = TimeSpan.Parse(Time_pr_doBox.Text),
                        FIO_gr = FIO_grBox.Text,
                        FIO_prin = FIO_prinBox.Text,
                        Bool_pr = BoolCheck.IsChecked,
                    };
                    
                    MainWindow.AppDoc = _appeal_Gr;
                    //docCon.Entry(_appeal_Gr).State = EntityState.Modified;
                   // docCon.SaveChanges();
                    Date_prBox = null;
                    Time_pr_doBox.Clear();
                    Time_pr_sBox.Clear();
                    FIO_grBox.Clear();
                    FIO_prinBox.Clear();
                    BoolCheck = null;
                    // mainWindow.PrimGrid.ItemsSource = docCon.appeal_Gr.Local;
                    this.DialogResult = true;
                    // App.Current.Windows as MainWindow;
                    //this.Close();
                }
                
            }
            catch (Exception errorShow)
            {
                MessageBox.Show(errorShow.Message);
            }

        }
    }
}
