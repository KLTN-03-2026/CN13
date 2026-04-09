using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace GPDH
{
    public partial class DangNhap : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Không cần xử lý đặc biệt khi load trang
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtUser.Text.Trim();
            string matKhau = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                ShowAlert("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(@"
                     SELECT MaTaiKhoan, TenDangNhap, LoaiTaiKhoan, MaThanhVien
                    FROM TAIKHOAN
                    WHERE TenDangNhap = @TenDangNhap 
                    AND MatKhau = @MatKhau 
                     AND TrangThai = 1", conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Session["VaiTro"] = reader["LoaiTaiKhoan"].ToString();
                            Session["TenDangNhap"] = reader["TenDangNhap"].ToString();
                            Session["MaThanhVien"] = reader["MaThanhVien"];
                            Session["MaTaiKhoan"] = reader["MaTaiKhoan"];  // ✅ Thêm dòng này

                            Response.Redirect("TrangChu.aspx");
                        }
                        else
                        {
                            ShowAlert("Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản bị khóa.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // ✅ Ghi log lỗi thực tế (nếu cần)
                ShowAlert("Lỗi hệ thống. Vui lòng thử lại sau.");
                // Ví dụ: Logger.LogError("Login error: " + ex.Message);
            }
        }

        private void ShowAlert(string message)
        {
            ScriptManager.RegisterStartupScript(
                this, GetType(), "alert",
                $"alert('{message.Replace("'", "\\'")}');", true);
        }
    }
}
