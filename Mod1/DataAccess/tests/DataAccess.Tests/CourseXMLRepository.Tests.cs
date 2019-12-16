using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DataAccess.XML.Tests
{
    public class CourseXMLRepositoryTests
    {
        [Fact]
        public void Ctor_Should_Throws_If_File_Not_Exists()
        {
            Assert.Throws<ArgumentException>(() => new CourseXMLRepository("unknow.xml"));
        }

        [Fact]
        public async Task GetAll_Should_Retrieve_All_Data()
        {
            var repo = new CourseXMLRepository("./Data/courses.xml");

            var allCourses = repo.GetAll();

            allCourses.Should().HaveCount(3);
        }

        [Fact]
        public async Task FindByIdAsync_Should_Retrieve_Correct_Item()
        {
            var repo = new CourseXMLRepository("./Data/courses.xml");

            var wpfCourse = repo.FindById(2);

            wpfCourse.CourseName.Should().Be("Apprendre WPF avec Prism");
            wpfCourse.Id.Should().Be(2);
        }

    }
}
