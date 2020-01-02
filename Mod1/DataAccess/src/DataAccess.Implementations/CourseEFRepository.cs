using DataAccess.Common.Abstractions;
using DataAccess.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Course course)
        {
            throw new NotImplementedException();
        }

        public void Remove(Course course)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
