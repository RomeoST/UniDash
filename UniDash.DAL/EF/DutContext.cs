using System.Collections.Generic;
using UniDash.Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UniDash.DAL.EF
{
    public class DutContext : IdentityDbContext<DutUser>
    {
        public DutContext(DbContextOptions<DutContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institute>().HasData(new List<Institute>
            {
                new Institute {Name = "Телекомунікацій та інформатизації"},
                new Institute
                {
                    Name = "Захисту інформації", Departments = new List<Department>
                    {
                        new Department {Name = "Інформаційної та кібернетичної безпеки "},
                        new Department {Name = "Управління інформаційною та кібернетичною безпекою"},
                        new Department {Name = "Систем інформаційного та кібернетичного захисту"}
                    }
                },
                new Institute
                {
                    Name = "Менеджменту та підприємництва", Departments = new List<Department>
                    {
                        new Department {Name = "Менеджменту"},
                        new Department {Name = "Маркетингу"},
                        new Department {Name = "Підприємництва, торгівлі та біржової діяльності"},
                        new Department {Name = "Економіки підприємств та соціальних технологій"},
                        new Department {Name = "Документознавства та інформаційної діяльності"},
                        new Department {Name = "Публичного управління та адміністрування"}
                    }
                },
                new Institute {Name = "Заочного та Дистанційного навчання"},
                new Institute {Name = "Післядипломної освіти"},
                new Institute
                {
                    Name = "Гуманітарних та природничих дисциплін", Departments = new List<Department>
                    {
                        new Department {Name = "Вищої математики"},
                        new Department {Name = "Іноземних мов"},
                        new Department {Name = "Фізики"},
                        new Department {Name = "Фізичної культури та охорони праці"}
                    }
                }
            });
            modelBuilder.Entity<Faculty>().HasData(new List<Faculty>
            {
                new Faculty
                {
                    Name = "Інформаційних технологій", Departments = new List<Department>
                    {
                        new Department {Name = "Комп'ютерних наук"},
                        new Department {Name = "Інформаційних систем та технологій"},
                        new Department {Name = "Комп'ютерної інженерії"},
                        new Department {Name = "Системного аналізу"},
                        new Department {Name = "Інженерії програмного забезпечення"}
                    }
                },
                new Faculty
                {
                    Name = "Телекомунікацій", Departments = new List<Department>
                    {
                        new Department {Name = "Енергоефективних технологій"},
                        new Department {Name = "Космічних систем та комплексів і супутникових телекомунікацій"},
                        new Department {Name = "Мобільних та відеоінформаційних технологій"},
                        new Department {Name = "Телекомунікаційних систем"},
                        new Department {Name = "Телекомунікаційних технологій"}
                    }
                }
            });
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<UserDetails> ClientProfiles { get; set; }
        public virtual DbSet<SubmissionDoc> SubmissionDocs { get; set; }
        public virtual DbSet<Applicant>     Applicants     { get; set; }

        // DB for structure of university
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Institute> Institutes { get; set; }

        // DB for functions
        public virtual DbSet<TController> Controllers { get; set; }
        public virtual DbSet<TAction> Actions { get; set; }
        public virtual DbSet<PermissionRoles> PermissionRoleses { get; set; }
    }
}
