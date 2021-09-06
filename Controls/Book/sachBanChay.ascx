<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sachBanChay.ascx.cs" Inherits="Controls_Book_sachBanChay" %>
<%=loadSach()%>
<script language="javascript" type="text/javascript">
      
    //hàm thêm vào giỏ hàng 1 cuốn sách
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
    //hàm thêm vào giỏ hàng 1 cuốn sách và hiển thị giỏ hàng
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