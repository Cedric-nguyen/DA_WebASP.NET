<%@ Control Language="C#" AutoEventWireup="true" CodeFile="order.ascx.cs" Inherits="Controls_Admin_Manager_order" %>
<%=loadDSDDH() %>

<script language="javascript" type="text/javascript">

    //hàm xoá 1 đơn đặt hàng theo mã đơn đặt hàng
    function xoaDDH(mas) {
        if (confirm("Bạn chắc muốn xoá đơn đặt hàng này?")) {
            $.post("Controls/Admin/Manager/Ajax/xuLy.aspx",
                {
                    "thaotac": "xoaDDH",
                    "maddh": mas,
                },
                function (data, status) {
                    if (data == 1 || data.substring(0, data.indexOf('<')) == 1) {
                        alert("Xoá thành công");
                        $("#madong_" + mas).slideUp();
                    }
                    else
                        alert("Không xoá được");
                });
        }
    }
</script>