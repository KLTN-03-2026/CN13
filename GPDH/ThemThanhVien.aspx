<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThemThanhVien.aspx.cs" Inherits="GPDH.ThemThanhVien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 style="text-align:left; margin-left:20px;">Thêm thành viên</h2>

    <div class="add-container">

        <div class="add-box">

            <!-- HÀNG 1: CHECKBOX DÂU/RỂ -->
            <div class="row-full">
                <asp:CheckBox ID="chkDauRe" runat="server"
                    AutoPostBack="true" OnCheckedChanged="chkDauRe_CheckedChanged" />
                <span style="margin-left:5px; font-weight:bold;">Là Dâu / Rể</span>
            </div>

            <!-- HÀNG 2–6: 10 TRƯỜNG CHIA 2 CỘT -->
            <div class="form-row">

                <div class="col">
                    <label>Họ Tên</label>
                    <asp:TextBox ID="txtHoTen" runat="server" CssClass="input" />

                    <label>Giới tính</label>
                    <asp:DropDownList ID="ddlGioiTinh" runat="server" CssClass="input"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlGioiTinh_SelectedIndexChanged">
                        <asp:ListItem Text="Nam" />
                        <asp:ListItem Text="Nữ" />
                    </asp:DropDownList>

                    <label>Ngày sinh</label>
                    <asp:TextBox ID="txtNgaySinh" runat="server" CssClass="input" TextMode="Date" />

                    <label>Ngày mất</label>
                    <asp:TextBox ID="txtNgayMat" runat="server" CssClass="input" TextMode="Date" />

                    <label>Đời</label>
                    <asp:DropDownList ID="ddlDoi" runat="server" CssClass="input"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlDoi_SelectedIndexChanged" />
                </div>

                <div class="col">
                    <label>Phái</label>
                    <asp:DropDownList ID="ddlPhai" runat="server" CssClass="input" />

                    <label>Chi</label>
                    <asp:DropDownList ID="ddlChi" runat="server" CssClass="input" />

                    <label>Tên Cha</label>
                    <asp:DropDownList ID="ddlTenCha" runat="server" CssClass="input" />

                    <label>Tên Vợ/Chồng</label>
                    <asp:DropDownList ID="ddlVoChong" runat="server" CssClass="input" />

                    <label>Vai trò trong họ</label>
                    <asp:DropDownList ID="ddlVaiTro" runat="server" CssClass="input">
                        <asp:ListItem Text="Trưởng họ" Value="Trưởng họ" />
                        <asp:ListItem Text="Trưởng Phái" Value="Trưởng Phái" />
                        <asp:ListItem Text="Trưởng Chi" Value="Trưởng Chi" />
                        <asp:ListItem Text="Thành viên" Value="Thành viên" />
                    </asp:DropDownList>
                </div>

            </div>

            <!-- NÚT -->
            <div style="text-align:center; margin-top:20px;">
                <asp:Button ID="btnSave" runat="server" Text="Thêm"
                    CssClass="btn-add" OnClick="btnSave_Click" />
            </div>
            <div style="text-align:center; margin-top:10px;">
    <asp:Label ID="lblMessage" runat="server" CssClass="notify"></asp:Label>
</div>

        </div>

    </div>

    <!-- CSS -->
    <style>
        .notify {
    font-weight: bold;
    font-size: 15px;
    margin-top: 10px;
    display: block;
}

        .add-container {
            width: 100%;
            display: flex;
            justify-content: center;
        }

        .add-box {
            background: #f7e6b5;
            padding: 25px 35px;
            border-radius: 12px;
            width: 650px;
            box-shadow: 0 0 8px rgba(0,0,0,0.15);
        }

        /* HÀNG 1 – full width */
        .row-full {
            width: 100%;
            display: flex;
            align-items: center;
            margin-bottom: 15px;
            padding-bottom: 5px;
            border-bottom: 1px solid #d0b46a;
        }

        /* HÀNG 2–6 */
        .form-row {
            display: flex;
            flex-direction: row;
            justify-content: space-between;
        }

        .col {
            width: 48%;
            display: flex;
            flex-direction: column;
        }

        label {
            font-weight: bold;
            margin-top: 10px;
            margin-bottom: 3px;
        }

        .input {
            width: 100%;
            padding: 7px;
            border: 1px solid #888;
            border-radius: 5px;
        }
        .input {
    width: 100% !important;
    height: 40px !important;
    padding: 5px 10px !important;
    border: 1px solid #888;
    border-radius: 6px;
    font-size: 15px;
    box-sizing: border-box; /* để width 100% chính xác */
    background-color: #fff;
}

        .btn-add {
            background: #c89b3c;
            color: white;
            border: none;
            padding: 10px 25px;
            border-radius: 8px;
            cursor: pointer;
            margin-right: 10px;
            font-weight: bold;
        }

        .btn-add:hover {
            background: #a57923;
        }
        h2 {
    text-align: left;
    width: 100%;
    margin-top: 10px;
    margin-bottom: 20px;
}


    </style>

</asp:Content>