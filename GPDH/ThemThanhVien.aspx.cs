using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlClient;
using System.Web.UI;

namespace GPDH
{
    public partial class ThemThanhVien : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDoi();
                LoadPhai();
                LoadChi();
                UpdateEnableControls();
            }
        }

        private void LoadDoi()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT MaDoi, TenDoi FROM DOI", conn))
            {
                conn.Open();
                ddlDoi.DataSource = cmd.ExecuteReader();
                ddlDoi.DataTextField = "TenDoi";
                ddlDoi.DataValueField = "MaDoi";
                ddlDoi.DataBind();
            }
        }

        private void LoadPhai()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT MaPhai, TenPhai FROM PHAI", conn))
            {
                conn.Open();
                ddlPhai.DataSource = cmd.ExecuteReader();
                ddlPhai.DataTextField = "TenPhai";
                ddlPhai.DataValueField = "MaPhai";
                ddlPhai.DataBind();
            }
        }

        private void LoadChi()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT MaChi, TenChi FROM CHI", conn))
            {
                conn.Open();
                ddlChi.DataSource = cmd.ExecuteReader();
                ddlChi.DataTextField = "TenChi";
                ddlChi.DataValueField = "MaChi";
                ddlChi.DataBind();
            }
        }

        // LOAD TÊN CHA (Nam, đời = D - 1)
        private void LoadTenCha()
        {
            ddlTenCha.Items.Clear();
            if (ddlDoi.SelectedValue == "")
                return;

            int doi = int.Parse(ddlDoi.SelectedValue);
            int doiCha = doi - 1;

            if (doiCha <= 0) return; // đời đầu tiên không có cha

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT MaThanhVien, HoTen, NgaySinh
                FROM THANHVIEN
                WHERE GioiTinh = 'Nam' AND MaDoi = @MaDoi", conn))
            {
                cmd.Parameters.AddWithValue("@MaDoi", doiCha);
                conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow r in dt.Rows)
                {
                    ddlTenCha.Items.Add(new System.Web.UI.WebControls.ListItem(
                        $"{r["HoTen"]} - {((DateTime)r["NgaySinh"]).ToString("yyyy-MM-dd")}",
                        r["MaThanhVien"].ToString()));
                }
            }
        }

        // LOAD TÊN VỢ/CHỒNG (khác giới, cùng đời)
        private void LoadVoChong()
        {
            ddlVoChong.Items.Clear();

            if (ddlDoi.SelectedValue == "")
                return;

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT MaThanhVien, HoTen, NgaySinh
                FROM THANHVIEN
                WHERE MaDoi = @MaDoi AND GioiTinh <> @GioiTinh", conn))
            {
                cmd.Parameters.AddWithValue("@MaDoi", ddlDoi.SelectedValue);
                cmd.Parameters.AddWithValue("@GioiTinh", ddlGioiTinh.SelectedValue);
                conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow r in dt.Rows)
                {
                    ddlVoChong.Items.Add(new System.Web.UI.WebControls.ListItem(
                        $"{r["HoTen"]} - {((DateTime)r["NgaySinh"]).ToString("yyyy-MM-dd")}",
                        r["MaThanhVien"].ToString()));
                }
            }
        }

        private void UpdateEnableControls()
        {
            if (chkDauRe.Checked)
            {
                ddlTenCha.Enabled = false;
                ddlVoChong.Enabled = true;
            }
            else
            {
                ddlTenCha.Enabled = true;
                ddlVoChong.Enabled = false;
            }
        }

        protected void ddlDoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTenCha();
            LoadVoChong();
        }

        protected void ddlGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVoChong();
        }

        protected void chkDauRe_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnableControls();
        }

        // ======================== ✅ UPDATED btnSave_Click ========================
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ======= ✅ CHECK: HỌ TÊN CHỈ LÀ CHỮ =======
                string hoTen = txtHoTen.Text.Trim();

                if (string.IsNullOrEmpty(hoTen))
                {
                    lblMessage.Text = "❌ Họ tên không được để trống!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                Regex nameRegex = new Regex(@"^[\p{L}\s]+$"); // chữ Unicode + khoảng trắng
                if (!nameRegex.IsMatch(hoTen))
                {
                    lblMessage.Text = "❌ Họ tên chỉ được chứa chữ cái và khoảng trắng!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // ======= ✅ CHECK: NGÀY MẤT > NGÀY SINH =======
                DateTime ngaySinh;
                if (!DateTime.TryParse(txtNgaySinh.Text, out ngaySinh))
                {
                    lblMessage.Text = "❌ Ngày sinh không hợp lệ!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                DateTime? ngayMat = null;
                if (!string.IsNullOrEmpty(txtNgayMat.Text))
                {
                    DateTime tempMat;
                    if (!DateTime.TryParse(txtNgayMat.Text, out tempMat))
                    {
                        lblMessage.Text = "❌ Ngày mất không hợp lệ!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    ngayMat = tempMat;

                    if (ngayMat <= ngaySinh)
                    {
                        lblMessage.Text = "❌ Ngày mất phải lớn hơn ngày sinh!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }

                int newID = 0;

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO THANHVIEN
                    (HoTen, GioiTinh, NgaySinh, NgayMat, MaDoi, MaChi, MaPhai, QuanHeCha, QuanHeVoChong, VaiTroTrongHo)
                    OUTPUT INSERTED.MaThanhVien
                    VALUES (@HoTen, @GioiTinh, @NgaySinh, @NgayMat, @MaDoi, @MaChi, @MaPhai, @Cha, @VoChong, @VaiTro)", conn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@GioiTinh", ddlGioiTinh.SelectedValue);

                    // ✅ giữ nguyên cách AddWithValue như bạn, nhưng truyền đúng kiểu DateTime để tránh lỗi
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@NgayMat", ngayMat.HasValue ? (object)ngayMat.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@MaDoi", ddlDoi.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaChi", ddlChi.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaPhai", ddlPhai.SelectedValue);
                    cmd.Parameters.AddWithValue("@Cha",
                        ddlTenCha.Enabled ? ddlTenCha.SelectedValue : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@VoChong",
                        ddlVoChong.Enabled ? ddlVoChong.SelectedValue : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@VaiTro", ddlVaiTro.SelectedValue);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null) newID = Convert.ToInt32(result);
                }

                // === THÊM TÀI KHOẢN MẶC ĐỊNH ===
                if (newID > 0)
                {
                    string ngaySinhText = txtNgaySinh.Text; // yyyy-MM-dd
                    string userBase = GenerateUsername(hoTen, ngaySinhText);

                    // ✅ NEW: nếu trùng username thì thêm hậu tố 1,2,3...
                    string user = GetUniqueUsername(userBase);

                    string pass = DateTime.Parse(ngaySinhText).ToString("yyyyMMdd");

                    using (SqlConnection conn2 = new SqlConnection(connStr))
                    using (SqlCommand cmd2 = new SqlCommand(@"
                        INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, LoaiTaiKhoan, TrangThai, NgayTao, MaThanhVien)
                        VALUES (@TenDangNhap, @MatKhau, @LoaiTaiKhoan, @TrangThai, GETDATE(), @MaThanhVien)", conn2))
                    {
                        cmd2.Parameters.AddWithValue("@TenDangNhap", user);
                        cmd2.Parameters.AddWithValue("@MatKhau", pass);
                        cmd2.Parameters.AddWithValue("@LoaiTaiKhoan", "ThanhVien");
                        cmd2.Parameters.AddWithValue("@TrangThai", true);
                        cmd2.Parameters.AddWithValue("@MaThanhVien", newID);

                        conn2.Open();
                        cmd2.ExecuteNonQuery();
                    }
                }

                lblMessage.Text = "✅ Thêm thành viên thành công!";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                ResetForm();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ Có lỗi xảy ra khi thêm thành viên!<br/>" + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void ResetForm()
        {
            txtHoTen.Text = "";
            txtNgaySinh.Text = "";
            txtNgayMat.Text = "";

            ddlGioiTinh.SelectedIndex = 0;
            ddlDoi.SelectedIndex = 0;
            ddlChi.SelectedIndex = 0;
            ddlPhai.SelectedIndex = 0;
            ddlVaiTro.SelectedIndex = 0;

            chkDauRe.Checked = false;

            LoadTenCha();
            LoadVoChong();

            UpdateEnableControls();
        }

        // ======= HÀM XỬ LÝ CHUỖI =======
        private string GenerateUsername(string hoTen, string ngaySinh)
        {
            string noSign = RemoveDiacritics(hoTen).ToLower();
            noSign = Regex.Replace(noSign, @"\s+", ""); // bỏ khoảng trắng

            string birth = DateTime.Parse(ngaySinh).ToString("yyyyMMdd");

            return noSign + birth;
        }

        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            string result = sb.ToString().Normalize(NormalizationForm.FormC);
            result = Regex.Replace(result, @"[^a-zA-Z0-9\s]", "");

            return result;
        }

        // ======================== ✅ NEW: USERNAME UNIQUE ========================

        private string GetUniqueUsername(string baseUsername)
        {
            // Nếu chưa tồn tại → dùng luôn
            if (!UsernameExists(baseUsername))
                return baseUsername;

            // Nếu trùng → thêm hậu tố 1,2,3...
            int suffix = 1;
            string newUser = baseUsername + suffix;

            while (UsernameExists(newUser))
            {
                suffix++;
                newUser = baseUsername + suffix;
            }

            return newUser;
        }

        private bool UsernameExists(string username)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap", conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", username);
                conn.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }
}