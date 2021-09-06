using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for quanTri
/// </summary>
public class quanTri
{
    #region kiểm tra thông tin khi 1 quản trị đăng nhập
    public static DataTable dangNhap(string email,string matkhau)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "quantri");
        dap.SelectCommand =new SqlCommand("select *from quantri where email='" + email + "'and matkhau='" + matkhau+"'",DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy danh sách quản trịs
    public static DataTable layDSQT()
    {
        string sql = "select *from quantri";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "quantri");
        dap.SelectCommand = new SqlCommand(sql, DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region xoá 1 quản trị theo mã quản trị
    public static int xoaQT(string ma)
    {
        int i = -1;
        if (ma.Equals("admin@gmail.com"))
            return i;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "delete quantri where email=@email";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@email", ma);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region thêm mới 1 quản trị
    public static int themMoi(string email, string matkhau,string tenhienthi)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "insert into quantri values(@email,@matkhau,@tenhienthi)";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@matkhau", matkhau);
        cmd.Parameters.AddWithValue("@tenhienthi", tenhienthi);
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
    #region chỉnh sửa thông tin 1 quản trị
    public static int chinhSua(string email, string matkhau,string tenhienthi)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update quantri set matkhau=@matkhau,tenhienthi=@tenhienthi where email=@email";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@matkhau", matkhau);
        cmd.Parameters.AddWithValue("@tenhienthi", tenhienthi);
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
    #region tìm tên quản trị theo email
    public static string timTenTheoEmail(string email)
    {
        string values = string.Empty;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select TENHIENTHI from quantri where email=@email";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@email", email);
        values = cmd.ExecuteScalar().ToString();
        DatabaseSql.con.Close();
        return values;
    }
    #endregion
    #region lấy thông tin 1 quản trị theo email
    public static DataTable layQTTheoEmail(string email)
    {
        string sql = "select *from quantri where email=@email";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "quantri");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.Parameters.AddWithValue("@email", email);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
}