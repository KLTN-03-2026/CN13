<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThongKe.aspx.cs" Inherits="GPDH.ThongKe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Thống kê</h2>

    <!-- Tabs -->
    <div class="tab-container">
        <asp:LinkButton ID="btnDoTuoi" runat="server" CssClass="tab-btn active-tab" OnClick="btnDoTuoi_Click">Độ tuổi</asp:LinkButton>
        <asp:LinkButton ID="btnGioiTinh" runat="server" CssClass="tab-btn" OnClick="btnGioiTinh_Click">Giới tính</asp:LinkButton>
        <asp:LinkButton ID="btnPhai" runat="server" CssClass="tab-btn" OnClick="btnPhai_Click">Phái</asp:LinkButton>
        <asp:LinkButton ID="btnDoi" runat="server" CssClass="tab-btn" OnClick="btnDoi_Click">Đời</asp:LinkButton>
    </div>

    <!-- Chart -->
    <div class="chart-container">
        <canvas id="chartThongKe" width="600" height="400"></canvas>
    </div>

    <!-- Nhận xét -->
    <div class="nhan-xet">
        <asp:Label ID="lblNhanXet" runat="server" Text=""></asp:Label>
    </div>

    <!-- Chart.js -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

<script>
    function veBieuDo(labels, data, title) {
        const ctx = document.getElementById('chartThongKe').getContext('2d');
        if (window.myChart) window.myChart.destroy(); // clear old chart

        window.myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: 'rgba(200,155,60,0.8)',
                    borderColor: '#a57923',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: false },
                    title: { display: false } // ✅ không hiển thị "Độ tuổi" nhỏ giữa biểu đồ
                },
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: title, // vẫn có nhãn trục hoành như "Độ tuổi", "Phái"...
                            font: { size: 14, weight: 'bold' }
                        }
                    },
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Số lượng',
                            font: { size: 14, weight: 'bold' }
                        }
                    }
                },
                barThickness: 30
            }
        });
    }
</script>


    <style>
        h2 { text-align:left; margin-bottom:20px; }

        .tab-container { display:flex; gap:10px; margin-bottom:25px; }
        .tab-btn { padding:10px 25px; border:none; background:#e8e8e8; border-radius:20px;
                   font-weight:bold; cursor:pointer; text-decoration:none; color: black;  }
        .active-tab { background:#c89b3c; color:black; }

        .chart-container { display:flex; justify-content:center; align-items:center; margin-bottom:30px; height:420px; }

        .nhan-xet {
    width: 100%;                /* ✅ mở rộng ra toàn trang */
    display: flex;              /* ✅ dùng flex để căn giữa */
    justify-content: center;    /* ✅ căn giữa theo chiều ngang */
    margin-top: 20px;
}

.nhan-xet label {
    font-size: 16px;
    line-height: 1.6;
    text-align: center;         /* ✅ căn giữa chữ */
    max-width: 70%;             /* ✅ giữ độ rộng hợp lý để đọc đẹp */
}

    </style>
</asp:Content>
