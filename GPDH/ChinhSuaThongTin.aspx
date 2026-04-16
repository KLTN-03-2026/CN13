<%@ Page Title="Chỉnh sửa thông tin cá nhân" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ChinhSuaThongTin.aspx.cs" Inherits="GPDH.ChinhSuaThongTin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <div class="title-wrapper">
    <div class="page-header">Chỉnh sửa thông tin cá nhân</div>
</div>


    <div class="edit-container">
        <div class="edit-box">

            <table class="form-table">

                <tr>
                    <td class="col-label"><label>Họ và Tên:</label></td>
                    <td colspan="3" class="col-value">
                        <asp:Label ID="lblHoTen" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="col-label"><label>Email:</label></td>
                    <td class="col-input">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>

                    <td class="col-label"><label>Số điện thoại:</label></td>
                    <td class="col-input">
                        <asp:TextBox ID="txtSoDT" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="col-label"><label>Nghề nghiệp:</label></td>
                    <td class="col-input">
                        <asp:TextBox ID="txtNgheNghiep" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>

                    <td class="col-label"><label>Địa chỉ:</label></td>
                    <td class="col-input">
                        <asp:TextBox ID="txtDiaChi" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="col-label"><label>Ghi chú:</label></td>
                    <td colspan="3" class="col-input">
                        <asp:TextBox ID="txtGhiChu" runat="server" TextMode="MultiLine" Rows="3" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td colspan="4" class="btn-row">
                        <asp:Button ID="btnLuu" runat="server" Text="Cập nhật" CssClass="btn-save" OnClick="btnLuu_Click" />
                        <asp:Button ID="btnHuy" runat="server" Text="Hủy" CssClass="btn-cancel" PostBackUrl="~/ThongTinCaNhan.aspx" />
                    </td>
                </tr>

            </table>

            <asp:Label ID="lblThongBao" runat="server" CssClass="message"></asp:Label>

        </div>
    </div>


    <!-- CSS CHUẨN GIỐNG MẪU -->
    <style>

        .page-header {
            font-size: 26px;
            font-weight: bold;
            color: #333;
            margin: 20px 0 10px 20px; /* LỆCH TRÁI */
        }

        .edit-container {
            display: flex;
            justify-content: center;
            margin-top: 10px;
        }

        .edit-box {
            background-color: #f3f3f3;
            width: 820px;
            padding: 30px 40px;
            border-radius: 18px;
            box-shadow: 0 3px 8px rgba(0,0,0,0.1);
        }

        .form-table {
            width: 100%;
            border-spacing: 18px;
        }

        .col-label {
            width: 140px;
            font-weight: 600;
        }

        .col-input {
            width: 240px;
        }

        .col-value {
            font-weight: bold;
        }

        .textbox {
            width: 100%;
            padding: 8px 12px;
            border: 1px solid #ccc;
            border-radius: 6px;
        }

        .btn-row {
            text-align: center;
            padding-top: 20px;
        }

        .btn-save {
            background-color: #c89b3c;
            color: white;
            padding: 10px 28px;
            border: none;
            border-radius: 10px;
            font-weight: bold;
            cursor: pointer;
            margin-right: 20px;
        }

        .btn-save:hover {
            background-color: #a67b23;
        }

        .btn-cancel {
            background-color: #e53935;
            color: white;
            padding: 10px 28px;
            border: none;
            border-radius: 10px;
            font-weight: bold;
            cursor: pointer;
        }

        .btn-cancel:hover {
            background-color: #c62828;
        }

        .message {
            text-align: center;
            margin-top: 10px;
            font-weight: 600;
        }
        .title-wrapper {
    text-align: left;
}


    </style>

</asp:Content>

