using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Xunit;

namespace XMLDemos
{
    public class XML
    {

        private class Student
        {
            public string Name { get; set; }
            public string Lastname { get; set; }
        }

        [Fact]
        public void SimpleXML()
        {

            string xml =
@"<?xml version='1.0' encoding='UTF-8'?>
<students>
  <student>
    <name>Jon</name>
    <lastname>Snow</lastname>
  </student>
  <student>
    <name>Tywin</name>
    <lastname>Lannister</lastname>
  </student>
</students>
";

            XDocument document = XDocument.Parse(xml);

            XElement elementStudents = document.Root;
            IEnumerable<XElement> elementsStudent = document.Root.Elements("student");

            List<Student> students = new List<Student>();

            foreach (var item in elementsStudent)
            {
                students.Add(new Student
                {
                    Name = item.Element("name").Value,
                    Lastname = item.Element("lastname").Value
                });
            }
            students.Should().HaveCount(2);
        }

        [Fact]
        public void SimpleXML_Attributes()
        {
            string xml =
   @"<?xml version='1.0' encoding='UTF-8'?>
<students>
  <student name='Jon' lastname='Snow'/>
  <student name='Tywin' lastname='Lannister' />
</students>
";

            XDocument document = XDocument.Parse(xml);

            XElement elementStudents = document.Root;
            IEnumerable<XElement> elementsStudent = document.Root.Elements("student");

            List<Student> students = new List<Student>();

            foreach (var item in elementsStudent)
            {
                students.Add(new Student
                {
                    Name = item.Attribute("name").Value,
                    Lastname = item.Attribute("lastname").Value
                });
            }
            students.Should().HaveCount(2);
        }

    }
}
