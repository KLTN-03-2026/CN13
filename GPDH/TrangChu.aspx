<%@ Page Title="Trang chủ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrangChu.aspx.cs" Inherits="GPDH.TrangChu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-home">
        <img src="Images/banner.png" alt="Banner Nhà thờ họ Nguyễn" class="banner-img" />

        <h1 class="slogan-title">Nơi lưu giữ, kết nối và phát huy truyền thống gia tộc</h1>

        <p class="intro-text">
            “Dòng họ Nguyễn Việt Nam có lịch sử lâu đời, với truyền thống hiếu học, đoàn kết và hướng thiện.
            Trang web này được xây dựng nhằm lưu giữ thông tin phả hệ, các nghi lễ, và giúp con cháu gần xa
            dễ dàng kết nối, tra cứu, và đóng góp thông tin cho dòng họ.”
        </p>

        <div class="image-section">
            <div class="image-card">
                <img src="Images/nha_tho.png" alt="Nhà thờ họ từ trên cao" />
                <p>Nhà thờ họ từ trên cao</p>
            </div>

            <div class="image-card">
                <img src="Images/ban_tho.png" alt="Không gian thờ tự chính điện" />
                <p>Không gian thờ tự chính điện</p>
            </div>
        </div>

        <footer class="footer">
            SĐT: 0123 456 789<br />
            Nhà thờ họ Nguyễn, Quận 5, TP. Đà Nẵng
        </footer>
    </div>
    <style>
        .main-home {
    min-height: calc(100vh - 150px); /* đẩy footer xuống đáy */
    display: flex;
    flex-direction: column;
    justify-content: space-between;
}

.footer {
    text-align: center;
    font-size: 14px;
    color: #777;
    padding: 10px 0;
    margin-top: 20px;
}

        .main-content {
    margin-left: 220px; /* vẫn né sidebar */
    padding-left: 0 !important; /* bỏ padding gây lệch */
    padding-right: 0 !important;
}


        .banner-img {
    width: 350px;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.2);
    display: block;
    margin: 0 auto; /* ← thêm dòng này */
}


        .intro-text {
            max-width: 700px;
            margin: 20px auto;
            color: #333;
            line-height: 1.6;
            font-size: 16px;
        }

        .image-section {
            display: flex;
            justify-content: center;
            gap: 120px;
            margin-top: 40px;
            flex-wrap: wrap;
        }

        .image-card img {
            width: 250px;
            height: 150px;
            border-radius: 6px;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
            transition: transform 0.2s;
        }

        .image-card img:hover {
            transform: scale(1.03);
        }

        .image-card p {
            margin-top: 8px;
            font-weight: 500;
            color: #654321;
        }

        

        @media (max-width: 768px) {
            .banner-img {
                width: 80%;
            }
            .image-section {
                gap: 50px;
            }
            .image-card img {
                width: 200px;
                height: 120px;
            }
        }
        .slogan-title {
    color: #b30000; /* đỏ đậm đẹp */
    font-weight: 700;
}

    </style>
</asp:Content>
