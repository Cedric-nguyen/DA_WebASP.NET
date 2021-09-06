using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for sach
/// </summary>
public class sach
{
    #region lấy 1 cuốn sách theo mã sách
    public static DataTable laySachTheoMa(string ma)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "Sach");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.CommandText = "select *from sach,nhaxuatban,theloai where sach.matheloai=theloai.matheloai and sach.manxb=nhaxuatban.manxb and masach=@ma";
        cmd.Parameters.AddWithValue("@ma", ma);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy danh sách sách
    public static DataTable layDanhSach()
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "Sach");
        dap.SelectCommand = new SqlCommand("select *from sach", DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy danh sách sách theo mã thể loại
    public static DataTable laySachTheoMaLoai(string maLoai)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "Sach");
        dap.SelectCommand = new SqlCommand("select *from sach,nhaxuatban,theloai where sach.matheloai=theloai.matheloai and sach.manxb=nhaxuatban.manxb and sach.matheloai='" + maLoai + "'", DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy danh sách theo mã nhà xuất bản
    public static DataTable laySachTheoMaNXB(string manxb)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "Sach");
        dap.SelectCommand = new SqlCommand("select *from sach,nhaxuatban,theloai where sach.matheloai=theloai.matheloai and sach.manxb=nhaxuatban.manxb and sach.manxb='" + manxb + "'", DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy danh sách theo từ khoá tìm kiếm
    public static DataTable timKiemSachTheoTuKhoa(string tuKhoa)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "Sach");
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandText = "TIMKIEMSACHTHEOTUKHOA";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@TUKHOA", tuKhoa);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy danh sách bán trên 10 lần (sách bán chạy)
    public static DataTable sachBanChay()
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "Sach");
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandText = "SACHBANCHAY";
        cmd.CommandType = CommandType.StoredProcedure;
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy danh sách sách theo từng thể lại và danh sách thể loại
    public static DataSet dsSachTungTheLoai()
    {
        if (DatabaseSql.con.State == System.Data.ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "theloai");
        dap.SelectCommand = new SqlCommand("select *from theloai", DatabaseSql.con);
        SqlDataAdapter dap1 = new SqlDataAdapter();
        dap1.TableMappings.Add("Table", "sach");
        dap1.SelectCommand = new SqlCommand("select *from sach", DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        dap1.Fill(ds);
        DatabaseSql.con.Close();
        return ds;
    }
    #endregion
    #region lấy danh sách được giảm giá (giảm giá >0)
    public static DataTable dsSachGiamGia()
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "Sach");
        dap.SelectCommand = new SqlCommand("select *from sach where giamGia>0", DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region xoá 1 cuốn sách theo mã sách
    public static int xoaSach(string ma)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "delete sach where masach=@masach";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@masach", ma);
        try
        {
            i = cmd.ExecuteNonQuery();

        }
        catch(Exception ex)
        {

        }
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region thêm mới 1 cuốn sách
    public static int themMoi(string masach, string tensach, int dongia, string dvt, string mota, string hinh, string namxb, string ngaycapnhat, string tentacgia, string matheloai, string manxb, string makm, int giamgia)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        if (makm.Equals(""))
            cmd.CommandText = "set dateformat dmy;insert into sach values(@masach,@tensach,@dongia,@donvitinh,@mota,@hinhminhhoa,@namxb,@ngaycapnhat,@tentacgia,@matheloai,@manxb,NULL,0)";
        else
        {
            cmd.CommandText = "set dateformat dmy;insert into sach values(@masach,@tensach,@dongia,@donvitinh,@mota,@hinhminhhoa,@namxb,@ngaycapnhat,@tentacgia,@matheloai,@manxb,@makm,@giamgia)";
            cmd.Parameters.AddWithValue("@makm", makm);
            cmd.Parameters.AddWithValue("@giamgia", giamgia);
        }
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@masach", masach);
        cmd.Parameters.AddWithValue("@tensach", tensach);
        cmd.Parameters.AddWithValue("@dongia", dongia);
        cmd.Parameters.AddWithValue("@donvitinh", dvt);
        cmd.Parameters.AddWithValue("@mota", mota);
        cmd.Parameters.AddWithValue("@hinhminhhoa", hinh);
        cmd.Parameters.AddWithValue("@namxb", namxb);
        cmd.Parameters.AddWithValue("@ngaycapnhat", ngaycapnhat);
        cmd.Parameters.AddWithValue("@tentacgia", tentacgia);
        cmd.Parameters.AddWithValue("@matheloai", matheloai);
        cmd.Parameters.AddWithValue("@manxb", manxb);

        try
        {
            i = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        { }
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region chỉnh sửa thông tin 1 cuốn sách
    public static int chinhSua(string masach, string tensach, int dongia, string dvt, string mota, string hinh, string namxb, string ngaycapnhat, string tentacgia, string matheloai, string manxb, string makm, int giamgia)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        if (makm.Equals(""))
            cmd.CommandText = "set dateformat dmy;update sach set tensach=@tensach,dongia=@dongia,donvitinh=@donvitinh,mota=@mota,hinhminhhoa=@hinhminhhoa,namxb=@namxb,ngaycapnhat=@ngaycapnhat,tentacgia=@tentacgia,matheloai=@matheloai,manxb=@manxb,makm=NULL,giamgia=0 where masach=@masach";
        else
        {
            cmd.CommandText = "set dateformat dmy;update sach set tensach=@tensach,dongia=@dongia,donvitinh=@donvitinh,mota=@mota,hinhminhhoa=@hinhminhhoa,namxb=@namxb,ngaycapnhat=@ngaycapnhat,tentacgia=@tentacgia,matheloai=@matheloai,manxb=@manxb,makm=@makm,giamgia=@giamgia where masach=@masach";
            cmd.Parameters.AddWithValue("@makm", makm);
            cmd.Parameters.AddWithValue("@giamgia", giamgia);
        }
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@masach", masach);
        cmd.Parameters.AddWithValue("@tensach", tensach);
        cmd.Parameters.AddWithValue("@dongia", dongia);
        cmd.Parameters.AddWithValue("@donvitinh", dvt);
        cmd.Parameters.AddWithValue("@mota", mota);
        cmd.Parameters.AddWithValue("@hinhminhhoa", hinh);
        cmd.Parameters.AddWithValue("@namxb", namxb);
        cmd.Parameters.AddWithValue("@ngaycapnhat", ngaycapnhat);
        cmd.Parameters.AddWithValue("@tentacgia", tentacgia);
        cmd.Parameters.AddWithValue("@matheloai", matheloai);
        cmd.Parameters.AddWithValue("@manxb", manxb);


        try
        {
            i = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        { }
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region lấy danh sách sách theo tên tác giả
    public static DataTable layDSTheoTacGia(string tenTG, string masach)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "Sach");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.CommandText = "select *from sach where tentacgia=@tentacgia and masach !=@masach";
        cmd.Parameters.AddWithValue("@tentacgia", tenTG);
        cmd.Parameters.AddWithValue("@masach", masach);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy danh sách theo mã nhà xuất bản
    public static DataTable laySachTheoMaNXB(string manxb, string masach)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "Sach");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.CommandText = "select *from sach where manxb=@manxb and masach !=@masach";
        cmd.Parameters.AddWithValue("@manxb", manxb);
        cmd.Parameters.AddWithValue("@masach", masach);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy danh sách theo mã thể loại
    public static DataTable laySachTheoMaLoai(string maLoai, string masach)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "Sach");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.CommandText = "select *from sach where masach!=@masach and matheloai=@matheloai";
        cmd.Parameters.AddWithValue("@masach", masach);
        cmd.Parameters.AddWithValue("@matheloai", maLoai);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion

}