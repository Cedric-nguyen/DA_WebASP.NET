using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_loadControls : System.Web.UI.UserControl
{
    private string modul = "";
    private string submodul = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["modul"] != null)
            modul = Request.QueryString["modul"];
        if (Request.QueryString["submodul"] != null)
            submodul = Request.QueryString["submodul"];
        switch (modul)
        {
            case "dn":
            case "dk":
                PlaceHolder1.Controls.Add(LoadControl("Account/loginRegisterControl.ascx"));
                break;            
            case "admin":
                switch (submodul)
                {
                    case "qldmb":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/categoryBook.ascx"));
                        break;
                    case "qldsb":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/listBook.ascx"));
                        break;
                    case "qldstk":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/listAccount.ascx"));
                        break;
                    case "tmdmb":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/AddNew/addCategoryBook.ascx"));
                        break;
                    case "tmb":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/AddNew/addBook.ascx"));
                        break;
                    case "tmnxb":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/AddNew/publishcompany.ascx"));
                        break;
                    case "tmqt":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/AddNew/addAdministrator.ascx"));
                        break;
                    case "tmctkm":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/AddNew/addSale.ascx"));
                        break;
                    case "tmnv":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/AddNew/addemployee.ascx"));
                        break;
                    case "qldsnv":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/employee.ascx"));
                        break;
                    case "qldsnxb":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/publishcompany.ascx"));
                        break;
                    case "qlct":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/details.ascx"));
                        break;
                    case "dddh":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/acceptOrder.ascx"));
                        break;
                    case "qldsqt":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/administrator.ascx"));
                        break;

                    case "qlctkm":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/sale.ascx"));
                        break;
                    case "qlddh":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/order.ascx"));
                        break;

                    case "qlhd":
                        PlaceHolder1.Controls.Add(LoadControl("Admin/Manager/bill.ascx"));
                        break;
                }
                break;
            case "s":
                PlaceHolder1.Controls.Add(LoadControl("Book/book.ascx"));
                break;
            case "gt":
                PlaceHolder1.Controls.Add(LoadControl("Introduce_history/introduce.ascx"));
                break;
            case "tl":
            case "nxb":
                PlaceHolder1.Controls.Add(LoadControl("Book/listBook.ascx"));
                break;
            case "tk":
                PlaceHolder1.Controls.Add(LoadControl("Book/timKiemSach.ascx"));
                break;
            case "sbc":
                PlaceHolder1.Controls.Add(LoadControl("Book/sachBanChay.ascx"));
                break;
            case "ctkm":
                PlaceHolder1.Controls.Add(LoadControl("Book/chuongTrinhKhuyenMai.ascx"));
                break;
            case "lsdh":
                PlaceHolder1.Controls.Add(LoadControl("Introduce_history/historyorder.ascx"));
                break;
            case "ktdh":
                PlaceHolder1.Controls.Add(LoadControl("Introduce_history/checkOrder.ascx"));
                break;
            case "ggdb":
                PlaceHolder1.Controls.Add(LoadControl("Book/giamGiaDB.ascx"));
                break;
        }



    }
}