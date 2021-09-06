<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sale.ascx.cs" Inherits="Controls_Admin_Manager_sale" %>
<%=loadDSGG() %>

<script language="javascript" type="text/javascript">
    //hàm xoá 1 chương trình khuyến mãi theo mã khuyễn mãi
    function xoaCTKM(ma) {
        if (confirm("Bạn chắc muốn xoá chương trình khuyến mãi này này?")) {
            $.post("Controls/Admin/Manager/Ajax/xuLy.aspx",
                {
                    "thaotac": "xoaCTKM",
                    "makm": ma,
                },
                function (data, status) {
                    if (data == 1 || data.substring(0, data.indexOf('<')) == 1) {
                        alert("Xoá thành công");
                        $("#madong_" + ma).slideUp();
                    }
                    else
                        alert("Không xoá được");
                });
        }
    }
</script>