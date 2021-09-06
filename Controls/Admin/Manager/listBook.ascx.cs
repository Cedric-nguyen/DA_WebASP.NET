using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Controls_Admin_Manager_listBook : System.Web.UI.UserControl
{
    int tongSoSach = 0;
    int soLuongSachTungTrang = 6;
    int soPage = 0;
    int trangHienTai = 0;
    int soBtn = 5;
    List<DataRow> listSach = new List<DataRow>();
    protected void Page_Load(object sender, EventArgs e)
    {
        loadSachVaoList();
    }
    void loadSachVaoList()
    {
        #region lấy dữ liệu bảng sách từ database lên lưu vào listSach và tính soPage,tongSoSach...        
        DataTable dsSach = sach.layDanhSach();
        foreach (DataRow r1 in dsSach.Rows)
            listSach.Add(r1);
        tongSoSach = listSach.Count;
        int tinhSoPage = tongSoSach % soLuongSachTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoSach / soLuongSachTungTrang;
        else
            soPage = tongSoSach / soLuongSachTungTrang + 1;
        #endregion
    }
    protected string loaddsSach()
    {
        #region xử lý chia trang và trả về chuỗi html load lên browser
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
            values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH MỤC SÁCH </h2>
<a href='empty.aspx?modul=admin&submodul=tmb&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i> Thêm mới sách
                           </a>
        </div>
    <div class='DMSach'>

       <table class='tbsach'>
   <tr>
                       <th class='cotstt'>STT</th>
                       <th class='cotma'>Mã sách</th>
                       <th class='cotten'>Tên sách</th>
                       <th class='cotdongia'>Đơn giá</th>
                       <th class='cotdongia'>Giảm giá</th>
                       <th class='cothinh'>Hình minh hoạ</th>
                       <th class='cotnxb'>Ngày xuất bản</th>
                       <th class='cotncn'>Ngày cập nhật</th>
                       <th class='cottentg'>Tên tác giả</th>
                       <th class='cotmatl'>Mã thể loại</th>
                       <th class='cotmansx'>Mã nhà xuất bản</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr></table></div></div>
";
            return values;
        }
        values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH MỤC SÁCH </h2>
<a href='empty.aspx?modul=admin&submodul=tmb&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i> Thêm mới sách
                           </a>
        </div>
    <div class='DMSach'>

       <table class='tbsach'>
   <tr>
                       <th class='cotstt'>STT</th>
                       <th class='cotma'>Mã sách</th>
                       <th class='cotten'>Tên sách</th>
                       <th class='cotdongia'>Đơn giá</th>
                       <th class='cotdongia'>Giảm giá</th>
                       <th class='cothinh'>Hình minh hoạ</th>
                       <th class='cotnxb'>Ngày xuất bản</th>
                       <th class='cotncn'>Ngày cập nhật</th>
                       <th class='cottentg'>Tên tác giả</th>
                       <th class='cotmatl'>Mã thể loại</th>
                       <th class='cotmansx'>Mã nhà xuất bản</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr>
";

        for (int i = trangBatDau; i < trangKetThuc; i++)
        {
            values += @"   <tr id='madong_" + listSach[i]["masach"].ToString().Trim() + @"'>
                       <td class='cotstt'>" + (i + 1) + @"</td>
                       <td class='cotma'>" + listSach[i]["masach"].ToString().Trim() + @"</td>
                       <td class='cotten'>" + listSach[i]["tensach"].ToString().Trim() + @"</td>
                       <td class='cotdongia'>" + String.Format("{0:#,##}", Convert.ToInt32(listSach[i]["dongia"].ToString().Trim())) + @"đ </td>
                       <td class='cotdongia'>" + listSach[i]["giamgia"].ToString().Trim() + @"</td>
                       <td class='cothinh'><img src='img/" + listSach[i]["hinhminhhoa"].ToString().Trim() + @"'/></td>
                       <td class='cotnxb'>" + DateTime.Parse(listSach[i]["namxb"].ToString().Trim()).ToShortDateString() + @"</td>
                       <td class='cotncn'>" + DateTime.Parse(listSach[i]["ngaycapnhat"].ToString().Trim()).ToShortDateString() + @"</td>
                       <td class='cottentg'>" + listSach[i]["tentacgia"].ToString().Trim() + @"</td>
                       <td class='cotmatl'>" + listSach[i]["matheloai"].ToString().Trim() + @"</td>
                       <td class='cotmansx'>" + listSach[i]["manxb"].ToString().Trim() + @"</td>
                       <td class='cotcongcu'>
                           
                          <a href='empty.aspx?modul=admin&submodul=tmb&thaotac=cs&ma=" + listSach[i]["masach"].ToString().Trim() + @"' title='Sửa'>
                               <i class='fas fa-user-edit'></i>

                          </a>
                           <a href='javascript:xoaSach(" + "\"" + listSach[i]["masach"].ToString().Trim() + "\"" + @")' title='Xoá'>
                              <i class='far fa-trash-alt'></i>

                           </a>

                       </td>

                   </tr>";
        }

        values += "</table></div></div>";
        if (soPage > 1)
        {
            values += "<div class='listpage'>";
            if (trangHienTai > 0)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qldsb&page=" + trangHienTai + @"' >Pre</a>";
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
                    values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsb&page=" + so + "'>" + so + @"
                    </a>";
                }
                so = trangHienTai;
                while (dem != soBtn)
                {
                    if (so >= soPage)
                        break;
                    if (trangHienTai == so)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qldsb&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsb&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    so++;
                    dem++;
                }
            }
            else
                for (int i = 0; i < soPage; i++)
                {
                    if (trangHienTai == i)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qldsb&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsb&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                }
            int next = trangHienTai + 2;
            if (next <= soPage)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qldsb&page=" + (trangHienTai + 2) + "'>Next</a>";
            values += "</div>";
        }
        return values;
        #endregion
    }

}