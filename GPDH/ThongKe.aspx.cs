using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace GPDH
{
    public partial class ThongKe : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadThongKeDoTuoi();
        }

        private void SetActiveTab(string tab)
        {
            btnDoTuoi.CssClass = "tab-btn";
            btnGioiTinh.CssClass = "tab-btn";
            btnPhai.CssClass = "tab-btn";
            btnDoi.CssClass = "tab-btn";

            switch (tab)
            {
                case "DoTuoi": btnDoTuoi.CssClass += " active-tab"; break;
                case "GioiTinh": btnGioiTinh.CssClass += " active-tab"; break;
                case "Phai": btnPhai.CssClass += " active-tab"; break;
                case "Doi": btnDoi.CssClass += " active-tab"; break;
            }
        }

        // ===== ĐỘ TUỔI =====
        protected void btnDoTuoi_Click(object sender, EventArgs e)
        {
            SetActiveTab("DoTuoi");
            LoadThongKeDoTuoi();
        }

        private void LoadThongKeDoTuoi()
        {
            string sql = @"
                WITH AgeGroups AS (
                    SELECT 
                        CASE 
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 1 AND 5 THEN '1-5'
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 6 AND 18 THEN '6-18'
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 19 AND 59 THEN '19-59'
                            ELSE '60+'
                        END AS DoTuoi
                    FROM THANHVIEN
                    WHERE NgayMat IS NULL
                )
                SELECT g.Nhom AS DoTuoi, COUNT(a.DoTuoi) AS SoLuong
                FROM (VALUES('1-5'),('6-18'),('19-59'),('60+')) g(Nhom)
                LEFT JOIN AgeGroups a ON g.Nhom = a.DoTuoi
                GROUP BY g.Nhom
                ORDER BY 
                    CASE g.Nhom 
                        WHEN '1-5' THEN 1
                        WHEN '6-18' THEN 2
                        WHEN '19-59' THEN 3
                        WHEN '60+' THEN 4 END";

            DrawChart(sql, "Độ tuổi");
            lblNhanXet.Text = "Thống kê độ tuổi của các thành viên còn sống.";
        }

        // ===== GIỚI TÍNH =====
        protected void btnGioiTinh_Click(object sender, EventArgs e)
        {
            SetActiveTab("GioiTinh");
            DrawChart(@"
                SELECT GioiTinh AS Nhom, COUNT(*) AS SoLuong
                FROM THANHVIEN 
                WHERE NgayMat IS NULL 
                GROUP BY GioiTinh
                ORDER BY GioiTinh", "Giới tính");

            lblNhanXet.Text = "Thống kê giới tính của các thành viên còn sống.";
        }

        // ===== PHÁI =====
        protected void btnPhai_Click(object sender, EventArgs e)
        {
            SetActiveTab("Phai");
            DrawChart(@"
                SELECT p.TenPhai AS Nhom, ISNULL(COUNT(t.MaThanhVien),0) AS SoLuong
                FROM PHAI p
                LEFT JOIN THANHVIEN t ON t.MaPhai = p.MaPhai AND t.NgayMat IS NULL
                GROUP BY p.TenPhai
                ORDER BY p.TenPhai", "Phái");

            lblNhanXet.Text = "Thống kê số lượng thành viên còn sống theo từng phái.";
        }

        // ===== ĐỜI =====
        protected void btnDoi_Click(object sender, EventArgs e)
        {
            SetActiveTab("Doi");
            DrawChart(@"
                SELECT d.TenDoi AS Nhom, ISNULL(COUNT(t.MaThanhVien),0) AS SoLuong
                FROM DOI d
                LEFT JOIN THANHVIEN t ON t.MaDoi = d.MaDoi AND t.NgayMat IS NULL
                GROUP BY d.TenDoi
                ORDER BY TRY_CAST(SUBSTRING(d.TenDoi, PATINDEX('%[0-9]%', d.TenDoi), 10) AS INT)", "Đời");

            lblNhanXet.Text = "Thống kê số lượng thành viên còn sống theo từng đời.";
        }

        // ===== CHUNG: VẼ BIỂU ĐỒ =====
        private void DrawChart(string sql, string title)
        {
            DataTable dt = GetData(sql);
            string[] labels = new string[dt.Rows.Count];
            int[] values = new int[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                labels[i] = dt.Columns.Contains("DoTuoi") ? dt.Rows[i]["DoTuoi"].ToString() : dt.Rows[i]["Nhom"].ToString();
                values[i] = Convert.ToInt32(dt.Rows[i]["SoLuong"]);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string lblJson = js.Serialize(labels);
            string valJson = js.Serialize(values);

            string script = $"veBieuDo({lblJson}, {valJson}, '{title}');";
            ClientScript.RegisterStartupScript(this.GetType(), "chartScript", script, true);
        }

        // ===== LẤY DỮ LIỆU =====
        private DataTable GetData(string query)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}