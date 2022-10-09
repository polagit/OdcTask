using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Models
{
    public class students
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<string> Skills { get; set; }

        public List<int> SkillsQuiz { get; set; }

        public List<string> StudentCourses { get; set; }
        public int Projects { get; set; }

    }
}