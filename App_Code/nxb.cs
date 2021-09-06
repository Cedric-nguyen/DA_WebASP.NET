using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for nxb
/// </summary>
public class nxb
{
    #region hàm lấy danh sách nhà xuất bản   
    public static DataTable layDSNXB()
    {
        string sql = "select *from nhaxuatban";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "nhaxuatban");
        dap.SelectCommand = new SqlCommand(sql, DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region hàm xoá 1 nhà xuất bản theo mã nhà xuất bản
    public static int xoaNXB(string ma)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "delete nhaxuatban where manxb=@manxb";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@manxb", ma);
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
    #region hàm thêm mới 1 nhà xuất bản
    public static int themMoi(string maNXB, string tenNXB)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "insert into nhaxuatban values(@manxb,@tennxb)";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@manxb", maNXB);
        cmd.Parameters.AddWithValue("@tennxb", tenNXB);
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
    #region hàm chỉnh sửa 1 nhà xuất bản
    public static int chinhSua(string maNXB, string tenNXB)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update nhaxuatban set tennxb=@tennxb where manxb=@manxb";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@manxb", maNXB);
        cmd.Parameters.AddWithValue("@tennxb", tenNXB);
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
    #region lấy nhà xuất bản theo mã 
    public static DataTable layNXBTheoMa(string manxb)
    {
        string sql = "select *from nhaxuatban where manxb=@manxb";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "nhaxuatban");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.Parameters.AddWithValue("@manxb", manxb);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy mã nhà xuất bản theo tên nhà xuất bản
    public static string timMaTheoTen(string tennxb)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();

        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select manxb from nhaxuatban where tennxb=@tennxb";
        cmd.Parameters.AddWithValue("@tennxb", tennxb);
        string manxb = cmd.ExecuteScalar().ToString();
        DatabaseSql.con.Close();
        return manxb;
    }
    #endregion

}