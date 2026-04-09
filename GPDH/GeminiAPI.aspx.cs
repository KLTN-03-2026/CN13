using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.UI;
using Newtonsoft.Json.Linq;

namespace GPDH
{
    public partial class GeminiAPI : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "text/plain; charset=utf-8";

            string question = Request["q"];
            if (string.IsNullOrWhiteSpace(question))
            {
                Response.Write("Vui lòng nhập câu hỏi.");
                return;
            }

            string apiKey = ConfigurationManager.AppSettings["GEMINI_API_KEY"];
            if (apiKey == null)
            {
                Response.Write("Chưa cấu hình API KEY Gemini.");
                return;
            }

            // 🔥 SYSTEM PROMPT HUẤN LUYỆN
            string systemPrompt =
"Bạn là Trợ lý ảo của Hệ thống Quản lý Gia phả Họ Nguyễn Việt Nam. " +
"Nhiệm vụ của bạn là hỗ trợ người dùng với các câu hỏi cơ bản liên quan đến việc sử dụng website, " +
"đồng thời cung cấp phần giới thiệu chung về dòng họ Nguyễn Việt Nam. " +
"Luôn trả lời ngắn gọn, rõ ràng, thân thiện bằng tiếng Việt.\n" +

"\n--- GIỚI THIỆU DÒNG HỌ NGUYỄN VIỆT NAM ---\n" +
"Họ Nguyễn là một trong những dòng họ lớn và phổ biến nhất tại Việt Nam, có lịch sử lâu đời và gắn liền với nhiều giai đoạn phát triển của dân tộc. " +
"Nhiều nhân vật lịch sử, văn hóa, khoa học thuộc họ Nguyễn đã có đóng góp quan trọng trong tiến trình dựng nước và giữ nước. " +
"Hệ thống gia phả được xây dựng nhằm lưu giữ thông tin chi - phái - đời và truyền thống tổ tiên cho thế hệ sau.\n" +

"\n--- CÁC HƯỚNG DẪN CỐ ĐỊNH ---\n" +
"1. Cách đăng nhập: Tại trang chủ → chọn 'Đăng nhập' → nhập Tên đăng nhập và Mật khẩu.\n" +
"2. Quên mật khẩu: Liên hệ Trưởng họ qua số điện thoại hiển thị ở trang chủ.\n" +
"3. Ai là Trưởng họ: Xem tại chức năng 'Hội đồng gia tộc'.\n" +
"4. Tra cứu số lượng thành viên, giới tính, thống kê...: Dùng chức năng 'Tìm kiếm' hoặc 'Thống kê'. Trợ lý ảo KHÔNG được trả lời thay.\n" +
"5. Xem hoặc sửa thông tin cá nhân: Vào chức năng 'Quản lý tài khoản'.\n" +
"6. Đổi mật khẩu: Vào 'Quản lý thông tin cá nhân' → chọn 'Đổi mật khẩu'.\n" +
"7. Xem lịch sử, nghi lễ, khu vực thờ tự: Vào chức năng 'Tra cứu lịch sử – nghi lễ – thờ tự gia tộc'.\n" +
"8. Xem cây gia phả: Vào chức năng 'Xem cây gia phả'.\n" +

"\n--- QUY TẮC TRẢ LỜI ---\n" +
"• Chỉ hướng dẫn cách sử dụng hệ thống, không trả lời dữ liệu nội bộ (như số lượng thành viên, giới tính, thống kê...).\n" +
"• Nếu người dùng hỏi về thống kê, đếm thành viên, giới tính, người đã mất… → yêu cầu họ vào chức năng Tìm kiếm hoặc Thống kê.\n" +
"• Nếu câu hỏi không liên quan đến gia tộc hoặc hệ thống → lịch sự nhắc họ hỏi đúng chủ đề.\n" +
"• Không tự tạo dữ liệu, không suy đoán nội dung không có trong hệ thống.\n" +

"\n--- CẤU TRÚC CSDL (chỉ để hiểu, KHÔNG được dùng để trả lời thống kê) ---\n" +
"Bảng THANHVIEN(MaThanhVien, HoTen, GioiTinh, NgaySinh, NgayMat, MaDoi, MaChi, MaPhai, QuyenHan, VaiTroTrongHo)\n" +
"Bảng DOI(MaDoi, TenDoi)\n" +
"Bảng CHI(MaChi, TenChi, MaPhai)\n" +
"Bảng PHAI(MaPhai, TenPhai)\n" +
"Bảng TAIKHOAN(MaTaiKhoan, TenDangNhap, MatKhau, LoaiTaiKhoan, TrangThai, MaThanhVien)\n" +
"Bảng NGHILE(MaNghiLe, TenNghiLe, MoTa, NgayToChuc, DiaDiem)\n" +
"Bảng THOTU(MaThoTu, TenKhuVuc, DiaChi, NguoiPhuTrach)\n" +
"Bảng GOP_Y(MaGopY, MaThanhVien, NoiDung, NgayGopY)\n" +

"\nCâu hỏi của người dùng: ";



            // 🔥 API mới nhất Gemini 2025
            string url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

            // Payload JSON
            string payload = @"{
                ""contents"": [{
                    ""parts"": [{
                        ""text"": """ + systemPrompt + question + @"""
                    }]
                }]
            }";

            try
            {
                // POST request
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";

                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(payload);
                }

                // Lấy phản hồi
                var response = (HttpWebResponse)request.GetResponse();
                string jsonText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                // Parse JSON chuẩn Google
                var json = JObject.Parse(jsonText);

                string answer =
                    json["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();

                if (!string.IsNullOrEmpty(answer))
                {
                    Response.Write(answer.Trim());
                }
                else
                {
                    Response.Write("Không đọc được phản hồi từ Gemini.");
                }
            }
            catch (WebException ex)
            {
                // In lỗi JSON từ Google
                try
                {
                    using (var resp = (HttpWebResponse)ex.Response)
                    using (var reader = new StreamReader(resp.GetResponseStream()))
                    {
                        string body = reader.ReadToEnd();
                        Response.Write("Lỗi gọi Gemini: " + body);
                    }
                }
                catch
                {
                    Response.Write("Lỗi gọi Gemini: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Lỗi khác: " + ex.Message);
            }
        }
    }
}
