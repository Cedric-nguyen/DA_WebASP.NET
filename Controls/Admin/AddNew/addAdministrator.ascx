<%@ Control Language="C#" AutoEventWireup="true" CodeFile="addAdministrator.ascx.cs" Inherits="Controls_Admin_AddNew_addAdministrator" %>
<div class="introduce">
    <h1 class="group__addnew-title"><%=title %></h1>
    <div class="group__addnew">
        <div class="group__addnew-group">
            <span>Email</span>
            <asp:TextBox CssClass="txtMaNXB" ID="txtEmail"  runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator  ID="RegularExpressionValidator1" Text="Email không đúng định dạng" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationGroup="ck" SetFocusOnError="False">Email không đúng định dạng</asp:RegularExpressionValidator>
        </div>
        <div class="group__addnew-group">
            <span>Mật khẩu</span>
            <asp:TextBox TextMode="Password" ID="txtPW" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtPW" Text="Mật khẩu không được để trống" ValidationGroup="ck" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
        <div class="group__addnew-group">
            <span>Nhập lại mật khẩu</span>
            <asp:TextBox TextMode="Password" ID="txtPrePW" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:CompareValidator ControlToCompare="txtPrePW" ControlToValidate="txtPW" Operator="Equal" Display="Dynamic" ValidationGroup="ck" Text="Nhập lại mật khẩu không giống nhau" ID="CompareValidator1" runat="server" ErrorMessage="CompareValidator"></asp:CompareValidator>
        </div>
        <div class="group__addnew-group">
            <span>Tên hiển thị</span>
            <asp:TextBox  ID="txtTenHienThi" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ID="RequiredFieldValidator1" ControlToValidate="txtTenHienThi" Display="Dynamic" Text="Tên hiển thị không được để trống" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
        <div class="group__addnew-group">              
                    <asp:CheckBox CssClass="check" ValidationGroup="ck" ID="cbContinue" runat="server" Text="Tiếp tục tạo" />              
        </div>
        <div class="group__addnew-group">
                  <asp:Button CssClass="btn btnThemMoi" ID="btnThemMoi" runat="server" OnClientClick="kiemTra()" Text="Thêm mới " OnClick="btnThemMoi_Click"/>
                <asp:Button CssClass="btn btnHuy" ID="btnHuy" runat="server" Text="Hủy" CausesValidation="false" OnClick="btnHuy_Click"/>
            <a href="empty.aspx?modul=admin&submodul=qldsqt" class="btn btnTroVe">Trở về</a>
       
             </div>
    </div>  
</div>