<%@ Control Language="C#" AutoEventWireup="true" CodeFile="listAccount.ascx.cs" Inherits="Controls_Admin_Manager_listAccount" %>
<%=loadDSTK() %>

<script language="javascript" type="text/javascript">
    //hàm xoá 1 khách hàng theo mã
    function xoaKH(ma,stt) {
        if (confirm("Bạn chắc muốn xoá khách hàng này")) {
            $.post("Controls/Admin/Manager/Ajax/xuLy.aspx",
                {
                    "thaotac": "xoakh",
                    "makh": ma,
                },
                function (data, status) {
                    if (data == 1 || data.substring(0, data.indexOf('<')) == 1) {
                        alert("Xoá thành công");
                        $("#madong_" + stt).slideUp();
                    }
                    else
                        alert("Không xoá được");
                });
        }
    }
    // hàm reset mật khẩu của khách hàng theo mã
    function reset(ma) {
        if (confirm("Bạn chắc muốn reset mật khẩu?")) {
            //Viết code xóa danh mục tại đây                 
            $.post("Controls/Admin/Manager/Ajax/xuLy.aspx",
                {
                    "thaotac": "reset",
                    "email": ma,
                },
                function (data, status) {
                    if (data==1||data.substring(0, data.indexOf('<')) == 1) {
                        alert("Đã reset thành công");                      
                    }
                    else
                        alert("Không xoá được");
                });
        }
    }
</script>