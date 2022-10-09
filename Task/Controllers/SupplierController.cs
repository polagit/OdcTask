using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using Task.Models;
using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace Task.Controllers
{
    public class SupplierController : ApiController
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-MBML2E0;database=Project;integrated Security=true;");

        [Route("api/Supplier/")]
        public List<Supplier> GetSupliers()
        {

            SqlDataAdapter da = new SqlDataAdapter("select SupplierName,Total,Deposit from Supplier s join CoursesSupplier cs on s.SupplierId = cs.SupplierId join Course c on cs.CourseId = c.CourseId",  con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        int  Totaldeposite=0;
            List<Supplier> Suppliers = new List<Supplier>();
            foreach (DataRow item in dt.Rows)
            {
                Supplier Supplier = new Supplier();
                Supplier.SupplierName = (item["SupplierName"].ToString());
                Supplier.SupplierTotal = Convert.ToInt32(item["Total"]);
                Supplier.SupplierDeposite = Convert.ToInt32(item["Total"]);
                Totaldeposite += Convert.ToInt32(item["Total"]);
                Suppliers.Add(Supplier);
            }
            return Suppliers;
        }


    }

}