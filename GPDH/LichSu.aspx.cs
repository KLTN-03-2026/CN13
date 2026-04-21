using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GPDH
{
    public partial class LichSu : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLichSu();
            }
        }

        private void LoadLichSu()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 NoiDung FROM LICHSU", conn))
            {
                conn.Open();
                object rs = cmd.ExecuteScalar();

                ltNoiDung.Text = rs != null ? rs.ToString() : "Chưa có nội dung lịch sử.";
            }
        }
    }
}
