using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Controls_Book_chuongTrinhKhuyenMai : System.Web.UI.UserControl
{
    DataTable ds = ctKhuyenMai.layChuongTrinhKhuyenMai();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataList1.DataSource = ds;
        DataList1.DataBind();
    }
    #region hàm lấy thông tin chi tiết của chương trình khuyến mãi
    protected string loadCTKM()
    {
       
        if (ds.Rows.Count == 0)
            return "<p>Chưa có chương trình khuyến mãi.Mời bạn quay lại sau</p>";
            return "<p>Mời bạn bấm vào link:<a style='margin-left:10px;' href='empty.aspx?modul=ggdb'>http://empty.aspx?modul=ggdb</a></p>";
    }
    #endregion
}