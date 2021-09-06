<%@ Page   Title="" Language="C#" MasterPageFile="~/masterPageTrangChu.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Src="~/Controls/loadControls.ascx" TagPrefix="uc1" TagName="loadControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/homes.css" rel="stylesheet" />
    <script src="js/jquery-1.8.3.min.js"></script>
    <style>
        a{
            text-decoration:none;
        }       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <% =load() %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <% =loadSach() %>  
    <script language="javascript" type="text/javascript">      
        //hàm thêm giỏ hàng
        function themGioHang(mas) {           
                $.post("Controls/Book/Ajax/xuLy.aspx",
                    {
                        "thaotac": "themGioHang",
                        "masach":mas,
                        "soluong":1
                    },
                    function (data, status) {
                        if (data == 1 || data.substring(0, data.indexOf('<')) == 1)
                            alert("Thêm thành công");
                        else
                            alert("Mua thất bại");
                    });
            
        }
        //hàm mua ngay
        function muaNgay(mas) {           
            $.post("Controls/Book/Ajax/xuLy.aspx",
                {
                    "thaotac": "muaNgay",
                    "masach": mas,
                    "soluong": 1
                },
                function (data, status) {
                    if (data == 1 || data.substring(0, data.indexOf('<')) == 1) {
                        location.reload();                        
                    }
                });           
        }
       //hàm hiển thị thông tin giỏ hàng
        function cart() {
            var cart = document.getElementById('body__cart');
            var overload = document.getElementById('overload');
            cart.style.setProperty('z-index', '10');
            cart.style.setProperty('opacity', '1');

            overload.style.setProperty('z-index', '10');
            overload.style.setProperty('opacity', '1');

        }
      //hàm thoát giỏ hàng
        function tiepTucMuaHang() {
            var cart = document.getElementById('body__cart');
            var overload = document.getElementById('overload');
            cart.style.setProperty('z-index', '-1');
            cart.style.setProperty('opacity', '0');
            overload.style.setProperty('z-index', '-1');
            overload.style.setProperty('opacity', '0');

        }       
</script>
</asp:Content>

