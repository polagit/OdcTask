using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using Task.Models;

namespace Task.Controllers
{
    public class StudentController : ApiController
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-MBML2E0;database=Project;integrated Security=true;");
        [Route("api/Student/")]

        public List<Student> Get()
        {

            // GET: Student

            List<string> g = new List<string>();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Student", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Student> StudentLList = new List<Student>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Student a = new Student();
                a.StudentId = Convert.ToInt32(dt.Rows[i]["StudentId"]);
                a.StudentName = dt.Rows[i]["StudentName"].ToString();
                StudentLList.Add(a);

            }

            return StudentLList;
        }
        [Route("api/Student/{id}/Student")]

        public Student GetStudent(int id)
        {

            // GET: Student
            Student Student = new Student();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Student where StudentId =" + id, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Student.StudentId = Convert.ToInt32(dt.Rows[i]["StudentId"]);
                Student.StudentName = dt.Rows[i]["StudentName"].ToString();
            }
            return Student;
        }
       
        [Route("api/Student/{id}/Skills")]
        public StudentClass GetStudentSkills(int id)
        {

            // GET: Student
            SqlDataAdapter da = new SqlDataAdapter("select k.StudentName ,quiz,SkillName from Student k join StudentSkills tk on k.StudentId = tk.StudentId join Skill t on tk.SkillId = t.SkillId where tk.StudentId =" + id, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<int> quiz = new List<int>();

            List<String> Skill = new List<String>();
            StudentClass Student = new StudentClass();
            foreach (DataRow item in dt.Rows)
            {
                Student.StudentName = (item["StudentName"].ToString());
                Skill.Add(item["SkillName"] + "");
                quiz.Add(Convert.ToInt32(item["quiz"]));

            }
            Student.Skills = Skill;
            Student.SkillsQuiz = quiz;
            return Student;
        }
        [Route("api/Student/{id}/Profile")]

        public StudentClass GetProfile(int id)
        {

            // GET: Student
            StudentClass student = GetStudentSkills(id);
            StudentClass studentPre = GetPrerequisites(id);
            StudentClass StudentProgress = GetProgress(id);
            student.Progress = StudentProgress.Progress;
            student.Prerequisites = studentPre.Prerequisites;
            SqlDataAdapter da = new SqlDataAdapter("select CourseName, project from Student join StudentCourses  on Student.StudentId = StudentCourses.StudentId join Course  on StudentCourses.CourseId = Course.CourseId where StudentCourses.StudentId =" + id, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<String> Courses = new List<String>();
            List<int> projects = new List<int>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Courses.Add(dt.Rows[i]["CourseName"].ToString());

                projects.Add(Convert.ToInt32(dt.Rows[i]["project"]));
            }
            student.StudentCourses = Courses;
            student.Projects = projects;
            return student;
        }
        [Route("api/Student/{job}/Job")]
        public List<StudentClass> GetJob(string job)
        {
            SqlDataAdapter da = new SqlDataAdapter("select s.StudentId ,Project from Student s join StudentCourses  on s.StudentId = StudentCourses.StudentId join Course  on StudentCourses.CourseId = Course.CourseId where Course.CourseName ='" + job + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<StudentClass> Students = new List<StudentClass>();
            int[] p = new int[1];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var a = Convert.ToInt32(dt.Rows[i]["project"]);
                var id = Convert.ToInt32(dt.Rows[i]["StudentId"]);
                if (a >= 70)
                {
                    Students.Add(GetProfile(id));
                }
            }
            return Students;
        }
        [Route("api/Student/{id}/Prerequisites")]
        public StudentClass GetPrerequisites(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("select PrerequisitesName from Student join StudentPrerequisites  on Student.StudentId = StudentPrerequisites.StudentId join Prerequisites  on Prerequisites.PrerequisitesId = StudentPrerequisites.PrerequisitesId where Student.StudentId =" + id, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<String> PrePrerequisites = new List<String>();
            foreach (DataRow item in dt.Rows)
            {
                PrePrerequisites.Add(item["PrerequisitesName"] + "");
            }
            StudentClass Student = new StudentClass();
            Student.Prerequisites = PrePrerequisites;
            return Student;
        }

        [Route("api/Student/{id}/Progress")]
        public StudentClass GetProgress(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select Student.Progress from Student where Student.StudentId =" + id, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            String Progress = "";
            foreach (DataRow item in dt.Rows)
            {
                Progress = (item["Progress"] + "");
            }
            StudentClass Student = new StudentClass();
            Student.Progress = Progress;
            return Student;
        }

        public String GetPrerequisiteName(string Coursename)
        {
            Coursename.ToUpper();
            SqlDataAdapter da = new SqlDataAdapter("select PrerequisitesName from Prerequisites p join CoursesPrerequisite cp on p.PrerequisitesId=cp.PrerequisitesId join Course c on c.CourseId=cp.CourseId where c.Coursename= '" + Coursename + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            String PrerequisitesName = "";
            foreach (DataRow item in dt.Rows)
            {
                PrerequisitesName = (item["PrerequisitesName"].ToString());
            }
            return PrerequisitesName;
        }
        [Route("api/Student/{Coursename}/OfferCourse")]
        public List<StudentClass> GetOfferCourse(string Coursename)
        {
            Coursename.ToUpper();
            string Courseprerequisite = GetPrerequisiteName(Coursename);
            SqlDataAdapter da = new SqlDataAdapter("Select s.StudentId from Student s join StudentPrerequisites sp on  sp.StudentId=s.StudentId join Prerequisites p on p.PrerequisitesId=sp.PrerequisitesId where PrerequisitesName = '" + Courseprerequisite + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<int> StudentsId = new List<int>();
            List<StudentClass> Students = new List<StudentClass>();
            List<StudentClass> recommmendedStudents = new List<StudentClass>();
            foreach (DataRow item in dt.Rows)
            {
                StudentsId.Add(Convert.ToInt32(item["StudentId"]));

            }
            for (int i = 0; i < StudentsId.Count; i++)
            {
                StudentClass Student = GetProfile(StudentsId[i]);

                Students.Add(Student);

            }
            for (int i = 0; i < Students.Count; i++)
            {
                if (!Students[i].StudentCourses.Contains("Css"))
                {
                    recommmendedStudents.Add(Students[i]);
                }
            }
            return recommmendedStudents;
        }
        [Route("api/Student/GetAll")]
        public List<StudentClass> GetAll()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select StudentId from Student", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<StudentClass> Students = new List<StudentClass>();

            foreach (DataRow item in dt.Rows)
            {
                int id = (Convert.ToInt32(item["StudentId"]));
                StudentClass student = GetProfile(id);
                Students.Add(student);
            }
            return Students;





        }






    }
}
