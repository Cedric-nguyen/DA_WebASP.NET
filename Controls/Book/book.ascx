<%@ Control Language="C#" AutoEventWireup="true" CodeFile="book.ascx.cs" Inherits="Controls_Book_book" %>
<%=loadSach() %>
<script language="javascript" type="text/javascript">
    //tăng giảm giá trị số lượng
    function up() {
        var value = Number.parseInt($(".input-quality").val());

        ++value;
        $(".input-quality").val(value);
    }
    function down() {
        var value = Number.parseInt($(".input-quality").val());
        if (value <= 1) {
            $(".input-quality").val(1);
        }
        else {
            --value;
            $(".input-quality").val(value);
        }
    }
    //------------------------------------------------------------------------
    //hàm thêm 1 sách vào giỏ hàng
    function themGioHang() {
        var masach = "<%=masach%>";
            var values = $(".input-quality").val();
            //Viết code xóa danh mục tại đây                 
            $.post("Controls/Book/Ajax/xuLy.aspx",
                {
                    "thaotac": "themGioHang",
                    "masach": masach,
                    "soluong": values
                },
                function (data, status) {
                    if (data == 1 || data.substring(0, data.indexOf('<')) == 1)
                        alert("Thêm thành công");
                });
        }
        //hàm thêm sách vào giỏ hàng và hiển thị giỏ hàng
        function muaNgay() {
            var masach = "<%=masach%>";
             var values = $(".input-quality").val();

             //Viết code xóa danh mục tại đây                 
             $.post("Controls/Book/Ajax/xuLy.aspx",
                 {
                     "thaotac": "muaNgay",
                     "masach": masach,
                     "soluong": values
                 },
                 function (data, status) {
                     if (data == 1 || data.substring(0, data.indexOf('<')) == 1)
                         location.reload();
                 });
         }

</script>


