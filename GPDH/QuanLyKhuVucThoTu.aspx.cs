using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPDH
{
    public partial class QuanLyKhuVucThoTu : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTabs(); // luôn load tab trước khi xử lý event

            if (!IsPostBack)
            {
                int firstID = GetFirstID();
                HighlightActiveTab(firstID);
                LoadKhuVuc(firstID);
            }
        }

        // Lấy ID đầu tiên
        private int GetFirstID()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT TOP 1 MaThoTu FROM KHUVUC_THOTU ORDER BY MaThoTu", conn))
            {
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Tạo TAB ĐỘNG từ bảng KHUVUC_THOTU
        private void LoadTabs()
        {
            tabContainer.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT MaThoTu, TenKhuVuc FROM KHUVUC_THOTU", conn))
            {
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id = Convert.ToInt32(r["MaThoTu"]);
                    string ten = r["TenKhuVuc"].ToString();

                    LinkButton btn = new LinkButton();
                    btn.Text = ten;
                    btn.CssClass = "tab-btn";
                    btn.CommandArgument = id.ToString();
                    btn.Command += Tab_Click;

                    tabContainer.Controls.Add(btn);
                }
            }
        }

        // Khi nhấn TAB
        protected void Tab_Click(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            HighlightActiveTab(id);
            LoadKhuVuc(id);
        }

        // Tô màu TAB đang chọn
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

        // LOAD DỮ LIỆU THEO TAB
        private void LoadKhuVuc(int maThoTu)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT t.TenKhuVuc, t.DiaChi, t.NguoiPhuTrach, t.MoTa, t.HinhAnh,
                       tv.HoTen
                FROM KHUVUC_THOTU t
                LEFT JOIN THANHVIEN tv ON t.NguoiPhuTrach = tv.MaThanhVien
                WHERE t.MaThoTu = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", maThoTu);

                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    lblDiaChi.Text = "Địa chỉ: " + r["DiaChi"];
                    lblNguoiPhuTrach.Text = "Người phụ trách: " + r["HoTen"];
                    lblMoTa.Text = r["MoTa"].ToString();

                    imgKhuVuc.ImageUrl = "~/Images/" + r["HinhAnh"];
                    hfID.Value = maThoTu.ToString();

                    txtTen.Text = r["TenKhuVuc"].ToString();
                    txtDiaChi.Text = r["DiaChi"].ToString();
                    txtNguoiPhuTrach.Text = r["NguoiPhuTrach"].ToString();
                    txtMoTa.Text = r["MoTa"].ToString();

                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string fileName = "";

            if (fileAnh.HasFile)
            {
                fileName = DateTime.Now.Ticks + "_" + fileAnh.FileName;
                string path = Server.MapPath("~/Images/" + fileName);
                try
                {
                    fileAnh.SaveAs(path);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Lỗi lưu ảnh: " + ex.Message + "');</script>");
                }

            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd;

                if (string.IsNullOrEmpty(hfID.Value)) // thêm mới
                {
                    cmd = new SqlCommand(
                        @"INSERT INTO KHUVUC_THOTU (TenKhuVuc, DiaChi, NguoiPhuTrach, MoTa, HinhAnh)
                  VALUES (@Ten, @DC, @NPT, @MoTa, @Hinh)", conn);
                }
                else // sửa
                {
                    cmd = new SqlCommand(
                        @"UPDATE KHUVUC_THOTU 
                  SET TenKhuVuc=@Ten, DiaChi=@DC, NguoiPhuTrach=@NPT, MoTa=@MoTa,
                      HinhAnh = CASE WHEN @Hinh<>'' THEN @Hinh ELSE HinhAnh END
                  WHERE MaThoTu=@ID", conn);

                    cmd.Parameters.AddWithValue("@ID", hfID.Value);
                }

                cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                cmd.Parameters.AddWithValue("@DC", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@NPT", txtNguoiPhuTrach.Text);
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                cmd.Parameters.AddWithValue("@Hinh", fileName);

                cmd.ExecuteNonQuery();
            }

            Response.Redirect(Request.RawUrl);
        }
        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM KHUVUC_THOTU WHERE MaThoTu=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", hfID.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            Response.Redirect(Request.RawUrl);
        }

    }
}
