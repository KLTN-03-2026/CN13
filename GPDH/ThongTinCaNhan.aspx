<%@ Page Title="Thông tin cá nhân" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThongTinCaNhan.aspx.cs" Inherits="GPDH.ThongTinCaNhan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <style>
    .page-title {
        font-size: 26px;
        font-weight: bold;
        color: black;
        margin-bottom: 20px;
        text-align: left; 
        margin-left: 10px; 
    }

    .profile-container {
        display: flex;
        justify-content: center;
        gap: 80px;
        padding: 40px 20px;
        font-family: "Segoe UI";
    }

    .left-panel {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .info-box {
        background-color: #F5F5F5;
        padding: 25px 50px;
        border-radius: 12px;
        width: 450px;
        box-shadow: 0 0 6px rgba(0,0,0,0.15);
    }

    .info-table {
        width: 100%;
        border-collapse: collapse;
    }

    .info-table td {
        padding: 6px 0;
        font-size: 16px;
    }

    .info-table .label {
        font-weight: bold;
        width: 180px;
        white-space: nowrap;
        text-align: right;
        padding-right: 15px;
    }

    .btn-edit {
        margin-top: 20px;
        background-color: #c89b3c;
        color: white;
        border: none;
        border-radius: 8px;
        padding: 10px 28px;
        cursor: pointer;
        font-weight: bold;
    }

    .btn-edit:hover {
        background-color: #a27a23;
    }

    .avatar-box {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .avatar-box img {
        width: 160px;
        height: 160px;
        border-radius: 50%;
        object-fit: cover;
        border: 3px solid #000;
    }

    .btn-avatar {
        background-color: #f8e7a0;
        border: none;
        padding: 7px 18px;
        margin-top: 12px;
        cursor: pointer;
        border-radius: 6px;
        font-weight: bold;
    }

    .msg-avatar {
        margin-top: 10px;
        font-weight: bold;
        text-align: center;
    }
    .hidden-file {
    display: none;
}

</style>

<div class="page-title">Thông tin cá nhân</div>

    <!-- BẮT ĐẦU KHỐI TRANG -->
    <div class="profile-page">
        <div class="profile-container">

            <!-- CỘT TRÁI -->
            <div class="left-panel">
                <div class="info-box">
                    <table class="info-table">
                        <tr><td class="label">Họ và Tên:</td><td><asp:Label ID="lblHoTen" runat="server" /></td></tr>
                        <tr><td class="label">Ngày sinh:</td><td><asp:Label ID="lblNgaySinh" runat="server" /></td></tr>
                        <tr><td class="label">Ngày mất:</td><td><asp:Label ID="lblNgayMat" runat="server" /></td></tr>
                        <tr><td class="label">Giới tính:</td><td><asp:Label ID="lblGioiTinh" runat="server" /></td></tr>
                        <tr><td class="label">Đời:</td><td><asp:Label ID="lblDoi" runat="server" /></td></tr>
                        <tr><td class="label">Phái:</td><td><asp:Label ID="lblPhai" runat="server" /></td></tr>
                        <tr><td class="label">Chi:</td><td><asp:Label ID="lblChi" runat="server" /></td></tr>
                        <tr><td class="label">Vai trò trong họ:</td><td><asp:Label ID="lblVaiTro" runat="server" /></td></tr>
                        <tr><td class="label">Email:</td><td><asp:Label ID="lblEmail" runat="server" /></td></tr>
                        <tr><td class="label">SĐT:</td><td><asp:Label ID="lblSDT" runat="server" /></td></tr>
                        <tr><td class="label">Nghề nghiệp:</td><td><asp:Label ID="lblNgheNghiep" runat="server" /></td></tr>
                        <tr><td class="label">Địa chỉ:</td><td><asp:Label ID="lblDiaChi" runat="server" /></td></tr>
                        <tr><td class="label">Ghi chú:</td><td><asp:Label ID="lblGhiChu" runat="server" /></td></tr>
                    </table>
                </div>

                <asp:Button ID="btnChinhSua" runat="server" Text="Chỉnh sửa" CssClass="btn-edit" OnClick="btnChinhSua_Click" />
            </div>

            <!-- CỘT PHẢI -->
            <div class="avatar-box">
    <asp:Image ID="imgAvatar" runat="server" ImageUrl="~/Images/avatar_default.png" />

    <!-- ✅ FileUpload ẩn -->
    <asp:FileUpload ID="fuAvatar" runat="server" CssClass="hidden-file" onchange="autoUpload();" />

    <!-- ✅ Button đẹp -->
    <asp:Button ID="btnThayAnh" runat="server" Text="Thay đổi avatar" CssClass="btn-avatar"
        OnClientClick="openFilePicker(); return false;" />

    <!-- ✅ Button upload hidden để postback -->
    <asp:Button ID="btnUploadHidden" runat="server" Style="display:none;" OnClick="btnThayAnh_Click" />

    <asp:Label ID="lblAvatarMsg" runat="server" CssClass="msg-avatar"></asp:Label>
</div>


        </div>
    </div>
    <!-- KẾT THÚC KHỐI TRANG -->
    <script>
    function openFilePicker() {
        document.getElementById("<%= fuAvatar.ClientID %>").click();
    }

    function autoUpload() {
        document.getElementById("<%= btnUploadHidden.ClientID %>").click();
    }
</script>

</asp:Content>
