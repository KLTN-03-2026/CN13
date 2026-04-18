<%@ Page Title="Cây Gia Phả" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="CayGiaPha.aspx.cs"
    Inherits="GPDH.CayGiaPha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Banner cố định, canh theo phần content -->
    <div class="banner-fixed">
        <img src="images/banner.png" alt="Banner Gia Phả" />
    </div>

    <!-- Cây gia phả -->
    <div class="tree-container">
        <div class="tree">
            <asp:Literal ID="ltCayGiaPha" runat="server"></asp:Literal>
        </div>
    </div>

    <style>
        /* === Bỏ padding mặc định Master === */
        .main-content {
            padding: 0 !important;
            margin: 0 !important;
            background: #fff !important;
        }

        /* === Banner cố định, nằm bên phải sidebar === */
        .banner-fixed {
    position: fixed;
    top: 0;
    left: 250px;    
    width: calc(100% - 250px);
    height: 110px;
    background: #fff;
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;

}


        .banner-fixed img {
            height: 90px;
            width: auto;
            object-fit: contain;
        }

        /* === Khung cây gia phả === */
        .tree-container {
    margin-top: 110px;
    padding: 10px 0 80px;
    overflow-x: auto;
    overflow-y: visible;
    width: 100%;
    background: #fff;
    box-sizing: border-box;
}

        .tree {
            display: block;
            text-align: center;
            margin: 0 auto;
            white-space: normal;
        }

        /* === Cấu trúc cây === */
        .tree ul {
            position: relative;
            padding-top: 20px;
            display: table;
            margin: 0 auto;
            text-align: center;
        }

        .tree li {
            display: table-cell;
            vertical-align: top;
            position: relative;
            padding: 40px 20px 0;
        }

        .tree li::before,
        .tree li::after {
            content: '';
            position: absolute;
            top: 0;
            border-top: 2px solid #b89532;
            width: 50%;
            height: 0;
        }

        .tree li::before { right: 50%; }
        .tree li::after  { left: 50%; }
        .tree li:first-child::before,
        .tree li:last-child::after { border: none; }

        .tree ul ul::before {
            content: '';
            position: absolute;
            top: 0;
            left: 50%;
            transform: translateX(-1px);
            border-left: 2px solid #b89532;
            height: 20px;
        }

        .tree li > a::before {
            content: '';
            position: absolute;
            top: -40px;
            left: 50%;
            transform: translateX(-50%);
            border-left: 2px solid #b89532;
            height: 40px;
        }

        /* === Nút thành viên === */
        .tree a {
            background: #ffef9f;
            border-radius: 8px;
            box-shadow: 0 1px 4px rgba(0,0,0,0.15);
            display: inline-block;
            padding: 10px 18px;
            text-decoration: none;
            color: #000;
            font-weight: 500;
            min-width: 200px;
            position: relative;
            z-index: 2;
            transition: transform 0.15s ease, background-color 0.15s ease;
        }

        .tree a:hover {
            background: #ffe97a;
            transform: scale(1.02);
        }

        .tree a .name {
            font-weight: 700;
            color: #6b3f00;
        }

        .tree a p {
            margin: 3px 0;
            font-size: 14px;
            color: #333;
        }

        .tree a .meta {
            font-size: 12px;
            color: #555;
        }

        /* === Trường hợp chỉ có 1 con === */
        .tree li:only-child {
            padding-top: 0;
        }

        .tree li:only-child::before,
        .tree li:only-child::after,
        .tree li:only-child > a::before {
            display: none;
        }

        /* === Responsive === */
        @media (max-width: 900px) {
            .banner-fixed {
                left: 200px; /* nếu sidebar nhỏ hơn */
                width: calc(100% - 200px);
                height: 90px;
            }

            .banner-fixed img {
                height: 70px;
            }

            .tree a {
                min-width: 140px;
                padding: 8px 12px;
                font-size: 13px;
            }
        }
    </style>

    

</asp:Content>
