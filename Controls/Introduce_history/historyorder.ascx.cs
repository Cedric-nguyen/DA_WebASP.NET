using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Controls_Introduce_history_historyorder : System.Web.UI.UserControl
{
    int tongSoDDH = 0;
    int soLuongDDHTungTrang = 6;
    int soBtn=5;
    int soPage = 0;
    int trangHienTai = 0;
    List<DataRow> listDDH = new List<DataRow>();
    string email = "";
    DataTable dtGridView = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["email"] != null)
            email = Session["email"].ToString();
        loadGridView();            
        loadDonHangVaoList();
        loadDSDDH();
        GridView1.DataSource = dtGridView;
        GridView1.DataBind();
    }
    #region hàm khởi tạo gridview
    void loadGridView()
    {
        dtGridView.Clear();
        dtGridView.Columns.Add("stt");
        dtGridView.Columns.Add("ma");
        dtGridView.Columns.Add("ngay");
        dtGridView.Columns.Add("thanhtien");
        dtGridView.Columns.Add("diachi");
        dtGridView.Columns.Add("email");
        dtGridView.Columns.Add("tinhtrang");
    }
    #endregion
    #region hàm kiểm tra phải quyền admin?
    protected string ktraAdmin(string admin)
    {
        //if (cookie != null && cookie["qtc"].ToString().Equals(admin))
        if (Session["qtc"] != null && Session["qtc"].ToString().Equals(admin))

            return "current";
        return "";

    }
    #endregion
    #region lấy dữ liệu bảng ddh từ database lên lưu vào listDDH và tính soPage,tongSoDDH...        

    void loadDonHangVaoList()
    {
        DataTable dsSach = donDatHang.timDonHangTheoEmail(email);            
        foreach (DataRow r1 in dsSach.Rows)        
            listDDH.Add(r1);                   
        tongSoDDH = listDDH.Count;
        int tinhSoPage = tongSoDDH % soLuongDDHTungTrang;
        if (tinhSoPage == 0)
            soPage = tongSoDDH / soLuongDDHTungTrang;
        else
            soPage = tongSoDDH / soLuongDDHTungTrang + 1;
    }
    #endregion
    #region xử lý chia trang và trả về chuỗi html đơn đặt hàng load lên browser
    protected string loadDSDDH()
    {
        if (Session["email"] == null)
            return @"<p>Bạn chưa có đơn hàng nào</p>";
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
         return @"<p>Bạn chưa có đơn hàng nào</p>";
        dtGridView.Clear();                 
        for (int i = trangBatDau; i < trangKetThuc; i++)
        {
            string tinhTrang = "Đã giao";
            if (listDDH[i]["tinhtrang"].ToString().Equals("0"))
                tinhTrang = "Chưa giao";
            dtGridView.Rows.Add((i+1), listDDH[i]["madonhang"].ToString().Trim(), DateTime.Parse(listDDH[i]["ngaytao"].ToString()).ToShortDateString(),
                String.Format("{0:#,##}", Convert.ToInt32(listDDH[i]["thanhtien"].ToString().Substring(0, listDDH[i]["thanhtien"].ToString().Trim().IndexOf('.')))) + "đ",
                listDDH[i]["diachinhanhang"].ToString().Trim(), listDDH[i]["email"].ToString().Trim(), tinhTrang);
        }       
        if (soPage > 1)
        {
            values += "</div><div class='listpage'>";
            if (trangHienTai > 0)
                values += "<a class='btn' href='empty.aspx?modul=lsdh&page=" + trangHienTai + @"' >Pre</a>";
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
                    values += @" <a class='btn' href='empty.aspx?modul=lsdh&page=" + so + "'>" + so + @"
                    </a>";
                }
                so = trangHienTai;
                while (dem != soBtn)
                {
                    if (so >= soPage)
                        break;
                    if (trangHienTai == so)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=lsdh&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=lsdh&page=" + (so + 1) + "'>" + (so + 1) + @"
                    </a>";
                    so++;
                    dem++;
                }
            }
            else
                for (int i = 0; i < soPage; i++)
                {
                    if (trangHienTai == i)
                        values += @" <a class='curentpage btn' href='empty.aspx?modul=lsdh&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                    else
                        values += @" <a class='btn' href='empty.aspx?modul=lsdh&page=" + (i + 1) + "'>" + (i + 1) + @"
                    </a>";
                }
            int next = trangHienTai + 2;
            if (next <= soPage)
                values += "<a class='btn' href='empty.aspx?modul=lsdh&page=" + next + "'>Next</a>";
            values += "</div>";
        }
        return values;
    }
    #endregion

}