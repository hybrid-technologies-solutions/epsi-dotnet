using DataAccess.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Common.Abstractions
{
    public interface ICourseRepository
    {
        void Insert(Course course);
        void Update(Course course);
        void Remove(Course course);
        IEnumerable<Course> GetAll();
        Course FindById(int id);
    }
}
