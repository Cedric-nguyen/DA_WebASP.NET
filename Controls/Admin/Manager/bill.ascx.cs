using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_Manager_bill : System.Web.UI.UserControl
{
    int tongSoHD = 0;
    int soLuongHDTungTrang = 6;
    int soPage = 0;
    int trangHienTai = 0;
    List<DataRow> listHD = new List<DataRow>();
    int soBtn = 6;
    protected void Page_Load(object sender, EventArgs e)
    {
        loadHDVaoList();
    }
    #region lấy dữ liệu bảng hoá đơn từ database lên lưu vào listHD và tính soPage,tongSoHD...        
    void loadHDVaoList()
    {
        DataTable dsHD = hoaDon.layDSHD();
        foreach (DataRow r1 in dsHD.Rows)
            listHD.Add(r1);
        tongSoHD = listHD.Count;
        int tinhSoPage = tongSoHD % soLuongHDTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoHD / soLuongHDTungTrang;
        else
            soPage = tongSoHD / soLuongHDTungTrang + 1;
    }
    #endregion

    #region xử lý chia trang và trả về chuỗi html load lên browser

    protected string loadDSHD()
    {
        string values = "";
        if (Request.QueryString["page"] != null)
            trangHienTai = Convert.ToInt32(Request.QueryString["page"].Trim());
        if (trangHienTai < 1)
            trangHienTai = 0;
        else
            trangHienTai -= 1;
        int trangBatDau = trangHienTai * soLuongHDTungTrang;
        int trangKetThuc = trangBatDau + soLuongHDTungTrang;
        if (trangKetThuc > tongSoHD)
            trangKetThuc = tongSoHD;
        if (soPage == 0)
        {
            values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH SÁCH HOÁ ĐƠN</h2>
        </div>
    <div class='DMSach'>

       <table class='tbsach'>
   <tr>
                     <th class='cotstt'>STT</th>
                       <th class='cotma'>Mã hoá đơn</th>
                       <th class='cotten'>Ngày lập</th>
                       <th class='cotdongia'>Thành tiền</th>
                       <th class='cotdongia'>Nhân viên giao</th>
                       <th class='cotnxb'>Email khách</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr></table></div></div>
";
            return values;
        }
        values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH SÁCH HOÁ ĐƠN </h2>
        </div>
    <div class='DMSach'>

       <table class='tbsach'>
   <tr>
                        <th class='cotstt'>STT</th>
                       <th class='cotma'>Mã hoá đơn</th>
                       <th class='cotten'>Ngày lập</th>
                       <th class='cotdongia'>Thành tiền</th>
                       <th class='cotdongia'>Nhân viên giao</th>
                       <th class='cotnxb'>Email khách</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr>
";

        for (int i = trangBatDau; i < trangKetThuc; i++)
        {
            values += @"   <tr id='madong_" + listHD[i]["mahd"].ToString().Trim() + @"'>
                       <td class='cotstt'>" + (i + 1) + @"</td>
                        <td class='cotma'>" + listHD[i]["mahd"].ToString().Trim() + @"</td>
                       <td class='cotten'>" + DateTime.Parse(listHD[i]["ngaylap"].ToString().Trim()).ToShortDateString() + @"</td>
                       <td class='cotdongia'>" + String.Format("{0:#,##}", Convert.ToInt32(listHD[i]["thanhtien"].ToString().Substring(0, listHD[i]["thanhtien"].ToString().Trim().IndexOf('.')))) + @"đ</td>
                       <td class='cotdongia'>" + listHD[i]["tennv"].ToString().Trim() + @"</td>
                       <td class='cotnxb'>" + listHD[i]["email"].ToString().Trim() + @"</td>                                    
                       <td class='cotcongcu'>
                            <a href='empty.aspx?modul=admin&submodul=qlct&thaotac=cthd&ma=" + listHD[i]["mahd"].ToString().Trim() + @"' title='Xem chi tiết'>
                               <i class='fas fa-info-circle'></i>
                             </a>                         
                           <a href='javascript:xoaHD(" + "\"" + listHD[i]["mahd"].ToString().Trim() + "\"" + @")' title='Xoá'>
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
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qlhd&page=" + trangHienTai + @"' >Pre</a>";
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
                    values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlhd&page=" + so + "'>" + so + @"
                    </a>";
                }
                so = trangHienTai;
                while (dem != soBtn)
                {
                    if (so >= soPage)
                        break;
                    if (trangHienTai == so)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qlhd&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlhd&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    so++;
                    dem++;
                }
            }
            else
                for (int i = 0; i < soPage; i++)
                {
                    if (trangHienTai == i)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qlhd&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlhd&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                }
            int next = trangHienTai + 2;
            if (next <= soPage)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qlhd&page=" + (trangHienTai + 2) + "'>Next</a>";
            values += "</div>";
        }
        return values;
    }
    #endregion
}