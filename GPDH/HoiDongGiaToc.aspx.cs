using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace GPDH
{
    public partial class HoiDongGiaToc : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT 
            tv.MaThanhVien,
            tv.HoTen,
            tv.NgaySinh,
            d.TenDoi,
            p.TenPhai,
            STRING_AGG(c.TenChi, ', ') AS TenChi,
            tv.VaiTroTrongHo
        FROM THANHVIEN tv
        LEFT JOIN DOI d ON tv.MaDoi = d.MaDoi
        LEFT JOIN PHAI p ON tv.MaPhai = p.MaPhai
        LEFT JOIN CHI c ON tv.MaChi = c.MaChi
        WHERE tv.VaiTroTrongHo IN (N'Trưởng họ', N'Trưởng Phái', N'Trưởng Chi')
        GROUP BY 
            tv.MaThanhVien, tv.HoTen, tv.NgaySinh,
            d.TenDoi, p.TenPhai, tv.VaiTroTrongHo
        ORDER BY tv.HoTen
    ", conn))
            {
                DataTable dt = new DataTable();
                conn.Open();
                dt.Load(cmd.ExecuteReader());

                gvHoiDong.DataSource = dt;
                gvHoiDong.DataBind();
            }
        }

    }
}
