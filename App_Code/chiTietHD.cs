using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for chiTietHD
/// </summary>
public class chiTietHD
{

    #region thêm 1 chi tiết hoá đơn
    public static int insert(string mahd, string masach, int soLuong, int donGia, int giamgia, int thanhTien)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        int i = -1;
        string sql = "insert into CHITIETHD values(@mahd,@masach,@soluong,@dongia,@giamgia,@thanhtien)";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@thanhtien", thanhTien);
        cmd.Parameters.AddWithValue("@soluong", soLuong);
        cmd.Parameters.AddWithValue("@dongia", donGia);
        cmd.Parameters.AddWithValue("@masach", masach);
        cmd.Parameters.AddWithValue("@giamgia", giamgia);    
        cmd.Parameters.AddWithValue("@mahd", mahd);
       try
        {
            i = cmd.ExecuteNonQuery();
        }catch(Exception ex)
        { }
        DatabaseSql.con.Close();
        return i;
    }
    #endregion

    #region lấy danh sách chi tiết hoá đơn theo mã hoá đơn
    public static DataTable layDSCTHDTheoMa(int mahd)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select *from CHITIETHD where MAHD=@mahd";
        cmd.Parameters.AddWithValue("@mahd", mahd);
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
}