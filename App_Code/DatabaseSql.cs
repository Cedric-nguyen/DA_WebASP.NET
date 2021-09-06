using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DatabaseSql
/// </summary>
public class DatabaseSql
{
    //chuỗi kết nối với sql
    public static SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["strcon"].ConnectionString);    
}