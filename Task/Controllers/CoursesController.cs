using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using Task.Models;
using System.Web.Http.Controllers;
using static Task.Controllers.StudentController;
namespace Task.Controllers
{
    public class CoursesController : ApiController
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-MBML2E0;database=Project;integrated Security=true;");

        [Route("api/Courses/Progress")]

        public List<Courses> GETProgressCourse()
        {
            // GET: Student
            SqlDataAdapter da = new SqlDataAdapter("select StartDate,EndDate,CourseName from Course ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Courses> Courses = new List<Courses>();
            foreach (DataRow item in dt.Rows)
            {
                Courses Course = new Courses();
                Course.CourseName = (item["CourseName"].ToString());
                DateTime Start = DateTime.Now;
                var End = item["EndDate"].ToString();
                DateTime EndDate = DateTime.Parse(End);
                Double Days = (EndDate - Start).Days;
                Course.progress = (Days).ToString();
                DateTime.Now.ToString();
                Course.EndDate = End;
                Course.StartDate= item["StartDate"].ToString();
                Courses.Add(Course);
            }
            return Courses;
        }

        public String GetPrerequisiteName()
        {
            SqlDataAdapter da = new SqlDataAdapter("select PrerequisitesName from Prerequisites p join CoursesPrerequisite cp on p.PrerequisitesId=cp.PrerequisitesId where cp.CourseId=1", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            String PrerequisitesName="";
            foreach (DataRow item in dt.Rows)
            {
                PrerequisitesName =(item["PrerequisitesName"].ToString());

            }
            return PrerequisitesName;

        }
     

    
    }
}

