using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Task.Controllers
{
    public class SkillController : ApiController
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-MBML2E0;database=Project;integrated Security=true;");
        public List<string> Get()
        {

            // GET: Student
            List<string> g = new List<string>();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Skill", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                var a = JsonConvert.SerializeObject(item);
                g.Add(item["SkillId"] + " " + item["Name"]);
            }

            return g;
        }
        public List<string> Get(int id)
        {

            // GET: Student
            List<string> g = new List<string>();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Skill where SkillId ="+ id, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                var a = JsonConvert.SerializeObject(item);
                g.Add(item["SKillId"] + " " + item["Name"]);
            }
            return g;
        }
    }
}
