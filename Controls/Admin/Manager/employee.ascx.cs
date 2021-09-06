using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_Manager_employee : System.Web.UI.UserControl
{
    int tongSoNV = 0;
    int soLuongNVTungTrang = 6;
    int soPage = 0;
    int trangHienTai = 0;
    int soBtn = 6;
    List<DataRow> listNV = new List<DataRow>();
    protected void Page_Load(object sender, EventArgs e)
    {
        loadNVVaoList();
    }
    #region lấy dữ liệu bảng nhân viên từ database lên lưu vào listNV và tính soPage,tongSoNV...        
    void loadNVVaoList()
    {
        DataTable dsSach = nhanVien.layDSNV();
        foreach (DataRow r1 in dsSach.Rows)
            listNV.Add(r1);
        tongSoNV = listNV.Count;
        int tinhSoPage = tongSoNV % soLuongNVTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoNV / soLuongNVTungTrang;
        else
            soPage = tongSoNV / soLuongNVTungTrang + 1;
    }
    #endregion
    #region xử lý chia trang và trả về chuỗi html load lên browser
    protected string loadDSNV()
    {
        string values = "";
        if (Request.QueryString["page"] != null)
            trangHienTai = Convert.ToInt32(Request.QueryString["page"].Trim());
        if (trangHienTai < 1)
            trangHienTai = 0;
        else
            trangHienTai -= 1;
        int trangBatDau = trangHienTai * soLuongNVTungTrang;
        int trangKetThuc = trangBatDau + soLuongNVTungTrang;
        if (trangKetThuc > tongSoNV)
            trangKetThuc = tongSoNV;
        if (soPage == 0)
        {
            values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH SÁCH NHÂN VIÊN</h2>
 <a href='empty.aspx?modul=admin&submodul=tmnv&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i> Thêm mới nhân viên
                           </a>
        </div>
    <div class='DMSach'>

       <table class='tbsach'>
   <tr>
                     <th class='cotstt'>STT</th>
                       <th class='cotma'>Mã nhân viên</th>
                       <th class='cotten'>Tên nhân viên</th>
                       <th class='cotdongia'>Địa chỉ</th>
                       <th class='cotdongia'>SDT</th>
                       <th class='cotnxb'>Giới tính</th>
                       <th class='cotncn'>Ngày vào làm</th>
                       <th class='cottentg'>Lương</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr></table></div></div>
";
            return values;
        }
        values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH MỤC NHÂN VIÊN </h2>
 <a href='empty.aspx?modul=admin&submodul=tmnv&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i>Thêm mới nhân viên
                           </a>
        </div>
    <div class='DMSach'>

       <table class='tbsach'>
   <tr>
                        <th class='cotstt'>STT</th>
                       <th class='cotma'>Mã nhân viên</th>
                       <th class='cotten'>Tên nhân viên</th>
                       <th class='cotdongia'>Địa chỉ</th>
                       <th class='cotdongia'>SDT</th>
                       <th class='cotnxb'>Giới tính</th>
                       <th class='cotncn'>Ngày vào làm</th>
                       <th class='cottentg'>Lương</th>
                       <th class='cotcongcu'>Công cụ</th>

                   </tr>
";

        for (int i = trangBatDau; i < trangKetThuc; i++)
        {
            values += @"   <tr id='madong_" + listNV[i]["manv"].ToString().Trim() + @"'>
                       <td class='cotstt'>" + (i + 1) + @"</td>
                       <td class='cotma'>" + listNV[i]["manv"].ToString().Trim() + @"</td>
                       <td class='cotten'>" + listNV[i]["tennv"].ToString().Trim() + @"</td>
                       <td class='cotdongia'>" + listNV[i]["diachi"].ToString().Trim() + @"</td>
                       <td class='cotdongia'>" + listNV[i]["sdt"].ToString().Trim() + @"</td>
                       <td class='cotnxb'>" + listNV[i]["gioitinh"].ToString().Trim() + @"</td>
                       <td class='cotncn'>" + DateTime.Parse(listNV[i]["ngayVL"].ToString().Trim()).ToShortDateString() + @"</td>
                       <td class='cottentg'>" + String.Format("{0:#,##}", Convert.ToInt32(listNV[i]["luong"].ToString().Substring(0, listNV[i]["luong"].ToString().Trim().IndexOf('.')))) + @"</td>
                       <td class='cotcongcu'>
                          
                          <a href='empty.aspx?modul=admin&submodul=tmnv&thaotac=cs&ma=" + listNV[i]["manv"].ToString().Trim() + @"' title='Sửa'>
                               <i class='fas fa-user-edit'></i>
                          </a>
                           <a href='javascript:xoaNV(" + "\"" + listNV[i]["manv"].ToString().Trim() + "\"" + @")' title='Xoá'>
                              <i class='far fa-trash-alt'></i>

                           </a>

                       </td>

                   </tr>";
        }
        values += "</table></div></div>";
        if(soPage>1)
        { 
        values+="<div class='listpage'>";
        if (trangHienTai > 0)
            values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qldsnv&page=" + trangHienTai + @"' >Pre</a>";
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
                values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsnv&page=" + so + "'>" + so + @"
                    </a>";
            }
            so = trangHienTai;
            while (dem != soBtn)
            {
                if (so >= soPage)
                    break;
                if (trangHienTai == so)
                    values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qldsnv&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                else
                    values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsnv&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                so++;
                dem++;
            }
        }
        else
            for (int i = 0; i < soPage; i++)
            {
                if (trangHienTai == i)
                    values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qldsnv&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                else
                    values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsnv&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
            }
        int next = trangHienTai + 2;
        if (next <= soPage)
            values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qldsnv&page=" + (trangHienTai + 2) + "'>Next</a>";
        values += "</div>";
        }
        return values;
    }
    #endregion
}