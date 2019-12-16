using System;

namespace DataAccess.Common.Models
{
    public enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public class Course
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public int DurationInDays { get; set; }

        public Difficulty Difficulty { get; set; }
    }
}