using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for donDatHang
/// </summary>
public class donDatHang
{
    #region thêm 1 đơn đặt hàng
    public static int insert(int thanhTien, string diaChiNhanHang, string email)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        int i = -1;
        string sql = "insert into dondathang(ngaytao,thanhtien,diachinhanhang,email,tinhtrang)  values(getdate(),@thanhtien,@diachi,@email,0)";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@thanhtien", thanhTien);
        cmd.Parameters.AddWithValue("@diachi", diaChiNhanHang);
        cmd.Parameters.AddWithValue("@email", email);
        i = cmd.ExecuteNonQuery();      
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region lấy ra mã đơn đặt hàng vừa tạo
    public static string maDonDatHangVuaTao()
    {
        string sql = "select top(1) madonhang from DONDATHANG order by MADONHANG desc";
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
    #region tìm đơn đặt hàng theo email khách hàng
    public static DataTable timDonHangTheoEmail(string email)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "dondathang");
        dap.SelectCommand = new SqlCommand("select * from DONDATHANG where email='" + email + "'", DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region tìm đơn đặt hàng theo eamil khách hàng và ngày tạo
    public static DataTable timDonHangTheoEmailAndNgayTao(string email, string ngayTao)
    {
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "dondathang");
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "set dateformat dmy; select * from DONDATHANG where email=@email and ngaytao=@ngaytao";
        cmd.Parameters.AddWithValue("@ngaytao", ngayTao);
        cmd.Parameters.AddWithValue("@email", email);

        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region lấy danh sách đơn đặt hàng
    public static DataTable layDSDDH()
    {
        string sql = "select *from dondathang";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "dondathang");
        dap.SelectCommand = new SqlCommand(sql, DatabaseSql.con);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region xoá 1 đơn đặt hàng theo mã đơn đặt hàng
    public static int xoaDDH(string ma)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "delete dondathang where madonhang=@madonhang";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@madonhang", ma);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
    #region lấy đơn đặt hàng theo mã đơn đặt hàng
    public static DataTable layDonHangTheoMa(int madonhang)
    {
        string sql = "select *from dondathang where madonhang=@madonhang";
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        SqlDataAdapter dap = new SqlDataAdapter();
        dap.TableMappings.Add("Table", "dondathang");
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DatabaseSql.con;
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@madonhang", madonhang);
        dap.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dap.Fill(ds);
        DatabaseSql.con.Close();
        return ds.Tables[0];
    }
    #endregion
    #region update lại tình trạng đơn đặt hàng lại thành đã giao(1)
    public static int updateTinhTrangDaGiao(int madonhang)
    {
        int i = -1;
        if (DatabaseSql.con.State == ConnectionState.Open)
            DatabaseSql.con.Close();
        DatabaseSql.con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update dondathang set tinhtrang=1 where madonhang=@madonhang";
        cmd.Connection = DatabaseSql.con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@madonhang", madonhang);
        i = cmd.ExecuteNonQuery();
        DatabaseSql.con.Close();
        return i;
    }
    #endregion
}