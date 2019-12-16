using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using DataAccess.Implementations;
using DataAccess.Common.Models;

namespace DataAccess.Tests
{
    public class CourseJsonRepositoryTests
    {
        [Fact]
        public void Ctor_Should_Throws_If_File_Not_Exists()
        {
            Assert.Throws<ArgumentException>(() => new CourseJsonRepository("unknow.json"));
        }

        [Fact]
        public void GetAll_Should_Retrieve_All_Data()
        {
            var repo = new CourseJsonRepository("./Data/courses.json");

            var allCourses = repo.GetAll();

            allCourses.Should().HaveCount(3);
        }

        [Fact]
        public void FindByIdAsync_Should_Retrieve_Correct_Item()
        {
            var repo = new CourseJsonRepository("./Data/courses.json");

            var wpfCourse = repo.FindById(2);

            wpfCourse.CourseName.Should().Be("Apprendre WPF avec Prism");
            wpfCourse.Id.Should().Be(2);
        }

        [Fact]
        public void Remove_Should_Remove_Element_From_JSON_Document()
        {
            var repo = new CourseJsonRepository("./Data/courses.json");

            var wpfElement = new Course
            {
                Id = 2,
                CourseName = "Apprendre WPF avec Prism"
            };

            repo.Remove(wpfElement);

            var elements = repo.GetAll();
            elements.Should().HaveCount(2);
            elements.Any(e => e.Id == 2).Should().BeFalse();
        }

        [Fact]
        public void Insert_Should_Add_Element_ToDocument()
        {
            var repo = new CourseJsonRepository("./Data/courses.json");

            var xamarinCourse = new Course
            {
                Id = 4,
                CourseName = "Apprendre Xamarin",
                DurationInDays = 10,
                Difficulty = Difficulty.Medium
            };

            repo.Insert(xamarinCourse);

            var elements = repo.GetAll();
            elements.Should().HaveCount(4);
            elements.Any(e => e.Id == 4).Should().BeTrue();
            elements.Any(e => e.CourseName == "Apprendre Xamarin").Should().BeTrue();
        }

        [Fact]
        public void Update_Should_Update_Document()
        {
            var repo = new CourseJsonRepository("./Data/courses.json");

            var wpfElement = new Course
            {
                Id = 2,
                CourseName = "Apprendre WPF"
            };

            repo.Update(wpfElement);

            var elements = repo.GetAll();
            elements.Should().HaveCount(3);
            elements.Any(e => e.Id == 2).Should().BeTrue();
            elements.Any(e => e.CourseName == "Apprendre WPF").Should().BeTrue();
            elements.Any(e => e.CourseName == "Apprendre WPF avec Prism").Should().BeFalse();
        }
    }
}
