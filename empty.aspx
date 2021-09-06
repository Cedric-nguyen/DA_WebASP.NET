<%@ Page Title="" Language="C#" MasterPageFile="~/masterPageTrangChu.master" AutoEventWireup="true" CodeFile="empty.aspx.cs" Inherits="empty" %>

<%@ Register Src="~/Controls/loadControls.ascx" TagPrefix="uc1" TagName="loadControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Css/homes.css" />
    <script src="js/jquery-1.8.3.min.js"></script>
    <link rel="stylesheet" href="Css/Emptys.css" />
    <style>
        .login__group-pass-link {
            color: #0f813f;
        }

        #icon_close {
            display: none;
        }

        .login_group-pass-input {
            width: 154px;
            flex-basis: 50%;
            border: none;
            display: inline-block;
        }

        .login_group-pass {
            width: 182.3px;
            display: flex;
            background: white;
        }

        .book__info-describe {
            padding: 12px 12px 12px 0;
        }

        .book__info-describe-title {
            color: #0f813f;
            margin-bottom: 12px;
            font-size: 3rem;
            position: relative;
            line-height: 3rem;
        }

            .book__info-describe-title::after {
                content: "";
                display: block;
                position: absolute;
                height: 4px;
                left: 0;
                right: 0;
                top: calc(100% + 8px);
                background: #ccc;
            }

        .book__info-describe-content {
            font-size: 2rem;
            line-height: 2.5rem;
            margin-top: 20px;
        }

        .book__info {
            display: flex;
        }

        .book__group {
            background-image: url(../img/headerbg.png);
            padding: 24px 24px 24px 24px;
            justify-content: space-between;
            min-width: 900px;
            max-width: 700px;
        }


        .introduce {
            min-height: 300px;
            min-width: 700px;
        }

        .content {
            flex-wrap: wrap;
        }

        .group_book {
            flex-basis: 100%;
        }

        .category__page {
        }

        .history__order.introduce {
            min-width: 1200px;
        }

        .current.history__order.introduce {
            min-width: 940px;
        }

        .check__order.header__search {
            width: 300px;
            margin-bottom: 2rem;
        }

        .category__page a {
            margin-left: 12px;
            text-decoration: none;
            font-size: 1.5rem;
        }

        .btn {
            left: unset;
            transform: unset;
        }

        .category__page a:first-child {
            margin-left: unset;
        }

        .curentpage {
            color: red;
        }

        a {
            text-decoration: none;
        }

        .btnMuaNgay.btn {
            text-align: center;
        }

        .history__order-item {
            display: flex;
            margin-bottom: 1rem;
        }

            .history__order-item span {
                font-size: 1.8rem;
                line-height: 2.5rem;
            }

            .history__order-item.history__order-item-title span {
                font-weight: bold;
            }

        .history_order-title {
            color: #0f813f;
            font-size: 2.2rem;
            line-height: 2.5rem;
            margin-bottom: 2rem;
        }

        .history__order-item span:first-child {
            flex-basis: 5%;
        }

        .history__order-item .history__order-item-address {
            flex-basis: 30%;
        }

        .history__order-item .history__order-item-code, .history__order-item .history__order-item-status, .history__order-item .history__order-item-date, .history__order-item .history__order-item-total {
            flex-basis: 10%;
        }

        .history__order-item .history__order-item-email {
            flex-basis: 25%;
        }

        .btn.btnTroVe {
            left: unset;
            transform: unset;
            margin-top: unset;
            min-width: 100px;
            font-size: 1.5rem;
            text-align: center;
            margin-left: 24px;
        }

        .dmsach {
            color: #050817;
            padding: 10px;
            display: flex;
            justify-content: space-between;
        }

            .dmsach a {
                font-size: 2rem;
                color: #0f813f;
            }

        .DMSach {
            padding: 10px;
        }

        .tbsach {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid #dfdfdf;
        }

            .tbsach th,.tbsach span {
                font-size: 14px;
                background: #5a102f;
                color: #ffffff;
                text-align: center;
            }

            .tbsach th,
            .tbsach td ,.tbsach span{
                border: 1px solid #dfdfdf;
                padding: 5px;
            }
            div.tbsach{
                display:flex;
            }
         
            .tbsach span.cotstt{
                flex-basis:5%;
            }
            .tbsach span.cotma{
                flex-basis:66%;
            }

        .tbsach__title {
            min-width: 100px;
        }

        .tbsach .cotstt {
            min-width: 10px;
            text-align: center;
        }


        .tbsach .cotten {
            width: 117px;
        }

        .tbsach .cotdongia {
            text-align: center;
        }

        .tbsach .cotdvt {
            text-align: center;
        }

        .tbsach .cotmota {
            text-align: center;
        }

        .tbsach .cothinh {
            text-align: center;
        }

        .tbsach .cotnxb {
            text-align: center;
        }

        .tbsach .cotncn {
            text-align: center;
        }

        .tbsach .cottentg {
            text-align: center;
        }

        .tbsach .cotmatl {
            text-align: center;
        }

        .tbsach .cotmansx {
            text-align: center;
        }

        .tbsach .cotcongcu {
            width: 216px;
        }

        .listbook.introduce {
            min-width: 1200px;
        }

        .tbsach td {
            font-size: 1.5rem;
        }

        .cothinh img {
            width: 50px;
            transition: all .5s linear;
        }

        .tbsach .cotcongcu {
            text-align: center;
        }

        .cotcongcu a {
            margin-left: 8px;
        }

            .cotcongcu a i {
                font-size: 2rem;
            }

        .listpage a {
            margin-left: 12px;
            font-size: 1.8rem;
        }

        .listpage {
            margin: 34px 0 8px;
            text-align: center;
            flex: 100%;
        }

        .cothinh:hover img {
            cursor: pointer;
            transform: scale(2);
        }

        .dmsach h2, .group__addnew-title {
            font-size: 2.5rem;
            color: #0f813f;
        }

        .group__addnew-title {
            margin-bottom: 12px;
            line-height: 3rem;
        }

        .group__addnew-group span {
            font-size: 2rem;
            margin-right: 8px;
            min-width: 168px;
            display: inline-block;
        }

        textarea, input {
            outline: none;
            border: 1px solid #ccc;
            border-radius: 2px;
        }

        select {
            cursor: pointer;
            padding: 4px;
        }

        .group__addnew-group label {
            font-size: 1.8rem;
            margin-left: 8px;
        }

        .group__addnew-group {
            margin-top: 12px;
            display: flex;
            align-items: center;
        }

        .btn.btnHuy {
            margin-top: unset;
            margin-left: 24px;
            background: #ccc;
            color: #0f813f;
        }

        .btn.btnHuy, .btn.btnThemMoi {
            min-width: 100px;
        }

        input[type=checkbox] {
            cursor: pointer;
        }

        .group__addnew-group .check {
            display: none;
        }

            .group__addnew-group .check.tm {
                display: block;
            }

        .container {
            padding: 0;
        }

        .book__info-same-as-group {
            border-bottom: 5px solid #ccc;
            margin-bottom: 9px;
        }

        .book__info-same-as-title {
            line-height: 2.5rem;
            font-size: 2.5rem;
            color: white;
            background: #0f813f;
            display: inline-block;
            padding: 4px;
            font-weight: 400;
            position: relative;
        }

            .book__info-same-as-title::after {
                content: "";
                position: absolute;
                display: block;
                border-style: solid;
                border-width: 16.4px;
                border-color: transparent transparent #0f813f #0f813f;
                left: 100%;
                top: 0;
            }

        .book__info-same-as-group-link {
            display: inline-block;
        }

            .book__info-same-as-group-link + .book__info-same-as-group-link {
                margin-left: 8px;
            }

        #ContentPlaceHolder3_loadControls_ctl00_GridView1 td, #ContentPlaceHolder3_loadControls_ctl00_GridView1 th {
            text-align: center;
            text-align: center;
            font-size: 1.8rem;
            padding: 8px;
        }
        #ContentPlaceHolder3_loadControls_ctl00_GridView1 a{
            margin-right:16px;
        }
        #ContentPlaceHolder3_loadControls_ctl00_GridView1{
            width:100% !important;
        }
        .search_check{
            font-size:2.5rem;
            line-height:3rem;
        }
    </style>
    <script language="javascript" type="text/javascript">

        //hàm hiển thị giỏ hàng
        function cart() {
            var cart = document.getElementById('body__cart');
            var overload = document.getElementById('overload');
            cart.style.setProperty('z-index', '11');
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%=load() %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <uc1:loadControls runat="server" ID="loadControls" />
</asp:Content>



