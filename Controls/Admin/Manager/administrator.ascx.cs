using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_Manager_administrator : System.Web.UI.UserControl
{
    int tongSoTK = 0;
    int soLuongTKTungTrang = 6;
    int soPage = 0;
    int trangHienTai = 0;
    List<DataRow> listTK = new List<DataRow>();
    DataTable dtGrid = new DataTable();
    int soBtn = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        loadTKVaoList();
       
    }
    #region lấy dữ liệu bảng quản trị từ database lên lưu vào listTK và tính soPage,tongSoTK...        

    void loadTKVaoList()
    {
        listTK.Clear();
        DataTable dsTK = quanTri.layDSQT();

        foreach (DataRow r1 in dsTK.Rows)
        {
            listTK.Add(r1);
        }
        tongSoTK = listTK.Count;
        int tinhSoPage = tongSoTK % soLuongTKTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoTK / soLuongTKTungTrang;
        else
            soPage = tongSoTK / soLuongTKTungTrang + 1;
    }
    #endregion
    #region xử lý chia trang và trả về chuỗi html load lên browser
    protected string loadDSTK()
    {
        string values = "";

        if (Request.QueryString["page"] != null)
            trangHienTai = Convert.ToInt32(Request.QueryString["page"].Trim());
        if (trangHienTai < 1)
            trangHienTai = 0;
        else
            trangHienTai -= 1;
        int trangBatDau = trangHienTai * soLuongTKTungTrang;
        int trangKetThuc = trangBatDau + soLuongTKTungTrang;
        if (trangKetThuc > tongSoTK)
            trangKetThuc = tongSoTK;
        if (soPage == 0)
        {
            values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH SÁCH QUẢN TRỊ</h2>
 <a href='empty.aspx?modul=admin&submodul=tmqt&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i> Thêm mới quản trị
                           </a>
        </div>
    <div class='DMSach'>
       <table class='tbsach'>
   <tr>
                       <th class='cotstt'>STT</th>
                       <th class='cotma'>Tên hiển thị</th>
                       <th class='cotten'>Email</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr>
</table></div></div>
";
            return values;
        }
        values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH SÁCH QUẢN TRỊ</h2>
 <a href='empty.aspx?modul=admin&submodul=tmqt&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i> Thêm mới quản trị
                           </a>
        </div>
    <div class='DMSach'>
       <table class='tbsach'>
   <tr>
                       <th class='cotstt'>STT</th>
                       <th class='cotma'>Tên hiển thị</th>
                       <th class='cotten'>Email</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr>
";

        for (int i = trangBatDau; i < trangKetThuc; i++)
        {
            values += @"   <tr id='madong_" + (i + 1) + @"'>
                       <td class='cotstt'>" + (i + 1) + @"</td>
                       <td class='cotma'>" + listTK[i]["tenhienthi"].ToString().Trim() + @"</td>
                       <td class='cotten'>" + listTK[i]["email"].ToString().Trim() + @"</td>
                       <td class='cotcongcu'>
                          
                          <a href='empty.aspx?modul=admin&submodul=tmqt&thaotac=cs&ma=" + listTK[i]["email"].ToString().Trim() + @"' title='Sửa'>
                               <i class='fas fa-user-edit'></i>

                          </a>
                           <a href='javascript:xoaQuanTri(" + "\"" + listTK[i]["email"].ToString().Trim() + "\"" + "," + "\"" + (i + 1) + "\"" + @")' title='Xoá'>
                              <i class='far fa-trash-alt'></i>
                           </a>

                       </td>

                   </tr>";
        }
        values += @"</table></div></div>";
        if (soPage > 1)
        {
            values += "<div class='listpage'>";
            if (trangHienTai > 0)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qldsqt&page=" + trangHienTai + @"' >Pre</a>";
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
                    values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsqt&page=" + so + "'>" + so + @"
                    </a>";
                }
                so = trangHienTai;
                while (dem != soBtn)
                {
                    if (so >= soPage)
                        break;
                    if (trangHienTai == so)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qldsqt&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsqt&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    so++;
                    dem++;
                }
            }
            else
                for (int i = 0; i < soPage; i++)
                {
                    if (trangHienTai == i)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qldsqt&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsqt&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                }
            int next = trangHienTai + 2;
            if (next <= soPage)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qldsqt&page=" + (trangHienTai + 2) + "'>Next</a>";
            values += "</div>";
        }
        return values;
    }
    #endregion
}