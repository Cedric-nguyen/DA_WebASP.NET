using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Controls_Admin_Manager_categoryBook : System.Web.UI.UserControl
{

    int tongSoTL = 0;
    int soLuongTLTungTrang = 6;
    int soPage = 0;
    int trangHienTai = 0;
    int soBtn = 6;
    List<DataRow> listTL = new List<DataRow>();
    protected void Page_Load(object sender, EventArgs e)
    {
        loadTLVaoList();
    }
    #region lấy dữ liệu bảng thể loại từ database lên lưu vào listTL và tính soPage,tongSoTL...        

    void loadTLVaoList()
    {
        DataTable dsTL = theLoai.layDSTheLoai();
        DataTable dbGrid = new DataTable();
        dbGrid.Columns.Add("stt");
        dbGrid.Columns.Add("matheloai");
        dbGrid.Columns.Add("tentheloai");

        int dem = 1;
        foreach (DataRow r1 in dsTL.Rows)
        {
            dbGrid.Rows.Add(dem, r1["matheloai"].ToString(), r1["tentheloai"].ToString());
            dem++;
            listTL.Add(r1);
        }
        tongSoTL = listTL.Count;
        int tinhSoPage = tongSoTL % soLuongTLTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoTL / soLuongTLTungTrang;
        else
            soPage = tongSoTL / soLuongTLTungTrang + 1;
    }
    #endregion

    #region xử lý chia trang và trả về chuỗi html load lên browser
    protected string loadDSTL()
    {
        string values = "";

        if (Request.QueryString["page"] != null)
            trangHienTai = Convert.ToInt32(Request.QueryString["page"].Trim());
        if (trangHienTai < 1)
            trangHienTai = 0;
        else
            trangHienTai -= 1;
        int trangBatDau = trangHienTai * soLuongTLTungTrang;
        int trangKetThuc = trangBatDau + soLuongTLTungTrang;
        if (trangKetThuc > tongSoTL)
            trangKetThuc = tongSoTL;
        if (soPage == 0)
        {
            values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH MỤC THỂ LOẠI</h2>
  <a href='empty.aspx?modul=admin&submodul=tmdmb&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i> Thêm mới thể loại
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
</table></div></div>
";
            return values;
        }
        values = @"<div class='listbook introduce'>
     <div class='dmsach' >
            <h2>DANH MỤC THỂ LOẠI</h2>
  <a href='empty.aspx?modul=admin&submodul=tmdmb&thaotac=tm' title='Thêm mới'>
                               <i class='fas fa-plus-circle'></i> Thêm mới thể loại
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
            values += @"   <tr id='madong_" + listTL[i]["matheloai"].ToString().Trim() + @"'>
                       <td class='cotstt'>" + (i + 1) + @"</td>
                       <td class='cotma'>" + listTL[i]["tentheloai"].ToString().Trim() + @"</td>
                       <td class='cotten'>" + listTL[i]["matheloai"].ToString().Trim() + @"</td>
                       <td class='cotcongcu'>
                         
                          <a href='empty.aspx?modul=admin&submodul=tmdmb&thaotac=cs&ma=" + listTL[i]["matheloai"].ToString().Trim() + @"' title='Sửa'>
                               <i class='fas fa-user-edit'></i>

                          </a>
                           <a href='javascript:xoaTheLoai(" + "\"" + listTL[i]["matheloai"].ToString().Trim() + "\"" + @")' title='Xoá'>
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
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qldmb&page=" + trangHienTai + @"' >Pre</a>";
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
                    values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldmb&page=" + so + "'>" + so + @"
                    </a>";
                }
                so = trangHienTai;
                while (dem != soBtn)
                {
                    if (so >= soPage)
                        break;
                    if (trangHienTai == so)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qldmb&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldmb&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    so++;
                    dem++;
                }
            }
            else
                for (int i = 0; i < soPage; i++)
                {
                    if (trangHienTai == i)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=admin&submodul=qldmb&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=admin&submodul=qldmb&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                }
            int next = trangHienTai + 2;
            if (next <= soPage)
                values += "<a class='btn' href='empty.aspx?modul=admin&submodul=qldmb&page=" + (trangHienTai + 2) + "'>Next</a>";
            values += "</div>";
        }
        return values;
    }
    #endregion

}