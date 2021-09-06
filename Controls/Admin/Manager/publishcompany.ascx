<%@ Control Language="C#" AutoEventWireup="true" CodeFile="publishcompany.ascx.cs" Inherits="Controls_Admin_Manager_publishcompany" %>
<%=loadDSNXB() %>

<script language="javascript" type="text/javascript">

    //hàm xoá 1 nhà xuất bản theo mã nhà xuất bản
    function xoaNXB(ma) {
        if (confirm("Bạn chắc muốn xoá nhà xuất bản này?")) {
            $.post("Controls/Admin/Manager/Ajax/xuLy.aspx",
                {
                    "thaotac": "xoaNXB",
                    "manxb": ma,
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