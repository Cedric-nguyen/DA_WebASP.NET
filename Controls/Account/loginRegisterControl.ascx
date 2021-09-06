<%@ Control Language="C#" AutoEventWireup="true" CodeFile="loginRegisterControl.ascx.cs" Inherits="Controls_Account_loginRegisterControl" %>
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="vwLogin" runat="server">
        <div class="login">
            <h1 class="login__title" style="text-align:center">Đăng nhập
            </h1>
            <div class="login__group">
                <p>Email</p>
                <asp:TextBox runat="server" ID="txtEmailLogin" ValidationGroup="login"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="login" ControlToValidate="txtEmailLogin" Text="Email không được bỏ trống" Display="Dynamic" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ControlToValidate="txtEmailLogin" ValidationGroup="login" ID="RegularExpressionValidator1" runat="server" Text="Không đúng định dạng email" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </div>
            <div class="login__group">
                <p>Mật khẩu</p>
                <div class="login_group-pass">
                    <asp:TextBox runat="server" CssClass="login_group-pass-input" ID="txtPWLogin" TextMode="Password"></asp:TextBox>
                    <a id="icon_open"  class="login__group-pass-link" href="javascript:open()"><i  class="fas fa-eye"></i></a>
                    <a id="icon_close"  href="javascript:close()" class="login__group-pass-link"><i class="fas fa-eye-slash"></i></a>
                </div>              
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2">
                    <asp:ListItem Value="Khách hàng" Selected Text="Khách hàng"></asp:ListItem>
                    <asp:ListItem Value="Quản trị" Text="Quản trị"></asp:ListItem>
                </asp:RadioButtonList>
            </div>         
            <asp:Button runat="server" TabIndex="0" ID="btnLogin" Text="Đăng nhập" OnClick="btnLogin_Click" ValidationGroup="login" CssClass="btn btnLogin" />
            <br />
            <asp:Label runat="server" CssClass="lbNotifyLogin" ID="lbNotifyLogin"></asp:Label>
            <p>
                <span>Chưa có tài khoản? Hãy</span>
                <asp:Button runat="server" Text="Đăng ký" ID="btnRegister" CssClass="btn" OnClick="btnRegister_Click" Style="height: 29px" />
            </p>
        </div>
    </asp:View>
    <asp:View ID="vwRegister" runat="server">
        <div class="register">
            <h1 class="register__title" style="text-align:center">Đăng ký
            </h1>
            <div class="register__group">
                <p>Email</p>
                <asp:TextBox runat="server" ID="txtEmailRegister" ValidationGroup="register"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="register" ControlToValidate="txtEmailRegister" Text="Email không được bỏ trống" Display="Dynamic" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>


                <asp:RegularExpressionValidator ControlToValidate="txtEmailRegister" ValidationGroup="register" ID="RegularExpressionValidator2" runat="server" Text="Không đúng định dạng email" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </div>
            <div class="register__group">
                <p>Mật khẩu</p>
                <asp:TextBox runat="server" ID="txtPWRegister" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="register" ControlToValidate="txtPWRegister" Text="Mật khẩu không được bỏ trống" Display="Dynamic" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>


            </div>
            <div class="register__group">
                <p>Xác nhận mật khẩu</p>
                <asp:TextBox runat="server" ID="txtPWPre" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtPWRegister" Operator="Equal" ControlToCompare="txtPWPre" runat="server" ValidationGroup="register" Text="Mật khẩu không trùng khớp" Display="Dynamic" ErrorMessage="CompareValidator"></asp:CompareValidator>
            </div>
            <div class="register__group">
                <p>Tên hiển thị</p>
                <asp:TextBox runat="server" ID="txtTenHienThi"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="register" ControlToValidate="txtTenHienThi" Text="Tên hiển thị không được bỏ trống" Display="Dynamic" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            </div>
            <div class="register__group">
                <p>Giới tính</p>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2">
                    <asp:ListItem Value="Nam" Text="Nam"></asp:ListItem>
                    <asp:ListItem Value="Nữ" Text="Nữ"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="register__group">
                <p>Số điện thoại</p>
                <asp:TextBox runat="server" ID="txtPhone"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="register" ControlToValidate="txtPhone" Text="Phone không được bỏ trống" Display="Dynamic" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ValidationExpression="0\d{9,10}" ID="RegularExpressionValidator3" runat="server" ValidationGroup="register" ControlToValidate="txtPhone" Text="Số điện thoại không đúng định dạng" Display="Dynamic" ErrorMessage="RegularExpressionValidator"></asp:RegularExpressionValidator>
            </div>

            <div class="register__group">
                <p>Đia chỉ</p>
                <asp:TextBox runat="server" ID="txtAddress"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="register" ControlToValidate="txtAddress" Text="Địa chỉ không được bỏ trống" Display="Dynamic" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>

            </div>

            <asp:Button runat="server" OnClick="btnRegister1_Click" ID="btnRegister1" Text="Đăng ký" ValidationGroup="register" CssClass="btn btnDangKy" />
            <p>
                <span>Bạn đã có tài khoản? Hãy </span>
                <asp:Button runat="server" Text="Đăng nhập" ID="btnLoginRegister" CssClass="btn" OnClick="btnLoginRegister_Click" />
            </p>
        </div>
    </asp:View>
</asp:MultiView>
<script language="javascript" type="text/javascript">         
    //hàm xem mật khẩu
    function open()
    {
        var icon_open = document.getElementById("icon_open");
        var icon_close = document.getElementById("icon_close");
        var input = document.getElementById("ContentPlaceHolder3_loadControls_ctl00_txtPWLogin");
       
        input.setAttribute("type", "text");
        icon_open.style.setProperty("display", "none");
        icon_close.style.setProperty("display", "block");        
    }
    //hàm ẩn mật khẩu
    function close() {
        var icon_open = document.getElementById("icon_open");
        var icon_close = document.getElementById("icon_close");
        var input = document.getElementById("ContentPlaceHolder3_loadControls_ctl00_txtPWLogin");
     
        input.setAttribute("type", "password");
        icon_open.style.setProperty("display", "block");
        icon_close.style.setProperty("display", "none");
    }
</script>
