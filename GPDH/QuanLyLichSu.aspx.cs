using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GPDH
{
    public partial class QuanLyLichSu : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadLichSu();
        }

        private void LoadLichSu()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM LICHSU", conn))
            {
                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    ViewState["ID"] = rd["MaLichSu"].ToString();
                    ltNoiDung.Text = rd["NoiDung"].ToString();

                    btnThem.Enabled = false;
                    btnThem.CssClass = "btn-action btn-disabled";

                    btnSua.Enabled = true;
                    btnSua.CssClass = "btn-action";

                    btnXoa.Enabled = true;
                    btnXoa.CssClass = "btn-action";

                    pnView.Visible = true;
                    pnEdit.Visible = false;
                }
                else
                {
                    ltNoiDung.Text = "<i>Chưa có lịch sử, hãy thêm mới.</i>";

                    btnThem.Enabled = true;
                    btnThem.CssClass = "btn-action";

                    btnSua.Enabled = false;
                    btnSua.CssClass = "btn-action btn-disabled";

                    btnXoa.Enabled = false;
                    btnXoa.CssClass = "btn-action btn-disabled";

                    pnView.Visible = true;
                    pnEdit.Visible = false;
                }
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            pnEdit.Visible = true;
            pnView.Visible = false;

            txtNoiDung.Text = "";
        }

        protected void btnSua_Click(object sender, EventArgs e)
        {
            pnEdit.Visible = true;
            pnView.Visible = false;

            txtNoiDung.Text = ltNoiDung.Text;
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            string id = ViewState["ID"]?.ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd;

                if (string.IsNullOrEmpty(id))   // thêm mới
                {
                    cmd = new SqlCommand("INSERT INTO LICHSU (NoiDung) VALUES (@NoiDung)", conn);
                }
                else  // sửa
                {
                    cmd = new SqlCommand("UPDATE LICHSU SET NoiDung=@NoiDung WHERE MaLichSu=@ID", conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                }

                cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text);
                cmd.ExecuteNonQuery();
            }

            pnEdit.Visible = false;
            pnView.Visible = true;

            LoadLichSu();
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            pnEdit.Visible = false;
            pnView.Visible = true;
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            string id = ViewState["ID"]?.ToString();
            if (id == null) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM LICHSU WHERE MaLichSu=@ID", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }

            ViewState["ID"] = null;
            LoadLichSu();
        }
    }
}
