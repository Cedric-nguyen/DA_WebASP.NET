<%@ Control Language="C#" AutoEventWireup="true" CodeFile="addSale.ascx.cs" Inherits="Controls_Admin_AddNew_addSale" %>
<div class="introduce">
    <h1 class="group__addnew-title"><%=title %></h1>
    <div class="group__addnew">
        <div class="group__addnew-group">
            <span>Mã khuyến mãi</span>
            <asp:TextBox CssClass="txtMaNXB" ID="txtMaCTKM"  runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" Text="Mã chương trình khuyến mãi không được để trống" ControlToValidate="txtMaCTKM" Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
        <div class="group__addnew-group">
            <span>Tên khuyến mãi</span>
            <asp:TextBox TextMode="MultiLine" Rows="5" Columns="23" ID="txtTenCTKM" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtTenCTKM" Text="Tên chương trình khuyến mãi không được để trống"  Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>

        </div>
        <div class="group__addnew-group">
            <span>Giảm giá(%)</span>
            <asp:TextBox  ID="txtGiamGia" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator Text="Hãy nhập giảm giá" Display="Dynamic" ControlToValidate="txtGiamGia" ValidationGroup="ck" ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" ValidationGroup="ck" ControlToValidate="txtGiamGia" Text="Giảm giá phải là số nguyên" ValidationExpression="\d{1,3}" runat="server" ErrorMessage="RegularExpressionValidator"></asp:RegularExpressionValidator>
        </div>
        <div class="group__addnew-group">
              
                    <asp:CheckBox CssClass="check" ID="cbContinue" runat="server"  Text="Tiếp tục tạo" />
              
        </div>
        <div class="group__addnew-group">
                  <asp:Button CssClass="btn btnThemMoi" ValidationGroup="ck" ID="btnThemMoi" runat="server" OnClientClick="kiemTra()" Text="Thêm mới " OnClick="btnThemMoi_Click"/>
                <asp:Button CssClass="btn btnHuy" ID="btnHuy" runat="server" Text="Hủy" CausesValidation="false" OnClick="btnHuy_Click"/>
            <a href="empty.aspx?modul=admin&submodul=qlctkm" class="btn btnTroVe">Trở về</a>
        
        </div>
    </div>  
</div>