using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Admin_Manager_Ajax_xuLy : System.Web.UI.Page
{
    string thaoTac = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        thaoTac = Request.Params["thaotac"].ToString();
        switch (thaoTac)
        {
            case "xoaSach":
                xoaSach();
                break;
            case "xoaTheLoai":
                xoaTheLoai();
                break;
            case "xoaNV":
                xoaNV();
                break;
            case "xoakh":
                xoaKH();
                break;
            case "xoaNXB":
                xoaNXB();
                break;
            case "xoaQuanTri":
                xoaQT();
                break;
            case "xoaCTKM":
                xoaCTKM();
                break;
            case "xoaDDH":
                xoaDDH();
                break;

            case "xoaHD":
                xoaHD();
                break;
            case "reset":
                reset();
                break;
        }
    }

    private void xoaSach()
    {
        string masach = Request.Params["masach"].ToString();
        int i = sach.xoaSach(masach);
        if (i != -1)
            Response.Write(1);
        else
            Response.Write(0);
    }
    private void xoaTheLoai()
    {
        string matheloai = Request.Params["matheloai"].ToString();
        int i = theLoai.xoaTheLoai(matheloai);
        if (i != -1)
            Response.Write(1);
        else
            Response.Write(0);
    }
    private void xoaNV()
    {
        string manv = Request.Params["manv"].ToString();
        int i = nhanVien.xoaNV(manv);
        if (i != -1)
            Response.Write(1);
        else
            Response.Write(0);
    }
    private void xoaKH()
    {
        string makh = Request.Params["makh"].ToString();
        int i = khacHang.xoaKH(makh);
        if (i != -1)
            Response.Write(1);
        else
            Response.Write(0);
    }
    private void xoaNXB()
    {
        string manxb = Request.Params["manxb"].ToString();
        int i = nxb.xoaNXB(manxb);
        if (i != -1)
            Response.Write(1);
        else
            Response.Write(0);
    }
    private void xoaQT()
    {
        string email = Request.Params["email"].ToString();
        int i = quanTri.xoaQT(email);
        if (i != -1)
            Response.Write(1);
        else
            Response.Write(0);
    }
    private void xoaCTKM()
    {
        string ma = Request.Params["makm"].ToString();
        int i = ctKhuyenMai.xoaCTKM(ma);
        if (i != -1)
            Response.Write(1);
        else
            Response.Write(0);
    }
    private void xoaDDH()
    {
        string ma = Request.Params["maddh"].ToString();
        int i = donDatHang.xoaDDH(ma);
        if (i != -1)
            Response.Write(1);
        else
            Response.Write(0);
    }
    private void xoaHD()
    {
        string ma = Request.Params["mahd"].ToString();
        int i = hoaDon.xoaHD(ma);
        if (i != -1)
            Response.Write(1);
        else
            Response.Write(0);
    }
    private void reset()
    {
        string ma = Request.Params["email"].ToString();
        int i = khacHang.resetMK(ma);
        if (i != -1)
            Response.Write(1);
        else
            Response.Write(0);
    }

}