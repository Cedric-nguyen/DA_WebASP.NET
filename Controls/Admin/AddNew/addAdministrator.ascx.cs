using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_AddNew_addAdministrator : System.Web.UI.UserControl
{
    protected string thaoTac = "";
    //HttpCookie cookie = null;
    protected string title = "Thêm mới quản trị";
    protected void Page_Load(object sender, EventArgs e)
    {
       // if (Request.Cookies["login"] != null)
         //   cookie = Request.Cookies["login"];
        if (Request.QueryString["thaotac"] != null)
            thaoTac = Request.QueryString["thaotac"];
        if (thaoTac.Equals("tm"))
            cbContinue.Style.Add(HtmlTextWriterStyle.Display, "block");
        if (thaoTac.Equals("cs"))
        {
            title = "Chỉnh sửa quản trị";
            btnThemMoi.Text = "Chỉnh sửa";
            DataTable ds = quanTri.layQTTheoEmail(Request.QueryString["ma"]);
            txtEmail.Text = ds.Rows[0]["email"].ToString();
            txtEmail.Enabled = false;
            txtTenHienThi.Text = ds.Rows[0]["tenhienthi"].ToString();
        }
    }
    #region hàm xử lý xoá trắng
    protected void btnHuy_Click(object sender, EventArgs e)
    {
        if (thaoTac.Equals("cs"))
            reset1();
        else
            reset();
    }
    #endregion
    #region hàm reset dành cho tạo mới
    void reset()
    {
        txtEmail.Text = "";
        txtPW.Text = "";
        txtPrePW.Text = "";
        txtTenHienThi.Text = "";       
    }
    #endregion
    #region hàm reset dành cho chỉnh sửa
    void reset1()
    {
        txtPW.Text = "";
        txtPrePW.Text = "";
        txtTenHienThi.Text = "";
    }
    #endregion
    #region hàm bấm vào nút thêm mới
    protected void btnThemMoi_Click(object sender, EventArgs e)
    {
        if (txtEmail.Text.Equals("") || txtPW.Text.Equals("")||txtPrePW.Text.Equals("")||txtTenHienThi.Text.Equals(""))
            return;
        if (!txtPW.Text.Trim().Equals(txtPrePW.Text.Trim()))
            return;
        int i = -1;
        if (thaoTac.Equals("cs"))
        {   
            i = quanTri.chinhSua(txtEmail.Text.Trim(),maHoaMD5.MaHoaMD5(txtPW.Text.Trim()),txtTenHienThi.Text.Trim());
            // if (cookie != null && cookie["email"].ToString().Equals(txtEmail.Text))
             if (Session != null && Session["email"].ToString().Equals(txtEmail.Text))
            {
                //HttpCookie cookies = new HttpCookie("login");
                //cookies["email"] = cookie["email"].ToString().Trim();
                //Session["tenhienthi"] = txtTenHienThi.Text.Trim();
                //cookies["checkDN"] = "1";
                //cookies["qtc"] = "admin";
                //cookies.Expires = DateTime.Now.AddDays(365);
                //Response.Cookies.Add(cookies);                 
                Session["tenhienthi"] = txtTenHienThi.Text.Trim();                                
            }
        }
        else        
            i = quanTri.themMoi(txtEmail.Text.Trim(), maHoaMD5.MaHoaMD5(txtPW.Text.Trim()), txtTenHienThi.Text.Trim());        
        if (i != -1)
        {
            reset();
            Response.Write("<script>alert('Thêm thành công')</script>");
            if ((thaoTac.Equals("tm") && !cbContinue.Checked) || thaoTac.Equals("cs"))
                Response.Redirect("empty.aspx?modul=admin&submodul=qldsqt");
        }
        else
            Response.Write("<script>alert('Trùng email')</script>");
    }
    #endregion
}