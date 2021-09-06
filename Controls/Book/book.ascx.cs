using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Controls_Book_book : System.Web.UI.UserControl
{
    protected string masach = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region hàm lấy thông tin chi tiết của 1 cuốn sách và hiển thị lên trang web
    protected string loadSach()
    {
        string values = "";        
        DataTable dbSach = sach.laySachTheoMa(Request.QueryString["ms"].ToString().Trim());
        DataRow r = dbSach.Rows[0];
        masach = r["masach"].ToString().Trim();
        values = @"
<div class='book__group'>
<div class='book__info'>
 <img style='max-height:230px;min-width:148px;' src='img/" + r["hinhminhhoa"].ToString() + @"' class='book__info-left' />
    <div class='book__info-right'>
        <h1>" + r["tensach"].ToString() + @"</h1>
        <div class='book__info-right-left'>
            <ul>
                <li class='book__info-right-item'>
                    <span>Mã sách:</span>
                    <span class='book__info-right-properties'>" + r["masach"].ToString() + @"</span>
                </li>
                <li class='book__info-right-item'>
                    <span>Tác giả:</span>
                    <span class='book__info-right-properties'>" + r["tentacgia"].ToString() + @"</span>
                </li>
                <li class='book__info-right-item'>
                    <span>Tên nhà xuất bản:</span>
                    <span class='book__info-right-properties'>" + r["tennxb"].ToString() + @"</span>
                </li>
                <li class='book__info-right-item'>
                    <span>Thể loại:</span>
                    <span class='book__info-right-properties'>" + r["tentheloai"].ToString() + @"</span>
                </li>
                <li class='book__info-right-item'>
                    <span>Đơn vị tính:</span>
                    <span class='book__info-right-properties'>" + r["donvitinh"].ToString() + @"</span>
                </li>
                <li class='book__info-right-item'>
                    <span>Ngày phát hành:</span>
                    <span class='book__info-right-properties'>" + DateTime.Parse(r["namxb"].ToString()).ToShortDateString().ToString() + @"</span>
                </li>
            </ul>
            <div class='book__info-right-right'>
                <p class='book__info-right-price-old'><span></span></p>
                <p class='book__info-right-price-new'>Giá sách:<span>" + String.Format("{0:#,##}", Convert.ToInt32(r["dongia"].ToString().Trim())) + "đ" + @"</span></p>
 <p class='right__info-right-quantity'>Số lượng:</p>
                    <p class='right__info-right-input'>
                    <input type='text' class='input-quality' value='1' />
                    <span class='right__info-right-group'>
                        <a href = 'javascript:up();' class='icon-up' ></a>
                        <a href = 'javascript:down();' class='icon-down'></a>
                    </span>
                    <span class='right_-info-right-group-btn'>
                        <a runat = 'server' href='javascript:themGioHang()' class='btnThemGioHang btn'>Thêm vào giỏ hàng</a>
                        <a runat = 'server' href= 'javascript:muaNgay()' class='btnMuaNgay btn'>Mua ngay</a>
                    </span>
                </p>
            </div>
        </div>        
    </div>    
</div>
<div class='book__info-describe'>
<h1 class='book__info-describe-title'>Mô tả</h1>
<p class='book__info-describe-content'>" + r["mota"].ToString().Trim() + @"</p>
</div>";
        //danh sách cùng tác giả
        dbSach = sach.layDSTheoTacGia(r["tentacgia"].ToString().Trim(), masach);
if(dbSach.Rows.Count>0)
        values += @"<div class='book__info-same-as'>
   <div class='book__info-same-as-group'>
    <h1 class='book__info-same-as-title'>Sách cùng tác giả</h1>
</div>
<div class='book__info-same-as-group-img'>
<marquee height='100%' width='100%' direction='left' height='100px'>";
        
        foreach(DataRow r1 in dbSach.Rows)
        { 
            values += @"<a class='book__info-same-as-group-link' href='empty.aspx?modul=s&ms="+r1["masach"].ToString().Trim()+@"'>
  <img style='max-height:230px;min-width:148px;' src='img/" + r1["hinhminhhoa"].ToString().Trim()+@"'/>
</a>";
        }
        if(dbSach.Rows.Count>0)
        values += @"</marquee></div></div>";
        // danh sách cùng nhà xuất bản
        dbSach = sach.laySachTheoMaNXB(r["manxb"].ToString().Trim(),masach);
        if(dbSach.Rows.Count>0)
        values += @"<div class='book__info-same-as'>
   <div class='book__info-same-as-group'>
    <h1 class='book__info-same-as-title'>Sách cùng nhà xuất bản </h1>
</div>
<div class='book__info-same-as-group-img'>
<marquee height='100%' width='100%' direction='left' height='100px'>";
        
        foreach(DataRow r1 in dbSach.Rows)
        { 
            values += @"<a class='book__info-same-as-group-link' href='empty.aspx?modul=s&ms=" + r1["masach"].ToString().Trim()+@"'>
  <img style='max-height:230px;min-width:148px;' src='img/" + r1["hinhminhhoa"].ToString().Trim()+@"'/>
</a>";
        }
        if (dbSach.Rows.Count > 0)
            values += @"</marquee></div></div>";
        // danh sách sách cùng thể loại
        dbSach = sach.laySachTheoMaLoai(r["matheloai"].ToString().Trim(), masach);
        if (dbSach.Rows.Count > 0)

            values += @"<div class='book__info-same-as'>
   <div class='book__info-same-as-group'>
    <h1 class='book__info-same-as-title'>Sách cùng thể loại </h1>
</div>
<div class='book__info-same-as-group-img'>
<marquee height='100%' width='100%' direction='left' height='100px'>";

        foreach (DataRow r1 in dbSach.Rows)
        {
            values += @"<a class='book__info-same-as-group-link' href='empty.aspx?modul=s&ms=" + r1["masach"].ToString().Trim() + @"'>
  <img style='max-height:230px;min-width:148px;' src='img/" + r1["hinhminhhoa"].ToString().Trim() + @"'/>
</a>";
        }
        if (dbSach.Rows.Count > 0)
            values += "</marquee></div></div>";
        values += "</div>";
        return values;
    }
    #endregion
}