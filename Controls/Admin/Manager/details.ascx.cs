using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_Manager_details : System.Web.UI.UserControl
{
    int tongSoCT = 0;
    int soLuongCTTungTrang = 6;
    int soPage = 0;
    int trangHienTai = 0;
    string thaoTac = "", ma = "";
    List<DataRow> listCT = new List<DataRow>();
    int soBtn = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            if (Request.QueryString["thaotac"] != null)
                thaoTac = Request.QueryString["thaotac"].Trim();
            if (Request.QueryString["ma"] != null)
                ma = Request.QueryString["ma"].Trim();
            loadCTVaoList();
        }

    }

    #region lấy dữ liệu bảng chi tiết hoá đơn/đơn đặt hàng từ database lên lưu vào listCT và tính soPage,tongSoCT...
    void loadCTVaoList()
    {
        DataTable dsCT = null;
        if (thaoTac.Equals("cthd"))
        {
            dsCT = chiTietHD.layDSCTHDTheoMa(Convert.ToInt32(ma));
        }
        else
            dsCT = chiTietDDH.layDSCTDDHTheoMa(Convert.ToInt32(ma));
        foreach (DataRow r1 in dsCT.Rows)
            listCT.Add(r1);
        tongSoCT = listCT.Count;
        int tinhSoPage = tongSoCT % soLuongCTTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoCT / soLuongCTTungTrang;
        else
            soPage = tongSoCT / soLuongCTTungTrang + 1;
    }
    #endregion

    #region xử lý chia trang và trả về chuỗi html load lên browser
    protected string loadDSCT()
    {
        string values = "";
        if (Request.QueryString["page"] != null)
            trangHienTai = Convert.ToInt32(Request.QueryString["page"].Trim());
        if (trangHienTai < 1)
            trangHienTai = 0;
        else
            trangHienTai -= 1;
        int trangBatDau = trangHienTai * soLuongCTTungTrang;
        int trangKetThuc = trangBatDau + soLuongCTTungTrang;
        if (trangKetThuc > tongSoCT)
            trangKetThuc = tongSoCT;
        string tieuDe = "DANH SÁCH CHI TIẾT ĐƠN ĐẶT HÀNG";
        string tieuDeCotMa = "Mã đơn đặt hàng";

        if (thaoTac.Equals("cthd"))
        {
            tieuDe = "DANH SÁCH CHI TIẾT HOÁ ĐƠN";
            tieuDeCotMa = "Mã hoá đơn";
        }
        if (soPage == 0)
        {
            values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>" + tieuDe + @"</h2>
        </div>
    <div class='DMSach'>
       <table class='tbsach'>
   <tr>
                     <th class='cotstt'>STT</th>
                       <th class='cotma'>" + tieuDeCotMa + @"</th>
                       <th class='cotten'>Mã sách</th>
                       <th class='cotdongia'>Số lượng</th>
                       <th class='cotdongia'>Đơn giá</th>
                       <th class='cotnxb'>Giảm giá</th>
                       <th class='cotncn'>Thành tiền</th>
                   </tr>
</table></div></div>
";
            return values;
        }
        values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>" + tieuDe + @"</h2>
        </div>
    <div class='DMSach'>

       <table class='tbsach'>
   <tr>
                     <th class='cotstt'>STT</th>
                       <th class='cotma'>" + tieuDeCotMa + @"</th>
                       <th class='cotten'>Mã sách</th>
                       <th class='cotdongia'>Số lượng</th>
                       <th class='cotdongia'>Đơn giá</th>
                       <th class='cotnxb'>Giảm giá</th>
                       <th class='cotncn'>Thành tiền</th>
                   </tr>
";


        for (int i = trangBatDau; i < trangKetThuc; i++)
        {
            values += @"   <tr>
                       <td class='cotstt'>" + (i + 1) + @"</td>
                        <td class='cotma'>" + ma + @"</td>
                       <td class='cotten'>" + listCT[i]["masach"].ToString().Trim() + @"</td>
                       <td class='cotdongia'>" + listCT[i]["SOLUONG"].ToString().Trim() + @"</td>
                       <td class='cotdongia'>" + String.Format("{0:#,##}", Convert.ToInt32(listCT[i]["DONGIA"].ToString().Trim())) + @"đ</td>
                       <td class='cotnxb'>" + listCT[i]["GIAMGIA"].ToString().Trim() + @"</td>                                    
                       <td class='cotncn'>" + String.Format("{0:#,##}", Convert.ToInt32(listCT[i]["THANHTIEN"].ToString().Trim())) + @"đ</td>

                   </tr>";
        }
        values += "</table></div></div>";
        if (soPage > 1)
        {
            values += "<div class='listpage'>";
            if (trangHienTai > 0)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qlct&thaotac=" + thaoTac + "&ma=" + ma + @"&page=" + trangHienTai + @"' >Pre</a>";
            if (soPage > soBtn)
            {
                int dem = 0;
                int so = trangHienTai;
                Stack<int> s = new Stack<int>();
                while (dem != soBtn / 2 && so > 0)
                {
                    s.Push(so);
                    so--;
                    dem++;
                }
                while (s.Count != 0)
                {
                    so = s.Pop();
                    values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlct&thaotac=" + thaoTac + "&ma=" + ma + @"&page=" + so + "'>" + so + @"
                    </a>";
                }
                so = trangHienTai;
                while (dem != soBtn)
                {
                    if (so >= soPage)
                        break;
                    if (trangHienTai == so)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qlct&thaotac=" + thaoTac + "&ma=" + ma + @"&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlct&thaotac=" + thaoTac + "&ma=" + ma + @"&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    so++;
                    dem++;
                }
            }
            else
                for (int i = 0; i < soPage; i++)
                {
                    if (trangHienTai == i)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qlct&thaotac=" + thaoTac + "&ma=" + ma + @"&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlct&thaotac=" + thaoTac + "&ma=" + ma + @"&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                }
            int next = trangHienTai + 2;
            if (next <= soPage)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qlct&thaotac=" + thaoTac + "&ma=" + ma + @"&page=" + (trangHienTai + 2) + "'>Next</a>";
            values += "</div>";
        }
        return values;
    }
    #endregion
}