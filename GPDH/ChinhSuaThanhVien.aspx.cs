using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

// ✅ THÊM 2 USING NÀY
using System.Text.RegularExpressions;
using System.Globalization;


namespace GPDH
{
    public partial class ChinhSuaThanhVien : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        private string deleteName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData(string keyword = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT tv.MaThanhVien, tv.HoTen, tv.NgaySinh,
                       d.TenDoi, p.TenPhai, c.TenChi
                FROM THANHVIEN tv
                LEFT JOIN DOI d ON tv.MaDoi = d.MaDoi
                LEFT JOIN PHAI p ON tv.MaPhai = p.MaPhai
                LEFT JOIN CHI c ON tv.MaChi = c.MaChi
                WHERE (@kw = '' OR tv.HoTen LIKE '%' + @kw + '%')
                ORDER BY tv.MaDoi ASC, tv.NgaySinh ASC
            ", conn))
            {
                cmd.Parameters.AddWithValue("@kw", keyword);
                DataTable dt = new DataTable();
                conn.Open();
                dt.Load(cmd.ExecuteReader());

                gvThanhVien.DataSource = dt;
                gvThanhVien.DataBind();
            }
        }

        private void LoadEditDropdowns()
        {
            // load đời
            ddlEditDoi.DataSource = GetData("SELECT MaDoi, TenDoi FROM DOI");
            ddlEditDoi.DataTextField = "TenDoi";
            ddlEditDoi.DataValueField = "MaDoi";
            ddlEditDoi.DataBind();
            ddlEditDoi.Items.Insert(0, new ListItem("-- Không có --", ""));

            // load phái
            ddlEditPhai.DataSource = GetData("SELECT MaPhai, TenPhai FROM PHAI");
            ddlEditPhai.DataTextField = "TenPhai";
            ddlEditPhai.DataValueField = "MaPhai";
            ddlEditPhai.DataBind();
            ddlEditPhai.Items.Insert(0, new ListItem("-- Không có --", ""));

            // load chi
            ddlEditChi.DataSource = GetData("SELECT MaChi, TenChi FROM CHI");
            ddlEditChi.DataTextField = "TenChi";
            ddlEditChi.DataValueField = "MaChi";
            ddlEditChi.DataBind();
            ddlEditChi.Items.Insert(0, new ListItem("-- Không có --", ""));
        }

        private DataTable GetData(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                DataTable dt = new DataTable();
                conn.Open();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        // ============================
        // XỬ LÝ SỰ KIỆN GRIDVIEW
        // ============================
        protected void gvThanhVien_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            if (e.CommandName == "EditRow")
            {
                LoadEditDropdowns();
                LoadEditInfo(id);
                ScriptManager.RegisterStartupScript(this, GetType(), "showEdit", "openEditPopup();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                LoadDeleteName(id);
                ScriptManager.RegisterStartupScript(this, GetType(), "showDelete",
                    "openDeletePopup('" + id + "', '" + deleteName + "');", true);
            }
        }

        // ============================
        // LOAD TÊN ĐỂ HIỂN THỊ POPUP XÓA
        // ============================
        private void LoadDeleteName(string id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT HoTen FROM THANHVIEN WHERE MaThanhVien = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();

                object name = cmd.ExecuteScalar();
                deleteName = name != null ? name.ToString() : "";
            }
        }

        // ============================
        // LOAD THÔNG TIN CHỈNH SỬA
        // ============================
        private void LoadEditInfo(string id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT * FROM THANHVIEN WHERE MaThanhVien = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    hfEditID.Value = id;
                    txtEditHoTen.Text = r["HoTen"].ToString();
                    ddlEditGioiTinh.SelectedValue = r["GioiTinh"].ToString();
                    txtEditNgaySinh.Text = Convert.ToDateTime(r["NgaySinh"]).ToString("yyyy-MM-dd");

                    txtEditNgayMat.Text = r["NgayMat"] != DBNull.Value
                        ? Convert.ToDateTime(r["NgayMat"]).ToString("yyyy-MM-dd")
                        : "";

                    string maDoi = r["MaDoi"].ToString();
                    if (ddlEditDoi.Items.FindByValue(maDoi) != null)
                        ddlEditDoi.SelectedValue = maDoi;

                    string maPhai = r["MaPhai"].ToString();
                    if (ddlEditPhai.Items.FindByValue(maPhai) != null)
                        ddlEditPhai.SelectedValue = maPhai;
                    else
                        ddlEditPhai.SelectedValue = ""; // chọn item rỗng

                    string maChi = r["MaChi"].ToString();
                    if (ddlEditChi.Items.FindByValue(maChi) != null)
                        ddlEditChi.SelectedValue = maChi;
                    else
                        ddlEditChi.SelectedValue = ""; // chọn item rỗng
                }
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM THANHVIEN WHERE MaThanhVien=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", hfDeleteID.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadData();

            lblNotify.Text = "🗑️ Xoá thành viên thành công!";
            lblNotify.ForeColor = System.Drawing.Color.Green;

            ScriptManager.RegisterStartupScript(this, GetType(), "hidePopup", "closePopup();", true);
        }


        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            // ✅ CLEAR thông báo popup
            lblPopupEditMsg.Text = "";

            // ===== ✅ CHECK HỌ TÊN CHỈ LÀ CHỮ =====
            string hoTen = txtEditHoTen.Text.Trim();

            if (string.IsNullOrEmpty(hoTen))
            {
                lblPopupEditMsg.Text = "❌ Họ tên không được để trống!";
                ScriptManager.RegisterStartupScript(this, GetType(), "showEditAgain", "openEditPopup();", true);
                return;
            }

            Regex nameRegex = new Regex(@"^[\p{L}\s]+$"); // chữ Unicode + khoảng trắng
            if (!nameRegex.IsMatch(hoTen))
            {
                lblPopupEditMsg.Text = "❌ Họ tên chỉ được chứa chữ cái và khoảng trắng!";
                ScriptManager.RegisterStartupScript(this, GetType(), "showEditAgain", "openEditPopup();", true);
                return;
            }

            // ===== ✅ CHECK NGÀY MẤT > NGÀY SINH =====
            DateTime ngaySinh;
            if (!DateTime.TryParse(txtEditNgaySinh.Text, out ngaySinh))
            {
                lblPopupEditMsg.Text = "❌ Ngày sinh không hợp lệ!";
                ScriptManager.RegisterStartupScript(this, GetType(), "showEditAgain", "openEditPopup();", true);
                return;
            }

            DateTime? ngayMat = null;
            if (!string.IsNullOrEmpty(txtEditNgayMat.Text))
            {
                DateTime tempMat;
                if (!DateTime.TryParse(txtEditNgayMat.Text, out tempMat))
                {
                    lblPopupEditMsg.Text = "❌ Ngày mất không hợp lệ!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showEditAgain", "openEditPopup();", true);
                    return;
                }

                ngayMat = tempMat;

                if (ngayMat <= ngaySinh)
                {
                    lblPopupEditMsg.Text = "❌ Ngày mất phải lớn hơn ngày sinh!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showEditAgain", "openEditPopup();", true);
                    return;
                }
            }

            // ===== CODE CŨ GIỮ NGUYÊN =====
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
        UPDATE THANHVIEN SET
            HoTen=@HoTen,
            GioiTinh=@GioiTinh,
            NgaySinh=@NgaySinh,
            NgayMat=@NgayMat,
            MaDoi=@MaDoi,
            MaPhai=@MaPhai,
            MaChi=@MaChi,
            VaiTroTrongHo=@VaiTro
        WHERE MaThanhVien=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", hfEditID.Value);

                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@GioiTinh", ddlEditGioiTinh.SelectedValue);

                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);

                cmd.Parameters.AddWithValue("@NgayMat",
                    ngayMat.HasValue ? (object)ngayMat.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@MaDoi", ddlEditDoi.SelectedValue);
                cmd.Parameters.AddWithValue("@MaPhai",
                    string.IsNullOrEmpty(ddlEditPhai.SelectedValue) ? (object)DBNull.Value : ddlEditPhai.SelectedValue);

                cmd.Parameters.AddWithValue("@MaChi",
                    string.IsNullOrEmpty(ddlEditChi.SelectedValue) ? (object)DBNull.Value : ddlEditChi.SelectedValue);

                cmd.Parameters.AddWithValue("@VaiTro", ddlEditVaiTro.SelectedValue);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadData();

            // ✅ Thông báo thành công vẫn để ngoài page
            lblNotify.Text = "✔️ Cập nhật thành viên thành công!";
            lblNotify.ForeColor = System.Drawing.Color.Green;

            ScriptManager.RegisterStartupScript(this, GetType(), "closeEdit", "closePopup();", true);
        }

    }
}
