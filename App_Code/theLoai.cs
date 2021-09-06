using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for theLoai
/// </summary>
public class theLoai
{
    #region lấy danh sách thể loại
    public static DataTable layDSTheLoai()
    {
        string sql = "select *from theloai";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "theloai");
        dap.SelectCommand = new SqlCommand(sql, DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region xoá 1 thể loại theo mã
    public static int xoaTheLoai(string ma)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "delete theloai where matheloai=@matheloai";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@matheloai", ma);
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
    #region thêm mới 1 thể loại
    public static int themMoi(string maTL, string tenTL)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "insert into theLoai values(@maTL,@tenTL)";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@maTL", maTL);
        cmd.Parameters.AddWithValue("@tenTL", tenTL);
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
    #region chỉnh sửa thông tin 1 thể loại
    public static int chinhSua(string maTL, string tenTL)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update theloai set tentheloai=@tentheloai where matheloai=@matheloai";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@matheloai", maTL);
        cmd.Parameters.AddWithValue("@tentheloai", tenTL);
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
    #region lấy thông tin 1 thể loại theo mã
    public static DataTable layTLTheoMa(string maTL)
    {
        string sql = "select *from theloai where matheloai=@matheloai";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "theloai");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.Parameters.AddWithValue("@matheloai", maTL);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy mã thể loại theo tên thể loại
    public static string timMaTheoTen(string tenTL)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();

        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select matheloai from theloai where tentheloai=@tentheloai";
        cmd.Parameters.AddWithValue("@tentheloai", tenTL);
        string matheloai = cmd.ExecuteScalar().ToString();
        DatabaseSql.con.Close();
        return matheloai;
    }
    #endregion
}