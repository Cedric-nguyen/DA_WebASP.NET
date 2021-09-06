using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_Manager_publishcompany : System.Web.UI.UserControl
{
    int tongSoNXB = 0;
    int soLuongNXBTungTrang = 6;
    int soPage = 0;
    int trangHienTai = 0;
    List<DataRow> listNXB = new List<DataRow>();
    int soBtn = 6;
    protected void Page_Load(object sender, EventArgs e)
    {
        loadNXBVaoList();
    }

    #region lấy dữ liệu bảng nhà xuất bản từ database lên lưu vào listNXB và tính soPage,tongSoNXB...        
    void loadNXBVaoList()
    {
        DataTable dsNXB = nxb.layDSNXB();
        foreach (DataRow r1 in dsNXB.Rows)
            listNXB.Add(r1);
        tongSoNXB = listNXB.Count;
        int tinhSoPage = tongSoNXB % soLuongNXBTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoNXB / soLuongNXBTungTrang;
        else
            soPage = tongSoNXB / soLuongNXBTungTrang + 1;
    }
    #endregion

    #region xử lý chia trang và trả về chuỗi html load lên browser
    protected string loadDSNXB()
    {
        string values = "";

        if (Request.QueryString["page"] != null)
            trangHienTai = Convert.ToInt32(Request.QueryString["page"].Trim());
        if (trangHienTai < 1)
            trangHienTai = 0;
        else
            trangHienTai -= 1;
        int trangBatDau = trangHienTai * soLuongNXBTungTrang;
        int trangKetThuc = trangBatDau + soLuongNXBTungTrang;
        if (trangKetThuc > tongSoNXB)
            trangKetThuc = tongSoNXB;
        if (soPage == 0)
        {
            values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH SÁCH NHÀ XUẤT BẢN</h2>
 <a href='empty.aspx?modul=admin&submodul=tmnxb&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i>Thêm mới nhà xuất bản
                           </a>
        </div>
    <div class='DMSach'>
       <table class='tbsach'>
   <tr>
                       <th class='cotstt'>STT</th>
                       <th class='cotma'>Tên nhà xuất bản</th>
                       <th class='cotten'>Mã nhà xuất bản</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr>
</table></div></div>
";
            return values;
        }
        values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH SÁCH NHÀ XUẤT BẢN</h2>
 <a href='empty.aspx?modul=admin&submodul=tmnxb&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i>Thêm mới nhà xuất bản
                           </a>
        </div>
    <div class='DMSach'>
       <table class='tbsach'>
   <tr>
                       <th class='cotstt'>STT</th>
                       <th class='cotma'>Tên thể loại</th>
                       <th class='cotten'>Mã thể loại</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr>
";

        for (int i = trangBatDau; i < trangKetThuc; i++)
        {
            values += @"   <tr id='madong_" + listNXB[i]["manxb"].ToString().Trim() + @"'>
                       <td class='cotstt'>" + (i + 1) + @"</td>
                       <td class='cotma'>" + listNXB[i]["tennxb"].ToString().Trim() + @"</td>
                       <td class='cotten'>" + listNXB[i]["manxb"].ToString().Trim() + @"</td>
                       <td class='cotcongcu'>
                          
                          <a href='empty.aspx?modul=admin&submodul=tmnxb&thaotac=cs&ma=" + listNXB[i]["manxb"].ToString().Trim() + @"' title='Sửa'>
                               <i class='fas fa-user-edit'></i>

                          </a>
                           <a href='javascript:xoaNXB(" + "\"" + listNXB[i]["manxb"].ToString().Trim() + "\"" + @")' title='Xoá'>
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
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qldsnxb&page=" + trangHienTai + @"' >Pre</a>";
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
                    values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsnxb&page=" + so + "'>" + so + @"
                    </a>";
                }
                so = trangHienTai;
                while (dem != soBtn)
                {
                    if (so >= soPage)
                        break;
                    if (trangHienTai == so)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qldsnxb&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsnxb&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    so++;
                    dem++;
                }
            }
            else
                for (int i = 0; i < soPage; i++)
                {
                    if (trangHienTai == i)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qldsnxb&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldsnxb&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                }
            int next = trangHienTai + 2;
            if (next <= soPage)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qldsnxb&page=" + (trangHienTai + 2) + "'>Next</a>";
            values += "</div>";
        }
        return values;
    }
    #endregion

}