<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GopY.aspx.cs" Inherits="GPDH.GopY" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Góp ý</h2>

<p class="gopy-intro">
    Rất mong quý thành viên đóng góp ý kiến để website dòng họ ngày càng hoàn thiện, tiện ích và gắn kết hơn.
    <br />
    Mọi ý kiến đều được ghi nhận, trân trọng và chuyển đến Ban Quản Trị để xem xét, cải thiện.
</p>

<div class="gopy-container">
    <asp:TextBox ID="txtNoiDung" runat="server" TextMode="MultiLine"
        CssClass="gopy-input" placeholder="Nội dung góp ý..." MaxLength="2000"></asp:TextBox>

    <div class="char-count" id="charCount">0 / 2000 ký tự</div>

    <asp:Button ID="btnGui" runat="server" Text="Gửi"
        CssClass="btn-gui" OnClick="btnGui_Click" />
</div>


    <!-- Popup -->
    <div id="popupOverlay" class="popup-overlay" style="display:none;"></div>

    <div id="popupNotify" class="popup-box" style="display:none;">
        <h3>✔ Gửi góp ý thành công!</h3>
        <p>Cảm ơn bạn đã đóng góp để website tốt hơn.</p>
        <button class="btn-close" onclick="closePopup()">Đóng</button>
    </div>

    <style>
           h2 {
    text-align: left;   
    width: 100%;
    margin-top: 10px;
    margin-bottom: 20px;
}

/* Đoạn giới thiệu căn trái */
.gopy-intro {
    text-align: left;
    width: 95%;
    margin-bottom: 15px;
    line-height: 1.5;
    font-size: 16px;
}

/* KHUNG chứa textarea + nút */
.gopy-container {
    margin-top: 10px;
    width: 95%;
}

/* Textarea */
.gopy-input {
    width: 100%;
    height: 260px;
    padding: 12px;
    border-radius: 10px;
    border: 1px solid #ccc;
    font-size: 16px;
    resize: none;
}

/* Nút gửi – căn phải */
.btn-gui {
    margin-top: 15px;
    float: right;        /* ⭐ Đưa nút sang phải */
    background: #c89b3c;
    padding: 10px 25px;
    border: none;
    border-radius: 8px;
    color: white;
    font-weight: bold;
    cursor: pointer;
}
.btn-gui:hover { background: #a57923; }

/* ===== Popup ===== */
.popup-overlay {
    position: fixed;
    top: 0; left: 0;
    width: 100%; height: 100%;
    background: rgba(0,0,0,0.45);
    z-index: 900;
}

.popup-box {
    position: fixed;
    top: 50%; left: 50%;
    transform: translate(-50%, -50%);
    background: white;
    padding: 25px 35px;
    border-radius: 12px;
    width: 350px;
    text-align: center;
    box-shadow: 0 5px 20px rgba(0,0,0,0.3);
    z-index: 1000;
}

.btn-close {
    margin-top: 15px;
    padding: 8px 20px;
    background: #c89b3c;
    color: white;
    border-radius: 6px;
    border: none;
    cursor: pointer;
}

.btn-close:hover { background: #a57923; }
    </style>

    <script>
        function showPopup() {
            document.getElementById("popupOverlay").style.display = "block";
            document.getElementById("popupNotify").style.display = "block";
        }

        function closePopup() {
            document.getElementById("popupOverlay").style.display = "none";
            document.getElementById("popupNotify").style.display = "none";
        }

    const textarea = document.getElementById('<%= txtNoiDung.ClientID %>');
    const counter = document.getElementById('charCount');

    textarea.addEventListener('input', () => {
        counter.textContent = `${textarea.value.length} / 2000 ký tự`;
    });
    </script>

</asp:Content>
