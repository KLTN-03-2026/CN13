<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HoiDongGiaToc.aspx.cs" Inherits="GPDH.HoiDongGiaToc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 style="margin-left:20px;">Hội đồng gia tộc</h2>

    <div class="table-container">

        <asp:GridView ID="gvHoiDong" runat="server" AutoGenerateColumns="False"
            CssClass="grid"
            HeaderStyle-CssClass="grid-header"
            RowStyle-CssClass="grid-row">

            <Columns>

                <asp:TemplateField HeaderText="STT">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>

                <asp:BoundField DataField="HoTen" HeaderText="Họ Tên" />
                <asp:BoundField DataField="NgaySinh" HeaderText="Ngày Sinh" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="TenDoi" HeaderText="Đời" />
                <asp:BoundField DataField="TenPhai" HeaderText="Phái" />
                <asp:BoundField DataField="TenChi" HeaderText="Chi" />
                <asp:BoundField DataField="VaiTroTrongHo" HeaderText="Chức vụ" />

            </Columns>

        </asp:GridView>

    </div>

    <!-- CSS -->
    <style>

.table-container {
    background: #f5f5f5;
    padding: 25px;
    border-radius: 16px;
    box-shadow: 0 0 12px rgba(0,0,0,0.07);
    width: 90%;
    margin: auto;
}

/* GRID */
.grid {
    width: 100%;
    border-collapse: collapse !important;
}

/* Header + dòng */
.grid-header th,
.grid-row td {
    background: #ececec !important;
    padding: 14px 12px;
    font-size: 15px;
    border-left: none !important;
    border-right: none !important;
}

.grid-header th {
    font-weight: 700;
    font-size: 16px;
}

.grid-row td {
    border-bottom: 1px solid #ddd !important;
}
h2 {
    text-align: left;  
    width: 100%;
    margin-top: 10px;
    margin-bottom: 20px;
}

    </style>

</asp:Content>

