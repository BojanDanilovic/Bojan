using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace jonybiz
{

    public partial class index : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=Bojan;Initial Catalog=bojan;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con1 = new SqlConnection(@"Data Source=Bojan;Initial Catalog=bojan;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
           
                int cena = droplista(DropDownList1.SelectedValue, con1);
                TextBox4.Text = cena.ToString();
   
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }
        int droplista(string drop, SqlConnection con1)
        {
            con1.Open();
            SqlParameter p1 = new SqlParameter();
            p1.Value = drop;
            p1.ParameterName = "@naziv";
            string upit = "Select * from Kursevi Where Naziv=@naziv";

            SqlCommand cmd = new SqlCommand(upit, con1);
            cmd.Parameters.Add(p1);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int Cena = int.Parse(dr[3].ToString());
            dr.Close();
            con1.Close();
            return Cena;


        }
        void uneti(string ime, string email, string telefon, string naziv)
        {
            using (con)
            {
                con.Open();


                int kursID;
                int licnipodaciID;

                SqlParameter naziv1 = new SqlParameter();

                naziv1.Value = naziv.Trim();
                naziv1.ParameterName = "@Naziv";

                string nupit = $"Select * From Kursevi Where Naziv=@Naziv";

                SqlCommand ncmd = new SqlCommand(nupit, con);
                ncmd.Parameters.Add(naziv1);

                SqlDataReader r1 = ncmd.ExecuteReader();

                if (r1.HasRows)
                {
                    r1.Read();
                    kursID = int.Parse(r1[0].ToString());
                    r1.Close();

                    SqlParameter p1 = new SqlParameter();
                    SqlParameter p2 = new SqlParameter();
                    SqlParameter p3 = new SqlParameter();
                    SqlParameter p4 = new SqlParameter();

                    p1.Value = ime;
                    p2.Value = email;
                    p3.Value = email;
                    p4.Value = telefon;

                    p1.ParameterName = "@IME";
                    p2.ParameterName = "@EMAIL";
                    p3.ParameterName = "@EMAIL1";
                    p4.ParameterName = "@TELEFON";

                    string select = "Select * FROM LicniPodaci Where Email=@EMAIL1";

                    SqlCommand cmd1 = new SqlCommand(select, con);
                    cmd1.Parameters.Add(p3);

                    SqlDataReader dr = cmd1.ExecuteReader();
                    dr.Read();

                    if (!dr.HasRows)
                    {
                        dr.Close();
                        string upit = "INSERT INTO LicniPodaci(PunoIme,Email,Telefon)" +
                                    "values(@IME,@EMAIL,@TELEFON)";

                        SqlCommand cmd = new SqlCommand(upit, con);
                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p4);

                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        licnipodaciID = int.Parse(dr[0].ToString());
  
                        dr.Close();
           
                        SqlParameter s1 = new SqlParameter();
                        SqlParameter s2 = new SqlParameter();

                        s1.Value = licnipodaciID;
                        s2.Value = kursID;

                        s1.ParameterName = "@licnipodaciID";
                        s2.ParameterName = "@kursID";

                        string check = "INSERT INTO Checkout(LPID,KID) " +
                                            "values(@licnipodaciID,@kursID)";

                        SqlCommand checkout = new SqlCommand(check, con);
                        checkout.Parameters.Add(s1);
                        checkout.Parameters.Add(s2);

                        checkout.ExecuteNonQuery();
                    }
                    dr.Close();

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Taj Kurs Ne postoji");
                    r1.Close();
                }
                r1.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
              
                uneti(TextBox1.Text, TextBox2.Text, TextBox3.Text, DropDownList1.SelectedValue);
            
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}


