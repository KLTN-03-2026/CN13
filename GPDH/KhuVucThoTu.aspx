<%@ Page Title="Khu vực thờ tự" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="KhuVucThoTu.aspx.cs"
    Inherits="GPDH.KhuVucThoTu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Khu vực thờ tự</h2>

    <!-- TAB KHU VỰC (TẠO ĐỘNG) -->
    <div class="tab-container" id="tabContainer" runat="server"></div>

    <!-- HIỂN THỊ THÔNG TIN -->
    <div class="content-box">

        <!-- ẢNH -->
        <asp:Image ID="imgKhuVuc" runat="server" CssClass="khuvuc-img" />

        <!-- ĐỊA CHỈ + NGƯỜI PHỤ TRÁCH -->
        <div class="info">
            <asp:Label ID="lblDiaChi" runat="server" CssClass="info-text"></asp:Label><br />
            <asp:Label ID="lblNguoiPhuTrach" runat="server" CssClass="info-text"></asp:Label>
        </div>

        <!-- MÔ TẢ -->
        <div class="mo-ta-box">
            <asp:Label ID="lblMoTa" runat="server" CssClass="mo-ta"></asp:Label>
        </div>

    </div>

<style>

h2 { text-align:left; margin-top:10px; margin-bottom:20px; }

.tab-container {
    display:flex;
    gap:15px;
    margin-bottom:25px;
}

.tab-btn {
    padding:10px 25px;
    background:#e8e8e8;
    border-radius:20px;
    border:none;
    cursor:pointer;
    font-weight:bold;
    color:black;
    text-decoration:none;
}

.tab-btn:hover { background:#d4d4d4; }
.active-tab { background:#c89b3c !important; color:white !important; }

/* ----- PHẦN HIỂN THỊ ----- */
.content-box {
    width:90%;
    margin:auto;
    text-align:left;
}

.khuvuc-img {
    width:500px;
    border-radius:12px;
    margin:20px auto;
    display:block;
}

.info-text {
    font-size:18px;
    font-weight:bold;
    display:block;
    margin-bottom:5px;
}

.mo-ta {
    font-size:17px;
    line-height:1.6;
}

</style>

</asp:Content>
