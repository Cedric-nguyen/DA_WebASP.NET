using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_AddNew_addCategoryBook : System.Web.UI.UserControl
{
    protected string thaoTac = "";
    protected string title = "Thêm mới thể loại";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["thaotac"] != null)
            thaoTac = Request.QueryString["thaotac"];
        if (thaoTac.Equals("tm"))
            cbContinue.Style.Add(HtmlTextWriterStyle.Display, "block");
        if (thaoTac.Equals("cs"))
        {
            title = "Chỉnh sửa thể loại";
            btnThemMoi.Text = "Chỉnh sửa";
            DataTable ds = theLoai.layTLTheoMa(Request.QueryString["ma"]);
            txtMaTheLoai.Text = ds.Rows[0]["matheloai"].ToString();
            txtMaTheLoai.Enabled = false;
            txtTenTheLoai.Text = ds.Rows[0]["tentheloai"].ToString();
        }
    }
    #region hàm xoá trắng
    protected void btnHuy_Click(object sender, EventArgs e)
    {
        if (thaoTac.Equals("cs"))
            reset1();
        else
            reset();
    }
    #endregion
    #region hàm reset cho nút thêm mới
    void reset()
    {
        txtMaTheLoai.Text = "";
        txtTenTheLoai.Text = "";
    }
    #endregion
    #region hàm reset cho nút chỉnh sửa
    void reset1()
    {
        txtTenTheLoai.Text = "";
    }
    #endregion
    #region hàm bấm vào nút thêm mới
    protected void btnThemMoi_Click(object sender, EventArgs e)
    {
        if (txtMaTheLoai.Text.Equals("") || txtTenTheLoai.Text.Equals(""))
            return;
        int i = -1;
        if (thaoTac.Equals("cs"))
        {
            i = theLoai.chinhSua(txtMaTheLoai.Text.Trim(), txtTenTheLoai.Text.Trim());
        }
        else
        {
            i = theLoai.themMoi(txtMaTheLoai.Text.Trim(), txtTenTheLoai.Text.Trim());
        }
        if (i != -1)
        {
            reset();
            Response.Write("<script>alert('Thêm thành công')</script>");
            if ((thaoTac.Equals("tm") && !cbContinue.Checked) || thaoTac.Equals("cs"))
                Response.Redirect("empty.aspx?modul=admin&submodul=qldmb");
        }
        else
            Response.Write("<script>alert('Trùng mã hoặc tên thể loại')</script>");
    }
    #endregion
}