using System;
using System.Web.UI;

namespace GPDH
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                HienThiMenuTheoVaiTro();
        }

        private void HienThiMenuTheoVaiTro()
        {
            string menuHtml = @"<a href='TrangChu.aspx'>Trang chủ</a>
                                <a href='DangNhap.aspx'>Đăng nhập</a>";

            string vaiTro = Session["VaiTro"]?.ToString();

            if (vaiTro == "TruongHo")
            {
                menuHtml = @"<a href='TrangChu.aspx'>Trang chủ</a>

                 <div class='menu-item'>
                                <a href='#'>Quản lý thành viên</a>
                                     <div class='submenu'>
                                        <a href='ThemThanhVien.aspx'>Thêm thành viên</a>
                                        <a href='ChinhSuaThanhVien.aspx'>Chỉnh sửa/xoá thành viên</a>
                                        <a href='KichHoatThanhVien.aspx'>Kích hoạt/vô hiệu hoá tài khoản</a>
                                     </div>
                            </div>

                 <div class='menu-item'>
                                <a href='#'>Quản lý lịch sử, nghi lễ và thờ tự gia tộc</a>
                                     <div class='submenu'>
                                        <a href='QuanLyLichSu.aspx'>Quản lý lịch sử</a>
                                        <a href='QuanLyNghiLe.aspx'>Quản lý nghi lễ</a>
                                        <a href='QuanLyKhuVucThoTu.aspx'>Quản lý khu vực thờ tự</a>
                                     </div>
                            </div>
                 <a href='XemGopY.aspx'>Góp ý của thành viên</a>
                 <a href='DangXuat.aspx' style='color:red;'>Đăng xuất</a>";
            }

            else if (vaiTro == "ThanhVien")
            {
                menuHtml = @"<a href='TrangChu.aspx'>Trang chủ</a>
                            <div class='menu-item'>
                                <a href='#'>Thông tin cá nhân</a>
                                     <div class='submenu'>
                                        <a href='ThongTinCaNhan.aspx'>Thông tin cá nhân</a>
                                        <a href='DoiMatKhau.aspx'>Đổi mật khẩu</a>
                                     </div>
                            </div>
                             <a href='CayGiaPha.aspx'>Cây gia phả</a>
                           <div class='menu-item'>
                                <a href='#'>Lịch sử, Nghi lễ và Thờ tự</a>
                                     <div class='submenu'>
                                        <a href='LichSu.aspx'>Lịch sử</a>
                                        <a href='NghiLe.aspx'>Nghi lễ</a>
                                        <a href='KhuVucThoTu.aspx'>Khu vực thờ tự</a>
                                     </div>
                            </div>
                             <a href='TimKiem.aspx'>Tìm kiếm</a>
                             <a href='HoiDongGiaToc.aspx'>Hội đồng gia tộc</a>
                             <a href='ThongKe.aspx'>Thống kê</a>
                             <a href='GopY.aspx'>Góp ý</a>
                             <a href='DangXuat.aspx' style='color:red;'>Đăng xuất</a>";
            }

            menu.InnerHtml = menuHtml;
        }
    }
}
