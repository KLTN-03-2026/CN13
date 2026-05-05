<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="XemGopY.aspx.cs" Inherits="GPDH.XemGopY" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2>Góp ý</h2>

<!-- KHUNG TÌM KIẾM -->
<div class="search-box">
    <asp:TextBox ID="txtHoTen" runat="server" CssClass="input-search" placeholder="Họ và tên" />
    <asp:TextBox ID="txtNgayGopY" runat="server" TextMode="Date" CssClass="filter-drop" />
    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn-search" OnClick="btnSearch_Click" />
</div>

<!-- BẢNG GÓP Ý -->
<div class="table-container">
    <asp:GridView ID="gvGopY" runat="server" AutoGenerateColumns="False"
        CssClass="grid" HeaderStyle-CssClass="grid-header"
        RowStyle-CssClass="grid-row" OnRowCommand="gvGopY_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="STT">
                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                <HeaderStyle Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="HoTen" HeaderText="Họ Tên" />
            <asp:BoundField DataField="NgayGopY" HeaderText="Ngày Góp Ý" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="NoiDungRutGon" HeaderText="Nội dung" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnDetail" runat="server" 
                        CommandName="Detail" 
                        CommandArgument='<%# Eval("MaGopY") %>'
                        CssClass="btn-detail">Xem chi tiết</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<!-- POPUP CHI TIẾT -->
<div id="popupOverlay" class="popup-overlay" style="display:none;"></div>
<div id="popupDetail" class="popup-box" style="display:none;">
    <div class="popup-close" onclick="closePopup()">X</div>
    <h3>Chi tiết góp ý</h3>
    <div class="detail-content">
        <div class="detail-label">Họ và tên:</div>
        <div class="detail-value" id="ctHoTen"></div>

        <div class="detail-label">Ngày góp ý:</div>
        <div class="detail-value" id="ctNgayGopY"></div>

        <div class="detail-label">Nội dung:</div>
        <div class="detail-value" id="ctNoiDung"></div>
    </div>
</div>

<style>
/* giống style trang Tìm kiếm */
h2 { margin: 10px 0 20px 0; text-align: left; }

.search-box {
    display:flex; gap:12px; justify-content:center;
    padding:15px; margin-bottom:25px;
}

.input-search, .filter-drop {
    padding:10px; border:1px solid #aaa; border-radius:6px;
}

.btn-search {
    background:#c89b3c; color:white; padding:10px 25px;
    border-radius:8px; font-weight:bold; border:none;
}

.table-container {
    background:#f5f5f5; padding:20px; border-radius:20px;
    width:90%; margin:auto;
}

.grid { width:100%; border-collapse:collapse !important; }

.grid-header th, .grid-row td {
    background:#ececec !important;
    padding:12px; border-left:none !important; border-right:none !important;
}
.grid-row td { border-bottom:1px solid #ddd !important; }

.btn-detail {
    background: #e8d1a3;
    padding: 6px 16px; border-radius: 20px;
    font-weight: bold; color: black; text-decoration:none;
}
.btn-detail:hover { background: #c89b3c; color:white; }

/* popup */
.popup-overlay {
    position:fixed; top:0; left:0; width:100%; height:100%;
    background:rgba(0,0,0,0.45); z-index:900;
}
.popup-box {
    position:fixed; top:50%; left:50%;
    transform:translate(-50%,-50%);
    background:#fff; padding:25px 35px; border-radius:12px;
    width:500px; z-index:1000; box-shadow:0 5px 20px rgba(0,0,0,0.3);
}
.popup-close {
    position:absolute; top:12px; right:15px; font-size:20px;
    font-weight:bold; cursor:pointer; color:red;
}
.detail-content {
    display:grid; grid-template-columns:120px auto; row-gap:8px;
}
.detail-label { font-weight:bold; text-align:right; padding-right:10px; }
.detail-value { text-align:left; }
</style>

<script>
function openDetailPopup(data){
    document.getElementById("popupOverlay").style.display="block";
    document.getElementById("popupDetail").style.display="block";
    document.getElementById("ctHoTen").innerText=data.HoTen;
    document.getElementById("ctNgayGopY").innerText=data.NgayGopY;
    document.getElementById("ctNoiDung").innerText=data.NoiDung;
}
function closePopup(){
    document.getElementById("popupOverlay").style.display="none";
    document.getElementById("popupDetail").style.display="none";
}
</script>

</asp:Content>