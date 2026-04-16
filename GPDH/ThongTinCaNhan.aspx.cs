using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace GPDH
{
    public partial class ThongTinCaNhan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MaThanhVien"] != null)
                {
                    int maTV = Convert.ToInt32(Session["MaThanhVien"]);
                    LoadThongTinCaNhan(maTV);
                }
                else
                {
                    Response.Redirect("DangNhap.aspx");
                }
            }
        }

        private void LoadThongTinCaNhan(int maThanhVien)
        {
            string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT tv.HoTen, tv.NgaySinh, tv.NgayMat, tv.GioiTinh, tv.Email, tv.SoDienThoai, 
                           tv.NgheNghiep, tv.DiaChi, tv.GhiChu, tv.AnhDaiDien, tv.VaiTroTrongHo,
                           d.TenDoi, p.TenPhai, c.TenChi
                    FROM THANHVIEN tv
                    LEFT JOIN DOI d ON tv.MaDoi = d.MaDoi
                    LEFT JOIN PHAI p ON tv.MaPhai = p.MaPhai
                    LEFT JOIN CHI c ON tv.MaChi = c.MaChi
                    WHERE tv.MaThanhVien = @MaThanhVien";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaThanhVien", maThanhVien);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblHoTen.Text = dr["HoTen"].ToString();
                    lblNgaySinh.Text = dr["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(dr["NgaySinh"]).ToString("dd-MM-yyyy") : "";
                    lblNgayMat.Text = dr["NgayMat"] != DBNull.Value ? Convert.ToDateTime(dr["NgayMat"]).ToString("dd-MM-yyyy") : "";
                    lblGioiTinh.Text = dr["GioiTinh"].ToString();
                    lblEmail.Text = dr["Email"].ToString();
                    lblSDT.Text = dr["SoDienThoai"].ToString();
                    lblNgheNghiep.Text = dr["NgheNghiep"].ToString();
                    lblDiaChi.Text = dr["DiaChi"].ToString();
                    lblGhiChu.Text = dr["GhiChu"].ToString();
                    lblVaiTro.Text = dr["VaiTroTrongHo"].ToString();

                    lblDoi.Text = dr["TenDoi"].ToString();
                    lblPhai.Text = dr["TenPhai"].ToString();
                    lblChi.Text = dr["TenChi"].ToString();

                    string anh = dr["AnhDaiDien"] == DBNull.Value ? "" : dr["AnhDaiDien"].ToString();

                    // ✅ Avatar load từ folder Avatar
                    imgAvatar.ImageUrl = !string.IsNullOrEmpty(anh)
                        ? $"~/Images/Avatar/{anh}"
                        : "~/Images/avatar_default.png";
                }

                conn.Close();
            }
        }

        protected void btnChinhSua_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChinhSuaThongTin.aspx");
        }

        // ✅ THÊM: Upload avatar
        protected void btnThayAnh_Click(object sender, EventArgs e)
        {
            if (Session["MaThanhVien"] == null) return;
            int maTV = Convert.ToInt32(Session["MaThanhVien"]);

            if (!fuAvatar.HasFile)
            {
                lblAvatarMsg.Text = "❌ Vui lòng chọn ảnh trước!";
                lblAvatarMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string ext = Path.GetExtension(fuAvatar.FileName).ToLower();
            int fileSize = fuAvatar.PostedFile.ContentLength;

            // ✅ Check loại file
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
            {
                lblAvatarMsg.Text = "❌ Chỉ cho phép file JPG/JPEG/PNG!";
                lblAvatarMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // ✅ Check dung lượng (2MB)
            if (fileSize > 2 * 1024 * 1024)
            {
                lblAvatarMsg.Text = "❌ Ảnh tối đa 2MB!";
                lblAvatarMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // ✅ Đổi tên file tránh trùng
            string fileName = DateTime.Now.Ticks + ext;

            // ✅ Lưu vào folder Images/Avatar
            string folderPath = Server.MapPath("~/Images/Avatar/");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string savePath = Path.Combine(folderPath, fileName);

            try
            {
                fuAvatar.SaveAs(savePath);
            }
            catch (Exception ex)
            {
                lblAvatarMsg.Text = "❌ Lỗi lưu ảnh: " + ex.Message;
                lblAvatarMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // ✅ Update DB
            string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "UPDATE THANHVIEN SET AnhDaiDien=@Anh WHERE MaThanhVien=@MaTV";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Anh", fileName);
                cmd.Parameters.AddWithValue("@MaTV", maTV);
                cmd.ExecuteNonQuery();
            }

            lblAvatarMsg.Text = "✅ Cập nhật avatar thành công!";
            lblAvatarMsg.ForeColor = System.Drawing.Color.Green;

            // ✅ Load lại để hiển thị avatar mới ngay
            LoadThongTinCaNhan(maTV);
        }
    }
}
