using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class Controls_Admin_AddNew_addemployee : System.Web.UI.UserControl
{
    protected string thaoTac = "";
    protected string title = "Thêm mới nhân viên";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["thaotac"] != null)
            thaoTac = Request.QueryString["thaotac"];
        if (thaoTac.Equals("tm"))
            cbContinue.Style.Add(HtmlTextWriterStyle.Display, "block");
        if (thaoTac.Equals("cs"))
        {
            title = "Chỉnh sửa nhân viên";
            btnThemMoi.Text = "Chỉnh sửa";
            DataTable ds = nhanVien.layNVTheoMa(Request.QueryString["ma"]);
            txtMaNV.Text = ds.Rows[0]["manv"].ToString();
            txtMaNV.Enabled = false;
            txtTenNV.Text = ds.Rows[0]["tennv"].ToString().Trim();
            txtDiaChi.Text = ds.Rows[0]["diachi"].ToString();
            txtSDT.Text = ds.Rows[0]["sdt"].ToString().Trim();
            string gioiTinh= ds.Rows[0]["gioitinh"].ToString().Trim().ToLower();
            if (gioiTinh.Equals("nam"))
                rdGioiTinh.SelectedIndex = 0;
            else
                rdGioiTinh.SelectedIndex = 1;            
            txtNgayVL.Text = DateTime.Parse(ds.Rows[0]["ngayvl"].ToString().Trim()).ToShortDateString();
            string luong = ds.Rows[0]["luong"].ToString().Trim();
            int vt = luong.IndexOf('.');
            txtLuong.Text = ds.Rows[0]["luong"].ToString().Substring(0, vt);
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
        txtMaNV.Text = txtTenNV.Text = txtDiaChi.Text = txtNgayVL.Text = txtSDT.Text = txtLuong.Text = "";
        rdGioiTinh.SelectedIndex = 0;
    }
    #endregion
    #region hàm reset cho nút chỉnh sửa
    void reset1()
    {
       txtTenNV.Text = txtDiaChi.Text = txtNgayVL.Text = txtSDT.Text = txtLuong.Text = "";
        rdGioiTinh.SelectedIndex = 0;
    }
    #endregion
    #region hàm bấm vào nút thêm mới
    protected void btnThemMoi_Click(object sender, EventArgs e)
    {
        if (txtMaNV.Text.Equals("") || txtTenNV.Text.Equals("") || txtDiaChi.Text.Equals("") || txtNgayVL.Text.Equals("") || txtSDT.Text.Equals("") || txtLuong.Text.Equals(""))
            return;
        try
        {
            Convert.ToInt32(txtLuong.Text.Trim());
        }
        catch (Exception ex)
        {
            return;
        }
        try
        {
            DateTime.Parse(txtNgayVL.Text.Trim());
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Ngày không đúng định dạng')</script>");
            return;
        }
        
            string ktra = "0\\d{9,10}";
        if( !Regex.IsMatch(txtSDT.Text.Trim(), ktra))        
            return;   
        
        int i = -1;
        string gioiTinh = "Nữ";
        if (rdGioiTinh.SelectedIndex == 0)
            gioiTinh = "Nam";
        if (thaoTac.Equals("cs"))
            i = nhanVien.chinhSua(txtMaNV.Text.Trim(),txtTenNV.Text.Trim(),txtDiaChi.Text.Trim(),txtSDT.Text.Trim(),gioiTinh,txtNgayVL.Text.Trim(),Convert.ToInt32(txtLuong.Text.Trim()));
        else
            i = nhanVien.themMoi(txtMaNV.Text.Trim(), txtTenNV.Text.Trim(), txtDiaChi.Text.Trim(), txtSDT.Text.Trim(), gioiTinh, txtNgayVL.Text.Trim(), Convert.ToInt32(txtLuong.Text.Trim()));
        if (i != -1)
        {
            reset();
            Response.Write("<script>alert('Thêm thành công')</script>");
            if ((thaoTac.Equals("tm") && !cbContinue.Checked) || thaoTac.Equals("cs"))
                Response.Redirect("empty.aspx?modul=admin&submodul=qldsnv");
        }
        else
            Response.Write("<script>alert('Trùng mã nhân viên')</script>");
    }
    #endregion
}