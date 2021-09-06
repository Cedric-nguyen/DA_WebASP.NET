using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for khacHang
/// </summary>
public class khacHang
{
    #region lấy thông tin khách hàng theo email khách hàng
    public static DataTable layThongTinKHTheoEmail(string email)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "khachhang");
        dap.SelectCommand = new SqlCommand("select *from khachhang where email='" + email + "'", DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region Kiểm tra thông tin khi khách hàng đăng nhập
    public static DataTable dangNhap(string email, string matKhau)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "khachhang");
        dap.SelectCommand = new SqlCommand("select *from khachhang where email='" + email + "' and matkhau='" + matKhau + "'", DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region hàm xử lý khi khách hàng đăng kí tài khoản
    public static int dangKy(string email, string matKhau, string diachi, string sdt, string tenkh, string gioiTinh)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        int i = -1;
        try {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DatabaseSql.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT KHACHHANG(EMAIL, MATKHAU, TENKH, DIACHI, SDT, GIOITINH) VALUES(@email,@matkhau,@tenkh,@diachi,@sdt,@gioitinh)";
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@matkhau", matKhau);
            cmd.Parameters.AddWithValue("@diachi", diachi);
            cmd.Parameters.AddWithValue("@sdt", sdt);
            cmd.Parameters.AddWithValue("@tenkh", tenkh);
            cmd.Parameters.AddWithValue("@gioitinh", gioiTinh);
            i = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region lấy danh sách khách hàng
    public static DataTable layDSKH()
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "khachhang");
        dap.SelectCommand = new SqlCommand("select *from khachhang", DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region xoá 1 khách hàng theo mã khách hàng
    public static int xoaKH(string ma)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "delete khachhang where email=@email";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@email", ma);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region reset mật khẩu của khách hàng theo email khách hàng
    public static int resetMK(string email)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update khachhang set matkhau='202cb962ac59075b964b07152d234b70' where email=@email";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@email",email);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region tìm tên khách hàng theo email khách hàng
    public static string timTenTheoEmail(string email)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        string values = string.Empty;
        string sql = "select tenkh from khachhang where email=@email";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@email", email);
        values = cmd.ExecuteScalar().ToString();
        DatabaseSql.con.Close();
        return values;
    }
    #endregion
    #region hàm xử lý đổi mật khẩu cho 1 khách hàng theo email
    public static int doiMatKhau(string email,string matKhauMoi)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update khachhang set matkhau=@matkhaumoi where email=@email";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@matkhaumoi", matKhauMoi);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region hàm lấy mật khẩu theo email của khách hàng

    public static string checkMK(string email)
    {
        string values = "";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select matkhau from khachhang where email=@email";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@email", email);
         values=cmd.ExecuteScalar().ToString();
        DatabaseSql.con.Close();
        return values;
    }
    #endregion
}