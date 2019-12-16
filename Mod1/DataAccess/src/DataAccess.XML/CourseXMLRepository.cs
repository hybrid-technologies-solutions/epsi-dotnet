using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using DataAccess.Common.Abstractions;
using DataAccess.Common.Models;

namespace DataAccess.XML
{
    public class CourseXMLRepository : ICourseRepository
    {
        private readonly XDocument document;
        private readonly string rootNodeName = "courses";
        private readonly string nodesName = "course";

        public CourseXMLRepository(
            string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException($"XMLBaseRepository.ctor() : {fileName} don't exists.");
            }
            document = XDocument.Parse(File.ReadAllText(fileName));
        }

        public Course FindById(int id)
            => GetAll().FirstOrDefault(c => c.Id == id);

        public IEnumerable<Course> GetAll()
        {
            var nodes = document.Root.Elements(nodesName);
            var courses = new List<Course>();
            foreach (var node in nodes)
            {
                courses.Add(ParseNode(node));
            }
            return courses;
        }

        public void Remove(Course entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(Course entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Course entity)
        {
            throw new NotImplementedException();
        }

        private Course ParseNode(XElement element)
            => throw new NotImplementedException();
    }
}
