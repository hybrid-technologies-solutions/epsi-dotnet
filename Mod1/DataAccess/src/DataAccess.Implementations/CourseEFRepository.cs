using DataAccess.Common.Abstractions;
using DataAccess.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Implementations
{
    public class DemoDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=demos.db");
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class CourseEFRepository : ICourseRepository
    {
        private readonly DemoDbContext context;

        public CourseEFRepository(
            DemoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Course FindById(int id)
        {
            return context.Courses.Find(id);
        }

        public IEnumerable<Course> GetAll()
        {
            return context.Courses.ToList();
        }

        public void Insert(Course course)
        {
            context.Courses.Add(course);
        }

        public void Remove(Course course)
        {
            context.Courses.Remove(course);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Course course)
        {
            context.Courses.Update(course);
        }
    }
}
