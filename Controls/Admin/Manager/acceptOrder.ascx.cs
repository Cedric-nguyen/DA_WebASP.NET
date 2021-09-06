using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_Manager_acceptOrder : System.Web.UI.UserControl
{
    int ma = -1;
    List<DataRow> list = new List<DataRow>();
    protected void Page_Load(object sender, EventArgs e)
    {
            ddlTenNV.Items.Clear();
            foreach (DataRow r in nhanVien.layDSNV().Rows)
            {
                ddlTenNV.Items.Add(r["tennv"].ToString().Trim());
                list.Add(r);
            }
            if (Request.QueryString["ma"] != null)
                ma = Convert.ToInt32(Request.QueryString["ma"].Trim());       
    }
    #region hàm quay lại quản lý danh sách đơn đặt hàng
    protected void btnTroLai_Click(object sender, EventArgs e)
    {
        Response.Redirect("empty.aspx?modul=admin&submodul=qlddh");
    }
    #endregion
    #region hàm bấm vào nút duyệt chuyển đơn đặt hàng đã được duyệt vào hoá đơn trong DB
    protected void btnDuyet_Click(object sender, EventArgs e)
    {
        DataTable donHang = donDatHang.layDonHangTheoMa(ma);
        DataTable dsCTDDH = chiTietDDH.layDSCTDDHTheoMa(ma);
        donDatHang.updateTinhTrangDaGiao(ma);
        string manv = list[ddlTenNV.SelectedIndex]["manv"].ToString();
        int index = donHang.Rows[0]["thanhtien"].ToString().Trim().IndexOf('.');
        int i = hoaDon.insert(Convert.ToInt32(donHang.Rows[0]["thanhtien"].ToString().Trim().Substring(0,index)), donHang.Rows[0]["email"].ToString(), manv);
        string mahd = hoaDon.maHDVuaTao();
        if (i != -1)
        {
            foreach (DataRow r in dsCTDDH.Rows)
            {
               i= chiTietHD.insert(mahd, r["masach"].ToString().Trim(), Convert.ToInt32(r["soluong"].ToString().Trim()), Convert.ToInt32(r["dongia"].ToString().Trim()), Convert.ToInt32(r["giamgia"].ToString().Trim()), Convert.ToInt32(r["thanhtien"].ToString().Trim()));
            }
            if (i != -1)
                Response.Redirect("empty.aspx?modul=admin&submodul=qlhd");
        }
    }
    #endregion
}