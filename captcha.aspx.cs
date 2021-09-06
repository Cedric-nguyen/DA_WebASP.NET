using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class captcha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int ImageWidth = 85;
        int ImageHeight = 28;
        string[] chars = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "r", "s", "t", "v", "u", "x", "y", "z", "w", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        Random MyRand = new Random();
        string captcha = string.Empty;
        for (int i = 0; i < 4; i++)
        {
            captcha += chars[MyRand.Next(0, 56)];
        }
        Session["Captcha"] = captcha.ToString();
        string TextToCreate = Session["Captcha"].ToString();
        Brush newBrush = Brushes.Black;
        HatchBrush myBrush = new HatchBrush(HatchStyle.DottedDiamond, Color.Tomato, Color.YellowGreen);
        Pen myPen = new Pen(myBrush, 6);
        Font newFont = new Font("Century", 18, FontStyle.Regular);
        Bitmap newBitmap = new Bitmap(ImageWidth, ImageHeight);
        Graphics newGraphics = Graphics.FromImage(newBitmap);
        newGraphics.FillRectangle(myBrush, 0, 0, ImageWidth, ImageHeight);
        newGraphics.DrawString(TextToCreate, newFont, newBrush, 1, 1);
        newGraphics.SmoothingMode = SmoothingMode.HighQuality;
        newGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        Response.ContentType = "image/png";
        newBitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
        newGraphics.Dispose();
        newBitmap.Dispose();
        myPen.Dispose();
        newFont.Dispose();
    }
}