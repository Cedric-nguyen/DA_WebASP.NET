using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_AddNew_publishcompany : System.Web.UI.UserControl
{
    protected string thaoTac = "";
    protected string title = "Thêm mới nhà xuất bản";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["thaotac"] != null)
            thaoTac = Request.QueryString["thaotac"];
        if (thaoTac.Equals("tm"))
            cbContinue.Style.Add(HtmlTextWriterStyle.Display, "block");
        if (thaoTac.Equals("cs"))
        {
            title = "Chỉnh sửa nhà xuất bản";
            btnThemMoi.Text = "Chỉnh sửa";
            DataTable ds = nxb.layNXBTheoMa(Request.QueryString["ma"]);
            txtMaNXB.Text = ds.Rows[0]["manxb"].ToString();
            txtMaNXB.Enabled = false;
            txtTenNXB.Text = ds.Rows[0]["tennxb"].ToString();

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
        txtMaNXB.Text = "";
        txtTenNXB.Text = "";
    }
    #endregion
    #region hàm reset cho nút chỉnh sửa
    void reset1()
    {
        txtTenNXB.Text = "";
    }
    #endregion
    #region hàm bấm vào nút thêm mới hoặc chỉnh sửa
    protected void btnThemMoi_Click(object sender, EventArgs e)
    {
        if (txtMaNXB.Text.Equals("") || txtTenNXB.Text.Equals(""))
            return;
        int i = -1;
        if (thaoTac.Equals("cs"))
        {
            i = nxb.chinhSua(txtMaNXB.Text.Trim(), txtTenNXB.Text.Trim());         
        }
        else
        {
            i = nxb.themMoi(txtMaNXB.Text.Trim(), txtTenNXB.Text.Trim());
        }
        if (i != -1)
        {
            reset();
            Response.Write("<script>alert('Thêm thành công')</script>");
            if ((thaoTac.Equals("tm") && !cbContinue.Checked)||thaoTac.Equals("cs"))
                Response.Redirect("empty.aspx?modul=admin&submodul=qldsnxb");
        }
        else
            Response.Write("<script>alert('Trùng mã hoặc tên nhà xuất bản')</script>");
    }
    #endregion

}