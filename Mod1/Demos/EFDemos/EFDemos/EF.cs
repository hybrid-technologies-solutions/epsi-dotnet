using FluentAssertions;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace EFDemos
{
    public class EF
    {
        [Fact]
        public void EF_Core_Usage()
        {
            if (File.Exists("test.db"))
            {
                File.Delete("test.db");
            }

            using (var ctx = new TestDbContext())
            {
                ctx.Database.EnsureCreated();

                ctx.Students.Add(new Student
                {
                    Name = "Tywin",
                    Lastname = "Lannister"
                });

                ctx.Students.Add(new Student
                {
                    Name = "Jon",
                    Lastname = "Snow"
                });

                ctx.SaveChanges();
            }

            using(var ctx = new TestDbContext())
            {
                var students = ctx.Students.ToList();

                students.Any(s => s.Lastname == "Snow").Should().BeTrue();
            }
        }

        [Fact]
        public void EF_Linq()
        {
            if (File.Exists("test.db"))
            {
                File.Delete("test.db");
            }

            using (var ctx = new TestDbContext())
            {
                ctx.Database.EnsureCreated();

                ctx.Students.Add(new Student
                {
                    Name = "Tywin",
                    Lastname = "Lannister"
                });

                ctx.Students.Add(new Student
                {
                    Name = "Cersei",
                    Lastname = "Lannister"
                });

                ctx.Students.Add(new Student
                {
                    Name = "Jon",
                    Lastname = "Snow"
                });

                ctx.SaveChanges();
            }

            using (var ctx = new TestDbContext())
            {
                var jonSnow = ctx.Students.Where(m => m.Name == "Jon").First();
                jonSnow.Lastname.Should().Be("Snow");

                var lastStudentByName = ctx.Students.OrderBy(m => m.Name).Last();
                lastStudentByName.Lastname.Should().Be("Lannister");

                var lannisters = ctx.Students.AsEnumerable()
                    .GroupBy(s => s.Lastname)
                    .Where(c => c.Key == "Lannister")
                    .SelectMany(c => c.ToList());
                lannisters.Should().HaveCount(2);
            }
        }
    }
}
