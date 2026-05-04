<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChinhSuaThanhVien.aspx.cs" Inherits="GPDH.ChinhSuaThanhVien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 style="text-align:left; margin-left:20px;">Chỉnh sửa/Xoá</h2>

    <!-- TÌM KIẾM -->
    <div class="search-box">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="input-search" placeholder="Nhập Họ Tên" />
        <asp:Button ID="btnSearch" runat="server" CssClass="btn-search" Text="Tìm kiếm"
            OnClick="btnSearch_Click" />
    <!--Thông báo-->
    </div>
    <div style="text-align:center; margin-top:10px;">
    <asp:Label ID="lblNotify" runat="server" CssClass="notify-msg"></asp:Label>
</div>
    <!-- BẢNG -->
    <div class="table-container">
        <asp:GridView ID="gvThanhVien" runat="server" AutoGenerateColumns="False"
            CssClass="grid"
            OnRowCommand="gvThanhVien_RowCommand"
            HeaderStyle-CssClass="grid-header"
            RowStyle-CssClass="grid-row">

            <Columns>
                <asp:TemplateField HeaderText="STT">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>

                <asp:BoundField DataField="HoTen" HeaderText="Họ Tên" />

                <asp:BoundField DataField="NgaySinh" HeaderText="Ngày Sinh" DataFormatString="{0:dd-MM-yyyy}" />

                <asp:BoundField DataField="TenDoi" HeaderText="Đời" />

                <asp:BoundField DataField="TenPhai" HeaderText="Phái" />

                <asp:BoundField DataField="TenChi" HeaderText="Chi" />

                <asp:TemplateField HeaderText="Thao tác">
                    <ItemTemplate>

                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditRow"
                            CommandArgument='<%# Eval("MaThanhVien") %>'
                            CssClass="btn-edit">Chỉnh sửa</asp:LinkButton>

                        &nbsp;

                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteRow"
                            CommandArgument='<%# Eval("MaThanhVien") %>'
                            CssClass="btn-delete">Xoá</asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <!-- ================= POPUP NỀN ================= -->
<div id="popupOverlay" class="popup-overlay" style="display:none;"></div>

<!-- ================= POPUP XÁC NHẬN XÓA ================= -->
<div id="popupDelete" class="popup-box" style="display:none;">
    <h3>Bạn có chắc muốn xoá?</h3>
    <p id="deleteName"></p>

    <div class="popup-buttons">
        <asp:Button ID="btnConfirmDelete" runat="server" Text="Xoá" CssClass="btn-delete"
            OnClick="btnConfirmDelete_Click" />
        <button type="button" class="btn-cancel" onclick="closePopup()">Hủy</button>
    </div>

    <asp:HiddenField ID="hfDeleteID" runat="server" />
</div>

<!-- ================= POPUP CHỈNH SỬA ================= -->
<div id="popupEdit" class="popup-box" style="display:none;">
    <h3>Chỉnh sửa thành viên</h3>


<asp:Label ID="lblPopupEditMsg" runat="server" CssClass="notify-msg"
    Style="color:red; font-weight:bold; display:block; text-align:center; margin-bottom:10px;"></asp:Label>

    <asp:HiddenField ID="hfEditID" runat="server" />

    <div class="form-edit">

        <label>Họ tên</label>
        <asp:TextBox ID="txtEditHoTen" runat="server" CssClass="input-edit" />

        <label>Giới tính</label>
        <asp:DropDownList ID="ddlEditGioiTinh" runat="server" CssClass="input-edit">
            <asp:ListItem Text="Nam" />
            <asp:ListItem Text="Nữ" />
        </asp:DropDownList>

        <label>Ngày sinh</label>
        <asp:TextBox ID="txtEditNgaySinh" runat="server" TextMode="Date" CssClass="input-edit" />

        <label>Ngày mất</label>
        <asp:TextBox ID="txtEditNgayMat" runat="server" TextMode="Date" CssClass="input-edit" />

        <label>Đời</label>
        <asp:DropDownList ID="ddlEditDoi" runat="server" CssClass="input-edit"></asp:DropDownList>

        <label>Phái</label>
        <asp:DropDownList ID="ddlEditPhai" runat="server" CssClass="input-edit"></asp:DropDownList>

        <label>Chi</label>
        <asp:DropDownList ID="ddlEditChi" runat="server" CssClass="input-edit"></asp:DropDownList>

        <label>Vai trò trong họ</label>
        <asp:DropDownList ID="ddlEditVaiTro" runat="server" CssClass="input-edit">
            <asp:ListItem Text="Trưởng họ" />
            <asp:ListItem Text="Trưởng Phái" />
            <asp:ListItem Text="Trưởng Chi" />
            <asp:ListItem Text="Thành viên" />
        </asp:DropDownList>

    </div>

    <div class="popup-buttons">
        <asp:Button ID="btnSaveEdit" runat="server" CssClass="btn-edit"
            Text="Lưu" OnClick="btnSaveEdit_Click" />
        <button type="button" class="btn-cancel" onclick="closePopup()">Hủy</button>
    </div>
</div>

    </div>

    <!-- CSS -->
    <style>
        /* Nền mờ */
.popup-overlay {
    position: fixed;
    top: 0; left: 0;
    width: 100%; height: 100%;
    background: rgba(0,0,0,0.45);
    z-index: 999;
}

/* Hộp popup */
.popup-box {
    position: fixed;
    top: 50%; left: 50%;
    transform: translate(-50%, -50%);
    background: #ffffff;
    padding: 20px 25px;
    border-radius: 12px;
    width: 400px;
    z-index: 1000;
    box-shadow: 0 5px 15px rgba(0,0,0,0.2);
}

.popup-box h3 {
    text-align: center;
    margin-bottom: 15px;
}

.form-edit label {
    font-weight: bold;
    margin-top: 10px;
}

.input-edit {
    width: 100%;
    padding: 7px;
    border-radius: 6px;
    border: 1px solid #aaa;
    margin-bottom: 8px;
}

.popup-buttons {
    text-align: center;
    margin-top: 15px;
}

.btn-cancel {
    background: #bbb;
    padding: 7px 15px;
    border-radius: 6px;
    border: none;
    margin-left: 10px;
    cursor: pointer;
    color: white;
}

       /* KHUNG NGOÀI */
.table-container {
    background: #f5f5f5;
    padding: 25px;
    border-radius: 16px;
    box-shadow: 0 0 12px rgba(0,0,0,0.07);
    width: 90%;
    margin: auto;
}

/* GRID */
.grid {
    width: 100%;
    border-collapse: collapse !important; /* Để đường kẻ ngang hoạt động */
}
.notify-msg {
    font-size: 16px;
    font-weight: bold;
    margin-top: 10px;
    display: block;
}


/* HEADER & ROW chung 1 màu */
.grid-header th,
.grid-row td {
    background: #ececec !important;  /* MỘT màu cho toàn bảng */
    padding: 14px 12px;
    font-size: 15px;
    border: none !important;
}

/* HEADER đậm hơn */
.grid-header th {
    font-size: 16px;
    font-weight: 700;
}

/* ĐƯỜNG KẺ NGANG dưới mỗi dòng */
.grid-row td {
    border-bottom: 1px solid #ddd !important;
}

/* Bo góc chỉ cho header: */
.grid-header th:first-child { border-top-left-radius: 10px; }
.grid-header th:last-child { border-top-right-radius: 10px; }

/* Nút chỉnh sửa */
.btn-edit {
    background: #ffeaa7;
    padding: 6px 14px;
    border-radius: 6px;
    color: #000;
    font-weight: bold;
    text-decoration: none;
}
.btn-edit:hover { background: #fdd365; }

/* Nút Xóa */
.btn-delete {
    background: #e63946;
    padding: 6px 16px;
    border-radius: 6px;
    color: white;
    font-weight: bold;
    text-decoration: none;
}
.btn-delete:hover { background: #c1121f; }

/* Thanh tìm kiếm */
.input-search {
    width: 260px;
    padding: 10px 12px;
    border-radius: 8px;
    border: 1px solid #aaa;
    font-size: 15px;
}

.btn-search {
    background: #c89b3c;
    color: white;
    padding: 10px 25px;
    border: none;
    border-radius: 8px;
    font-weight: bold;
    cursor: pointer;
}
.btn-search:hover { background: #a57923; }

.search-box {
    display: flex;
    justify-content: center;
    margin-bottom: 25px;
    gap: 15px;
}

h2 {
    text-align: left;  
    width: 100%;
    margin-top: 10px;
    margin-bottom: 20px;
}

    </style>
    <script>
function openDeletePopup(id, name) {
    document.getElementById("popupOverlay").style.display = "block";
    document.getElementById("popupDelete").style.display = "block";

    document.getElementById("<%= hfDeleteID.ClientID %>").value = id;
    document.getElementById("deleteName").innerText = name;
}

function openEditPopup() {
    document.getElementById("popupOverlay").style.display = "block";
    document.getElementById("popupEdit").style.display = "block";
}

function closePopup() {
    document.getElementById("popupOverlay").style.display = "none";
    document.getElementById("popupEdit").style.display = "none";
    document.getElementById("popupDelete").style.display = "none";
}
</script>

</asp:Content>