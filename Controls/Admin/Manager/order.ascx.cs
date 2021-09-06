using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_Manager_order : System.Web.UI.UserControl
{
    int tongSoDDH = 0;
    int soLuongDDHTungTrang = 6;
    int soPage = 0;
    int trangHienTai = 0;
    List<DataRow> listDDH = new List<DataRow>();
    int soBtn = 6;
    protected void Page_Load(object sender, EventArgs e)
    {
        loadDDHVaoList();
    }

    #region lấy dữ liệu bảng đơn đặt hàng từ database lên lưu vào listDDH và tính soPage,tongSoDDH...        
    void loadDDHVaoList()
    {
        DataTable dsDDH = donDatHang.layDSDDH();
        foreach (DataRow r1 in dsDDH.Rows)
            listDDH.Add(r1);
        tongSoDDH = listDDH.Count;
        int tinhSoPage = tongSoDDH % soLuongDDHTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoDDH / soLuongDDHTungTrang;
        else
            soPage = tongSoDDH / soLuongDDHTungTrang + 1;
    }
    #endregion


    #region xử lý chia trang và trả về chuỗi html load lên browser
    protected string loadDSDDH()
    {
        string values = "";
        if (Request.QueryString["page"] != null)
            trangHienTai = Convert.ToInt32(Request.QueryString["page"].Trim());
        if (trangHienTai < 1)
            trangHienTai = 0;
        else
            trangHienTai -= 1;
        int trangBatDau = trangHienTai * soLuongDDHTungTrang;
        int trangKetThuc = trangBatDau + soLuongDDHTungTrang;
        if (trangKetThuc > tongSoDDH)
            trangKetThuc = tongSoDDH;
        if (soPage == 0)
        {
            values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH SÁCH ĐƠN ĐẶT HÀNG</h2>
        </div>
    <div class='DMSach'>

       <table class='tbsach'>
   <tr>
                     <th class='cotstt'>STT</th>
                       <th class='cotma'>Mã đơn hàng</th>
                       <th class='cotten'>Ngày tạo</th>
                       <th class='cotdongia'>Thành tiền</th>
                       <th class='cotdongia'>Địa chỉ nhận hàng</th>
                       <th class='cotnxb'>Email khách</th>
                       <th class='cotncn'>Tình trạng</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr></table></div></div>
";
            return values;
        }
        values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH MỤC ĐƠN ĐẶT HÀNG </h2>
        </div>
    <div class='DMSach'>

       <table class='tbsach'>
   <tr>
                     <th class='cotstt'>STT</th>
                       <th class='cotma'>Mã đơn hàng</th>
                       <th class='cotten'>Ngày tạo</th>
                       <th class='cotdongia'>Thành tiền</th>
                       <th class='cotdongia'>Địa chỉ nhận hàng</th>
                       <th class='cotnxb'>Email khách</th>
                       <th class='cotncn'>Tình trạng</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr>
";

        for (int i = trangBatDau; i < trangKetThuc; i++)
        {
            string tinhTrang = "Chưa duyệt";
            string value = @"<a href='empty.aspx?modul=admin&submodul=dddh&ma=" + listDDH[i]["madonhang"].ToString().Trim() + @"' title='Duyệt đơn hàng'>
                              <i class='fas fa-clipboard-check'></i>
                            </a>";
            if (listDDH[i]["tinhtrang"].ToString().Trim().Equals("1"))
            {
                tinhTrang = "Đã duyệt";
                value = "";
            }

            values += @"   <tr id='madong_" + listDDH[i]["madonhang"].ToString().Trim() + @"'>
                       <td class='cotstt'>" + (i + 1) + @"</td>
                        <td class='cotma'>" + listDDH[i]["madonhang"].ToString().Trim() + @"</td>
                       <td class='cotten'>" + DateTime.Parse(listDDH[i]["ngaytao"].ToString().Trim()).ToShortDateString() + @"</td>
                       <td class='cotdongia'>" + String.Format("{0:#,##}", Convert.ToInt32(listDDH[i]["thanhtien"].ToString().Substring(0, listDDH[i]["thanhtien"].ToString().Trim().IndexOf('.')))) + @"đ</td>
                       <td class='cotdongia'>" + listDDH[i]["diachinhanhang"].ToString().Trim() + @"</td>
                       <td class='cotnxb'>" + listDDH[i]["email"].ToString().Trim() + @"</td>
                       <td class='cotncn'>" + tinhTrang + @"</td>                       
                       <td class='cotcongcu'>
                           <a href='empty.aspx?modul=admin&submodul=qlct&thaotac=ctdh&ma=" + listDDH[i]["madonhang"].ToString().Trim() + @"' title='Xem chi tiết'>
                               <i class='fas fa-info-circle'></i>
                             </a>
                        " + value + @"
                           <a href='javascript:xoaDDH(" + "\"" + listDDH[i]["madonhang"].ToString().Trim() + "\"" + @")' title='Xoá'>
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
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qlddh&page=" + trangHienTai + @"' >Pre</a>";
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
                    values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlddh&page=" + so + "'>" + so + @"
                    </a>";
                }
                so = trangHienTai;
                while (dem != soBtn)
                {
                    if (so >= soPage)
                        break;
                    if (trangHienTai == so)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qlddh&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlddh&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    so++;
                    dem++;
                }
            }
            else
                for (int i = 0; i < soPage; i++)
                {
                    if (trangHienTai == i)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qlddh&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlddh&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                }
            int next = trangHienTai + 2;
            if (next <= soPage)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qlddh&page=" + (trangHienTai + 2) + "'>Next</a>";
            values += "</div>";
        }
        return values;
    }
    #endregion

}