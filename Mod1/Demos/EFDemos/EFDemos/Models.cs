using System;
using System.Collections.Generic;
using System.Text;

namespace EFDemos
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public ICollection<ApprenticeLink> TeachersId { get; set; }
        public Teacher Referent { get; set; }
        public int? ReferentId { get; set; } // Utiliser le "?" sur un type primitif (structure) en fait quelque chose de "nullable" ==> peut-être non défini
    }

    public class ApprenticeLink
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public ICollection<ApprenticeLink> StudentsIds { get; set; }
        public ICollection<Student> ReferalsStudents { get; set; }
    }
}
