using DataAccess.Common.Models;
using DataAccess.Implementations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DataAccess.Tests
{
    public class CourseEFRepositoryTests
    {
        [Fact]
        public void Ctor_Should_Throws_If_Null_Is_Passed_As_Context()
        {
            Assert.Throws<ArgumentNullException>(() => new CourseEFRepository(null));
        }

        [Fact]
        public void GetAll_Should_Retrieve_All_Data()
        {
            using (var ctx = new DemoDbContext())
            {
                var repo = new CourseEFRepository(ctx);

                var allCourses = repo.GetAll();

                allCourses.Should().HaveCount(3);
            }
        }

        [Fact]
        public void FindByIdAsync_Should_Retrieve_Correct_Item()
        {
            using (var ctx = new DemoDbContext())
            {
                var repo = new CourseEFRepository(ctx);

                var wpfCourse = repo.FindById(2);

                wpfCourse.CourseName.Should().Be("Apprendre WPF avec Prism");
                wpfCourse.Id.Should().Be(2);
            }
        }

        [Fact]
        public void Remove_Should_Remove_Element_From_JSON_Document()
        {
            using (var ctx = new DemoDbContext())
            {
                var repo = new CourseEFRepository(ctx);

                var wpfElement = repo.FindById(2);

                repo.Remove(wpfElement);

                var elements = repo.GetAll();
                elements.Should().HaveCount(2);
                elements.Any(e => e.Id == 2).Should().BeFalse();
            }
        }

        [Fact]
        public void Insert_Should_Add_Element_ToDocument()
        {
            using (var ctx = new DemoDbContext())
            {
                var repo = new CourseEFRepository(ctx);

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
        }

        [Fact]
        public void Update_Should_Update_Document()
        {
            using (var ctx = new DemoDbContext())
            {
                var repo = new CourseEFRepository(ctx);

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
}
