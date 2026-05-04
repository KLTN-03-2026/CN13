<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KichHoatThanhVien.aspx.cs" Inherits="GPDH.KichHoatThanhVien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 style="margin-left:20px;">Kích hoạt / Vô hiệu hoá</h2>

    <!-- TÌM KIẾM -->
    <div class="search-box">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="input-search" placeholder="Nhập Họ Tên" />
        <asp:Button ID="btnSearch" runat="server" CssClass="btn-search" Text="Tìm kiếm"
            OnClick="btnSearch_Click" />
    </div>

    <!-- THÔNG BÁO -->
    <div style="text-align:center; margin-top:10px;">
        <asp:Label ID="lblNotify" runat="server" CssClass="notify-msg"></asp:Label>
    </div>

    <!-- BẢNG -->
    <div class="table-container">

        <asp:GridView ID="gvThanhVien" runat="server" AutoGenerateColumns="False"
    CssClass="grid"
    HeaderStyle-CssClass="grid-header"
    RowStyle-CssClass="grid-row"
    OnRowCommand="gvThanhVien_RowCommand"
    OnRowDataBound="gvThanhVien_RowDataBound"
    DataKeyNames="MaTaiKhoan">



            <Columns>

                <asp:TemplateField HeaderText="STT">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>

                <asp:BoundField DataField="HoTen" HeaderText="Họ Tên" />

                <asp:BoundField DataField="NgaySinh" HeaderText="Ngày sinh"
                    DataFormatString="{0:dd-MM-yyyy}" />

                <asp:BoundField DataField="TenDoi" HeaderText="Đời" />

                <asp:BoundField DataField="TenDangNhap" HeaderText="Tên Đăng nhập" />


                <asp:TemplateField HeaderText="Trạng Thái">
    <ItemTemplate>
        <asp:Label ID="lblTrangThai" runat="server"></asp:Label>
    </ItemTemplate>
</asp:TemplateField>


              <asp:TemplateField HeaderText="Hành Động">
    <ItemTemplate>
        <asp:LinkButton ID="btnDisable" runat="server"
            Text="Vô hiệu hoá"
            CssClass="btn-disable"
            CommandName="Disable"
            CommandArgument='<%# Container.DataItemIndex %>' />

        <asp:LinkButton ID="btnEnable" runat="server"
            Text="Kích hoạt"
            CssClass="btn-enable"
            CommandName="Enable"
            CommandArgument='<%# Container.DataItemIndex %>' />
    </ItemTemplate>
</asp:TemplateField>



            </Columns>

        </asp:GridView>

    </div>

    <!-- CSS -->
    <style>

/* KHUNG */
.table-container {
    background: #f5f5f5;
    padding: 25px;
    border-radius: 16px;
    box-shadow: 0 0 12px rgba(0,0,0,0.07);
    width: 90%;
    margin: auto;
}

/* GRID */
.grid { width: 100%; border-collapse: collapse !important; }

/* MÀU NỀN */
.grid-header th,
.grid-row td {
    background: #ececec !important;
    padding: 14px 12px;
    font-size: 15px;
}

/* HEADER */
.grid-header th {
    font-size: 16px;
    font-weight: 700;
}

/* KẺ NGANG */
.grid-row td { border-bottom: 1px solid #ddd !important; }

/* NÚT */
.btn-disable {
    background: #e63946;
    color: white;
    padding: 6px 14px;
    border-radius: 6px;
    font-weight: bold;
    text-decoration:none;
}
.btn-disable:hover { background: #c1121f; }

.btn-enable {
    background: #06d6a0;
    color: white;
    padding: 6px 14px;
    border-radius: 6px;
    font-weight: bold;
    text-decoration:none;
}
.btn-enable:hover { background: #04b383; }

/* SEARCH */
.input-search {
    width: 260px;
    padding: 10px 12px;
    border-radius: 8px;
    border: 1px solid #aaa;
}

.btn-search {
    background: #c89b3c;
    color: white;
    padding: 10px 25px;
    border-radius: 8px;
    font-weight: bold;
    border:none;
}

.notify-msg {
    font-size: 16px;
    font-weight:bold;
}

.search-box {
    display:flex;
    justify-content:center;
    margin-bottom:20px;
    gap:15px;
}

    </style>

</asp:Content>

