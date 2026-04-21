<%@ Page Title="Lịch sử dòng họ" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="LichSu.aspx.cs"
    Inherits="GPDH.LichSu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Lịch sử dòng họ</h2>

    <div class="history-box">
        <asp:Literal ID="ltNoiDung" runat="server"></asp:Literal>
    </div>

    <style>
        h2 {
    text-align: left;  
    width: 100%;
    margin-top: 10px;
    margin-bottom: 20px;
}

        /* Khung nội dung */
        .history-box {
            background: #f5f5f5;
            padding: 25px;
            width: 95%;
            margin: auto;
            border-radius: 16px;
            font-size: 17px;
            line-height: 1.7;
            text-align: left;
            color: #333;
            box-shadow: 0 0 10px rgba(0,0,0,0.05);
        }
    </style>

</asp:Content>
