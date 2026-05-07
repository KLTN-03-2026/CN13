<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuanLyNghiLe.aspx.cs" Inherits="GPDH.QuanLyNghiLe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Nghi lễ</h2>
    <!-- QUẢN LÝ NGHI LỄ -->
<div class="action-bar">
    <button type="button" class="btn-add" onclick="openPopup('add')">Thêm</button>
    <button type="button" class="btn-edit" onclick="openPopup('edit')">Sửa</button>
    <button type="button" class="btn-delete" onclick="openDeletePopup()">Xóa</button>
</div>


    <!-- TAB động -->
    <div class="tab-container" id="tabContainer" runat="server"></div>

    <!-- NỘI DUNG -->
    <div class="content-box">

        <div class="count-box">
            <asp:Label ID="lblSoNgay" runat="server"></asp:Label><br />
            <span>ngày</span>
        </div>

        <div class="info-text">
             <asp:Label ID="lblNgayToChuc" runat="server"></asp:Label><br />
             <asp:Label ID="lblDiaDiem" runat="server"></asp:Label><br />
             <asp:Label ID="lblDiaDiemDuPhong" runat="server"></asp:Label><br />
             <asp:Label ID="lblMoTa" runat="server"></asp:Label>
        </div>


    </div>

<style>
h2 { text-align:left; margin-bottom:20px; }

.tab-container {
    display:flex;
    gap:15px;
    margin-bottom:25px;
}

.tab-btn {
    padding:10px 25px;
    background:#e8e8e8;
    border-radius:20px;
    border:none;
    cursor:pointer;
    font-weight:bold;
    text-decoration:none;
    color:black;
}

.tab-btn:hover { background:#d4d4d4; }

.active-tab {
    background:#c89b3c !important;
    color:white !important;
}

.content-box { width:90%; margin-left:20px; position:relative; }

.count-box {
    background:#f2b21b;
    width:120px;
    height:120px;
    border-radius:100%;
    text-align:center;
    padding-top:25px;
    font-size:24px;
    font-weight:bold;
    float:right;
}

.info-text {
    text-align:left;
    font-size:18px;
    line-height:1.7;
    width:75%;
}
.action-bar {
    text-align:right;
    margin-bottom:15px;
}
/* Nút THÊM – Xanh dương */
.btn-add {
    padding: 8px 16px;
    background: #007bff;
    border: none;
    color: white;
    border-radius: 6px;
    cursor: pointer;
    margin-left: 10px;
}
.btn-add:hover {
    background: #0069d9;
}

/* Nút SỬA – Vàng */
.btn-edit {
    padding: 8px 16px;
    background: #f0ad4e;
    border: none;
    color: white;
    border-radius: 6px;
    cursor: pointer;
    margin-left: 10px;
}
.btn-edit:hover {
    background: #ec9a26;
}

/* Nút XÓA – Đỏ */
.btn-delete {
    padding: 8px 16px;
    background: #d9534f;
    border: none;
    color: white;
    border-radius: 6px;
    cursor: pointer;
    margin-left: 10px;
}
.btn-delete:hover {
    background: #c9302c;
}

.popup {
    display:none;
    position:fixed;
    top:0; left:0;
    width:100%; height:100%;
    background:rgba(0,0,0,0.4);
}
.popup-content {
    background:white;
    width:420px;
    padding:20px;
    margin:100px auto;
    border-radius:12px;
}
.textbox {
    width:100%;
    padding:8px; 
    margin-bottom:10px;
}
.btn-save {
    background:#4CAF50;
    color:white;
    padding:8px 16px;
    border:none;
    border-radius:6px;
}
.btn-cancel {
    background:#777;
    color:white;
    padding:8px 16px;
    border:none;
    border-radius:6px;
}

</style>
    <!-- POPUP XÓA -->
<div id="popupDelete" class="popup">
  <div class="popup-content">
      <h3>Bạn chắc chắn muốn xóa nghi lễ này?</h3>

      <asp:Button ID="btnDeleteConfirm" runat="server" Text="Xóa"
                   CssClass="btn-save" OnClick="btnDeleteConfirm_Click" />

      <button type="button" class="btn-cancel" onclick="closeDeletePopup()">Hủy</button>
  </div>
</div>

    <!-- POPUP THÊM / SỬA -->
<div id="popupForm" class="popup">
  <div class="popup-content">
      <h3 id="popupTitle"></h3>

      <asp:HiddenField ID="hfID" runat="server" />

      <label>Tên nghi lễ:</label>
      <asp:TextBox ID="txtTen" CssClass="textbox" runat="server"></asp:TextBox><br />

      <label>Ngày tổ chức (dd/MM âm lịch):</label>
      <asp:TextBox ID="txtNgay" CssClass="textbox" runat="server"></asp:TextBox><br />

      <!-- ✅ THÊM LABEL BÁO LỖI -->
      <asp:Label ID="lblPopupMessage" runat="server" ForeColor="Red"
          Style="font-weight:bold; display:block; margin-bottom:10px;"></asp:Label>

      <label>Địa điểm:</label>
      <asp:TextBox ID="txtDiaDiem" CssClass="textbox" runat="server"></asp:TextBox><br />

      <label>Địa điểm dự phòng:</label>
      <asp:TextBox ID="txtDiaDiemDP" CssClass="textbox" runat="server"></asp:TextBox><br />

      <label>Mô tả:</label>
      <asp:TextBox ID="txtMoTa" TextMode="MultiLine" Rows="5" CssClass="textbox" runat="server"></asp:TextBox><br />

      <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-save" OnClick="btnSave_Click" />
      <button type="button" class="btn-cancel" onclick="closePopup()">Hủy</button>
  </div>
</div>
    <script>
    function openPopup(mode) {
        document.getElementById("popupForm").style.display = "block";

        if (mode === "add") {
            document.getElementById("popupTitle").innerText = "Thêm nghi lễ";
            document.getElementById("<%= hfID.ClientID %>").value = "";
            document.getElementById("<%= txtTen.ClientID %>").value = "";
            document.getElementById("<%= txtNgay.ClientID %>").value = "";
            document.getElementById("<%= txtDiaDiem.ClientID %>").value = "";
            document.getElementById("<%= txtDiaDiemDP.ClientID %>").value = "";
            document.getElementById("<%= txtMoTa.ClientID %>").value = "";
        }
        else {
            document.getElementById("popupTitle").innerText = "Sửa nghi lễ";
        }
    }

    function closePopup() {
        document.getElementById("popupForm").style.display = "none";
    }

    function openDeletePopup() {
        document.getElementById("popupDelete").style.display = "block";
    }

    function closeDeletePopup() {
        document.getElementById("popupDelete").style.display = "none";
    }
</script>

</asp:Content>
