using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;  // ✅ THÊM

namespace GPDH
{
    public partial class DoiMatKhau : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MaTaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        protected void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string maTK = Session["MaTaiKhoan"].ToString();
            string matKhauCu = txtMatKhauCu.Text.Trim();
            string matKhauMoi = txtMatKhauMoi.Text.Trim();
            string nhapLai = txtNhapLai.Text.Trim();

            // ✅ Kiểm tra nhập lại mật khẩu
            if (matKhauMoi != nhapLai)
            {
                lblThongBao.Text = "❌ Mật khẩu nhập lại không khớp.";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // ✅ KIỂM TRA MẬT KHẨU MẠNH
            // >= 6 ký tự, có chữ hoa, có số, có ký tự đặc biệt
            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$";
            if (!Regex.IsMatch(matKhauMoi, pattern))
            {
                lblThongBao.Text = "❌ Mật khẩu mới phải tối thiểu 6 ký tự, có chữ hoa, số và ký tự đặc biệt.";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sqlCheck = "SELECT COUNT(*) FROM TAIKHOAN WHERE MaTaiKhoan = @MaTK AND MatKhau = @MatKhauCu";
                SqlCommand cmd = new SqlCommand(sqlCheck, conn);
                cmd.Parameters.AddWithValue("@MaTK", maTK);
                cmd.Parameters.AddWithValue("@MatKhauCu", matKhauCu);

                int count = (int)cmd.ExecuteScalar();
                if (count == 0)
                {
                    lblThongBao.Text = "❌ Mật khẩu cũ không đúng.";
                    lblThongBao.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string sqlUpdate = "UPDATE TAIKHOAN SET MatKhau = @MatKhauMoi WHERE MaTaiKhoan = @MaTK";
                SqlCommand updateCmd = new SqlCommand(sqlUpdate, conn);
                updateCmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                updateCmd.Parameters.AddWithValue("@MaTK", maTK);
                updateCmd.ExecuteNonQuery();

                lblThongBao.Text = "✅ Đổi mật khẩu thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}
