using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for nhanVien
/// </summary>
public class nhanVien
{
    #region lấy danh sách nhân viên
    public static DataTable layDSNV()
    {
        string sql = "select *from nhanvien";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "nhanvien");
        dap.SelectCommand = new SqlCommand(sql, DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region xoá 1 nhân viên theo mã nhân viên
    public static int xoaNV(string ma)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "delete nhanvien where manv=@manv";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@manv", ma);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region thêm mới 1 nhân viên
    public static int themMoi(string manv, string tennv, string diachi, string sdt, string gioitinh, string ngayvl, int luong)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "set dateformat dmy;insert into nhanvien values(@manv,@tennv,@diachi,@sdt,@gioitinh,@ngayvl,@luong)";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@manv", manv);
        cmd.Parameters.AddWithValue("@tennv", tennv);
        cmd.Parameters.AddWithValue("@diachi", diachi);
        cmd.Parameters.AddWithValue("@sdt", sdt);
        cmd.Parameters.AddWithValue("@gioitinh", gioitinh);
        cmd.Parameters.AddWithValue("@ngayvl", ngayvl);
        cmd.Parameters.AddWithValue("@luong", luong);
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
    #region hàm chỉnh sửa 1 nhân viên
    public static int chinhSua(string manv, string tennv,string diachi,string sdt,string gioitinh,string ngayvl,int luong)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "set dateformat dmy; update nhanvien set tennv=@tennv,diachi=@diachi,sdt=@sdt,gioitinh=@gioitinh,ngayvl=@ngayvl,luong=@luong where manv=@manv";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@manv", manv);
        cmd.Parameters.AddWithValue("@tennv", tennv);
        cmd.Parameters.AddWithValue("@diachi", diachi);
        cmd.Parameters.AddWithValue("@sdt", sdt);
        cmd.Parameters.AddWithValue("@gioitinh", gioitinh);
        cmd.Parameters.AddWithValue("@ngayvl", ngayvl);
        cmd.Parameters.AddWithValue("@luong", luong);
        try { 
        i = cmd.ExecuteNonQuery();
        }
        catch(Exception ex)
        {

        }
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region hàm lấy 1 nhân viên theo mã nhân viên
    public static DataTable layNVTheoMa(string manv)
    {
        string sql = "select *from nhanvien where manv=@manv";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "nhanvien");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = DatabaseSql.con;
        cmd.Parameters.AddWithValue("@manv", manv);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
}