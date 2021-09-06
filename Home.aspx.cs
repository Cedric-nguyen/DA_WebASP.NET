using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{

    protected string masach = "";
    protected bool check = false;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["qtc"] != null)
            if (Session["qtc"].ToString().Equals("admin"))
                Session["tenhienthi"] = quanTri.timTenTheoEmail(Session["email"].ToString().Trim());
            else
                Session["tenhienthi"] = khacHang.timTenTheoEmail(Session["email"].ToString().Trim());

    }
    #region check đăng nhập hay chưa và đăng xuất
    protected string load()
    {
        string value = "";
        if (Request.QueryString["modul"] != null && Request.QueryString["modul"].Equals("dx"))
        {
            Session["tenhienthi"]=Session["qtc"] = Session["email"] = Session["checkDN"] = null;             
            Response.Redirect("Home.aspx");
        }
        if (Session["checkDN"] != null && Session["checkDN"].ToString().Equals("1"))
        {

            string vl = @"<a class='btnDangXuat header__first-menu-link'>Tài khoản</a>
                    <ul class='header__first-menu-sub'>
                        <li class='header__first-menu-sub-item'>
                        
                          <a  href='javascript:thongTinCaNhan()' ><i class='fas fa-info-circle'></i>Thông tin cá nhân</a>
                        </li>
                        <li class='header__first-menu-sub-item'>
                       
                        <a   href='javascript:doiMatKhau()' > <i class='fas fa-key'></i>Đổi mật khẩu</a>
                        </li>
                        <li class='header__first-menu-sub-item'>
        
                         <a  href='Home.aspx?modul=dx' ><i class='fas fa-sign-out-alt'></i>Đăng xuất</a>
                        </li>
                    </ul>";
            if (Session["qtc"] != null && Session["qtc"].ToString().Equals("admin"))
                vl = "<a style='cursor:pointer;' class='btnDangXuat header__first-menu-link' href='Home.aspx?modul=dx' >Đăng xuất</a>";
            value = @"
        <ul class='header__first-menu'>
              <li class='header__first-menu-item'>
                  <a Class='header__first-menu-link' runat='server' >Xin chào " + Session["tenhienthi"].ToString().Trim() + @"</a>
              </li>
              <li class='header__first-menu-item'>
                  " + vl + @"
              </li>
         </ul>";

        }
        else
        {
            value = @"
        <ul class='header__first-menu'>
              <li class='header__first-menu-item'>
                  <a class='header__first-menu-link' runat='server' href='empty.aspx?modul=dn'>Đăng nhập</a>
              </li>
              <li class='header__first-menu-item'>
                   <a class='header__first-menu-link' runat='server' href='empty.aspx?modul=dk'>Đăng ký</a>
              </li>
         </ul>";
        }

        return value;
    }
    #endregion
    #region load thể loại và từng loại sách lên trang chủ
    protected string loadSach()
    {
        string values = "";
        DataSet ds = sach.dsSachTungTheLoai();
        DataTable dbTheLoai = ds.Tables["theloai"];
        DataTable dbSach = ds.Tables["sach"];
        int i = 0;
        foreach (DataRow r in dbTheLoai.Rows)
        {
            values += @" <h1 class='title'>
                <a class='title__link' href='empty.aspx?modul=tl&ml=" + r["matheloai"].ToString() + @"'Style='background-image: url(img/lbcenter.png)'>
                   " + r["tentheloai"].ToString() + @"
                </a>
            </h1>
            <div class='group_book'>
            <ul>";
            int dem = 0;

            #region load sách lên trang chủ
            foreach (DataRow r1 in dbSach.Rows)
            {
                i++;
                if (r1["matheloai"].ToString().Equals(r["matheloai"].ToString()))
                {
                    dem++;
                    //Giới hạn số lượng sách
                    if (dem > 4)
                        break;
                    values += @"
                <li class='book__item'>
                    <a href='empty.aspx?modul=s&ms=" + r1["masach"].ToString() + @"' class='book'>
                       <img src='img/" + r1["hinhminhhoa"].ToString() + @"'/>
                    </a>
                    <div class='book__item-notify'>
                        <h3 class='book__item-notify-title'>" + r1["tensach"].ToString() + @"
                        </h3>
                        <ul class='book__item-notify-info'>
                            <li>Tên tác giả:" + r1["tentacgia"].ToString() + @"</li>
                            <li>Năm sản xuất:" + DateTime.Parse(r1["namxb"].ToString()).ToShortDateString().ToString() + @"</li>
                            <li>Ngày cập nhật:" + DateTime.Parse(r1["ngaycapnhat"].ToString()).ToShortDateString().ToString() + @"</li>
                        </ul>
                        <div class='book__item-price'>
                            <span class='book__item-price-new'>" + String.Format("{0:#,##0}", Convert.ToInt32(r1["dongia"].ToString().Trim())) + @"đ</span>
                            <span class='book__item-price-old'>235.000đ</span>
                        </div>
                        <div class='group__btn'>
                        <a  href='javascript:themGioHang(" + "\"" + r1["masach"].ToString().Trim() + "\"" + @")' class='btn btnInfo btngiohang'>Thêm giỏ hàng</a>
                        <a href='javascript:muaNgay(" + "\"" + r1["masach"].ToString().Trim() + "\"" + @")' class='btn btnInfo'>Mua ngay</a>
                        </div>
                    </div>
                </li>";
                }
            }
            #endregion

            values += "</ul></div>";
        }

        return values;
    }
    #endregion


}