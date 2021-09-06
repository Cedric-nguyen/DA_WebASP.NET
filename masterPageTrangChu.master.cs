using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class masterPageTrangChu : System.Web.UI.MasterPage
{
   // protected HttpCookie cookie = null;
    protected int sum = 0;
    protected string email = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        if (Session["cart"] != null)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cart()", true);
            Session["cart"] = null;
        }
       if(Session["email"]!=null)
            email = Session["email"].ToString().Trim();
    }
    #region hàm load thể loại lên menu
    protected string loadMenu()
    {
        string values = "";
        DataTable ds = theLoai.layDSTheLoai();
        foreach (DataRow r in ds.Rows)
        {
            values += @"<li class='header__nav-sub-menu-item'>
                                            <a href='empty.aspx?modul=tl&ml=" + r["matheloai"].ToString() + "'" + @"  >" + r["tentheLoai"] + @"
                    </a>
                                        </li>";
        }
        return values;
    }
    #endregion
    #region hàm load nhà xuất bản lên menu
    protected string loadMenu1()
    {
        string values = "";
        DataTable ds = nxb.layDSNXB();
        foreach (DataRow r in ds.Rows)
        {
            values += @"<li class='header__nav-sub-menu-item'>
                                            <a href='empty.aspx?modul=nxb&mnxb=" + r["manxb"].ToString() + "'" + @"  >" + r["tennxb"] + @"
                    </a>
                                        </li>";
        }
        return values;
    }
    #endregion
    #region hàm kiểm tra là admin?
    protected string ktraAdmin(string admin)
    {
        string values = "";
        if (Session["qtc"] != null && Session["qtc"].ToString().Equals(admin))
            values = "menu-admin";
        return values;
    }
    #endregion
    #region hàm kiểm tra là khách hàng?
    protected string ktraKH(string admin)
    {
        string values = "";
        if (!(Session["qtc"] != null && Session["qtc"].ToString().Equals(admin)))
            values = "menu-kh";

        return values;
    }
    #endregion
    #region hàm tìm kiếm
    protected void timKiem_Click(object sender, EventArgs e)
    {
        if (txtSearch.Value.Equals(""))
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "Thông báo", "<script>alert('Từ khoá không được rỗng')</script>");
        else
            Response.Redirect("empty.aspx?modul=tk&tuKhoa=" + txtSearch.Value.Trim());

    }
    #endregion
    #region hàm load giỏ hàng lần đầu tiên bấm vào giỏ hàng
    protected string loadGioHang()
    {
        if (Session["gioHang"] == null)
            return "";
        string values = "";
        DataTable gioHang = (DataTable)Session["gioHang"];
        sum = 0;
        foreach (DataRow r in gioHang.Rows)
        {
            sum += Convert.ToInt32(r["tongcong"].ToString().Trim());
            values += @"
 <div class='modal__item' id='madong_" + r[1].ToString().Trim() + @"'>
                       <span class='modal__item-id'  >" + r["stt"].ToString() + @"</span>
                        <span class='modal__item-code'>" + r["masach"].ToString() + @"</span>
                        <span class='modal__item-name' >" + r["tensach"].ToString() + @"</span>
                        <span class='modal__item-quanlity' > <span id='quanlity_" + r["masach"].ToString().Trim() + "' >" + r["soluong"].ToString() + @"</span><div> <a href='javascript:tangSL(" + "\"" + r[1].ToString().Trim() + "\"" + @")'><img style='margin-bottom: -4px;' src='img/checkout-uparrow.png' /></a> <a href='javascript:giamSL(" + "\"" + r[1].ToString().Trim() + "\"" + @")'><img src='img/checkout-downarrow.png' /> </a></div>    </span>
                        <span class='modal__item-price' id='modal__item-price_" + r["masach"].ToString().Trim() + "'> " + String.Format("{0:#,##}", Convert.ToInt32(r["dongia"].ToString().Trim())) + @"đ</span>
                        <span class='modal__item-sale' id='modal__item-sale_" + r["masach"].ToString().Trim() + "' >" + r["giamgia"].ToString() + @"</span>
                        <span class='modal__item-total' id='modal__item-total_" + r["masach"].ToString().Trim() + "' >" + String.Format("{0:#,##}", Convert.ToInt32(r["tongcong"].ToString().Trim())) + @"đ</span>
                        <span class='modal__item-setting'><a title='Xoá' href='javascript:xoaGioHang(" + "\"" + r[1].ToString().Trim() + "\"" + @")'><i class='far fa-trash-alt'></i></a></span>
                    </div>";
        }
        if(sum==0)
            lbTongCong.Text = 0 + " VND";
            else
        lbTongCong.Text = String.Format("{0:#,##}", sum) + " VND";
        return values;
    }
    #endregion
    #region hàm load giỏ hàng sau khi đã nhập đầy đủ thông tin thanh toán
    protected string loadGioHang1()
    {
        if (Session["gioHang"] == null)
            return "";
        string values = "";
        DataTable gioHang = (DataTable)Session["gioHang"];
        sum = 0;
        foreach (DataRow r in gioHang.Rows)
        {
            sum += Convert.ToInt32(r["tongcong"].ToString().Trim());
            values += @"
 <div class='modal__item' id='madong_" + r[1].ToString().Trim() + @"'>

                       <span  class='modal__item-id'  >" + r["stt"].ToString() + @"</span>
                        <span class='modal__item-code'>" + r["masach"].ToString() + @"</span>
                        <span class='modal__item-name' >" + r["tensach"].ToString() + @"</span>
                        <span class='modal__item-quanlity' >" + r["soluong"].ToString() + @"</span>
                        <span class='modal__item-price'>" + String.Format("{0:#,##}", Convert.ToInt32(r["dongia"].ToString().Trim())) + @"đ</span>
                        <span class='modal__item-sale' >" + r["giamgia"].ToString() + @"</span>
                        <span class='modal__item-total'>" + String.Format("{0:#,##}", Convert.ToInt32(r["tongcong"].ToString().Trim())) + @"đ</span>
                    </div>";
        }
        lbTongCong1.Text = String.Format("{0:#,##}", sum) + " VND";
        return values;
    }
    #endregion
    #region hàm bấm nút thanh toán 
    protected void btnThanhToan_Click(object sender, EventArgs e)
    {
        //gọi lại hiển thị giỏ hàng 
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cart()", true);

        loadThanhToan();
    }
    #endregion
    #region hàm load thanh toán, kiểm tra thông tin khách hàng đã đăng nhập hay chưa?,phải quyền khách hàng hay không và load thông tin khách hàng
    void loadThanhToan()
    {
        bool check = false;

        if (Session["checkDN"] == null || Session["checkDN"].ToString().Equals(""))
        {
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Thông báo", "<script>alert('Bạn chưa đăng nhập')</script>");
            check = true;
        }
        else
        {
            if (Session["qtc"].ToString().Trim().Equals("admin"))
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Thông báo", "<script>alert('Bạn đang đăng nhập với quyền admin không thể mua hàng')</script>");
                check = true;
            }
            else
            if (Session["gioHang"] == null)
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Thông báo", "<script>alert('Giỏ hàng rỗng')</script>");
                check = true;
            }

        }
        if (check)
            MultiView1.ActiveViewIndex = 0;
        else
        {
            MultiView1.ActiveViewIndex = 1;
            DataTable tb = khacHang.layThongTinKHTheoEmail(Session["email"].ToString());
            lbEmail.Text = Session["email"].ToString();
            lbDiaChi.Text = tb.Rows[0]["diachi"].ToString();
            lbHoTen.Text = tb.Rows[0]["tenkh"].ToString();
            lbSDT.Text = tb.Rows[0]["sdt"].ToString();
        }

    }
    #endregion
    #region hàm quay lại
    protected void btnQuayLai_Click(object sender, EventArgs e)
    {
        //gọi lại hiển thị giỏ hàng 

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cart()", true);
        MultiView1.ActiveViewIndex = 0;
    }
    #endregion
    #region hàm bấm nút tiếp theo để điền thông tin địa chỉ
    protected void btnTiepTheo_Click(object sender, EventArgs e)
    {
        //gọi lại hiển thị giỏ hàng 

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cart()", true);

        MultiView1.ActiveViewIndex = 2;
        DataTable tb = khacHang.layThongTinKHTheoEmail(Session["email"].ToString());
        lbEmailNguoiMua.Text = Session["email"].ToString();
        lbHoTenNguoiMua.Text = tb.Rows[0]["tenkh"].ToString();
        lbSDTNguoiMua.Text = tb.Rows[0]["sdt"].ToString();
        lbDiaChiNguoiMua.Text = txtDiaChi.Text.Trim() + " " + txtQuanHuyen.Text.Trim() + " " + txtThanhPho.Text.Trim();
    }
    #endregion
    #region hàm quay lại
    protected void btnQuayLai1_Click(object sender, EventArgs e)
    {
        //gọi lại hiển thị giỏ hàng 
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cart()", true);
        MultiView1.ActiveViewIndex = 1;
    }
    #endregion
    #region hàm thêm giỏ hàng vào database sau khi đã nhập thông tin
    protected void btnTiepTheo1_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cart()", true);

        MultiView1.ActiveViewIndex = 3;
        sum = 0;
        DataTable gioHang = (DataTable)Session["gioHang"];

        foreach (DataRow r in gioHang.Rows)
        {
            sum += Convert.ToInt32(r["tongcong"].ToString().Trim());
        }
        int i = donDatHang.insert(sum, lbDiaChiNguoiMua.Text, lbEmail.Text);
        if (i != -1)
        {
            string maddh = donDatHang.maDonDatHangVuaTao();
            foreach (DataRow r in gioHang.Rows)
            {
                i = chiTietDDH.insert(maddh, r["masach"].ToString(), Convert.ToInt32(r["soluong"].ToString()), Convert.ToInt32(r["dongia"].ToString()), Convert.ToInt32(r["giamgia"].ToString().Substring(0, r["giamgia"].ToString().Length - 1)), Convert.ToInt32(r["tongcong"].ToString().Trim()));
            }
            if (i != -1)
            {
                Session["gioHang"] = null;
            }
        }

    }
    #endregion
    #region hàm hiển thị thông tin cá nhân của khách hàng     
    protected string loadTTCN()
    {
        string values = "";
        if (Session["email"] == null || Session["qtc"].ToString().Equals("admin"))
            return values;
        DataTable tb = khacHang.layThongTinKHTheoEmail(Session["email"].ToString().Trim());
        values = @"
   <div id='info-customer' class='info-customer'>
            <div class='info-customer-group-title'>
                    <h1>Thông tin cá nhân</h1>
                    <a href='javascript:thoat()' style='color: #0f813f;'>
                        <i class='fas fa-times' id='icon_close-info'></i>
                    </a>
                </div>
            <div class='info-customer__group'>
                <span class='info-customer__group-info-title'>Email:</span>
                <span class='info-customer__group-info'>" + tb.Rows[0]["email"].ToString().Trim() + @"</span>
            </div>
            <div class='info-customer__group'>
                <span class='info-customer__group-info-title'>Tên khách hàng</span>
                <span class='info-customer__group-info'>" + tb.Rows[0]["tenkh"].ToString().Trim() + @"</span>
            </div>
            <div class='info-customer__group'>
                <span class='info-customer__group-info-title'>Giới tính:</span>
                <span class='info-customer__group-info'>" + tb.Rows[0]["gioitinh"].ToString().Trim() + @"</span>
            </div>
            <div class='info-customer__group'>
                <span class='info-customer__group-info-title'>Số điện thoại:</span>
                <span class='info-customer__group-info'>" + tb.Rows[0]["sdt"].ToString().Trim() + @"</span>
            </div>
            <div class='info-customer__group'>
                <span class='info-customer__group-info-title'>Địa chỉ:</span>
                <span class='info-customer__group-info'>" + tb.Rows[0]["diachi"].ToString().Trim() + @"</span>
            </div>
            <div class='info-customer-group-btn'>
                <a href='javascript:thoat()' class='info-customer-group-btn-btn btn'>Quay lại</a>
            </div>
        </div>
";
        return values;
    }
    #endregion
   
}
