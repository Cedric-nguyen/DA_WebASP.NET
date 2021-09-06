<%@ Control Language="C#" AutoEventWireup="true" CodeFile="addBook.ascx.cs" Inherits="Controls_Admin_AddNew_addBook" %>
<div class="introduce">
    <h1 class="group__addnew-title"><%=title %></h1>
    <div class="group__addnew">
        <div class="group__addnew-group">
            <span>Mã sách</span>
            <asp:TextBox CssClass="txtMaNXB" ID="txtMaSach"  runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" Text="Mã sách không được để trống" ControlToValidate="txtMaSach" Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
        <div class="group__addnew-group">
            <span>Tên sách</span>
            <asp:TextBox ID="txtTenSach" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtTenSach" Text="Tên sách không được để trống"  Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
         <div class="group__addnew-group">
            <span>Đơn giá</span>
            <asp:TextBox ID="txtDonGia" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtDonGia" Text="Đơn giá không được để trống"  Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
             <asp:CompareValidator ValidationGroup="ck" Operator="GreaterThan" ControlToValidate="txtDonGia" Text="Đơn giá phải là số nguyên lớn 0" ValueToCompare="0" Type="Integer" Display="Dynamic" ID="CompareValidator3" runat="server" ErrorMessage="CompareValidator"></asp:CompareValidator>
              </div>
        <div class="group__addnew-group">
            <span>Đơn vị tính</span>
            <asp:TextBox ID="txtDonViTinh" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtDonViTinh" Text="Đơn vị tính không được để trống"  Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
        <div class="group__addnew-group">
            <span>Mô tả (Nếu có)</span>
            <asp:TextBox ID="txtMoTa" TextMode="MultiLine" Rows="7" Columns="50" CssClass="txtTenNXB" runat="server"></asp:TextBox>
        </div>
        <div class="group__addnew-group">
            <span>Hình minh hoạ</span>
            <asp:Image ID="imgHinhMinhHoa"  style="width:100px;" runat="server" />
        </div>
        <div class="group__addnew-group">
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <span>Upload hình</span>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
         <div class="group__addnew-group">
            <span>Năm xuất bản</span>
            <asp:TextBox ID="txtNamXB" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtNamXB" Text="Năm xuất bản không được để trống"  Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            <asp:CompareValidator Operator="GreaterThanEqual" Text="Năm xuất bản không hợp lệ" Display="Dynamic" Type="Date" ValueToCompare="1/1/1900" ValidationGroup="ck" ControlToValidate="txtNamXB" ID="CompareValidator2" runat="server" ErrorMessage="CompareValidator"></asp:CompareValidator>
            
         </div>
        <div class="group__addnew-group">
            <span>Ngày cập nhật</span>
            <asp:TextBox ID="txtNgayCapNhat" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtNgayCapNhat" Text="Ngày cập nhật không được để trống"  Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            <asp:CompareValidator Operator="GreaterThanEqual" Text="Ngày cập nhật không hợp lệ" Display="Dynamic" Type="Date" ValueToCompare="1/1/1900" ValidationGroup="ck" ControlToValidate="txtNgayCapNhat" ID="CompareValidator1" runat="server" ErrorMessage="CompareValidator"></asp:CompareValidator>
             </div>
        
        <div class="group__addnew-group">
            <span>Tên tác giả</span>
            <asp:TextBox ID="txtTenTacGia" CssClass="txtTenNXB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="ck" ControlToValidate="txtTenTacGia" Text="Tên tác giả không được để trống"  Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </div>
        <div class="group__addnew-group">
            <span>Tên nhà sản xuất</span>
            <asp:DropDownList ID="ddlTenNSX" runat="server">

            </asp:DropDownList>
        </div>
        <div class="group__addnew-group">
            <span>Tên thể loại</span>
            <asp:DropDownList ID="ddlTenTheLoai" runat="server">

            </asp:DropDownList>
        </div>
         <div class="group__addnew-group">
            <span>Tên khuyến mãi</span>
            <asp:DropDownList ID="ddlTenCTKM" runat="server">
            </asp:DropDownList>
        </div>       
        <div class="group__addnew-group">
              
                    <asp:CheckBox CssClass="check" ID="cbContinue" runat="server"  Text="Tiếp tục tạo" />
              
        </div>
        <div class="group__addnew-group">
                  <asp:Button CssClass="btn btnThemMoi" ValidationGroup="ck" ID="btnThemMoi" OnClick="btnThemMoi_Click" runat="server" Text="Thêm mới"/>
                <asp:Button CssClass="btn btnHuy" ID="btnHuy" runat="server" Text="Hủy" OnClick="btnHuy_Click" CausesValidation="false" />
            <a href="empty.aspx?modul=admin&submodul=qldsb" class="btn btnTroVe">Trở về</a>
       
             </div>
    </div>  
</div>