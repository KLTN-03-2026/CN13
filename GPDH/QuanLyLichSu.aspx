<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuanLyLichSu.aspx.cs" Inherits="GPDH.QuanLyLichSu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2 class="title-history">Lịch sử dòng họ</h2>

<div class="action-bar">
    <div></div> <!-- giữ chỗ để tiêu đề luôn bên trái -->

    <div class="action-buttons">
        <asp:Button ID="btnThem" runat="server" Text="Thêm" CssClass="btn-action" OnClick="btnThem_Click" />
        <asp:Button ID="btnSua" runat="server" Text="Chỉnh sửa" CssClass="btn-action" OnClick="btnSua_Click" />
        <asp:Button ID="btnXoa" runat="server" Text="Xóa" CssClass="btn-action"
            OnClick="btnXoa_Click" OnClientClick="return confirm('Bạn chắc chắn muốn xoá?');" />
    </div>
</div>



<!-- Khung hiển thị lịch sử -->
<div class="history-box">
    <asp:Panel ID="pnView" runat="server">
        <div class="history-content">
            <asp:Literal ID="ltNoiDung" runat="server"></asp:Literal>
        </div>
    </asp:Panel>

    <!-- Khung chỉnh sửa -->
    <asp:Panel ID="pnEdit" runat="server" Visible="false">
        <asp:TextBox ID="txtNoiDung" runat="server" TextMode="MultiLine" CssClass="textbox"></asp:TextBox>
        <br /><br />
        <asp:Button ID="btnLuu" runat="server" Text="Lưu" CssClass="btn-save" OnClick="btnLuu_Click" />
        <asp:Button ID="btnHuy" runat="server" Text="Hủy" CssClass="btn-cancel" OnClick="btnHuy_Click" />
    </asp:Panel>
</div>

<!-- CKEditor 4 -->
<script src="https://cdn.ckeditor.com/4.25.1-lts/full/ckeditor.js"></script>

<script>
    CKEDITOR.replace('<%= txtNoiDung.ClientID %>', {
        height: 350,
        toolbar: [
            { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'RemoveFormat'] },
            { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'JustifyLeft',
                'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
            { name: 'colors', items: ['TextColor', 'BGColor'] },
            { name: 'styles', items: ['Format', 'Font', 'FontSize'] },
            { name: 'insert', items: ['Image', 'Table'] }
        ],
        removeButtons: ''
    });
</script>

<style>
    .title-history {
    text-align: left !important;
    margin-bottom: 10px;
    font-size: 26px;
    font-weight: bold;
}

    .action-bar {
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 95%;
    margin-bottom: 15px;
}

/* Núm nằm bên phải */
.action-buttons {
    text-align: right;
}

    .btn-action {
    padding: 8px 16px;
    border: none;
    background: #ffcc00;
    border-radius: 6px;
    cursor: pointer;
    margin-left: 10px;
}

.btn-disabled {
    background: #ccc !important;
    color: #666 !important;
    cursor: not-allowed !important;
}

    .history-box {
        background: #f6f6f6;
        padding: 20px;
        border-radius: 12px;
        width: 95%;
    }

    /* Nội dung căn đều 2 bên */
    .history-content {
        text-align: justify;
        font-size: 18px;
        line-height: 1.7;
        white-space: normal;
        word-wrap: break-word;
    }

    .textbox {
        width: 100%;
        height: 300px;
        border-radius: 6px;
    }

    .btn-save {
        padding: 8px 16px;
        background: #4CAF50;
        color: white;
        border-radius: 6px;
        border: none;
    }

    .btn-cancel {
        padding: 8px 16px;
        background: #999;
        color: white;
        border-radius: 6px;
        border: none;
    }
</style>

</asp:Content>
