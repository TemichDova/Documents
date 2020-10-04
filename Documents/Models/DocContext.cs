using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Documents.Models
{
    class DocContext : DbContext
    {
        public DocContext():base("DefaultConnection") {} // подключение к БД
        public DbSet<Documents_vh> Doc_vh { get; set; } // таблицы бд
        public DbSet<Documents_iz> Doc_iz { get; set; }
        public DbSet<Characteristic_doc> Character_doc { get; set; }
        public DbSet<FIO_sotr> Fio_s { get; set; }
        public DbSet<Form_doc> Form_Docs { get; set; }
       
        public DbSet<Razdel> Razdels { get; set; }
        public DbSet<Status_doc> Status_docs { get; set; }
        public DbSet<Type_doc> Type_docs { get; set; }
        // public DbSet<Org_FIO> Org_FIOs { get; set; }
        public DbSet<Authorization_BD> LoginsBD { get; set; }

        public DbSet<Appeal_gr> appeal_Gr { get; set; }
        public DbSet<Prikaze_1> prikaze_1 { get; set; }
        public DbSet<Prikaze_2> prikaze_2 { get; set; }
        public DbSet<Prikaze_3> prikaze_3 { get; set; }
        public DbSet<Form_pr> from_Pr { get; set; }
    }

    class LogCon : DbContext
    {
        public LogCon() : base("DefaultConnection") { }

        public DbSet<Authorization_BD> GetLogins { get; set; }


    }

}
