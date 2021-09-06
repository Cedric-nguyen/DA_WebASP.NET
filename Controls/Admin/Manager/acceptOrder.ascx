<%@ Control Language="C#" AutoEventWireup="true" CodeFile="acceptOrder.ascx.cs" Inherits="Controls_Admin_Manager_acceptOrder" %>
<div class="introduce">
    <h1 class="group__addnew-title">DUYỆT ĐƠN HÀNG</h1>
    <div class="group__addnew">
        <div class="group__addnew-group">
            <span>Tên nhân viên</span>
            <asp:DropDownList ID="ddlTenNV" runat="server"></asp:DropDownList>
        </div>
        
        <div class="group__addnew-group">
                  <asp:Button CssClass="btn btnThemMoi" ValidationGroup="ck" ID="btnDuyet" runat="server" Text="Chấp nhận" OnClick="btnDuyet_Click" />
                <asp:Button CssClass="btn btnHuy" ID="btnTroLai" runat="server" Text="Trở lại" OnClick="btnTroLai_Click" />
        </div>
    </div>  
</div>