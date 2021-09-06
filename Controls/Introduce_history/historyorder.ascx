<%@ Control Language="C#" AutoEventWireup="true" CodeFile="historyorder.ascx.cs" Inherits="Controls_Introduce_history_historyorder" %>
<div class="<%=ktraAdmin("admin")%> history__order introduce">
    <h1 class="history_order-title">
        Lịch sử đơn hàng
    </h1>
<asp:GridView ID="GridView1"  runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="stt" HeaderText="STT" />
        <asp:BoundField DataField="ma" HeaderText="Mã đơn hàng" />
        <asp:BoundField DataField="ngay" HeaderText="Ngày tạo" />
        <asp:BoundField DataField="thanhtien" HeaderText="Thành tiền" />
        <asp:BoundField DataField="diachi" HeaderText="Địa chỉ nhận hàng" />
        <asp:BoundField DataField="email" HeaderText="Email" />
        <asp:BoundField DataField="tinhtrang" HeaderText="Tình trạng" />
    </Columns>
    <FooterStyle BackColor="#CCCC99" />
    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
    <RowStyle BackColor="#F7F7DE" />
    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FBFBF2" />
    <SortedAscendingHeaderStyle BackColor="#848384" />
    <SortedDescendingCellStyle BackColor="#EAEAD3" />
    <SortedDescendingHeaderStyle BackColor="#575357" />

</asp:GridView>
        <%=loadDSDDH() %>   
</div>
