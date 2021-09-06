using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Controls_Admin_AddNew_addBook : System.Web.UI.UserControl
{
    protected string thaoTac = "";
    protected string title = "Thêm mới sách";
    protected void Page_Load(object sender, EventArgs e)
    {
        loadItem();
        if (Request.QueryString["thaotac"] != null)
            thaoTac = Request.QueryString["thaotac"];
        if (thaoTac.Equals("tm"))
            cbContinue.Style.Add(HtmlTextWriterStyle.Display, "block");
        // load lại thông tin chi tiết sách muốn chỉnh sửa
        if (thaoTac.Equals("cs"))
        {
            title = "Chỉnh sửa sách";
            btnThemMoi.Text = "Chỉnh sửa";
            DataTable ds = sach.laySachTheoMa(Request.QueryString["ma"]);
            txtMaSach.Text = ds.Rows[0]["masach"].ToString();
            txtMaSach.Enabled = false;
            txtTenSach.Text = ds.Rows[0]["tensach"].ToString();
            txtDonViTinh.Text = ds.Rows[0]["donvitinh"].ToString();
            txtMoTa.Text = ds.Rows[0]["mota"].ToString();
            txtMoTa.Text = ds.Rows[0]["mota"].ToString();
            imgHinhMinhHoa.ImageUrl= "..\\..\\..\\img\\"+ds.Rows[0]["hinhminhhoa"].ToString().Trim();
            txtNamXB.Text = DateTime.Parse(ds.Rows[0]["NAMXB"].ToString().Trim()).ToShortDateString();
            txtNgayCapNhat.Text = DateTime.Parse(ds.Rows[0]["NGAYCAPNHAT"].ToString().Trim()).ToShortDateString() ;
            txtTenTacGia.Text = ds.Rows[0]["TENTACGIA"].ToString();
            string tenTheLoai= ds.Rows[0]["tentheloai"].ToString().Trim();
            ddlTenTheLoai.SelectedValue = tenTheLoai;
            string tennxb = ds.Rows[0]["TENNXB"].ToString().Trim();
            ddlTenNSX.SelectedValue = tennxb;
            txtDonGia.Text = ds.Rows[0]["dongia"].ToString().Trim();
            DataTable ds1= ctKhuyenMai.layCTKMTheoMa(ds.Rows[0]["makm"].ToString().Trim());
           if(ds1.Rows.Count>0)
                ddlTenCTKM.SelectedValue = ds1.Rows[0]["tenkm"].ToString();
            HiddenField1.Value = ds.Rows[0]["hinhminhhoa"].ToString().Trim();
        }

    }
    #region hàm load thông tin chương trình khuyến mãi,thể loại vào combobox
    void loadItem()
    {
        DataTable ds = nxb.layDSNXB();
        ddlTenNSX.Items.Clear();
        foreach (DataRow r in ds.Rows)
        {
            ddlTenNSX.Items.Add(r["tennxb"].ToString().Trim());
        }
        ds = theLoai.layDSTheLoai();
        ddlTenTheLoai.Items.Clear();
        foreach (DataRow r in ds.Rows)
        {
            ddlTenTheLoai.Items.Add(r["tentheloai"].ToString().Trim());
        }
        ds = ctKhuyenMai.layChuongTrinhKhuyenMai();
        ddlTenCTKM.Items.Clear();
        ddlTenCTKM.Items.Add("----Chọn tên khuyến mãi nếu có----");
        foreach (DataRow r in ds.Rows)
        {
            ddlTenCTKM.Items.Add(r["tenkm"].ToString().Trim());
        }

    }
    #endregion
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
        txtDonGia.Text = txtMaSach.Text = txtTenSach.Text = txtDonViTinh.Text = txtMoTa.Text = txtNamXB.Text = txtNgayCapNhat.Text = txtTenTacGia.Text = "";
        ddlTenCTKM.SelectedIndex = ddlTenNSX.SelectedIndex = ddlTenTheLoai.SelectedIndex = 0;
        HiddenField1.Value = "";
        imgHinhMinhHoa.ImageUrl = "";
    }
    #endregion
    #region hàm reset cho nút chỉnh sửa

    void reset1()
    {
       txtDonGia.Text= txtTenSach.Text = txtDonViTinh.Text = txtMoTa.Text = txtNamXB.Text = txtNgayCapNhat.Text = txtTenTacGia.Text = "";
        ddlTenCTKM.SelectedIndex = ddlTenNSX.SelectedIndex = ddlTenTheLoai.SelectedIndex = 0;
        HiddenField1.Value = "";
        imgHinhMinhHoa.ImageUrl = "";
    }
    #endregion
    #region hàm bấm vào nút thêm mới
    protected void btnThemMoi_Click(object sender, EventArgs e)
    {
        if (txtMaSach.Text.Equals("") || txtTenSach.Text.Equals("") || txtDonViTinh.Text.Equals("") || txtNamXB.Text.Equals("") || txtNgayCapNhat.Text.Equals("") || txtTenTacGia.Text.Equals(""))
            return;
        int i = -1;
        int dongia = 0;
        try {
            dongia = Convert.ToInt32(txtDonGia.Text.Trim());
        }
        catch (Exception ex)
        {
            return;
        }
        
        try
        {
            DateTime.Parse(txtNamXB.Text.Trim());
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Năm xuất bản không đúng định dạng')</script>");
            return;
        }
        try
        {
            DateTime.Parse(txtNgayCapNhat.Text.Trim());

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Ngày cập nhật không đúng định dạng')</script>");
            return;
        }
        if (thaoTac.Equals("cs"))
        {
            if (!FileUpload1.FileName.Equals(""))
            {
                HiddenField1.Value = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("img/" + FileUpload1.FileName));
            }
            DataTable ds = ctKhuyenMai.timMaTheoTen(ddlTenCTKM.Text.Trim());
            string makm = "";
            int giamGia = 0;
            if (ds.Rows.Count > 0)
            {
                makm = ds.Rows[0]["makm"].ToString();
                giamGia = Convert.ToInt32(ds.Rows[0]["giamgia"].ToString());
            }
                i = sach.chinhSua(txtMaSach.Text,txtTenSach.Text.Trim(),dongia,txtDonViTinh.Text.Trim(),txtMoTa.Text.Trim(),HiddenField1.Value,txtNamXB.Text.Trim(),txtNgayCapNhat.Text.Trim(),txtTenTacGia.Text.Trim(),theLoai.timMaTheoTen(ddlTenTheLoai.SelectedValue.Trim()),nxb.timMaTheoTen(ddlTenNSX.SelectedValue.Trim()),makm,giamGia);
        }
        else
        {
            if (FileUpload1.FileName.Equals(""))
            {
                Response.Write("<script>Bạn chưa upload file ảnh</script>");
                return;
            }
            DataTable ds = ctKhuyenMai.timMaTheoTen(ddlTenCTKM.SelectedValue.Trim());
            string makm = "";
            int giamGia = 0;
            if (ds.Rows.Count > 0)
            {
                makm = ds.Rows[0]["makm"].ToString();
                giamGia = Convert.ToInt32(ds.Rows[0]["giamgia"].ToString());
            }
            HiddenField1.Value = FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath("img/" + FileUpload1.FileName));
            i = sach.themMoi(txtMaSach.Text, txtTenSach.Text.Trim(), dongia, txtDonViTinh.Text.Trim(), txtMoTa.Text.Trim(), HiddenField1.Value, txtNamXB.Text.Trim(), txtNgayCapNhat.Text.Trim(), txtTenTacGia.Text.Trim(), theLoai.timMaTheoTen(ddlTenTheLoai.SelectedValue.Trim()), nxb.timMaTheoTen(ddlTenNSX.SelectedValue.Trim()), makm, giamGia);
        }
        if (i != -1)
        {
            reset();
            Response.Write("<script>alert('Thêm thành công')</script>");
            if ((thaoTac.Equals("tm") && !cbContinue.Checked) || thaoTac.Equals("cs"))
                Response.Redirect("empty.aspx?modul=admin&submodul=qldsb");
        }
        else
            Response.Write("<script>alert('Trùng mã hoặc tên sách')</script>");
    }


    #endregion

}