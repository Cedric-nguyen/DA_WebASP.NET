using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for chiTietDDH
/// </summary>
public class chiTietDDH
{
    #region thêm 1 chi tiết đơn đặt hàng
    public static int insert(string maddh,string masach,int soLuong,int donGia,int giamgia,int thanhTien)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        int i = -1;
        string sql = "insert into CHITIETDONDATHANG values(@maddh,@masach,@soluong,@dongia,@giamgia,@thanhtien)";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@thanhtien", thanhTien);
        cmd.Parameters.AddWithValue("@soluong", soLuong);
        cmd.Parameters.AddWithValue("@dongia", donGia);
        cmd.Parameters.AddWithValue("@masach", masach);
        cmd.Parameters.AddWithValue("@giamgia", giamgia);    
        cmd.Parameters.AddWithValue("@maddh", maddh);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion

    #region lấy danh sách chi tiết đơn hàng theo mã đơn đặt hàng
    public static DataTable layDSCTDDHTheoMa(int maddh)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select *from CHITIETDONDATHANG where MADONDATHANG=@maddh";
        cmd.Parameters.AddWithValue("@maddh", maddh);
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "chitietdondathang");
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
}