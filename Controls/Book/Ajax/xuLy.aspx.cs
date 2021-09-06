using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Controls_Book_Ajax_xuLy : System.Web.UI.Page
{
    protected string thaoTac = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        thaoTac = Request.Params["thaotac"].ToString();
        switch (thaoTac)
        {
            case "themGioHang":
                themGioHang();
                break;
            case "muaNgay":
                muaNgay();
                break;
            case "gioHang":
                Session["cart"] = "1";
                break;
            case "xoaGioHang":
                xoaGioHang();
                break;
            case "tangSL":
                tangSL();
                break;
            case "giamSL":
                giamSL();
                break;
        }
    }
    #region hàm tăng số lượng 1 sản phẩm trong giỏ hàng theo mã sản phẩm khi được gọi
    private void tangSL()
    {
        string masach = Request.Params["masach"].ToString().Trim();
        DataTable gioHang = (DataTable)Session["gioHang"];
        DataTable dsSach = sach.laySachTheoMa(masach);
        int vt = -1;
        for (int i = 0; i < gioHang.Rows.Count; i++)
        {
            if (gioHang.Rows[i]["masach"].ToString().Trim().Equals(masach))
            {
                vt = i;
                break;
            }
        }

        int sl = Convert.ToInt32(gioHang.Rows[vt]["soluong"].ToString()) + 1;

        gioHang.Rows[vt]["soluong"] = sl;
        int donGia = Convert.ToInt32(gioHang.Rows[vt]["dongia"].ToString());
        double thanhtien = donGia * sl - donGia * sl * Convert.ToInt32(dsSach.Rows[0]["giamgia"].ToString()) / 100.0;
        gioHang.Rows[vt]["tongcong"] = thanhtien;
        int sum = 0;
        for (int i = 0; i < gioHang.Rows.Count; i++)
            sum += Convert.ToInt32(gioHang.Rows[i]["tongcong"].ToString().Trim());
        Session["gioHang"] = gioHang;
        string dinhDang = String.Empty;
        dinhDang = String.Format("{0:#,##}", thanhtien) + " đ-" + String.Format("{0:#,##}", sum) + " VND";
        Response.Write(dinhDang);
    }
    #endregion

    #region hàm giảm số lượng 1 sản phẩm trong giỏ hàng theo mã sản phẩm khi được gọi
    private void giamSL()
    {
        string masach = Request.Params["masach"].ToString().Trim();
        DataTable gioHang = (DataTable)Session["gioHang"];
        DataTable dsSach = sach.laySachTheoMa(masach);
        int vt = -1;
        for (int i = 0; i < gioHang.Rows.Count; i++)
        {
            if (gioHang.Rows[i]["masach"].ToString().Trim().Equals(masach))
            {
                vt = i;
                break;
            }
        }

        int sl = Convert.ToInt32(gioHang.Rows[vt]["soluong"].ToString());
        double thanhtien = 0;
        if (sl != 1)
        {
            sl -= 1;
            gioHang.Rows[vt]["soluong"] = sl;
            int donGia = Convert.ToInt32(gioHang.Rows[vt]["dongia"].ToString());
            thanhtien = donGia * sl - donGia * sl * Convert.ToInt32(dsSach.Rows[0]["giamgia"].ToString()) / 100.0;
            gioHang.Rows[vt]["tongcong"] = thanhtien;
        }
        else
            gioHang.Rows.RemoveAt(vt);
        int sum = 0;
        for (int i = 0; i < gioHang.Rows.Count; i++)
            sum += Convert.ToInt32(gioHang.Rows[i]["tongcong"].ToString().Trim());
        string dinhDang = String.Empty;
        if (thanhtien == 0 && sum != 0)
            dinhDang = 0 + "-" + String.Format("{0:#,##}", sum) + " VND";
        else
            dinhDang = String.Format("{0:#,##}", thanhtien) + " đ-" + String.Format("{0:#,##}", sum) + " VND";
        if (sum == 0)
        {
            Session["gioHang"] = null;
            dinhDang = 0 + "-0VND";
        }
        else
            Session["gioHang"] = gioHang;
        Response.Write(dinhDang);
    }
    #endregion  
    #region hàm xoá 1 sản phẩm trong giỏ hàng khi được gọi
    private void xoaGioHang()
    {
        string masach = Request.Params["masach"].ToString().Trim();
        DataTable tb = (DataTable)Session["gioHang"];
        foreach (DataRow r in tb.Rows)
            if (r[1].ToString().Trim().Equals(masach))
            {
                tb.Rows.Remove(r);
                break;
            }
        int sum = 0;
        for (int i = 0; i < tb.Rows.Count; i++)
        {
            tb.Rows[i][0] = (i + 1);
            sum += Convert.ToInt32(tb.Rows[i]["tongcong"].ToString().Trim());
        }
        if (sum == 0)
            Session["gioHang"] = null;
        else
            Session["gioHang"] = tb;
        string dinhDang = String.Empty;
        if (sum == 0)
            dinhDang = "0";
        else
            dinhDang = String.Format("{0:#,##}", sum);
        Response.Write(dinhDang + "VND");
    }
    #endregion    
    #region hàm xử lý khi người dùng thêm 1 cuốn sách vào giỏ hàng
    private void themGioHang()
    {
        string maSach = Request.Params["masach"].ToString();
        int soLuong = Convert.ToInt32(Request.Params["soluong"].ToString());
        int vt = -1;
        DataTable dsSach = sach.laySachTheoMa(maSach);
        if (Session["gioHang"] == null)//chưa có giỏ hàng
        {

            DataTable tb = new DataTable();
            tb.Columns.Add("stt");
            tb.Columns.Add("masach");
            tb.Columns.Add("tensach");
            tb.Columns.Add("soluong");
            tb.Columns.Add("dongia");
            tb.Columns.Add("giamgia");
            tb.Columns.Add("tongcong");
            tb.Rows.Add(1, dsSach.Rows[0]["masach"].ToString(), dsSach.Rows[0]["tensach"].ToString()
                , soLuong, dsSach.Rows[0]["dongia"].ToString(), dsSach.Rows[0]["giamgia"].ToString() + "%", soLuong * Convert.ToInt32(dsSach.Rows[0]["dongia"].ToString()) - soLuong * Convert.ToInt32(dsSach.Rows[0]["dongia"].ToString()) * Convert.ToInt32(dsSach.Rows[0]["giamgia"].ToString()) / 100.0);
            Session["gioHang"] = tb;
        }
        else //đã có giỏ hàng
        {
            DataTable gioHang = (DataTable)Session["gioHang"];
            //check xem có sách đó chưa
            for (int i = 0; i < gioHang.Rows.Count; i++)
            {
                if (gioHang.Rows[i]["masach"].ToString().Trim().Equals(maSach))
                {
                    vt = i;
                    break;
                }
            }
            if (vt == -1)//chưa có mã sách đó
            {
                gioHang.Rows.Add((gioHang.Rows.Count + 1), dsSach.Rows[0]["masach"].ToString(), dsSach.Rows[0]["tensach"].ToString()
                    , soLuong, dsSach.Rows[0]["dongia"].ToString(), dsSach.Rows[0]["giamgia"].ToString() + "%", soLuong * Convert.ToInt32(dsSach.Rows[0]["dongia"].ToString()) - soLuong * Convert.ToInt32(dsSach.Rows[0]["dongia"].ToString()) * Convert.ToInt32(dsSach.Rows[0]["giamgia"].ToString()) / 100.0);
            }
            else
            {//có mã sách đó
                int slht = Convert.ToInt32(gioHang.Rows[vt]["soluong"].ToString());
                slht += soLuong;
                gioHang.Rows[vt]["soluong"] = slht;
                int donGia = Convert.ToInt32(gioHang.Rows[vt]["dongia"].ToString());
                gioHang.Rows[vt]["tongcong"] = donGia * slht - donGia * slht * Convert.ToInt32(dsSach.Rows[0]["giamgia"].ToString()) / 100.0;
            }
            Session["gioHang"] = gioHang;
        }
        //trả về thông báo đã thêm thành công
        Response.Write(1);
    }
    #endregion
    #region hàm thêm 1 cuốn sách vào giỏ hàng và đồng thời hiển thị giỏ hàng lên cho người dùng thanh toán
    private void muaNgay()
    {
        string maSach = Request.Params["masach"].ToString();
        int soLuong = Convert.ToInt32(Request.Params["soluong"].ToString());
        int vt = -1;
        DataTable dsSach = sach.laySachTheoMa(maSach);
        if (Session["gioHang"] == null)//chưa có giỏ hàng
        {

            DataTable tb = new DataTable();
            tb.Columns.Add("stt");
            tb.Columns.Add("masach");
            tb.Columns.Add("tensach");
            tb.Columns.Add("soluong");
            tb.Columns.Add("dongia");
            tb.Columns.Add("giamgia");
            tb.Columns.Add("tongcong");
            tb.Rows.Add(1, dsSach.Rows[0]["masach"].ToString(), dsSach.Rows[0]["tensach"].ToString()
                , soLuong, dsSach.Rows[0]["dongia"].ToString(), dsSach.Rows[0]["giamgia"].ToString() + "%", soLuong * Convert.ToInt32(dsSach.Rows[0]["dongia"].ToString()) - soLuong * Convert.ToInt32(dsSach.Rows[0]["dongia"].ToString()) * Convert.ToInt32(dsSach.Rows[0]["giamgia"].ToString()) / 100.0);
            Session["gioHang"] = tb;
        }
        else //đã có giỏ hàng
        {
            DataTable gioHang = (DataTable)Session["gioHang"];
            //check xem có sách đó chưa
            for (int i = 0; i < gioHang.Rows.Count; i++)
            {
                if (gioHang.Rows[i]["masach"].ToString().Trim().Equals(maSach))
                {
                    vt = i;
                    break;
                }
            }
            if (vt == -1)//chưa có mã sách đó
            {
                gioHang.Rows.Add((gioHang.Rows.Count + 1), dsSach.Rows[0]["masach"].ToString(), dsSach.Rows[0]["tensach"].ToString()
                    , soLuong, dsSach.Rows[0]["dongia"].ToString(), dsSach.Rows[0]["giamgia"].ToString() + "%", soLuong * Convert.ToInt32(dsSach.Rows[0]["dongia"].ToString()) - soLuong * Convert.ToInt32(dsSach.Rows[0]["dongia"].ToString()) * Convert.ToInt32(dsSach.Rows[0]["giamgia"].ToString()) / 100.0);
            }
            else
            {//có mã sách đó
                int slht = Convert.ToInt32(gioHang.Rows[vt]["soluong"].ToString());
                slht += soLuong;
                gioHang.Rows[vt]["soluong"] = slht;
                int donGia = Convert.ToInt32(gioHang.Rows[vt]["dongia"].ToString());
                gioHang.Rows[vt]["tongcong"] = donGia * slht - donGia * slht * Convert.ToInt32(dsSach.Rows[0]["giamgia"].ToString()) / 100.0;
            }
            Session["gioHang"] = gioHang;
        }
        //báo hiệu người dùng muốn thanh toán (mua ngay)
        Session["cart"] = "1";
        //trả về thông báo đã thêm thành công
        Response.Write(1);
    }
    #endregion
}