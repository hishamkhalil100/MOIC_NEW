using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //MociServiceReference.OrdersAdmissionClient moci = new MociServiceReference.OrdersAdmissionClient();
        //moci.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WSUser"];
        //moci.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WSPassword"];

        //MociServiceReference.WSPrintPermission item = moci.GetPrintAdmissionInfo("100220190016565");

        //Response.Write(item.BookTitle);

        //foreach (var VARIABLE in moci.OrdersListNeedISBN())
        //{
        //    Response.Write(VARIABLE.BookTitle +"      "+VARIABLE.BookType+"      </br>");
        //}


       // Response.Write( new Helper().Encrypt("100220190016565"));

    }
}