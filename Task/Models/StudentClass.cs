using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.MobileControls;
using System.Windows.Documents;

namespace Task.Models
{
    public class StudentClass
    {
        public string StudentName { get; set; }
        public List<string> Skills { get; set; }
        public List<string> Prerequisites { get; set; }
        public string Progress { get; set; }
        public List<int> SkillsQuiz { get; set; }
        public List<string>  StudentCourses { get; set; }
        public List<int> Projects { get; set; }


    }
}