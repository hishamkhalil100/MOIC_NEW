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
using System.Web.Script.Serialization;
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
                getdata();
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    ViewState["id"] = Helper.Decrypt(Request["id"]);
                    if (ValidCheack())
                    {
                        loadPrevData();
                    }
                    else
                    {
                        Response.Redirect("error.aspx?error=1", false);
                    }
                }
                else
                {
                    ViewState["id"] = "";
                }
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(ex, true);
                Helper.logError(ex);
                Response.Redirect("error.aspx?error=3");
            }
        }
    }
    private void getdata()
    {



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

    }

    protected void btnSubmet_Click(object sender, EventArgs e)
    {
        bool isCapthcaValid = ValidateCaptcha(Request["g-recaptcha-response"]);
        try
        {
            if (isCapthcaValid)
            {
                if (ModelState.IsValid)
                {
                    int? result = null;
                    if (!string.IsNullOrEmpty(ViewState["id"].ToString()))
                    {
                        result = edit();
                    }
                    else
                    {
                        result = insert();
                    }
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
            }
            else
            {
                Response.Redirect("error.aspx?error=4", false);
            }
        }
        catch
        {
            Response.Redirect("Error.aspx", false);
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
            Guid id = Guid.NewGuid();
            OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
            con1.Open();
            if (txtRemarks.Value.Length > 254)
            {
                txtRemarks.Value = txtRemarks.Value.Substring(0, 254);
            }

            OdbcCommand command = new OdbcCommand(@"insert into dbo.GovRequests ( 
                                                        id,
                                                        authName, 
                                                        authBirthDate,
                                                        authNationality,
                                                        bookPrice,
                                                        bookSize,
                                                        bookTitle,
                                                        deptCity,
                                                        deptEmail,
                                                        deptName,
                                                        deptNID,
                                                        editionNo,
                                                        fax,
                                                        gDate,
                                                        hDate,
                                                        mobile,
                                                        pagesNo,
                                                        partNo,
                                                        postOffice,
                                                        pubcityID,
                                                        pubName,
                                                        remarks,
                                                        seriesName,
                                                        seriesNo,
                                                        zip,
                                                        workTypeID)
	                                                values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", con1);
            command.Parameters.Add(new OdbcParameter("@id", id.ToString()));
            command.Parameters.Add(new OdbcParameter("@authName", txtAuthName.Value));
            command.Parameters.Add(new OdbcParameter("@authBirthDate", mask_date2.Value));
            command.Parameters.Add(new OdbcParameter("@authNationality", countries_list.Items[countries_list.SelectedIndex].Text));
            command.Parameters.Add(new OdbcParameter("@bookPrice", txtBookPrice.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@bookSize", txtBooSize.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@bookTitle", txtBookTitle.Value));
            command.Parameters.Add(new OdbcParameter("@deptCity", deptCity.Items[deptCity.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@deptEmail", txtDepEmail.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptName", txtDepName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptNID", txtDepNID.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@editionNO", txtEditionNO.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@fax", txtDepFax.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@GDate", gerg.Items[gerg.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@Hdate", hijry.Items[hijry.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@mobile", txtDepPhone.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@pagesNo", txtPagesNo.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@partNo", txtNumOfParts.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@postOffice", txtDepPost.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@pubCityID", pubCity.Items[pubCity.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@pubName", txtPubName.Value));
            command.Parameters.Add(new OdbcParameter("@remarks", txtRemarks.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@seriesName", txtSeriesName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@seriesNo", txtSeriesNum.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@zip", txtDepZip.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@workTypeID", types.Items[types.SelectedIndex].Value));

            command.ExecuteNonQuery();
            foreach (string image in images)
            {
                OdbcCommand command2 = new OdbcCommand(@"insert into dbo.ImagesRequest ( 
                                                                                    ImageID,
                                                                                    RefID,
                                                                                    TYPE )
	                                                                            values (?,?,?)", con1);
                command2.Parameters.Add(new OdbcParameter("@ImageID", image));
                command2.Parameters.Add(new OdbcParameter("@RefID", id.ToString()));
                command2.Parameters.Add(new OdbcParameter("@TYPE", 1));// for PrintedGov Type
                command2.ExecuteNonQuery();
            }
            return 1;
        }
        catch (Exception ex)
        {
            StackTrace st = new StackTrace(ex, true);
            Helper.logError(ex);
            return -2;
        }
    }
    public int edit()
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
            Guid id = Guid.Parse(ViewState["id"].ToString());
            OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
            con1.Open();
            OdbcCommand command = new OdbcCommand(@"update GovRequests set 
                                                        authName=?, 
                                                        authBirthDate=?,
                                                        authNationality=?,
                                                        bookPrice=?,
                                                        bookSize=?,
                                                        bookTitle=?,
                                                        deptCity=?,
                                                        deptEmail=?,
                                                        deptName=?,
                                                        deptNID=?,
                                                        editionNo=?,
                                                        fax=?,
                                                        gDate=?,
                                                        hDate=?,
                                                        mobile=?,
                                                        pagesNo=?,
                                                        partNo=?,
                                                        postOffice=?,
                                                        pubcityID=?,
                                                        pubName=?,
                                                        remarks=?,
                                                        seriesName=?,
                                                        seriesNo=?,
                                                        zip=?,
                                                        workTypeID=?,
                                                        note=''
	                                                where id = ? ", con1);

            command.Parameters.Add(new OdbcParameter("@authName", txtAuthName.Value));
            command.Parameters.Add(new OdbcParameter("@authBirthDate", mask_date2.Value));
            command.Parameters.Add(new OdbcParameter("@authNationality", countries_list.Items[countries_list.SelectedIndex].Text));
            command.Parameters.Add(new OdbcParameter("@bookPrice", txtBookPrice.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@bookSize", txtBooSize.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@bookTitle", txtBookTitle.Value));
            command.Parameters.Add(new OdbcParameter("@deptCity", deptCity.Items[deptCity.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@deptEmail", txtDepEmail.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptName", txtDepName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptNID", txtDepNID.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@editionNO", txtEditionNO.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@fax", txtDepFax.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@GDate", gerg.Items[gerg.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@Hdate", hijry.Items[hijry.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@mobile", txtDepPhone.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@pagesNo", txtPagesNo.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@partNo", txtNumOfParts.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@postOffice", txtDepPost.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@pubCityID", pubCity.Items[pubCity.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@pubName", txtPubName.Value));
            command.Parameters.Add(new OdbcParameter("@remarks", txtRemarks.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@seriesName", txtSeriesName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@seriesNo", txtSeriesNum.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@zip", txtDepZip.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@workTypeID", types.Items[types.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@id", id.ToString()));
            command.ExecuteNonQuery();
            foreach (string image in images)
            {
                OdbcCommand command2 = new OdbcCommand(@"insert into dbo.ImagesRequest ( 
                                                                                    ImageID,
                                                                                    RefID,
                                                                                    TYPE )
	                                                                            values (?,?,?)", con1);
                command2.Parameters.Add(new OdbcParameter("@ImageID", image));
                command2.Parameters.Add(new OdbcParameter("@RefID", id.ToString()));
                command2.Parameters.Add(new OdbcParameter("@TYPE", 1));// for PrintedGov Type
                command2.ExecuteNonQuery();
            }
            return 1;
        }
        catch (Exception ex)
        {
            StackTrace st = new StackTrace(ex, true);
            Helper.logError(ex);
            return -2;
        }
    }

    public void loadPrevData()
    {
        OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
        con1.Open();
        OdbcCommand command = new OdbcCommand("SELECT * FROM dbo.GovRequests where id = ? ", con1);
        command.Parameters.Add(new OdbcParameter("@id", ViewState["id"].ToString()));
        DataSet results = new DataSet();
        OdbcDataAdapter usersAdapter = new OdbcDataAdapter(command);
        usersAdapter.Fill(results);
        DataTable dt = results.Tables[0];
        foreach (DataRow row in dt.Rows)
        {

            txtAuthName.Value = row["authName"].ToString();
            mask_date2.Value = row["authBirthDate"].ToString();
            ListItem li = countries_list.Items.FindByText(row["authNationality"].ToString());
            li.Selected = true;
            txtBookPrice.Value = row["bookPrice"].ToString();
            txtBooSize.Value = row["bookSize"].ToString();
            txtBookTitle.Value = row["bookTitle"].ToString();
            li = deptCity.Items.FindByValue(row["deptCity"].ToString());
            li.Selected = true;
            txtDepEmail.Value = row["deptEmail"].ToString();
            txtDepName.Value = row["deptName"].ToString();
            txtDepNID.Value = row["deptNID"].ToString();
            txtEditionNO.Value = row["editionNo"].ToString();
            txtDepFax.Value = row["fax"].ToString();
            li = gerg.Items.FindByText(row["gDate"].ToString());
            li.Selected = true;
            li = hijry.Items.FindByText(row["hDate"].ToString());
            li.Selected = true;
            txtDepPhone.Value = row["mobile"].ToString();
            txtPagesNo.Value = row["pagesNo"].ToString();
            txtNumOfParts.Value = row["partNo"].ToString();
            txtDepPost.Value = row["postOffice"].ToString();

            li = pubCity.Items.FindByValue(row["pubcityID"].ToString());
            li.Selected = true;
            txtPubName.Value = row["pubName"].ToString();
            txtRemarks.Value = row["remarks"].ToString();
            txtSeriesName.Value = row["seriesName"].ToString();
            txtSeriesNum.Value = row["seriesNo"].ToString();
            txtDepZip.Value = row["zip"].ToString();
            li = types.Items.FindByValue(row["workTypeID"].ToString());
            li.Selected = true;
        }
    }

    private bool ValidCheack()
    {
        OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
        con1.Open();
        OdbcCommand command = new OdbcCommand("SELECT * FROM dbo.GovRequests where id = ? and (note ='' or note is null) ", con1);
        command.Parameters.Add(new OdbcParameter("@id", ViewState["id"].ToString()));
        DataSet results = new DataSet();
        OdbcDataAdapter usersAdapter = new OdbcDataAdapter(command);
        usersAdapter.Fill(results);
        DataTable dt = results.Tables[0];
        foreach (DataRow row in dt.Rows)
        {
            return false;
        }
        return true;
    }
    public static bool ValidateCaptcha(string response)
    {
        string secret = "6LfSaHcUAAAAANlkLYebRa1dtBHuG_uKZ0fVq0PU";
        var client = new WebClient();
        var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        var result = jsSerializer.DeserializeObject(reply);
        Dictionary<string, object> obj2 = new Dictionary<string, object>();
        obj2 = (Dictionary<string, object>)(result);
        var captchaResponse = obj2["success"].ToString();
        bool res = Convert.ToBoolean(captchaResponse);
        return res;
    }

}