using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Models
{
    class Doc_db_forms
    {
        
    }
    public class Documents_vh
    {
        [Key]
        public int Id_doc_vh { get; set; }

        public int Reg_num { get; set; }
        public DateTime? Data_reg { get; set; }
        public int? Copies_num { get; set; }
        public int? Sheets_doc_el { get; set; }
        public int? Sheets_doc { get; set; }
        public string Link_doc { get; set; }
        public int? Sheets_notes_doc { get; set; }
        public int? Sheets_apps_doc { get; set; }
        public string Sign_doc { get; set; } // ФИО подписи
        public string From_doc { get; set; } // Откуда, организация
        public int? Id_Div_iz { get; set; }
        public string Comment_doc { get; set; }
        public string Summary_doc { get; set; }
        public string Adr_doc { get; set; }
        public string Griph { get; set; }
        public DateTime? Data_doc_el { get; set; }
        public int? Post_num { get; set; }
        public DateTime? Post_data { get; set; }
        public DateTime? Data_isp { get; set; }
        public bool? Bool_post_data { get; set; }
        public bool? Bool_sign_doc { get; set; }
        public int? Id_type_doc { get; set; }
        [ForeignKey("Id_type_doc")]
        public Type_doc type_Doc { get; set; }
        public int? Id_status_doc { get; set; }
        public int? Id_form_doc { get; set; }
        [ForeignKey("Id_form_doc")]
        public Form_doc form_Doc { get; set; }
        public int? Id_characteristic_doc { get; set; }

    }
    public class Documents_iz
    {
        [Key]
        public int Id_doc_iz { get; set; }

        public int Reg_num { get; set; }
        public DateTime? Data_reg { get; set; }
        public int? Copies_num { get; set; }
        public int? Sheets_doc_el { get; set; }
        public int? Sheets_doc { get; set; }
        public string Link_doc { get; set; }
        public int? Sheets_notes_doc { get; set; }
        public int? Sheets_apps_doc { get; set; }
        public string Comment_doc { get; set; }
        public string Summary_doc { get; set; }
        public string Griph { get; set; }
        public int? Post_num_isp { get; set; }
        public int? Post_num { get; set; }
        public int? Isp_num_doc { get; set; }
        public DateTime? Post_data { get; set; }
        public int? Id_type_doc { get; set; }
        public int? Id_status_doc { get; set; }
        public int? Id_form_doc { get; set; }
        public int? Id_characteristic_doc { get; set; }
        public bool? Bool_el_doc { get; set; }
        public string Shtrih_kod { get; set; }
        public bool? Bool_dop_doc { get; set; }
        public string FIO_podpis { get;set;}
        public string Org_podpis { get; set; }
        public string FIO_ispoln { get; set; }
        public string Org_ispoln { get; set; }
        public int? Id_razdel { get; set; }


    }
    public class Characteristic_doc
    {
        [Key]
        public int Id_characteristic_doc { get; set; }
        public string Name_Characteristic_doc { get; set; }
    }
    public class FIO_sotr
    {
        [Key]
        public int Id_FIo { get; set; }
        public string F_sotr { get; set; }
        public string I_sotr { get; set; }
        public string O_sotr { get; set; }

    }
    public class Form_doc
    {
        [Key]
        public int Id_form_doc { get; set; }
        public string Name_form_doc { get; set; }

        public List<Documents_vh>  documents_Vhs { get; set; }
    }
    public class Org_FIO
    {
        public int? Id_org { get; set; }
        public int? Id_FIo { get; set; }
    }
    public class Organization
    {
        [Key]
        public int Id_org { get; set; }
        public string Name_org { get; set; }
    }
    [Table("Razdel")]
    public class Razdel
    {
        [Key]
        public int Id_razdel { get; set; }
        public string Name_razdel { get; set; }
    }
    public class Status_doc
    {
        [Key]
        public int Id_status_doc { get; set; }
        public string Name_status_doc { get; set; }
    }
    public class Type_doc
    {
        [Key]
        public int Id_type_doc { get; set; }
        public string Name_type_doc { get; set; }
        public List<Documents_vh> documents_Vhs { get; set; }
    }
    public class Authorization_BD
    {
        [Key]
        public int Id_user { get; set; }
        public string Lg { get; set; }
        public string Ps { get; set; }
    }
    public class Appeal_gr
    {
        [Key]
        public int Id_appeal_gr { get; set; }
        public DateTime? Date_pr { get; set; }
        public TimeSpan? Time_pr_s { get; set; }
        public TimeSpan? Time_pr_do { get; set; }
        public string FIO_prin { get; set; }
        public string FIO_gr { get; set; }
        public bool? Bool_pr { get; set; }
    }
    public class Prikaze_1
    {

        [Key]
        public int Id_prikaz { get; set; }
        public int? Reg_num { get; set; }
        public DateTime? Date_reg { get; set; }
        public int? Griph { get; set; }
        public int? Count_ekz { get; set; }
        public int? Ekz_num { get; set; }
        public int? Count_sheets { get; set; }
        public int? Count_pril { get; set; }
        public string Comment { get; set; }
        public string Summary { get; set; }
        public string Fio_pod { get; set; }
        public string Fio_isp { get; set; }
        public int Id_form_prikaz { get; set; }
    }
    public class Prikaze_2
    {

        [Key]
        public int Id_prikaz { get; set; }
        public int? Reg_num { get; set; }
        public DateTime? Date_reg { get; set; }
        public int? Griph { get; set; }
        public int? Count_ekz { get; set; }
        public int? Ekz_num { get; set; }
        public int? Count_sheets { get; set; }
        public int? Count_pril { get; set; }
        public string Comment { get; set; }
        public string Summary { get; set; }
        public string Fio_pod { get; set; }
        public string Fio_isp { get; set; }
        public int Id_form_prikaz { get; set; }
    }
    public class Prikaze_3
    {

        [Key]
        public int Id_prikaz { get; set; }
        public int? Reg_num { get; set; }
        public DateTime? Date_reg { get; set; }
        public int? Griph { get; set; }
        public int? Count_ekz { get; set; }
        public int? Ekz_num { get; set; }
        public int? Count_sheets { get; set; }
        public int? Count_pril { get; set; }
        public string Comment { get; set; }
        public string Summary { get; set; }
        public string Fio_pod { get; set; }
        public string Fio_isp { get; set; }
        public int Id_form_prikaz { get; set; }
    }
    public class Form_pr
    {
        [Key]
        public int Id_form_prikaz { get; set; }
        public string Name_form { get; set; }
    }
}
