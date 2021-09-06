<%@ Control Language="C#" AutoEventWireup="true" CodeFile="timKiemSach.ascx.cs" Inherits="Controls_Book_timKiemSach" %>
<%=loadSach()%>
<script language="javascript" type="text/javascript">
      
    //hàm thêm 1 cuốn sách vào giỏ hàng
        function themGioHang(mas) {
           
                //Viết code xóa danh mục tại đây                 
                $.post("Controls/Book/Ajax/xuLy.aspx",
                    {
                        "thaotac": "themGioHang",
                        "masach":mas,
                        "soluong":1
                    },
                    function (data, status) {
                        if (data == 1 || data.substring(0, data.indexOf('<')) == 1)
                        alert("Thêm thành công");
                    });
        }
    //hàm thêm 1 cuốn sách vào giỏ hàng và hiển thị giỏ hàng
        function muaNgay(mas) {

            //Viết code xóa danh mục tại đây                 
            $.post("Controls/Book/Ajax/xuLy.aspx",
                {
                    "thaotac": "muaNgay",
                    "masach": mas,
                    "soluong": 1
                },
                function (data, status) {
                    if (data == 1 || data.substring(0, data.indexOf('<')) == 1)
                        location.reload();
                });
        }

</script>