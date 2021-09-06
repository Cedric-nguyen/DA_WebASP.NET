<%@ Control Language="C#" AutoEventWireup="true" CodeFile="addCategoryBook.ascx.cs" Inherits="Controls_Admin_AddNew_addCategoryBook" %>
 <div class="introduce">
    <h1 class="group__addnew-title"><%=title %></h1>
    <div class="group__addnew">
        <div class="group__addnew-group">
            <span>Mã thể loại</span>
            <asp:TextBox CssClass="txtMaNXB" ID="txtMaTheLoai"  runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" Text="Mã thể loại không được để trống" ControlToValidate="txtMaTheLoai" Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
        <div class="group__addnew-group">
            <span>Tên thể loại</span>
            <asp:TextBox ID="txtTenTheLoai" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtTenTheLoai" Text="Tên thể loại không được để trống"  Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>

        </div>
        <div class="group__addnew-group">
              
                    <asp:CheckBox CssClass="check" ID="cbContinue" runat="server"  Text="Tiếp tục tạo" />
              
        </div>
        <div class="group__addnew-group">
                  <asp:Button CssClass="btn btnThemMoi" ValidationGroup="ck" ID="btnThemMoi" runat="server" OnClientClick="kiemTra()" Text="Thêm mới " OnClick="btnThemMoi_Click"/>
                <asp:Button CssClass="btn btnHuy" ID="btnHuy" runat="server" Text="Hủy" CausesValidation="false" OnClick="btnHuy_Click"/>
            <a href="empty.aspx?modul=admin&submodul=qldmb" class="btn btnTroVe">Trở về</a>
        </div>
    </div>  
</div>