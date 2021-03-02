using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["error"]) && Request["error"].Equals("1"))
        {
            imgError.Src = "assets/layouts/layout2/img/error2.png";
        }else  if (!string.IsNullOrEmpty(Request["error"]) && Request["error"].Equals("2"))
        {
            imgError.Src = "assets/layouts/layout2/img/error3.png";
        }
        else if (!string.IsNullOrEmpty(Request["error"]) && Request["error"].Equals("3"))
        {
            imgError.Src = "assets/layouts/layout2/img/error4.png";
        }
        else if (!string.IsNullOrEmpty(Request["error"]) && Request["error"].Equals("4"))
        {
            imgError.Src = "assets/layouts/layout2/img/error5.png";
        }
    }
}