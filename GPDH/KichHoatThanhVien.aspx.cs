using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;   // ⭐ BẮT BUỘC


namespace GPDH
{
    public partial class KichHoatThanhVien : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData(string keyword = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT tk.MaTaiKhoan, tk.TenDangNhap, tk.TrangThai,
                       tv.HoTen, tv.NgaySinh, d.TenDoi
                FROM TAIKHOAN tk
                LEFT JOIN THANHVIEN tv ON tk.MaThanhVien = tv.MaThanhVien
                LEFT JOIN DOI d ON tv.MaDoi = d.MaDoi
                WHERE (@kw = '' OR tv.HoTen LIKE '%' + @kw + '%')
                ORDER BY tv.HoTen ASC", conn))
            {
                cmd.Parameters.AddWithValue("@kw", keyword);

                DataTable dt = new DataTable();
                conn.Open();
                dt.Load(cmd.ExecuteReader());

                gvThanhVien.DataSource = dt;
                gvThanhVien.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }
        protected void gvThanhVien_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Lấy giá trị trạng thái
                object val = DataBinder.Eval(e.Row.DataItem, "TrangThai");
                bool trangThai = (val != DBNull.Value && Convert.ToBoolean(val));

                // Hiển thị trạng thái
                Label lbl = (Label)e.Row.FindControl("lblTrangThai");

                // Nút
                LinkButton btnDisable = (LinkButton)e.Row.FindControl("btnDisable");
                LinkButton btnEnable = (LinkButton)e.Row.FindControl("btnEnable");

                if (trangThai == true)
                {
                    lbl.Text = "<span style='color:green;font-weight:bold;'>Hoạt động</span>";
                    btnDisable.Visible = true;   // Hiện nút vô hiệu hoá
                    btnEnable.Visible = false;
                }
                else
                {
                    lbl.Text = "<span style='color:red;font-weight:bold;'>Vô hiệu hoá</span>";
                    btnDisable.Visible = false;
                    btnEnable.Visible = true;    // Hiện nút kích hoạt
                }
            }
        }

        protected void gvThanhVien_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string id = gvThanhVien.DataKeys[index].Value.ToString();

            if (e.CommandName == "Disable")
            {
                UpdateTrangThai(id, false);
                lblNotify.Text = "🛑 Đã vô hiệu hoá tài khoản!";
                lblNotify.ForeColor = System.Drawing.Color.Red;
            }
            else if (e.CommandName == "Enable")
            {
                UpdateTrangThai(id, true);
                lblNotify.Text = "✔️ Đã kích hoạt tài khoản!";
                lblNotify.ForeColor = System.Drawing.Color.Green;
            }

            LoadData();
        }


        private void UpdateTrangThai(string id, bool trangThai)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE TAIKHOAN SET TrangThai=@TrangThai WHERE MaTaiKhoan=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateStatus(string id, bool status)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE TAIKHOAN SET TrangThai=@TrangThai WHERE MaTaiKhoan=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@TrangThai", status);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
