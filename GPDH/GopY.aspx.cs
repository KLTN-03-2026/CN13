using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace GPDH
{
    public partial class GopY : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void btnGui_Click(object sender, EventArgs e)
        {
            string noiDung = txtNoiDung.Text.Trim();

            if (string.IsNullOrWhiteSpace(noiDung))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('Vui lòng nhập nội dung góp ý!');", true);
                return;
            }

            if (noiDung.Length > 2000)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('Nội dung góp ý không được vượt quá 2000 ký tự!');", true);
                return;
            }

            int maThanhVien = Convert.ToInt32(Session["MaThanhVien"] ?? 0);

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
        INSERT INTO GOPY (MaThanhVien, NoiDung, NgayGopY)
        VALUES (@MaTV, @ND, GETDATE())", conn))
            {
                cmd.Parameters.AddWithValue("@MaTV", maThanhVien == 0 ? (object)DBNull.Value : maThanhVien);
                cmd.Parameters.AddWithValue("@ND", noiDung);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            txtNoiDung.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "popup", "showPopup();", true);
        }

    }
}
