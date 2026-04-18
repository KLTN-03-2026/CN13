using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPDH
{
    public partial class TimKiem : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdowns();
                LoadData();
            }
        }

        private void LoadDropdowns()
        {
            ddlDoi.DataSource = GetData("SELECT MaDoi, TenDoi FROM DOI");
            ddlDoi.DataTextField = "TenDoi";
            ddlDoi.DataValueField = "MaDoi";
            ddlDoi.DataBind();
            ddlDoi.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Đời", ""));

            ddlPhai.DataSource = GetData("SELECT MaPhai, TenPhai FROM PHAI");
            ddlPhai.DataTextField = "TenPhai";
            ddlPhai.DataValueField = "MaPhai";
            ddlPhai.DataBind();
            ddlPhai.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Phái", ""));

            ddlChi.DataSource = GetData("SELECT MaChi, TenChi FROM CHI");
            ddlChi.DataTextField = "TenChi";
            ddlChi.DataValueField = "MaChi";
            ddlChi.DataBind();
            ddlChi.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Chi", ""));
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

        private void LoadData()
        {
            LoadData("");
        }

        private void LoadData(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT tv.MaThanhVien, tv.HoTen, tv.NgaySinh,
               d.TenDoi, p.TenPhai, c.TenChi,
               tv.QuanHeCha, tv.QuanHeVoChong
        FROM THANHVIEN tv
        LEFT JOIN DOI d ON tv.MaDoi = d.MaDoi
        LEFT JOIN PHAI p ON tv.MaPhai = p.MaPhai
        LEFT JOIN CHI c ON tv.MaChi = c.MaChi
        WHERE
            (@kw = '' OR tv.HoTen LIKE '%' + @kw + '%')
            AND (@doi = '' OR tv.MaDoi = @doi)
            AND (@phai = '' OR tv.MaPhai = @phai)
            AND (@chi = '' OR tv.MaChi = @chi)
            AND (@gioitinh = '' OR tv.GioiTinh = @gioitinh)
            AND (@ngaysinh = '' OR CONVERT(date, tv.NgaySinh) = @ngaysinh)
        ORDER BY tv.NgaySinh ASC
    ", conn))
            {
                cmd.Parameters.AddWithValue("@kw", keyword);
                cmd.Parameters.AddWithValue("@doi", ddlDoi.SelectedValue);
                cmd.Parameters.AddWithValue("@phai", ddlPhai.SelectedValue);
                cmd.Parameters.AddWithValue("@chi", ddlChi.SelectedValue);
                cmd.Parameters.AddWithValue("@gioitinh", ddlGioiTinh.SelectedValue);
                cmd.Parameters.AddWithValue("@ngaysinh",
                    string.IsNullOrEmpty(txtNgaySinh.Text) ? "" : txtNgaySinh.Text);

                DataTable dt = new DataTable();
                conn.Open();
                dt.Load(cmd.ExecuteReader());


                gvResult.DataSource = dt;
                gvResult.DataBind();
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtHoTen.Text.Trim());
        }
        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                string id = e.CommandArgument.ToString();
                ShowDetailPopup(id);
            }
        }

        private void ShowDetailPopup(string id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT tv.HoTen, tv.NgaySinh, tv.NgayMat, tv.GioiTinh,
               d.TenDoi, p.TenPhai, c.TenChi, tv.VaiTroTrongHo,
               tv.Email, tv.SoDienThoai, tv.NgheNghiep, tv.DiaChi, tv.GhiChu
        FROM THANHVIEN tv
        LEFT JOIN DOI d ON tv.MaDoi = d.MaDoi
        LEFT JOIN PHAI p ON tv.MaPhai = p.MaPhai
        LEFT JOIN CHI c ON tv.MaChi = c.MaChi
        WHERE tv.MaThanhVien = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    string json = $@"
            {{
                HoTen: '{r["HoTen"]}',
                NgaySinh: '{FormatDate(r["NgaySinh"])}',
                NgayMat: '{FormatDate(r["NgayMat"])}',
                GioiTinh: '{r["GioiTinh"]}',
                Doi: '{r["TenDoi"]}',
                Phai: '{r["TenPhai"]}',
                Chi: '{r["TenChi"]}',
                VaiTro: '{r["VaiTroTrongHo"]}',
                Email: '{r["Email"]}',
                SDT: '{r["SoDienThoai"]}',
                Nghe: '{r["NgheNghiep"]}',
                DiaChi: '{r["DiaChi"]}',
                GhiChu: '{r["GhiChu"]}'
            }}";

                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowDetail",
                        $"openDetailPopup({json});", true);
                }
            }
        }

        private string FormatDate(object dt)
        {
            return dt != DBNull.Value ? Convert.ToDateTime(dt).ToString("dd-MM-yyyy") : "";
        }


    }
}
