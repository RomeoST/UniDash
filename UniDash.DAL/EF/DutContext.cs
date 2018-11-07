using System;
using System.Collections.Generic;
using UniDash.Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniDash.Model.Models.StructUniversity;

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
            modelBuilder.Entity<Institute>().HasData(
                new Institute { Id = 1, Name = "Телекомунікацій та інформатизації"},
                new Institute { Id = 2, Name = "Захисту інформації"},
                new Institute { Id = 3, Name = "Менеджменту та підприємництва"},
                new Institute { Id = 4, Name = "Заочного та Дистанційного навчання" }, 
                new Institute { Id = 5, Name = "Післядипломної освіти" },
                new Institute { Id = 6, Name = "Гуманітарних та природничих дисциплін"});

            modelBuilder.Entity<Faculty>().HasData(
                new Faculty { Id = 1, Name = "Інформаційних технологій"},
                new Faculty { Id = 2, Name = "Телекомунікацій"});

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1,  InstituteId = 2, Name = "Інформаційної та кібернетичної безпеки "},
                new Department { Id = 2,  InstituteId = 2, Name = "Управління інформаційною та кібернетичною безпекою"},
                new Department { Id = 3,  InstituteId = 2, Name = "Систем інформаційного та кібернетичного захисту"},
                new Department { Id = 4,  InstituteId = 3, Name = "Менеджменту" },
                new Department { Id = 5,  InstituteId = 3, Name = "Маркетингу" },
                new Department { Id = 6,  InstituteId = 3, Name = "Підприємництва, торгівлі та біржової діяльності" },
                new Department { Id = 7,  InstituteId = 3, Name = "Економіки підприємств та соціальних технологій" },
                new Department { Id = 8,  InstituteId = 3, Name = "Документознавства та інформаційної діяльності" },
                new Department { Id = 9,  InstituteId = 3, Name = "Публичного управління та адміністрування" },
                new Department { Id = 10, InstituteId = 6, Name = "Вищої математики" },
                new Department { Id = 11, InstituteId = 6, Name = "Іноземних мов" },
                new Department { Id = 12, InstituteId = 6, Name = "Фізики" },
                new Department { Id = 13, InstituteId = 6, Name = "Фізичної культури та охорони праці" },
                new Department { Id = 14, FacultyId = 1,   Name = "Комп'ютерних наук" },
                new Department { Id = 15, FacultyId = 1,   Name = "Інформаційних систем та технологій" },
                new Department { Id = 16, FacultyId = 1,   Name = "Комп'ютерної інженерії" },
                new Department { Id = 17, FacultyId = 1,   Name = "Системного аналізу" },
                new Department { Id = 18, FacultyId = 1,   Name = "Інженерії програмного забезпечення" },
                new Department { Id = 19, FacultyId = 2,   Name = "Енергоефективних технологій" },
                new Department { Id = 20, FacultyId = 2,   Name = "Космічних систем та комплексів і супутникових телекомунікацій" },
                new Department { Id = 21, FacultyId = 2,   Name = "Мобільних та відеоінформаційних технологій" },
                new Department { Id = 22, FacultyId = 2,   Name = "Телекомунікаційних систем" },
                new Department { Id = 23, FacultyId = 2,   Name = "Телекомунікаційних технологій" }
                );

            modelBuilder.Entity<Specialty>().HasData(
                new Specialty { Id = 1, Code = "029", Name = "Інформаційна, бібліотечна та архівна справа" },
                new Specialty { Id = 2, Code = "051", Name = "Економіка" },
                new Specialty { Id = 3, Code = "054", Name = "Соціологія" },
                new Specialty { Id = 4, Code = "073", Name = "Менеджмент" },
                new Specialty { Id = 5, Code = "075", Name = "Маркетинг" },
                new Specialty { Id = 6, Code = "076", Name = "Підприємництво, торгівля та біржова діяльність" },
                new Specialty { Id = 7, Code = "121", Name = "Інженерія програмного забезпечення" },
                new Specialty { Id = 8, Code = "122", Name = "Комп’ютерні науки" },
                new Specialty { Id = 9, Code = "123", Name = "Комп’ютерна інженерія" },
                new Specialty { Id = 10, Code = "124", Name = "Системний аналіз" },
                new Specialty { Id = 11, Code = "125", Name = "Кібербезпека" },
                new Specialty { Id = 12, Code = "126", Name = "Інформаційні системи та технології" },
                new Specialty { Id = 13, Code = "172", Name = "Телекомунікації та радіотехніка" },
                new Specialty { Id = 14, Code = "171", Name = "Електроніка" },
                new Specialty { Id = 15, Code = "173", Name = "Авіоніка" },
                new Specialty { Id = 16, Code = "281", Name = "Публічне управління та адміністрування" } 
                );

            modelBuilder.Entity<Applicant>().HasData(
                new Applicant
                {
                    ApplicantId = 1,
                    Address = "м.Київ",
                    MailApplicant = "vikaklimenko2001@ukr.net",
                    NameApplicant = "Клименко Вікторія Олександрівна",
                    PhoneApplicant = "634466218",
                    NameFound = "Вальченко О.І.",
                    SchoolCollege = "СШ №218",
                    Speciality = "8 12",
                    DateAdd = new DateTime(2017,10,10,13,43,10),
                    MarkResult = "",
                    //UserId = "DEV_TEST"
                },
                new Applicant
                {
                    ApplicantId = 2,
                    Address = "м.Київ",
                    MailApplicant = "sasha.lazarenko@neoil.ua",
                    NameApplicant = "Мумінов Асір Олександрович",
                    PhoneApplicant = "984196839",
                    NameFound = "Вальченко О.І.",
                    SchoolCollege = "СШ №213",
                    Speciality = "1 4 7",
                    DateAdd = new DateTime(2017, 10, 11, 13, 43, 10),
                    MarkResult = "",
                    //UserId = "DEV_TEST"
                }
                );
            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Need for use virtual
            optionsBuilder.UseLazyLoadingProxies();
        }

        public virtual DbSet<UserDetails> ClientProfiles { get; set; }
        public virtual DbSet<SubmissionDoc> SubmissionDocs { get; set; }
        public virtual DbSet<Applicant>     Applicants     { get; set; }

        // DB for structure of university
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Institute> Institutes { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }

        // DB for functions
        public virtual DbSet<TController> Controllers { get; set; }
        public virtual DbSet<TAction> Actions { get; set; }
        public virtual DbSet<PermissionRoles> PermissionRoles { get; set; }
    }
}
