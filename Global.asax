<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %> 
<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        if(!File.Exists(Server.MapPath("demSL.txt")))
           File.WriteAllText(Server.MapPath("demSL.txt"),"0");
        Application["DaTruyCap"] =int.Parse(File.ReadAllText(Server.MapPath("demSL.txt")));  
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
       if (Application["DangTruyCap"] == null) 
            Application["DangTruyCap"] = 1; 
        else 
            Application["DangTruyCap"] = (int)Application["DangTruyCap"] + 1; 
        // Tăng số đã truy cập lên 1 nếu có khách truy cập
        Application["DaTruyCap"] = (int)Application["DaTruyCap"] + 1;
       File.WriteAllText(Server.MapPath("demSL.txt"), Application["DaTruyCap"].ToString()); 

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Application["DangTruyCap"] = (int)Application["DangTruyCap"] - 1; 

    }

</script>
