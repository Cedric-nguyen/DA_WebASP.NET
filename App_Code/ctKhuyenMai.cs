using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ctKhuyenMai
/// </summary>
public class ctKhuyenMai
{

    #region lấy dữ liệu của bảng chương trình khuyến mãi  
    public static DataTable layChuongTrinhKhuyenMai()
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "ctkhuyenmai");
        dap.SelectCommand = new SqlCommand("select * from ctkhuyenmai", DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion

    #region xoá 1 chương trình khuyến mãi theo mã
    public static int xoaCTKM(string ma)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "delete ctkhuyenmai where makm=@makm";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@makm", ma);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion

    #region thêm mới 1 chương trình khuyến mãi
    public static int themMoi(string makm, string tenkm, int giamgia)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "insert into ctkhuyenmai values(@makm,@tenkm,@giamgia)";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@makm", makm);
        cmd.Parameters.AddWithValue("@tenkm", tenkm);
        cmd.Parameters.AddWithValue("@giamgia", giamgia);
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

    #region chỉnh sửa 1 chương trình khuyến mãi
    public static int chinhSua(string makm, string tenkm, int giamGia)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update ctkhuyenmai set tenkm=@tenkm,giamgia=@giamgia where makm=@makm";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@makm", makm);
        cmd.Parameters.AddWithValue("@tenkm", tenkm);
        cmd.Parameters.AddWithValue("@giamgia", giamGia);
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

    #region lấy 1 chương trình khuyễn mãi theo mã
    public static DataTable layCTKMTheoMa(string makm)
    {
        string sql = "select *from ctkhuyenmai where makm=@makm";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "ctkhuyenmai");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.Parameters.AddWithValue("@makm", makm);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion

    #region lấy  chương trình khuyến mãi theo tên
    public static DataTable layCTKMTheoTen(string tenkm)
    {
        string sql = "select *from ctkhuyenmai where tenkm=@tenkm";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "ctkhuyenmai");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.Parameters.AddWithValue("@tenkm", tenkm);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion

    #region tìm mã chương trình khuyến mãi theo tên chương trình khuyến mãi
    public static DataTable timMaTheoTen(string tenkm)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();

        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from ctkhuyenmai where tenkm=@tenkm";
        cmd.Parameters.AddWithValue("@tenkm", tenkm);
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "ctkhuyenmai");
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        return ds.Tables[0];
    }
    #endregion
}