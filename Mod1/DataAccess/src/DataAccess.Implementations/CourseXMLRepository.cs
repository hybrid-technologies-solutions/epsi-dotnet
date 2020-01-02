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

namespace DataAccess.Implementations
{
    public class CourseXMLRepository : ICourseRepository
    {
        private readonly XDocument document;
        private readonly string nodesName = "course";
        private readonly string fileName;

        public CourseXMLRepository(
            string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException($"CourseXMLRepository.ctor() : {fileName} don't exists.");
            }
            document = XDocument.Parse(File.ReadAllText(fileName));
            this.fileName = fileName;
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
            var concernedElement = document
                .Root
                .Elements(nodesName)
                .FirstOrDefault(e => int.Parse(e.Attribute("id").Value) == entity.Id);
            if (concernedElement != null)
            {
                concernedElement.Remove();
            }
        }

        public void Insert(Course entity)
        {
            document.Root.Add(GetNodeFromCourse(entity));
        }

        public void Update(Course entity)
        {
            Remove(entity);
            Insert(entity);
        }

        public void Save()
        {
            File.WriteAllText(fileName, document.ToString());
        }

        private XElement GetNodeFromCourse(Course entity)
        {
            return new XElement(nodesName,
                   new XAttribute("name", entity.CourseName),
                   new XAttribute("id", entity.Id),
                   new XElement("details",
                       new XAttribute("difficulty", (int)entity.Difficulty),
                       new XAttribute("duration", entity.DurationInDays)));
        }

        private Course ParseNode(XElement element)
        {
            return new Course
            {
                CourseName = element.Attribute("name").Value,
                Difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), element.Element("details").Attribute("difficulty").Value),
                Id = int.Parse(element.Attribute("id").Value),
                DurationInDays = int.Parse(element.Element("details").Attribute("duration").Value)
            };
        }

    }
}
