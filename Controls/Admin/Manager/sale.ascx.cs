using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Admin_Manager_sale : System.Web.UI.UserControl
{
    int tongSoGG = 0;
    int soLuongGGTungTrang = 6;
    int soPage = 0;
    int trangHienTai = 0;
    int soBtn = 6;
    List<DataRow> listGG = new List<DataRow>();
    protected void Page_Load(object sender, EventArgs e)
    {
        loadGGVaoList();
    }

    #region lấy dữ liệu bảng sách giảm giá từ database lên lưu vào listGG và tính soPage,tongSoGG...        

    void loadGGVaoList()
    {
        DataTable dsGG = ctKhuyenMai.layChuongTrinhKhuyenMai();
        foreach (DataRow r1 in dsGG.Rows)
            listGG.Add(r1);
        tongSoGG = listGG.Count;
        int tinhSoPage = tongSoGG % soLuongGGTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoGG / soLuongGGTungTrang;
        else
            soPage = tongSoGG / soLuongGGTungTrang + 1;
    }
    #endregion

    #region xử lý chia trang và trả về chuỗi html load lên browser
    protected string loadDSGG()
    {
        string values = "";

        if (Request.QueryString["page"] != null)
            trangHienTai = Convert.ToInt32(Request.QueryString["page"].Trim());
        if (trangHienTai < 1)
            trangHienTai = 0;
        else
            trangHienTai -= 1;
        int trangBatDau = trangHienTai * soLuongGGTungTrang;
        int trangKetThuc = trangBatDau + soLuongGGTungTrang;
        if (trangKetThuc > tongSoGG)
            trangKetThuc = tongSoGG;
        if (soPage == 0)
        {
            values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH SÁCH CHƯƠNG TRÌNH KHUYẾN MÃI</h2>
 <a href='empty.aspx?modul=admin&submodul=tmctkm&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i>Thêm mới chương trình khuyến mãi
                           </a>
        </div>
    <div class='DMSach'>
       <table class='tbsach'>
   <tr>
                       <th class='cotstt'>STT</th>
                       <th class='cotma'>Tên chương trình</th>
                       <th class='cotten'>Mã khuyến mãi</th>
                       <th class='cotdongia'>Giảm giá</th>

                       <th class='cotcongcu'>Công cụ</th>
                   </tr>
</table></div></div>
";
            return values;
        }
        values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH SÁCH CHƯƠNG TRÌNH KHUYẾN MÃI</h2>
 <a href='empty.aspx?modul=admin&submodul=tmctkm&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i>Thêm mới chương trình khuyến mãi
                           </a>
        </div>
    <div class='DMSach'>
       <table class='tbsach'>
   <tr>
                      <th class='cotstt'>STT</th>
                       <th class='cotma'>Tên chương trình</th>
                       <th class='cotten'>Mã khuyến mãi</th>
                       <th class='cotdongia'>Giảm giá</th>
                       <th class='cotcongcu'>Công cụ</th>
                   </tr>
";

        for (int i = trangBatDau; i < trangKetThuc; i++)
        {
            values += @"   <tr id='madong_" + listGG[i]["MAKM"].ToString().Trim() + @"'>
                       <td class='cotstt'>" + (i + 1) + @"</td>
                       <td class='cotma'>" + listGG[i]["TENKM"].ToString().Trim() + @"</td>
                       <td class='cotten'>" + listGG[i]["MAKM"].ToString().Trim() + @"</td>
                       <td class='cotdongia'>" + listGG[i]["giamgia"].ToString().Trim() + @"</td>
                        
                       <td class='cotcongcu'>
                          
                          <a href='empty.aspx?modul=admin&submodul=tmctkm&thaotac=cs&ma=" + listGG[i]["MAKM"].ToString().Trim() + @"' title='Sửa'>
                               <i class='fas fa-user-edit'></i>
                          </a>
                           <a href='javascript:xoaCTKM(" + "\"" + listGG[i]["makm"].ToString().Trim() + "\"" + @")' title='Xoá'>
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
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qlctkm&page=" + trangHienTai + @"' >Pre</a>";
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
                    values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlctkm&page=" + so + "'>" + so + @"
                    </a>";
                }
                so = trangHienTai;
                while (dem != soBtn)
                {
                    if (so >= soPage)
                        break;
                    if (trangHienTai == so)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qlctkm&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlctkm&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    so++;
                    dem++;
                }
            }
            else
                for (int i = 0; i < soPage; i++)
                {
                    if (trangHienTai == i)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qlctkm&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qlctkm&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                }
            int next = trangHienTai + 2;
            if (next <= soPage)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qlctkm&page=" + (trangHienTai + 2) + "'>Next</a>";
            values += "</div>";
        }
        return values;

    }
    #endregion

}