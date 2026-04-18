using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GPDH
{
    public partial class CayGiaPha : System.Web.UI.Page
    {
        private HashSet<int> rendered;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                HienThiCayGiaPha();
        }

        private class ThanhVien
        {
            public int MaThanhVien { get; set; }
            public string HoTen { get; set; }
            public string GioiTinh { get; set; }
            public DateTime? NgaySinh { get; set; }
            public DateTime? NgayMat { get; set; }
            public int? MaDoi { get; set; }
            public int? QuanHeCha { get; set; }
            public int? QuanHeVoChong { get; set; }
        }

        private void HienThiCayGiaPha()
        {
            string connStr = ConfigurationManager.ConnectionStrings["GPDHConnectionString"].ConnectionString;
            List<ThanhVien> ds = LayDanhSachThanhVien(connStr);

            rendered = new HashSet<int>();

            // Tìm các gốc (người không có cha)
            var gocs = ds.Where(x => x.QuanHeCha == null)
                         .OrderBy(x => x.NgaySinh ?? DateTime.MinValue)
                         .ToList();

            if (!gocs.Any())
            {
                var allIds = new HashSet<int>(ds.Select(x => x.MaThanhVien));
                var childIds = new HashSet<int>(ds.Where(x => x.QuanHeCha.HasValue)
                                                  .Select(x => x.QuanHeCha.Value));
                gocs = ds.Where(x => allIds.Except(childIds).Contains(x.MaThanhVien)).ToList();
            }

            var html = new StringBuilder("<ul>");
            foreach (var goc in gocs)
                html.Append(GenerateNode(goc, ds));
            html.Append("</ul>");

            ltCayGiaPha.Text = html.ToString();
        }

        private List<ThanhVien> LayDanhSachThanhVien(string connStr)
        {
            var ds = new List<ThanhVien>();
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("SELECT * FROM THANHVIEN", conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ds.Add(new ThanhVien
                    {
                        MaThanhVien = (int)reader["MaThanhVien"],
                        HoTen = reader["HoTen"]?.ToString() ?? "",
                        GioiTinh = reader["GioiTinh"]?.ToString() ?? "",
                        NgaySinh = reader["NgaySinh"] as DateTime?,
                        NgayMat = reader["NgayMat"] as DateTime?,
                        MaDoi = reader["MaDoi"] as int?,
                        QuanHeCha = reader["QuanHeCha"] as int?,
                        QuanHeVoChong = reader["QuanHeVoChong"] as int?
                    });
                }
            }
            return ds;
        }

        private string GenerateNode(ThanhVien tv, List<ThanhVien> ds)
        {
            if (rendered.Contains(tv.MaThanhVien))
                return "";

            rendered.Add(tv.MaThanhVien);

            // Tìm vợ/chồng
            var spouse = tv.QuanHeVoChong.HasValue
                ? ds.FirstOrDefault(x => x.MaThanhVien == tv.QuanHeVoChong.Value)
                : ds.FirstOrDefault(x => x.QuanHeVoChong == tv.MaThanhVien);

            if (spouse != null)
                rendered.Add(spouse.MaThanhVien);

            var html = new StringBuilder("<li><a href='#'>");

            // Thông tin người chính
            html.Append($"<p class='name'>{Escape(tv.HoTen)}{FormatYear(tv.NgaySinh)}{(tv.NgayMat.HasValue ? " (đã mất)" : "")}</p>");

            // Thông tin vợ/chồng
            if (spouse != null)
                html.Append($"<p class='meta'>{Escape(spouse.HoTen)}{FormatYear(spouse.NgaySinh)}{(spouse.NgayMat.HasValue ? " (đã mất)" : "")}</p>");

            html.Append("</a>");

            // Tìm con
            var children = ds.Where(x =>
                (x.QuanHeCha == tv.MaThanhVien) ||
                (spouse != null && x.QuanHeCha == spouse.MaThanhVien))
                .OrderBy(x => x.NgaySinh ?? DateTime.MinValue)
                .ToList();

            if (children.Any())
            {
                html.Append("<ul>");
                foreach (var child in children)
                    html.Append(GenerateNode(child, ds));
                html.Append("</ul>");
            }

            html.Append("</li>");
            return html.ToString();
        }

        private static string Escape(string s) =>
            string.IsNullOrEmpty(s) ? "" : s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

        private static string FormatYear(DateTime? date) =>
            date.HasValue ? $" - {date.Value.Year}" : "";
    }
}
