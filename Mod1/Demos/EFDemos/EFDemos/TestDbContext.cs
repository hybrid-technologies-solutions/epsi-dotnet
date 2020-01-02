using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDemos
{
    public class TestDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // création de la classe de configuration pour l'entité "Student"
            var studentEntity = modelBuilder.Entity<Student>();
            // définition du mapping
            studentEntity.HasKey(m => m.Id); // définition du champ clé
            studentEntity.Property(m => m.Name).HasMaxLength(256).IsRequired(); // définition de contrainte
            studentEntity.Property(m => m.Lastname).HasMaxLength(256).IsRequired(); // définition de contrainte
            //création des relations

            studentEntity // Un étudiant a ...
                .HasOne(m => m.Referent) // un professeur référent ...
                .WithMany(m => m.ReferalsStudents) // qui suit plusieurs étudiants ...
                .HasForeignKey(m => m.ReferentId); // et son Id est xxx

            studentEntity // Un étudiant a ...
                .HasMany(m => m.TeachersId) // plusieurs professeurs différents ...
                .WithOne(t => t.Student) // qui le connaisse ...
                .HasForeignKey(t => t.StudentId); // par son id 

            // création de la classe de configuration pour l'entité "Teacher"
            var teacherEntity = modelBuilder.Entity<Teacher>();
            // définition du mapping
            teacherEntity.HasKey(m => m.Id);
            teacherEntity.Property(m => m.Name).HasMaxLength(256).IsRequired(); // définition de contrainte
            teacherEntity.Property(m => m.Lastname).HasMaxLength(256).IsRequired(); // définition de contrainte
            //création des relations

            teacherEntity // Un professeur a ...
                .HasMany(m => m.ReferalsStudents) // plusieurs étudiants qu'il suit ...
                .WithOne(m => m.Referent) // et qui le connaisse ...
                .HasForeignKey(m => m.ReferentId); // par son id

            teacherEntity // Un professeur a ...
                .HasMany(m => m.StudentsIds) // plusieurs étudiants différents ...
                .WithOne(t => t.Teacher) // qui le connaissse ...
                .HasForeignKey(t => t.TeacherId); // par son id

            // création de la classe de configuration pour l'entité "ApprenticeLink"
            var teacherStudentLink = modelBuilder.Entity<ApprenticeLink>();
            //définition du mapping
            teacherStudentLink.HasKey(m => new { m.TeacherId, m.StudentId }); // définition d'une clé composée


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // définition de la base de données à utiliser ainsi que de la chaine de connexion
            optionsBuilder.UseSqlite("Filename=test.db");

            base.OnConfiguring(optionsBuilder);
        }

    }
}
