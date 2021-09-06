<%@ Control Language="C#" AutoEventWireup="true" CodeFile="administrator.ascx.cs" Inherits="Controls_Admin_Manager_administrator" %>
<%=loadDSTK() %>
<script language="javascript" type="text/javascript">
    //hàm xoá 1 quản trị tk:admin@gmail.com không được xoá
    function xoaQuanTri(ma, stt) {
        if (confirm("Bạn chắc muốn xoá admin này?")) {
            $.post("Controls/Admin/Manager/Ajax/xuLy.aspx",
                {
                    "thaotac": "xoaQuanTri",
                    "email": ma,
                },
                function (data, status) {
                    if (data == 1 || data.substring(0, data.indexOf('<')) == 1) {
                        alert("Xoá thành công");
                        $("#madong_" + stt).slideUp();
                    }
                    else
                        alert("Không được xoá tài khoản admin@gmail.com");
                });
        }
    }
</script>
