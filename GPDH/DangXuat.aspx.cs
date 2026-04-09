using System;
using System.Web.UI;

namespace GPDH
{
    public partial class DangXuat : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Xóa toàn bộ session
            Session.Clear();
            Session.Abandon();

            // Quay về trang chủ
            Response.Redirect("TrangChu.aspx");
        }
    }
}
