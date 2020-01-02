using DataAccess.Common.Abstractions;
using DataAccess.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccess.Implementations
{
    public class CourseJsonRepository : ICourseRepository
    {
        private readonly string fileName;
        private List<Course> courses = new List<Course>();

        public CourseJsonRepository(
            string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException($"CourseJsonRepository.ctor() : {fileName} don't exists.");
            }
            this.fileName = fileName; 
            courses = JsonConvert.DeserializeObject<List<Course>>(File.ReadAllText(fileName));
        }

        public Course FindById(int id)
            => GetAll().FirstOrDefault(c => c.Id == id);

        public IEnumerable<Course> GetAll()
        {
            return courses;
        }

        public void Remove(Course entity)
        {
            courses.RemoveAll(c => c.Id == entity.Id);
        }

        public void Insert(Course entity)
        {
            courses.Add(entity);
        }

        public void Update(Course entity)
        {
            Remove(entity);
            Insert(entity);
        }

        public void Save()
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(courses));
        }
    }
}
