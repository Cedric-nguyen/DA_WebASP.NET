using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Account_Ajax_xuLy : System.Web.UI.Page
{
    string thaotac = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        thaotac = Request.Params["thaotac"].ToString();
        switch (thaotac)
        {
            case "doimatkhau":
                doiMatKhau();
                break;
        }
    }

    #region hàm xử lý đổi mật khẩu
    void doiMatKhau()
    {
        bool check = false;
        string values = "";
        string matKhauCu = Request.Params["matkhaucu"].ToString();
        string matKhauMoi = Request.Params["matkhaumoi"].ToString();
        string nhapLaiMK = Request.Params["nhaplaimk"].ToString();
        string email = Request.Params["email"].ToString();

        if (matKhauCu.Equals(""))
            values = "Mật khẩu cũ không được để trống.";
        else
            if (matKhauMoi.Equals(""))
            values = "Mật khẩu mới không được để trống.";
        else
            if (nhapLaiMK.Equals(""))
            values = "Nhập lại mật khẩu không được để trống.";
        else
            if (!maHoaMD5.MaHoaMD5(matKhauCu).Equals(khacHang.checkMK(email)))
             values= "Mật khẩu không chính xác";
        else
            if(!matKhauMoi.Equals(nhapLaiMK))
            values = "Nhập lại mật khẩu không giống nhau.";
        else
        {
            check = true;
            khacHang.doiMatKhau(email,maHoaMD5.MaHoaMD5(matKhauMoi));
            //HttpCookie cookie = new HttpCookie("login");
            //cookie["email"] = "";
            //Session["tenhienthi"] = "";
            //cookie["checkDN"] = "";
            //cookie["qtc"] = "";
            //cookie.Expires = DateTime.Now.AddDays(365);
            //Response.Cookies.Add(cookie);            
            Session["qtc"] = Session["checkDN"] = Session["email"] = Session["tenhienthi"] = null;            
        }
        if (!check)
            Response.Write(values);
        else
            Response.Write(1);
    }
    #endregion
}