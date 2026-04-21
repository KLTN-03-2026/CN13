<%@ Page Title="Nghi lễ" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="NghiLe.aspx.cs"
    Inherits="GPDH.NghiLe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Nghi lễ</h2>

    <!-- TAB động -->
    <div class="tab-container" id="tabContainer" runat="server"></div>

    <!-- NỘI DUNG -->
    <div class="content-box">

        <div class="count-box">
            <asp:Label ID="lblSoNgay" runat="server"></asp:Label><br />
            <span>ngày</span>
        </div>

        <div class="info-text">
             <asp:Label ID="lblNgayToChuc" runat="server"></asp:Label><br />
             <asp:Label ID="lblDiaDiem" runat="server"></asp:Label><br />
             <asp:Label ID="lblDiaDiemDuPhong" runat="server"></asp:Label><br />
                <asp:Label ID="lblMoTa" runat="server"></asp:Label>
        </div>


    </div>

<style>
h2 { text-align:left; margin-bottom:20px; }

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
    text-decoration:none;
    color:black;
}

.tab-btn:hover { background:#d4d4d4; }

.active-tab {
    background:#c89b3c !important;
    color:white !important;
}

.content-box { width:90%; margin-left:20px; position:relative; }

.count-box {
    background:#f2b21b;
    width:120px;
    height:120px;
    border-radius:100%;
    text-align:center;
    padding-top:25px;
    font-size:24px;
    font-weight:bold;
    float:right;
}

.info-text {
    text-align:left;
    font-size:18px;
    line-height:1.7;
    width:75%;
}
</style>

</asp:Content>
