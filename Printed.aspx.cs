using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Printed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

                if (string.IsNullOrEmpty(Request["id"]))
                {
                    Response.Redirect("error.aspx",false);
                }
                else
                {
                    string admissionNo = Helper.Decrypt(Request["id"]);
                    if (vaildNumber(admissionNo) || RefusedNumber(admissionNo))
                    {
                        //Helper.setConnectionProxy();
                        //MociServiceReference.PrintAdmissionClient moci = new MociServiceReference.PrintAdmissionClient();
                        //moci.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WSUser"];
                        //moci.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WSPassword"];
                        getdata(admissionNo);
                    }
                    else
                    {
                        Response.Redirect("error.aspx?error=1", false);
                    }
                }
            }
            catch (Exception ex)
            {
                  StackTrace st = new StackTrace(ex,true);
                  Helper.logError(ex);
                  Response.Redirect("error.aspx?error=3");
            }
        }
    }
    private bool vaildNumber(string admissionNo)
    {
        OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
        con1.Open();
        OdbcCommand command = new OdbcCommand(@"select * from AdmissionRequest where admissionNo =? ", con1);
        command.Parameters.Add(new OdbcParameter("@admissionNo", admissionNo));
        DataSet results = new DataSet();
        OdbcDataAdapter usersAdapter = new OdbcDataAdapter(command);
        usersAdapter.Fill(results);
        DataTable dt = results.Tables[0];
        if (dt.Rows.Count > 0)
        {
			return false;
        }
        else
        {
            return true;
        }
    }
	
	private bool RefusedNumber(string admissionNo)
	{
		OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
		con1.Open();
		OdbcCommand command = new OdbcCommand(@"select * from AdmissionDept where admissionNo =? and (Notes is not null or notes !='') and (deptno is null or deptno ='') ", con1);
		command.Parameters.Add(new OdbcParameter("@admissionNo", admissionNo));
		DataSet results = new DataSet();
		OdbcDataAdapter usersAdapter = new OdbcDataAdapter(command);
		usersAdapter.Fill(results);
		DataTable dt = results.Tables[0];
		if (dt.Rows.Count > 0)
        {
			return true;
        }
        else
        {
            return false;
        }
		
	}
	
	
	
	
	
	
	
    private void getdata(string AdmissionNo)
    {
        //Helper.setConnectionProxy();
        MociServiceReference.OrdersAdmissionClient moci = new MociServiceReference.OrdersAdmissionClient();
        moci.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WSUser"];
        moci.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WSPassword"];
        MociServiceReference.WSPrintPermission item = moci.GetPrintAdmissionInfo(AdmissionNo);
        if (item != null && !string.IsNullOrEmpty(item.AdmissionNo))
        {
            txtAuthName.Value = item.Authors;
            if (Helper.correctDate(item.ApplicaintBirthdate) == "")
            {
                mask_date2.Disabled = false;
            }
            else
            {
                mask_date2.Value = Helper.correctDate(item.ApplicaintBirthdate);
                mask_date2.Disabled = true;
            }

            txtBookTitle.Value = item.BookTitle;
            txtPubName.Value = item.Publishers;
            txtEditionNO.Value = item.EditionNo;
            txtPagesNo.Value = item.PagesNo;
            txtDepPhone.Value = item.ApplicaintMobile;
            txtDepEmail.Value = item.ApplicaintEmail;
            hijry.Items.Add(new ListItem("", ""));
            gerg.Items.Add(new ListItem("", ""));

            foreach (int element in Helper.getHijryDDL())
            {
                hijry.Items.Add(new ListItem(element.ToString(), element.ToString()));
            }
            foreach (int element in Helper.getGerDDL())
            {
                gerg.Items.Add(new ListItem(element.ToString(), element.ToString()));
            }
            foreach (Tuple<string, string> element in Helper.getPrintedWorkTypes())
            {
                types.Items.Add(new ListItem(element.Item2, element.Item1));
            }
            foreach (Tuple<string, string> element in Helper.getCities())
            {
                pubCity.Items.Add(new ListItem(element.Item2, element.Item1));
            }
            foreach (Tuple<string, string> element in Helper.getCities())
            {
                deptCity.Items.Add(new ListItem(element.Item2, element.Item1));
            }
            txtRemarks.Value = item.AboutBook;
        }
        else
        {
            Response.Redirect("error.aspx?error=3", false);
        }
    }
    protected void btnSubmet_Click(object sender, EventArgs e)
    {
        int? result = insert();

        if (result == 1)
        {
            Response.Redirect("Done.aspx", false);

        }
        else if (result == -2)
        {
            Response.Redirect("Error.aspx", false);
        }
        else if (result == -3)
        {
            Response.Redirect("error.aspx?error=2", false);
        }
    }
    public int insert()
    {
        try
        {
            List<string> images = new List<string>();
            try
            {
                for (int i = 0; i < int.Parse(hcfCount.Value); i++)
                {
                    HttpPostedFile file = Request.Files["image" + (i + 1).ToString()];

                    //check file was submitted
                    if (file != null && file.ContentLength > 0)
                    {
                        string fname = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                        file.SaveAs(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings["Files"], fname)));
                        images.Add(fname);
                    }
                }
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(ex, true);
                Helper.logError(ex);
                return -3;
            }
            //Helper.setConnectionProxy();
            MociServiceReference.OrdersAdmissionClient moci = new MociServiceReference.OrdersAdmissionClient();
            moci.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WSUser"];
            moci.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WSPassword"];
            string adminsstionNum = Helper.Decrypt(Request["id"]);
            MociServiceReference.WSPrintPermission item = moci.GetPrintAdmissionInfo(adminsstionNum);
            if (txtRemarks.Value.Length > 254)
            {
                txtRemarks.Value = txtRemarks.Value.Substring(0, 254);
            }

            OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
            con1.Open();
			
			if(RefusedNumber(item.AdmissionNo.Trim()))
			{
				OdbcCommand delete = new OdbcCommand(@"delete from admissionrequest where admissionNo = ?", con1);
				delete.Parameters.Add(new OdbcParameter("@admissionNo", item.AdmissionNo.Trim()));
				delete.ExecuteNonQuery();
				
				delete = new OdbcCommand(@"delete from admissiondept where admissionNo = ?", con1);
				delete.Parameters.Add(new OdbcParameter("@admissionNo", item.AdmissionNo.Trim()));
				delete.ExecuteNonQuery();
				
				
				delete = new OdbcCommand(@"delete from AdmissionImages where admissionNo = ?", con1);
				delete.Parameters.Add(new OdbcParameter("@admissionNo", item.AdmissionNo.Trim()));
				delete.ExecuteNonQuery();
			}
			
            OdbcCommand command = new OdbcCommand(@"insert into dbo.AdmissionRequest 
                                                                ( admissionNo   ,
                                                                  authName,
                                                                  authNationality,
                                                                  authBirthDate,
                                                                  bookTitle,
                                                                  pubName,
                                                                  pubCityID,
                                                                  Hdate,
                                                                  GDate,
                                                                  workTypeID,
                                                                  editionNO,
                                                                  pagesNo,
                                                                  partNo,
                                                                  bookSize,
                                                                  bookPrice,
                                                                  seriesName,
                                                                  seriesNo,
                                                                  remarks,
                                                                  mobile,
                                                                  fax,
                                                                  postOffice,
                                                                  deptCity,
                                                                  zip,
                                                                  deptNID,
                                                                  deptName,
                                                                  deptEmail, 
                                                                  admissionDate,
                                                                  AdmissionFile,
                                                                  CreationDate,
																  expiry_Date)
	                                                     values ( ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,? )", con1);

            command.Parameters.Add(new OdbcParameter("@admissionNo", item.AdmissionNo.Trim()));
            command.Parameters.Add(new OdbcParameter("@authName", item.Authors.Trim()));
            command.Parameters.Add(new OdbcParameter("@authNationality", countries_list.Items[countries_list.SelectedIndex].Text));
            command.Parameters.Add(new OdbcParameter("@authBirthDate", item.ApplicaintBirthdate.Trim()));
            command.Parameters.Add(new OdbcParameter("@bookTitle", item.BookTitle.Trim()));
            command.Parameters.Add(new OdbcParameter("@pubName", item.Publishers.Trim()));
            command.Parameters.Add(new OdbcParameter("@pubCityID", pubCity.Items[pubCity.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@Hdate", hijry.Items[hijry.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@GDate", gerg.Items[gerg.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@workTypeID", types.Items[types.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@editionNO", txtEditionNO.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@pagesNo", txtPagesNo.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@partNo", txtNumOfParts.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@bookSize", txtBooSize.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@bookPrice", txtBookPrice.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@seriesName", txtSeriesName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@seriesNo", txtSeriesNum.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@remarks", txtRemarks.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@mobile", txtDepPhone.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@fax", txtDepFax.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@postOffice", txtDepPost.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptCity", deptCity.Items[deptCity.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@zip", txtDepZip.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptNID", txtDepNID.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptName", txtDepName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptEmail", txtDepEmail.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@admissionDate", item.AdmissionDate.Trim()));
            command.Parameters.Add(new OdbcParameter("@AdmissionFile", item.AdmissionFile.Trim()));
            command.Parameters.Add(new OdbcParameter("@CreationDate", DateTime.Now));
			command.Parameters.Add(new OdbcParameter("@expiryDate",  DateTime.Parse(item.ExpiryDate)));
            command.ExecuteNonQuery();

            foreach (string image in images)
            {
                OdbcCommand command2 = new OdbcCommand(@"insert into dbo.AdmissionImages 
                                                                ( AdmissionNo,
                                                                  ImageID
                                                                )
	                                                     values ( ?,? )", con1);
                command2.Parameters.Add(new OdbcParameter("@AdmissionNo", item.AdmissionNo));
                command2.Parameters.Add(new OdbcParameter("@ImageID", image));
                command2.ExecuteNonQuery();
            }
            moci.PropertyChanged(item.AdmissionNo);
            return 1;
        }
        catch (Exception ex)
        {
            StackTrace st = new StackTrace(ex, true);
            Helper.logError(ex);
            return -2;
        }
    }
}