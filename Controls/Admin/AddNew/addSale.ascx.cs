using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_AddNew_addSale : System.Web.UI.UserControl
{
    protected string thaoTac = "";
    protected string title = "Thêm mới chương trình khuyến mãi";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["thaotac"] != null)
            thaoTac = Request.QueryString["thaotac"];
        if (thaoTac.Equals("tm"))
            cbContinue.Style.Add(HtmlTextWriterStyle.Display, "block");
        if (thaoTac.Equals("cs"))
        {
            title = "Chỉnh sửa chương trình khuyến mãi";
            btnThemMoi.Text = "Chỉnh sửa";
            DataTable ds = ctKhuyenMai.layCTKMTheoMa(Request.QueryString["ma"]);
            txtMaCTKM.Text = ds.Rows[0]["makm"].ToString();
            txtMaCTKM.Enabled = false;
            txtTenCTKM.Text = ds.Rows[0]["tenkm"].ToString();
            txtGiamGia.Text = ds.Rows[0]["giamgia"].ToString();
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
        txtMaCTKM.Text = "";
        txtTenCTKM.Text = "";
        txtGiamGia.Text = "";
    }
    #endregion
    #region hàm reset cho nút chỉnh sửa
    void reset1()
    {
        txtTenCTKM.Text = "";
        txtGiamGia.Text = "";
    }
    #endregion
    #region hàm bấm vào nút thêm mới
    protected void btnThemMoi_Click(object sender, EventArgs e)
    {
        if (txtMaCTKM.Text.Equals("") || txtTenCTKM.Text.Equals("")|| txtGiamGia.Text.Equals(""))
            return;
        try
        {
            Convert.ToInt32(txtGiamGia.Text.Trim());
        }
            catch(Exception ex)
        {
            return;
        }
        int i = -1;
        if (thaoTac.Equals("cs"))        
            i = ctKhuyenMai.chinhSua(txtMaCTKM.Text.Trim(), txtTenCTKM.Text.Trim(),Convert.ToInt32(txtGiamGia.Text.Trim()));        
        else        
            i = ctKhuyenMai.themMoi(txtMaCTKM.Text.Trim(), txtTenCTKM.Text.Trim(), Convert.ToInt32(txtGiamGia.Text.Trim()));        
        if (i != -1)
        {
            reset();
            Response.Write("<script>alert('Thêm thành công')</script>");
            if ((thaoTac.Equals("tm") && !cbContinue.Checked) || thaoTac.Equals("cs"))
                Response.Redirect("empty.aspx?modul=admin&submodul=qlctkm");
        }
        else
            Response.Write("<script>alert('Trùng mã hoặc tên chương trình khuyến mãi')</script>");
    }
    #endregion

}