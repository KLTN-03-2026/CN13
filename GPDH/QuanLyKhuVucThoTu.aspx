<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuanLyKhuVucThoTu.aspx.cs" Inherits="GPDH.QuanLyKhuVucThoTu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Khu vực thờ tự</h2>
    <div class="action-bar">
    <button type="button" class="btn-add" onclick="openPopup('add')">Thêm</button>
    <button type="button" class="btn-edit" onclick="openPopup('edit')">Sửa</button>
    <button type="button" class="btn-delete" onclick="openDeletePopup()">Xóa</button>
</div>

    <!-- TAB KHU VỰC (TẠO ĐỘNG) -->
    <div class="tab-container" id="tabContainer" runat="server"></div>

    <!-- HIỂN THỊ THÔNG TIN -->
    <div class="content-box">

        <!-- ẢNH -->
        <asp:Image ID="imgKhuVuc" runat="server" CssClass="khuvuc-img" />

        <!-- ĐỊA CHỈ + NGƯỜI PHỤ TRÁCH -->
        <div class="info">
            <asp:Label ID="lblDiaChi" runat="server" CssClass="info-text"></asp:Label><br />
            <asp:Label ID="lblNguoiPhuTrach" runat="server" CssClass="info-text"></asp:Label>
        </div>

        <!-- MÔ TẢ -->
        <div class="mo-ta-box">
            <asp:Label ID="lblMoTa" runat="server" CssClass="mo-ta"></asp:Label>
        </div>

    </div>

<style>

h2 { text-align:left; margin-top:10px; margin-bottom:20px; }

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
    color:black;
    text-decoration:none;
}

.tab-btn:hover { background:#d4d4d4; }
.active-tab { background:#c89b3c !important; color:white !important; }

/* ----- PHẦN HIỂN THỊ ----- */
.content-box {
    width:90%;
    margin:auto;
    text-align:left;
}

.khuvuc-img {
    width:500px;
    border-radius:12px;
    margin:20px auto;
    display:block;
}

.info-text {
    font-size:18px;
    font-weight:bold;
    display:block;
    margin-bottom:5px;
}

.mo-ta {
    font-size:17px;
    line-height:1.6;
}
.action-bar { text-align:right; margin-bottom:15px; }

.btn-add {
    background:#007bff; color:white; padding:8px 16px; border:none;
    border-radius:6px; cursor:pointer; margin-left:10px;
}
.btn-edit {
    background:#f0ad4e; color:white; padding:8px 16px; border:none;
    border-radius:6px; cursor:pointer; margin-left:10px;
}
.btn-delete {
    background:#d9534f; color:white; padding:8px 16px; border:none;
    border-radius:6px; cursor:pointer; margin-left:10px;
}

.popup {
    display:none;
    position:fixed;
    top:0; left:0;
    width:100%; height:100%;
    background:rgba(0,0,0,0.5);
}
.popup-content {
    background:white;
    width:420px;
    padding:20px;
    border-radius:12px;
    margin:80px auto;
}
.textbox {
    width:100%;
    padding:8px;
    margin-bottom:10px;
}
.btn-save {
    background:#4CAF50; color:white; padding:8px 16px; border:none; border-radius:6px;
}
.btn-cancel {
    background:#777; color:white; padding:8px 16px; border:none; border-radius:6px;
}

</style>
    <!-- POPUP THÊM / SỬA -->
<div id="popupForm" class="popup">
    <div class="popup-content">

        <h3 id="popupTitle"></h3>

        <asp:HiddenField ID="hfID" runat="server" />

        <label>Tên khu vực:</label>
        <asp:TextBox ID="txtTen" CssClass="textbox" runat="server"></asp:TextBox><br />

        <label>Địa chỉ:</label>
        <asp:TextBox ID="txtDiaChi" CssClass="textbox" runat="server"></asp:TextBox><br />

        <label>Người phụ trách (Mã thành viên):</label>
        <asp:TextBox ID="txtNguoiPhuTrach" CssClass="textbox" runat="server"></asp:TextBox><br />

        <label>Mô tả:</label>
        <asp:TextBox ID="txtMoTa" TextMode="MultiLine" Rows="5" CssClass="textbox" runat="server"></asp:TextBox><br />

        <label>Hình ảnh:</label><br />
        <asp:FileUpload ID="fileAnh" runat="server" /><br /><br />

        <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-save" OnClick="btnSave_Click" />
        <button type="button" class="btn-cancel" onclick="closePopup()">Hủy</button>

    </div>

</div>
    <div id="popupDelete" class="popup">
    <div class="popup-content">
        <h3>Bạn chắc chắn muốn xóa khu vực này?</h3>

        <asp:Button ID="btnDeleteConfirm" runat="server" Text="Xóa" CssClass="btn-delete"
                    OnClick="btnDeleteConfirm_Click" />

        <button type="button" class="btn-cancel" onclick="closeDeletePopup()">Hủy</button>
    </div>
</div>
    <script>
    function openPopup(mode) {
        document.getElementById("popupForm").style.display = "block";

        if (mode === "add") {
            document.getElementById("popupTitle").innerText = "Thêm khu vực thờ tự";
            document.getElementById("<%= hfID.ClientID %>").value = "";
            document.getElementById("<%= txtTen.ClientID %>").value = "";
            document.getElementById("<%= txtDiaChi.ClientID %>").value = "";
            document.getElementById("<%= txtNguoiPhuTrach.ClientID %>").value = "";
            document.getElementById("<%= txtMoTa.ClientID %>").value = "";
        }
        else {
            document.getElementById("popupTitle").innerText = "Sửa khu vực thờ tự";
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
