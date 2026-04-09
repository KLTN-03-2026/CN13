<%@ Page Title="Đăng nhập"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="DangNhap.aspx.cs"
    Inherits="GPDH.DangNhap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="login-page">

        <div class="login-header">Đăng nhập</div>

        <div class="login-box">
            <table>
                <tr>
                    <td><label for="txtUser">Tên đăng nhập:</label></td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" CssClass="input-text" placeholder="Nhập tên đăng nhập..." />
                    </td>
                </tr>
                <tr>
                    <td><label for="txtPass">Mật khẩu:</label></td>
                    <td>
                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="input-text" placeholder="Nhập mật khẩu..." />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnLogin" runat="server"
                            Text="Đăng nhập"
                            CssClass="btn-login"
                            OnClick="btnLogin_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <style>

        /* Tiêu đề giống trang Chỉnh sửa thông tin cá nhân */
        .login-header {
            font-size: 26px;
            font-weight: bold;
            color: #333;
            margin: 20px 0 10px 20px;
            text-align: left;
        }

        .login-page {
            margin-top: 20px;
            position: relative;
        }

        .login-box {
            background-color: #f8e7a0;
            display: block;
            width: fit-content;
            margin: 0 auto;
            padding: 25px 40px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.25);
        }

        .login-box table {
            margin: 0 auto;
        }

        label {
            font-weight: 600;
            color: #333;
        }

        .input-text {
            width: 200px;
            padding: 6px 8px;
            border: 1px solid #aaa;
            border-radius: 5px;
        }

        .btn-login {
            background-color: #c89b3c;
            color: white;
            padding: 8px 20px;
            border: none;
            border-radius: 5px;
            font-weight: bold;
            cursor: pointer;
            margin-top: 10px;
        }

        .btn-login:hover {
            background-color: #a57923;
        }

        @media (max-width: 600px) {
            .login-header {
                text-align: center;
                margin-left: 0;
            }
        }

    </style>
</asp:Content>
