<%@ Page Title="Đổi mật khẩu" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="DoiMatKhau.aspx.cs" Inherits="GPDH.DoiMatKhau" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Đổi mật khẩu</h1>

    <div class="form-container">
        <table class="form-table">
            <tr>
                <td><label for="txtMatKhauCu">Mật khẩu cũ:</label></td>
                <td><asp:TextBox ID="txtMatKhauCu" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox></td>
            </tr>
            <tr>
                <td><label for="txtMatKhauMoi">Mật khẩu mới:</label></td>
                <td><asp:TextBox ID="txtMatKhauMoi" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox></td>
            </tr>
            <tr>
                <td><label for="txtNhapLai">Nhập lại mật khẩu mới:</label></td>
                <td><asp:TextBox ID="txtNhapLai" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="btnDoiMatKhau" runat="server" Text="Đổi mật khẩu" CssClass="btn-doimatkhau" OnClick="btnDoiMatKhau_Click" />
                </td>
            </tr>
        </table>

        <asp:Label ID="lblThongBao" runat="server" CssClass="message"></asp:Label>
    </div>

    <style>
        .form-container {
            background-color: #f8e7a0;
            border-radius: 10px;
            width: 420px;
            margin: 40px auto;
            padding: 30px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.1);
        }
        .form-table {
            width: 100%;
            border-spacing: 10px;
        }
        label {
            font-weight: 600;
            color: #333;
        }
        .textbox {
            width: 100%;
            padding: 6px 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .btn-doimatkhau {
            background-color: #c89b3c;
            color: white;
            padding: 8px 16px;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            font-weight: bold;
            transition: 0.3s;
        }
        .btn-doimatkhau:hover {
            background-color: #a57923;
        }
        .message {
            display: block;
            text-align: center;
            margin-top: 10px;
            font-weight: bold;
        }
        h1 { text-align:left; margin-top:10px; margin-bottom:20px; color: black; }
    </style>
</asp:Content>
