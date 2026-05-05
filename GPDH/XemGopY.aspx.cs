using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace GPDH
{
    public partial class XemGopY : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData(string hoten = "", string ngay = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT g.MaGopY, tv.HoTen, g.NgayGopY,
                       LEFT(g.NoiDung, 40) + CASE WHEN LEN(g.NoiDung) > 40 THEN '...' ELSE '' END AS NoiDungRutGon
                FROM GOPY g
                INNER JOIN THANHVIEN tv ON g.MaThanhVien = tv.MaThanhVien
                WHERE (@ht = '' OR tv.HoTen LIKE '%' + @ht + '%')
                      AND (@ngay = '' OR CONVERT(date, g.NgayGopY) = @ngay)
                ORDER BY g.NgayGopY DESC", conn))
            {
                cmd.Parameters.AddWithValue("@ht", hoten);
                cmd.Parameters.AddWithValue("@ngay", string.IsNullOrEmpty(ngay) ? "" : ngay);

                DataTable dt = new DataTable();
                conn.Open();
                dt.Load(cmd.ExecuteReader());

                gvGopY.DataSource = dt;
                gvGopY.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtHoTen.Text.Trim(), txtNgayGopY.Text);
        }

        protected void gvGopY_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
                ShowDetailPopup(e.CommandArgument.ToString());
        }

        private void ShowDetailPopup(string id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT tv.HoTen, g.NgayGopY, g.NoiDung
        FROM GOPY g
        INNER JOIN THANHVIEN tv ON g.MaThanhVien = tv.MaThanhVien
        WHERE g.MaGopY = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    string hoTen = r["HoTen"].ToString();
                    string ngay = Convert.ToDateTime(r["NgayGopY"]).ToString("dd-MM-yyyy");
                    string noiDung = HttpUtility.JavaScriptStringEncode(r["NoiDung"].ToString());

                    string script = $@"
                openDetailPopup({{
                    HoTen: '{hoTen}',
                    NgayGopY: '{ngay}',
                    NoiDung: '{noiDung}'
                }});";

                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowDetail", script, true);
                }
            }
        }
    }
}
