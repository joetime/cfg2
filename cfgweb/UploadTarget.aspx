<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadTarget.aspx.cs" Inherits="cfgweb.UploadTarget" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Files.Count > 0)
        {
            Response.Clear();
            
            // (handles one file only)
            HttpPostedFile file = Request.Files[0];
            
            // get a valid file name
            string testname = file.FileName;
            string testpath = Server.MapPath("~/Imports/" + testname);
            
            int count = 1;
            while (System.IO.File.Exists(testpath)) {
                
                // somename_1.txt, somename_2.txt, etc. 
                testname = file.FileName.Replace(".", 
                    String.Format("_{0},", count));
                testpath = Server.MapPath("~/Imports/" + testname);
                
                count++;
            } 
            
            // save file
            file.SaveAs(testpath);

            // collect other form data
            int month = Int32.Parse(Request["month"]);
            int year = Int32.Parse(Request["year"]);
            string provider = Request["provider"];

            cfglib.Repos repos = new cfglib.Repos("UploadTarget"); 
            repos.ImportFile(testpath, month, year, provider);
            
            // send response
            Response.Write("200");
            Response.End();
        }
        
    }
</script>