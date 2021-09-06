<%@ Control Language="C#" AutoEventWireup="true" CodeFile="addemployee.ascx.cs" Inherits="Controls_Admin_AddNew_addemployee" %>
<div class="introduce">
    <h1 class="group__addnew-title"><%=title %></h1>
    <div class="group__addnew">
        <div class="group__addnew-group">
            <span>Mã nhân viên</span>
            <asp:TextBox CssClass="txtMaNXB" ID="txtMaNV" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" Text="Mã nhân viên không được để trống" ControlToValidate="txtMaNV" Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
        <div class="group__addnew-group">
            <span>Tên nhân viên</span>
            <asp:TextBox CssClass="txtMaNXB" ID="txtTenNV" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtTenNV" Text="Tên nhân viên không được để trống" Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
        <div class="group__addnew-group">
            <span>Địa chỉ</span>
            <asp:TextBox CssClass="txtMaNXB" ID="txtDiaChi" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtDiaChi" Text="Địa chỉ không được để trống" Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
        <div class="group__addnew-group">
            <span>Số điện thoại</span>
            <asp:TextBox CssClass="txtMaNXB" ID="txtSDT" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtSDT" Text="Số điện thoại không được để trống" Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="ck" ControlToValidate="txtSDT" Text="Số điện thoại phải 10-11 số" Display="Dynamic" ValidationExpression="0\d{9,10}" runat="server" ErrorMessage="RegularExpressionValidator"></asp:RegularExpressionValidator>
        </div>
        <div class="group__addnew-group">
            <span>Giới tính</span>
            <asp:RadioButtonList ID="rdGioiTinh" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Nam" Value="Nam" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Nữ" Value="Nữ"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="group__addnew-group">
            <span>Ngày vào làm</span>
            <asp:TextBox CssClass="txtMaNXB" ID="txtNgayVL" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtNgayVL" Text="Ngày vào làm không được để trống" Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            <asp:CompareValidator ControlToValidate="txtNgayVL" Type="Date" Operator="GreaterThanEqual" ValueToCompare="1/1/1900" Text="Ngày không đúng định dạng" ID="CompareValidator1" runat="server" ErrorMessage="CompareValidator"></asp:CompareValidator>
        </div>
        <div class="group__addnew-group">
            <span>Lương</span>
            <asp:TextBox ID="txtLuong" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator Text="Hãy nhập lương" ControlToValidate="txtLuong" ValidationGroup="ck" ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="ck" ControlToValidate="txtLuong" Text="Lương phải là số nguyên" ValidationExpression="\d{1,}" runat="server" ErrorMessage="RegularExpressionValidator"></asp:RegularExpressionValidator>
        </div>
        <div class="group__addnew-group">

            <asp:CheckBox CssClass="check" ID="cbContinue" runat="server" Text="Tiếp tục tạo" />

        </div>
        <div class="group__addnew-group">
            <asp:Button CssClass="btn btnThemMoi" ValidationGroup="ck" ID="btnThemMoi" runat="server" OnClientClick="kiemTra()" Text="Thêm mới " OnClick="btnThemMoi_Click" />
            <asp:Button CssClass="btn btnHuy" ID="btnHuy" runat="server" Text="Hủy" CausesValidation="false" OnClick="btnHuy_Click" />
            <a href="empty.aspx?modul=admin&submodul=qldsnv" class="btn btnTroVe">Trở về</a>

        </div>
    </div>
</div>
