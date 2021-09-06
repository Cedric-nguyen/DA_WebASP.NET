using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Controls_Book_sachBanChay : System.Web.UI.UserControl
{
    List<DataRow> listSach = new List<DataRow>();
    int tongSoSach = 0;
    int soPage = 0;
    int soLuongSachTungTrang = 8;
    int trangHienTai = 0;
    int soBtn = 6;
    protected void Page_Load(object sender, EventArgs e)
    {
        loadSachVaoList();
    }
    #region lấy dữ liệu bảng sách từ database lên lưu vào listSach và tính soPage,tongSoSach...

    void loadSachVaoList()
    {

        DataTable dsSach = sach.sachBanChay();
        foreach (DataRow r1 in dsSach.Rows)
            listSach.Add(r1);
        tongSoSach = listSach.Count;
        int tinhSoPage = tongSoSach % soLuongSachTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoSach / soLuongSachTungTrang;
        else
            soPage = tongSoSach / soLuongSachTungTrang + 1;
    }
    #endregion
    #region xử lý chia trang và trả về chuỗi html sách bán chạy theo quy định số lượng load lên browser

    protected string loadSach()
    {
        string values = "";
        if (Request.QueryString["page"] != null)
            trangHienTai = Convert.ToInt32(Request.QueryString["page"].Trim());
        if (trangHienTai < 1)
            trangHienTai = 0;
        else
            trangHienTai -= 1;
        int trangBatDau = trangHienTai * soLuongSachTungTrang;
        int trangKetThuc = trangBatDau + soLuongSachTungTrang;
        if (trangKetThuc > tongSoSach)
            trangKetThuc = tongSoSach;
        if (soPage == 0)
        {
            values = @" <h1 class='title'>
                <a class='title__link' Style='background-image: url(img/lbcenter.png)'>
                   Sách bán chạy   
                </a>
            </h1>";
            return values;
        }
        values = @" <h1 class='title'>
                <a class='title__link' Style='background-image: url(img/lbcenter.png)'>
                  Sách bán chạy
                </a>
            </h1>
            <div class='group_book'>
            <ul>";
        for (int i = trangBatDau; i < trangKetThuc; i++)
        {
            values += @"
           <li class='book__item'>
                <a href='empty.aspx?modul=s&ms=" + listSach[i]["masach"].ToString() + @"' class='book'>
                   <img src='img/" + listSach[i]["hinhminhhoa"].ToString() + @"'/>
                </a>
                <div class='book__item-notify'>
                    <h3 class='book__item-notify-title'>" + listSach[i]["tensach"].ToString() + @"
                    </h3>
                    <ul class='book__item-notify-info'>
                        <li>Tên tác giả:" + listSach[i]["tentacgia"].ToString() + @"</li>
                        <li>Năm sản xuất:" + DateTime.Parse(listSach[i]["namxb"].ToString()).ToShortDateString().ToString() + @"</li>
                        <li>Ngày cập nhật:" + DateTime.Parse(listSach[i]["ngaycapnhat"].ToString()).ToShortDateString().ToString() + @"</li>
                    </ul>
                    <div class='book__item-price'>
                        <span class='book__item-price-new'>" + listSach[i]["dongia"].ToString() + @"</span>
                        <span class='book__item-price-old'>235.000đ</span>
                    </div>
                    <div class='group__btn'>
                    <a  href='javascript:themGioHang(" + "\"" + listSach[i]["masach"].ToString().Trim() + "\"" + @")' class='btn btnInfo'>Thêm giỏ hàng</a>
                    <a href='javascript:muaNgay(" + "\"" + listSach[i]["masach"].ToString().Trim() + "\"" + @")' class='btn btnInfo'>Mua ngay</a>
                    </div>
                </div>
            </li>";
        }
        values += "</ul></div><div class='category__page'>";
        if (trangHienTai > 0)
            values += "<a class='btn' href='empty.aspx?modul=sbc&page=" + trangHienTai + @"' >Pre</a>";
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
                values += @" <a class='btn' href='empty.aspx?modul=sbc&page=" + so + "'>" + so + @"
                    </a>";
            }
            so = trangHienTai;
            while (dem != soBtn)
            {
                if (so >= soPage)
                    break;
                if (trangHienTai == so)
                    values += @" <a class='curentpage btn' href='empty.aspx?modul=sbc&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                else
                    values += @" <a class='btn' href='empty.aspx?modul=sbc&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                so++;
                dem++;
            }
        }
        else
            if (soPage > 1)
            for (int i = 0; i < soPage; i++)
            {
                if (trangHienTai == i)
                    values += @" <a class='curentpage btn' href='empty.aspx?modul=sbc&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                else
                    values += @" <a class='btn' href='empty.aspx?modul=sbc&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
            }
        int next = trangHienTai + 2;
        if (next <= soPage)
            values += "<a class='btn' href='empty.aspx?modul=sbc&page=" + (trangHienTai + 2) + "'>Next</a>";
        values += "</div>";
        return values;
    }
    #endregion
}