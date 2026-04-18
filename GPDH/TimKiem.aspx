<%@ Page Title="Tìm kiếm" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="TimKiem.aspx.cs"
    Inherits="GPDH.TimKiem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Tìm kiếm</h2>

    <!-- KHUNG TÌM KIẾM -->
    <div class="search-box">

        <asp:TextBox ID="txtHoTen" runat="server" CssClass="input-search"
            placeholder="Họ và Tên" />

        <asp:DropDownList ID="ddlDoi" runat="server" CssClass="filter-drop"></asp:DropDownList>

        <asp:DropDownList ID="ddlPhai" runat="server" CssClass="filter-drop"></asp:DropDownList>

        <asp:DropDownList ID="ddlChi" runat="server" CssClass="filter-drop"></asp:DropDownList>

        <asp:DropDownList ID="ddlGioiTinh" runat="server" CssClass="filter-drop">
            <asp:ListItem Value="">Giới tính</asp:ListItem>
            <asp:ListItem Value="Nam">Nam</asp:ListItem>
            <asp:ListItem Value="Nữ">Nữ</asp:ListItem>
        </asp:DropDownList>

        <asp:TextBox ID="txtNgaySinh" runat="server" TextMode="Date"
            CssClass="filter-drop" />

        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm"
            CssClass="btn-search" OnClick="btnSearch_Click" />

    </div>

    <!-- BẢNG KẾT QUẢ -->
    <div class="table-container">

        <asp:GridView ID="gvResult" runat="server"
            AutoGenerateColumns="False"
            CssClass="grid"
            HeaderStyle-CssClass="grid-header"
            RowStyle-CssClass="grid-row"
            OnRowCommand="gvResult_RowCommand">

            <Columns>

                <asp:TemplateField HeaderText="STT">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>

                <asp:BoundField DataField="HoTen" HeaderText="Họ Tên" />
                <asp:BoundField DataField="NgaySinh" HeaderText="Ngày Sinh"
                    DataFormatString="{0:dd-MM-yyyy}" />

                <asp:BoundField DataField="TenDoi" HeaderText="Đời" />
                <asp:BoundField DataField="TenPhai" HeaderText="Phái" />
                <asp:BoundField DataField="TenChi" HeaderText="Chi" />

                <asp:TemplateField HeaderText="">
                <ItemTemplate>
                 <asp:LinkButton ID="btnDetail" runat="server"
                  CommandName="Detail"
                 CommandArgument='<%# Eval("MaThanhVien") %>'
                     CssClass="btn-detail">Chi tiết</asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateField>


            </Columns>

        </asp:GridView>
        <!-- ===== NỀN MỜ ===== -->
<div id="popupOverlay" class="popup-overlay" style="display:none;"></div>

<!-- ===== POPUP CHI TIẾT ===== -->
<div id="popupDetail" class="popup-box" style="display:none;">

    <div class="popup-close" onclick="closePopup()">X</div>

    <h3>Thông tin chi tiết</h3>

    <div class="detail-content">

    <div class="detail-label">Họ và Tên :</div>
    <div class="detail-value" id="ctHoTen"></div>

    <div class="detail-label">Ngày sinh :</div>
    <div class="detail-value" id="ctNgaySinh"></div>

    <div class="detail-label">Ngày mất :</div>
    <div class="detail-value" id="ctNgayMat"></div>

    <div class="detail-label">Giới tính :</div>
    <div class="detail-value" id="ctGioiTinh"></div>

    <div class="detail-label">Đời :</div>
    <div class="detail-value" id="ctDoi"></div>

    <div class="detail-label">Phái :</div>
    <div class="detail-value" id="ctPhai"></div>

    <div class="detail-label">Chi :</div>
    <div class="detail-value" id="ctChi"></div>

    <div class="detail-label">Vai trò :</div>
    <div class="detail-value" id="ctVaiTro"></div>

    <div class="detail-label">Email :</div>
    <div class="detail-value" id="ctEmail"></div>

    <div class="detail-label">SĐT :</div>
    <div class="detail-value" id="ctSDT"></div>

    <div class="detail-label">Nghề nghiệp :</div>
    <div class="detail-value" id="ctNghe"></div>

    <div class="detail-label">Địa chỉ :</div>
    <div class="detail-value" id="ctDiaChi"></div>

    <div class="detail-label">Ghi chú :</div>
    <div class="detail-value" id="ctGhiChu"></div>

</div>

</div>


    </div>

    <!-- CSS -->
    <style>
        h2 {
    text-align: left;   
    width: 100%;
    margin-top: 10px;
    margin-bottom: 20px;
}

        /* Nền mờ */
/* Nền mờ */
.popup-overlay {
    position: fixed;
    top: 0; left: 0;
    width: 100%; height: 100%;
    background: rgba(0,0,0,0.45);
    z-index: 900;
}

/* Hộp popup */
.popup-box {
    position: fixed;
    top: 50%; left: 50%;
    transform: translate(-50%, -50%);
    background: #ffffff;
    padding: 25px 35px;
    border-radius: 12px;
    width: 500px;
    z-index: 1000;
    box-shadow: 0 5px 20px rgba(0,0,0,0.3);
}

/* Nút đóng */
.popup-close {
    position: absolute;
    top: 12px;
    right: 15px;
    font-size: 20px;
    font-weight: bold;
    cursor: pointer;
    color: red;
}

.popup-box h3 {
    text-align: center;
    margin-bottom: 20px;
    font-size: 22px;
    font-weight: bold;
}

/* ===== Căn 2 cột ===== */
.detail-content {
    display: grid;
    grid-template-columns: 160px auto;   /* Cột trái rộng 160px, cột phải tự co */
    row-gap: 8px;
    column-gap: 10px;
    font-size: 18px;
}

/* Cột trái (nhãn) */
.detail-label {
    text-align: right;
    font-weight: bold;
    padding-right: 10px;
}

/* Cột phải (giá trị) */
.detail-value {
    text-align: left;
}


/* KHUNG TÌM KIẾM – bỏ viền */
.search-box {
    display:flex;
    gap:12px;
    justify-content:center;
    padding:15px;
    margin-bottom:25px;
    border:none !important;        /* 🔥 Bỏ khung viền */
    box-shadow:none !important;    /* 🔥 Bỏ bóng */
    background:transparent;         /* 🔥 Cho sạch sẽ */
}

/* Input và dropdown */
.input-search, .filter-drop {
    padding:10px;
    border:1px solid #aaa;
    border-radius:6px;
}

/* NÚT TÌM KIẾM */
.btn-search {
    background:#c89b3c;
    color:white;
    padding:10px 25px;
    border-radius:8px;
    font-weight:bold;
    border:none;
}

/* BẢNG KẾT QUẢ */
.table-container {
    background:#f5f5f5;
    padding:20px;
    border-radius:20px;
    width:90%;
    margin:auto;
}

/* GRID – bỏ đường kẻ dọc */
.grid {
    width:100%;
    border-collapse:collapse !important;
}

.grid-header th,
.grid-row td {
    background:#ececec !important;
    padding:12px;
    border-right:none !important;   /* 🔥 Bỏ kẻ dọc */
    border-left:none !important;    /* 🔥 Bỏ kẻ dọc */
}

/* Chỉ giữ kẻ ngang */
.grid-row td {
    border-bottom:1px solid #ddd !important;
}

/* Nút chi tiết */
.btn-detail {
    background: #ffeb99;
    padding: 6px 16px;
    border-radius: 20px;
    font-weight: bold;
    text-decoration:none;
    color: black;
}
.btn-detail:hover {
    background: #ffdd55;
}


    </style>
    <script>
function openDetailPopup(data) {
    document.getElementById("popupOverlay").style.display = "block";
    document.getElementById("popupDetail").style.display = "block";

    // Đổ dữ liệu vào popup
    document.getElementById("ctHoTen").innerText = data.HoTen;
    document.getElementById("ctNgaySinh").innerText = data.NgaySinh;
    document.getElementById("ctNgayMat").innerText = data.NgayMat;
    document.getElementById("ctGioiTinh").innerText = data.GioiTinh;
    document.getElementById("ctDoi").innerText = data.Doi;
    document.getElementById("ctPhai").innerText = data.Phai;
    document.getElementById("ctChi").innerText = data.Chi;
    document.getElementById("ctVaiTro").innerText = data.VaiTro;
    document.getElementById("ctEmail").innerText = data.Email;
    document.getElementById("ctSDT").innerText = data.SDT;
    document.getElementById("ctNghe").innerText = data.Nghe;
    document.getElementById("ctDiaChi").innerText = data.DiaChi;
    document.getElementById("ctGhiChu").innerText = data.GhiChu;
}

function closePopup() {
    document.getElementById("popupOverlay").style.display = "none";
    document.getElementById("popupDetail").style.display = "none";
}
</script>

</asp:Content>
