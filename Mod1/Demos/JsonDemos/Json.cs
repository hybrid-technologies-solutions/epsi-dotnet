using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JsonDemos
{
    public class Json
    {
        private class Student
        {
            public string Name { get; set; }
            public string Lastname { get; set; }
        }

        [Fact]
        public void ParseJson()
        {
            string json =
@"[
    {
       ""name"": ""Jon"",
       ""lastname"": ""Snow""
    },
    {
       ""name"": ""Tywin"",
       ""lastname"": ""Lannister""
    }
]";

            List<Student> students = JsonConvert.DeserializeObject<Student[]>(json).ToList();
            students.Should().HaveCount(2);

        }

        [Fact]
        public void WriteJson()
        {
            string expected =
@"[
    {
       ""name"": ""Jon"",
       ""lastname"": ""Snow""
    },
    {
       ""name"": ""Tywin"",
       ""lastname"": ""Lannister""
    }
]".Replace(" ", "").Replace(Environment.NewLine, "");
            List<Student> students = new List<Student>
            {
                new Student
                {
                    Name = "Jon",
                    Lastname= "Snow"
                },
                new Student
                {
                    Name="Tywin",
                    Lastname = "Lannister"
                }
            };

            string json = JsonConvert.SerializeObject(students).Replace(" ", "");
            json.Equals(expected, StringComparison.OrdinalIgnoreCase).Should().BeTrue();
        }
    }
}
