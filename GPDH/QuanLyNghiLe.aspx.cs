using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPDH
{
    public partial class QuanLyNghiLe : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTabs();   // luôn load tabs trước khi check event

            if (!IsPostBack)
            {
                int firstID = GetFirstID();
                HighlightActiveTab(firstID);
                LoadNghiLe(firstID);
            }
        }

        private int GetFirstID()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 MaNghiLe FROM NGHILE ORDER BY MaNghiLe", conn))
            {
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void LoadTabs()
        {
            tabContainer.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT MaNghiLe, TenNghiLe FROM NGHILE", conn))
            {
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id = Convert.ToInt32(r["MaNghiLe"]);
                    string ten = r["TenNghiLe"].ToString();

                    LinkButton btn = new LinkButton();
                    btn.Text = ten;
                    btn.CssClass = "tab-btn";
                    btn.CommandArgument = id.ToString();
                    btn.Command += Tab_Click;

                    tabContainer.Controls.Add(btn);
                }
            }
        }

        protected void Tab_Click(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            HighlightActiveTab(id);
            LoadNghiLe(id);
        }

        private void HighlightActiveTab(int id)
        {
            foreach (Control c in tabContainer.Controls)
            {
                if (c is LinkButton btn)
                {
                    if (btn.CommandArgument == id.ToString())
                        btn.CssClass = "tab-btn active-tab";
                    else
                        btn.CssClass = "tab-btn";
                }
            }
        }

        private void LoadNghiLe(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT TenNghiLe, MoTa, NgayToChuc, DiaDiem, DiaDiemDuPhong FROM NGHILE WHERE MaNghiLe=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    string ngayAm = r["NgayToChuc"].ToString();   // dd/MM
                    DateTime ngayDuong = ConvertLunarToSolar(ngayAm, DateTime.Now.Year);

                    if (ngayDuong < DateTime.Today)
                        ngayDuong = ConvertLunarToSolar(ngayAm, DateTime.Now.Year + 1);

                    lblNgayToChuc.Text = $"<b>Ngày tổ chức:</b> {ngayAm} âm lịch hằng năm";
                    lblDiaDiem.Text = "<b>Địa điểm:</b> " + r["DiaDiem"];

                    if (r["DiaDiemDuPhong"] != DBNull.Value && !string.IsNullOrWhiteSpace(r["DiaDiemDuPhong"].ToString()))
                        lblDiaDiemDuPhong.Text = "<b>Địa điểm dự phòng:</b> " + r["DiaDiemDuPhong"];
                    else
                        lblDiaDiemDuPhong.Text = "";

                    lblMoTa.Text = "<b>Nội dung:</b><br/>• " + r["MoTa"].ToString().Replace(";", "<br/>• ");

                    int soNgay = (ngayDuong - DateTime.Today).Days;
                    lblSoNgay.Text = "Còn " + soNgay.ToString();

                    // 🟢 GÁN CHO FORM POPUP NGAY TẠI ĐÂY
                    hfID.Value = id.ToString();
                    txtTen.Text = r["TenNghiLe"].ToString();
                    txtNgay.Text = r["NgayToChuc"].ToString();
                    txtDiaDiem.Text = r["DiaDiem"].ToString();
                    txtDiaDiemDP.Text = r["DiaDiemDuPhong"].ToString();
                    txtMoTa.Text = r["MoTa"].ToString();
                }
            }
        }

        private DateTime ConvertLunarToSolar(string ngayAm, int year)
        {
            string[] arr = ngayAm.Split('/');
            int day = int.Parse(arr[0]);
            int month = int.Parse(arr[1]);

            ChineseLunisolarCalendar lunar = new ChineseLunisolarCalendar();

            int leapMonth = lunar.GetLeapMonth(year);
            if (leapMonth > 0 && month >= leapMonth)
                month++;

            return lunar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        // ====================== ✅ UPDATED btnSave_Click ======================
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // ✅ CLEAR MESSAGE CŨ
            lblPopupMessage.Text = "";

            // ===== ✅ CHECK NGÀY TỔ CHỨC THEO KIỂU dd/MM (ngày/tháng) =====
            string ngay = txtNgay.Text.Trim();

            // Regex bắt buộc đúng định dạng dd/MM hoặc d/M
            System.Text.RegularExpressions.Regex regNgay =
                new System.Text.RegularExpressions.Regex(@"^(0?[1-9]|[12][0-9]|3[01])/(0?[1-9]|1[0-2])$");

            if (!regNgay.IsMatch(ngay))
            {
                string mode = string.IsNullOrEmpty(hfID.Value) ? "add" : "edit";
                lblPopupMessage.Text = "❌ Nhập đúng định dạng ngày/tháng dd/MM (VD: 21/12). Không được nhập 12/21!";
                ScriptManager.RegisterStartupScript(this, GetType(), "openPopup", $"openPopup('{mode}');", true);
                return;
            }

            // Check chắc chắn bằng TryParseExact (d/M)
            DateTime tmp;
            if (!DateTime.TryParseExact(ngay, "d/M", CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
            {
                string mode = string.IsNullOrEmpty(hfID.Value) ? "add" : "edit";
                lblPopupMessage.Text = "❌ Ngày/tháng không hợp lệ!";
                ScriptManager.RegisterStartupScript(this, GetType(), "openPopup", $"openPopup('{mode}');", true);
                return;
            }

            // ===== CODE CŨ GIỮ NGUYÊN =====
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd;

                if (string.IsNullOrEmpty(hfID.Value)) // thêm mới
                {
                    cmd = new SqlCommand(
                        "INSERT INTO NGHILE (TenNghiLe, NgayToChuc, DiaDiem, DiaDiemDuPhong, MoTa)" +
                        " VALUES (@Ten, @Ngay, @DD, @DP, @MoTa)", conn);
                }
                else // sửa
                {
                    cmd = new SqlCommand(
                        "UPDATE NGHILE SET TenNghiLe=@Ten, NgayToChuc=@Ngay, DiaDiem=@DD," +
                        " DiaDiemDuPhong=@DP, MoTa=@MoTa WHERE MaNghiLe=@ID", conn);

                    cmd.Parameters.AddWithValue("@ID", hfID.Value);
                }

                cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                cmd.Parameters.AddWithValue("@Ngay", txtNgay.Text);
                cmd.Parameters.AddWithValue("@DD", txtDiaDiem.Text);
                cmd.Parameters.AddWithValue("@DP", txtDiaDiemDP.Text);
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);

                cmd.ExecuteNonQuery();
            }

            Response.Redirect(Request.RawUrl);
        }

        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM NGHILE WHERE MaNghiLe=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", hfID.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            Response.Redirect(Request.RawUrl);
        }
    }
}
