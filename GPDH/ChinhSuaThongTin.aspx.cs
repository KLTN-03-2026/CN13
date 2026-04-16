using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GPDH
{
    public partial class ChinhSuaThongTin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MaThanhVien"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
                HienThiThongTin();
        }

        private void HienThiThongTin()
        {
            string maTV = Session["MaThanhVien"].ToString();
            string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = @"
                    SELECT 
                        TV.HoTen, TV.Email, TV.SoDienThoai,
                        TV.DiaChi, TV.NgheNghiep, TV.GhiChu
                    FROM THANHVIEN TV
                    WHERE TV.MaThanhVien = @MaThanhVien";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaThanhVien", maTV);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Các control tồn tại trong giao diện
                    lblHoTen.Text = reader["HoTen"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtSoDT.Text = reader["SoDienThoai"].ToString();
                    txtDiaChi.Text = reader["DiaChi"].ToString();
                    txtNgheNghiep.Text = reader["NgheNghiep"].ToString();
                    txtGhiChu.Text = reader["GhiChu"].ToString();
                }

                reader.Close();
            }
        }


        protected void btnLuu_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string phone = txtSoDT.Text.Trim();

            // ===== KIỂM TRA EMAIL =====
            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    if (addr.Address != email)
                        throw new Exception();
                }
                catch
                {
                    lblThongBao.Text = "❌ Email không hợp lệ!";
                    lblThongBao.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }

            // ===== KIỂM TRA SỐ ĐIỆN THOẠI =====
            if (!string.IsNullOrEmpty(phone))
            {
                // Regex: số 0 + 9 số tiếp theo (tổng 10 số)
                System.Text.RegularExpressions.Regex regex =
                    new System.Text.RegularExpressions.Regex(@"^0\d{9}$");

                if (!regex.IsMatch(phone))
                {
                    lblThongBao.Text = "❌ Số điện thoại phải có 10 chữ số và bắt đầu bằng số 0!";
                    lblThongBao.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }

            // ===== UPDATE DATABASE =====
            string maTV = Session["MaThanhVien"].ToString();
            string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = @"UPDATE THANHVIEN
                       SET Email = @Email, SoDienThoai = @SoDienThoai,
                           DiaChi = @DiaChi, NgheNghiep = @NgheNghiep, GhiChu = @GhiChu
                       WHERE MaThanhVien = @MaThanhVien";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@SoDienThoai", phone);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("@NgheNghiep", txtNgheNghiep.Text.Trim());
                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text.Trim());
                cmd.Parameters.AddWithValue("@MaThanhVien", maTV);

                int rows = cmd.ExecuteNonQuery();

                lblThongBao.Text = rows > 0
                    ? "✅ Cập nhật thông tin thành công!"
                    : "❌ Lỗi khi cập nhật.";

                lblThongBao.ForeColor = rows > 0
                    ? System.Drawing.Color.Green
                    : System.Drawing.Color.Red;
            }
        }

    }
}
