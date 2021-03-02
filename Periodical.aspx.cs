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

public partial class Periodical : System.Web.UI.Page
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
                        Response.Redirect("error.aspx?error=1",false);
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
    private int? edit()
    {
        try
        {
            List<string> images = new List<string>();
            string moicImg = "";
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
                HttpPostedFile file2 = Request.Files["moicImg"];
                moicImg = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file2.FileName);
                file2.SaveAs(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings["Files"], moicImg)));
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(ex, true);
                Helper.logError(ex);
                return -3;
            }
            Guid id =Guid.Parse(ViewState["id"].ToString());
            OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
            con1.Open();
            OdbcCommand command = new OdbcCommand(@"update PeriodicalRequest set 
                                                                                        deptName=?,
                                                                                        deptNID=?, 
                                                                                        titleAr=?, 
                                                                                        titleEn=?, 
                                                                                        email=?, 
                                                                                        issn=?, 
                                                                                        pubName=?, 
                                                                                        Mobile=?, 
                                                                                        fax=?, 
                                                                                        postOffice=?, 
                                                                                        PostBox=?, 
                                                                                        typeID=?, 
                                                                                        freqID=?, 
                                                                                        langID=?, 
                                                                                        firstIssueDate=?, 
                                                                                        stopDate=?, 
                                                                                        previousTitle=?, 
                                                                                        subject=?, 
                                                                                        deptCity=?, 
                                                                                        note='',
																						URL=? 
	                                                where id = ?", con1);
            command.Parameters.Add(new OdbcParameter("@deptName", txtDepName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptNID", txtDepNID.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@titleAr", txtTitleAr.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@titleEn", txtTitleEn.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@email", txtDepEmail.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@issn", txtISSN.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@pubName", txtPubName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@mobile", txtDepPhone.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@fax", txtDepFax.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@postOffice", txtDepZip.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@PostBox", txtDepPost.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@typeID", int.Parse(typeID.Items[typeID.SelectedIndex].Value)));
            command.Parameters.Add(new OdbcParameter("@freqID", int.Parse(freqID.Items[freqID.SelectedIndex].Value)));
            command.Parameters.Add(new OdbcParameter("@langID", int.Parse(langID.Items[langID.SelectedIndex].Value)));
            command.Parameters.Add(new OdbcParameter("@mobile", txtFristIssueDate.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@mobile", txtStopDate.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@previousTitle", txtpreviousTitle.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@subject", txtSubject.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptCity", deptCity.Items[deptCity.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@URL", txtDepURL.Value.Trim()));
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
                command2.Parameters.Add(new OdbcParameter("@TYPE", 2));// for Periodical Type
                command2.ExecuteNonQuery();
            }

            OdbcCommand command3 = new OdbcCommand(@"insert into dbo.ImagesRequest ( 
                                                                                    ImageID,
                                                                                    RefID,
                                                                                    TYPE )
	                                                                            values (?,?,?)", con1);
            command3.Parameters.Add(new OdbcParameter("@ImageID", moicImg));
            command3.Parameters.Add(new OdbcParameter("@RefID", id.ToString()));
            command3.Parameters.Add(new OdbcParameter("@TYPE", 21));// for Moic image in Periodical Type
            command3.ExecuteNonQuery();
            return 1;
        }
        catch (Exception ex)
        {
            StackTrace st = new StackTrace(ex, true);
            Helper.logError(ex);
            return -2;
        }
    }
    public int insert()
    {
        try
        {
            List<string> images = new List<string>();
            string moicImg = "";
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
                HttpPostedFile file2 = Request.Files["moicImg"];
                moicImg = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file2.FileName);
                file2.SaveAs(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings["Files"], moicImg)));
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(ex, true);
                Helper.logError(ex);
                return -3;
            }
            Guid id = Guid.NewGuid();
            OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
            con1.Open();
            OdbcCommand command = new OdbcCommand(@"insert into dbo.PeriodicalRequest ( ID,
                                                                                        deptName,
                                                                                        deptNID, 
                                                                                        titleAr, 
                                                                                        titleEn, 
                                                                                        email, 
                                                                                        issn, 
                                                                                        pubName, 
                                                                                        Mobile, 
                                                                                        fax, 
                                                                                        postOffice, 
                                                                                        PostBox, 
                                                                                        typeID, 
                                                                                        freqID, 
                                                                                        langID, 
                                                                                        firstIssueDate, 
                                                                                        stopDate, 
                                                                                        previousTitle, 
                                                                                        subject, 
                                                                                        deptCity,
																						URL)
	                                                values ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,  ? , ? )", con1);

            command.Parameters.Add(new OdbcParameter("@id", id.ToString()));
            command.Parameters.Add(new OdbcParameter("@deptName", txtDepName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptNID", txtDepNID.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@titleAr", txtTitleAr.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@titleEn", txtTitleEn.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@email", txtDepEmail.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@issn", txtISSN.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@pubName", txtPubName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@mobile", txtDepPhone.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@fax", txtDepFax.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@postOffice", txtDepZip.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@PostBox", txtDepPost.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@typeID", int.Parse(typeID.Items[typeID.SelectedIndex].Value)));
            command.Parameters.Add(new OdbcParameter("@freqID", int.Parse(freqID.Items[freqID.SelectedIndex].Value)));
            command.Parameters.Add(new OdbcParameter("@langID", int.Parse(langID.Items[langID.SelectedIndex].Value)));
            command.Parameters.Add(new OdbcParameter("@mobile", txtFristIssueDate.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@mobile", txtStopDate.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@previousTitle", txtpreviousTitle.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@subject", txtSubject.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptCity", deptCity.Items[deptCity.SelectedIndex].Value));
			command.Parameters.Add(new OdbcParameter("@URL", txtDepURL.Value.Trim()));
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
                command2.Parameters.Add(new OdbcParameter("@TYPE", 2));// for Periodical Type
                command2.ExecuteNonQuery();
            }

            OdbcCommand command3 = new OdbcCommand(@"insert into dbo.ImagesRequest ( 
                                                                                    ImageID,
                                                                                    RefID,
                                                                                    TYPE )
	                                                                            values (?,?,?)", con1); 
            command3.Parameters.Add(new OdbcParameter("@ImageID", moicImg));
            command3.Parameters.Add(new OdbcParameter("@RefID", id.ToString()));
            command3.Parameters.Add(new OdbcParameter("@TYPE", 21));// for Moic image in Periodical Type
            command3.ExecuteNonQuery();
            return 1;
        }
        catch (Exception ex)
        {
            StackTrace st = new StackTrace(ex, true);
            Helper.logError(ex);
            return -2;
        }
    }
    private void getdata()
    {
        foreach (Tuple<string, string> element in getTypes())
        {
            typeID.Items.Add(new ListItem(element.Item2, element.Item1));
        }
        foreach (Tuple<string, string> element in getLang())
        {
            langID.Items.Add(new ListItem(element.Item2, element.Item1));
        }
        foreach (Tuple<string, string> element in getFreq())
        {
            freqID.Items.Add(new ListItem(element.Item2, element.Item1));
        }
        foreach (Tuple<string, string> element in Helper.getCities())
        {
            deptCity.Items.Add(new ListItem(element.Item2, element.Item1));
        }
    }

    public static List<Tuple<string, string>> getTypes()
    {
        List<Tuple<string, string>> list = new List<Tuple<string, string>>();
        list.Add(Tuple.Create<string, string>("", ""));
        list.Add(Tuple.Create<string, string>("0", "دورية"));
        list.Add(Tuple.Create<string, string>("1", "سلسلة"));
        return list;
    }
    public static List<Tuple<string, string>> getLang()
    {
        OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
        con1.Open();
        OdbcCommand command = new OdbcCommand("SELECT * FROM PeriodicalLang order by LangID", con1);
        DataSet results = new DataSet();
        OdbcDataAdapter usersAdapter = new OdbcDataAdapter(command);
        usersAdapter.Fill(results);
        DataTable dt = results.Tables[0];
        List<Tuple<string, string>> list = new List<Tuple<string, string>>();
        list.Add(Tuple.Create<string, string>("", ""));
        foreach (DataRow row in dt.Rows)
        {
            list.Add(Tuple.Create<string, string>(row["LangID"].ToString(), row["LangName"].ToString()));
        }
        return list;
    }
    public static List<Tuple<string, string>> getFreq()
    {
        OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
        con1.Open();
        OdbcCommand command = new OdbcCommand("SELECT FreqID,FreqName FROM PeriodicalFrequency order by FreqID", con1);
        DataSet results = new DataSet();
        OdbcDataAdapter usersAdapter = new OdbcDataAdapter(command);
        usersAdapter.Fill(results);
        DataTable dt = results.Tables[0];
        List<Tuple<string, string>> list = new List<Tuple<string, string>>();
        list.Add(Tuple.Create<string, string>("", ""));
        foreach (DataRow row in dt.Rows)
        {
            list.Add(Tuple.Create<string, string>(row["FreqID"].ToString(), row["FreqName"].ToString()));
        }
        return list;
    }
    public void loadPrevData()
    {
        OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
        con1.Open();
        OdbcCommand command = new OdbcCommand("SELECT * FROM dbo.PeriodicalRequest where id = ? ", con1);
        command.Parameters.Add(new OdbcParameter("@id", ViewState["id"].ToString()));
        DataSet results = new DataSet();
        OdbcDataAdapter usersAdapter = new OdbcDataAdapter(command);
        usersAdapter.Fill(results);
        DataTable dt = results.Tables[0];
        foreach (DataRow row in dt.Rows)
        {
            txtTitleAr.Value = row["titleAr"].ToString();
            txtTitleEn.Value = row["titleEn"].ToString();
            txtISSN.Value = row["issn"].ToString();
            txtPubName.Value = row["pubName"].ToString();
            ListItem li = typeID.Items.FindByValue(row["typeID"].ToString());
            li.Selected = true;
            li = langID.Items.FindByValue(row["langID"].ToString());
            li.Selected = true;
            li = freqID.Items.FindByValue(row["freqID"].ToString());
            li.Selected = true;
            txtFristIssueDate.Value = row["firstIssueDate"].ToString();
            txtStopDate.Value = row["stopDate"].ToString();
            txtpreviousTitle.Value = row["previousTitle"].ToString();
            txtSubject.Value = row["subject"].ToString();
            txtDepPhone.Value = row["Mobile"].ToString();
            txtDepFax.Value = row["fax"].ToString();
            txtDepPost.Value = row["PostBox"].ToString();
            li = deptCity.Items.FindByValue(row["deptCity"].ToString());
            li.Selected = true;
            txtDepZip.Value = row["postOffice"].ToString();
            txtDepNID.Value = row["deptNID"].ToString();
            txtDepName.Value = row["deptName"].ToString();
            txtDepEmail.Value = row["email"].ToString();
        }
    }
    private bool ValidCheack()
    {
        OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
        con1.Open();
        OdbcCommand command = new OdbcCommand("SELECT * FROM dbo.PeriodicalRequest where id = ? and (note ='' or note is null ) ", con1);
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