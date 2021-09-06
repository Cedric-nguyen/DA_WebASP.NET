using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for hoaDon
/// </summary>
public class hoaDon
{
    #region lấy mã hoá đơn vừa tạo
    public static string maHDVuaTao()
    {
        string sql = "select top(1) mahd from hoadon order by mahd desc";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        int i = -1;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        string ma = cmd.ExecuteScalar().ToString();
        DatabaseSql.con.Close();
        return ma;
    }
    #endregion
    #region thêm 1 hoá đơn
    public static int insert(int thanhTien, string email, string manv)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        int i = -1;
        string sql = "insert into HOADON(ngaylap,thanhtien,email,manv)  values(getdate(),@thanhtien,@email,@manv)";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@thanhtien", thanhTien);
        cmd.Parameters.AddWithValue("@manv", manv);
        cmd.Parameters.AddWithValue("@email", email);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region lấy danh sách hoá đơn
    public static DataTable layDSHD()
    {
        string sql = "select *from hoadon,nhanvien where hoadon.manv=nhanvien.manv";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "hoadon");
        dap.SelectCommand = new SqlCommand(sql, DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region xoá 1 hoá đơn theo mã hoá đơn
    public static int xoaHD(string ma)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "delete hoadon where mahd=@mahd";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@mahd", ma);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
}