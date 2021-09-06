<%@ Control Language="C#" AutoEventWireup="true" CodeFile="categoryBook.ascx.cs" Inherits="Controls_Admin_Manager_categoryBook" %>
<%=loadDSTL() %>
<script language="javascript" type="text/javascript">
    //hàm xoá thể loại theo mã thể loại
    function xoaTheLoai(ma) {
        if (confirm("Bạn chắc muốn xoá thể loại này?")) {
            $.post("Controls/Admin/Manager/Ajax/xuLy.aspx",
                {
                    "thaotac": "xoaTheLoai",
                    "matheloai": ma,
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
