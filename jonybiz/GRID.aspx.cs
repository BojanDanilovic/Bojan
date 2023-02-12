using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jonybiz
{
    public partial class GRID : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=Bojan;Initial Catalog=bojan;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gridview();

            } 
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }
        void gridview()
        {

            using (con)
            {
                con.Open();
                string upit = "SELECT * FROM Checkout";
                SqlCommand cmd=new SqlCommand(upit,con);
                SqlDataAdapter ad=new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                GridView1.DataSource= dt;
                GridView1.DataBind();
            }

        }
    }
}