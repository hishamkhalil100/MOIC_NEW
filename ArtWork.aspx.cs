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
public partial class ArtWork : System.Web.UI.Page
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
                Response.Redirect("error.aspx?error=4",false); 
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
            Guid id = Guid.Parse(ViewState["id"].ToString());
            OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
            con1.Open();
            OdbcCommand command = new OdbcCommand(@"UPDATE ArtWorkRequest  set deptName= ?, 
                                                                            deptNID=?, 
                                                                            title=?, 
                                                                            email=?, 
                                                                            artist=?, 
                                                                            artist1=?, 
                                                                            artist2=?, 
                                                                            Mobile=?, 
                                                                            fax=?, 
                                                                            postOffice=?, 
                                                                            PostBox=?, 
                                                                            typeID=?, 
                                                                            episodeNo=?, 
                                                                            dimentions=?, 
                                                                            material=?, 
                                                                            prodHDate=?, 
                                                                            prodADate=?, 
                                                                            deptCity=?,
                                                                            note=''
	                                                        WHERE id =?", con1);
            command.Parameters.Add(new OdbcParameter("@deptName", txtDepName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptNID", txtDepNID.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@title", txtTitle.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@email", txtDepEmail.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@authors", txtArtist.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@authors", txtArtist1.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@authors", txtArtist2.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@mobile", txtDepPhone.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@fax", txtDepFax.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@postOffice", txtDepZip.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@PostBox", txtDepPost.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@typeID", int.Parse(typeID.Items[typeID.SelectedIndex].Value)));
            command.Parameters.Add(new OdbcParameter("@episodeNo", txtepisodeNo.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@totalNoEpisode", txtDimentions.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@txtMaterial", txtMaterial.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@Hdate", hijry.Items[hijry.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@GDate", gerg.Items[gerg.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@deptCity", deptCity.Items[deptCity.SelectedIndex].Value));
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
                command2.Parameters.Add(new OdbcParameter("@TYPE", 4));// for artWork Type
                command2.ExecuteNonQuery();
            }
            OdbcCommand command3 = new OdbcCommand(@"insert into dbo.ImagesRequest ( 
                                                                                    ImageID,
                                                                                    RefID,
                                                                                    TYPE )
	                                                                            values (?,?,?)", con1);
            command3.Parameters.Add(new OdbcParameter("@ImageID", moicImg));
            command3.Parameters.Add(new OdbcParameter("@RefID", id.ToString()));
            command3.Parameters.Add(new OdbcParameter("@TYPE", 41));// for Moic image in artWork Type
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
            OdbcCommand command = new OdbcCommand(@"insert into dbo.ArtWorkRequest ( 
                                                                            ID,
                                                                            deptName, 
                                                                            deptNID, 
                                                                            title, 
                                                                            email, 
                                                                            artist, 
                                                                            artist1, 
                                                                            artist2, 
                                                                            Mobile, 
                                                                            fax, 
                                                                            postOffice, 
                                                                            PostBox, 
                                                                            typeID, 
                                                                            episodeNo, 
                                                                            dimentions, 
                                                                            material, 
                                                                            prodHDate, 
                                                                            prodADate, 
                                                                            deptCity )
	                                                        values ( ?, ?, ? ,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )", con1);

            command.Parameters.Add(new OdbcParameter("@id", id.ToString()));
            command.Parameters.Add(new OdbcParameter("@deptName", txtDepName.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@deptNID", txtDepNID.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@title", txtTitle.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@email", txtDepEmail.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@authors", txtArtist.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@authors", txtArtist1.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@authors", txtArtist2.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@mobile", txtDepPhone.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@fax", txtDepFax.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@postOffice", txtDepZip.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@PostBox", txtDepPost.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@typeID", int.Parse(typeID.Items[typeID.SelectedIndex].Value)));
            command.Parameters.Add(new OdbcParameter("@episodeNo", txtepisodeNo.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@totalNoEpisode", txtDimentions.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@txtMaterial", txtMaterial.Value.Trim()));
            command.Parameters.Add(new OdbcParameter("@Hdate", hijry.Items[hijry.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@GDate", gerg.Items[gerg.SelectedIndex].Value));
            command.Parameters.Add(new OdbcParameter("@deptCity", deptCity.Items[deptCity.SelectedIndex].Value));
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
                command2.Parameters.Add(new OdbcParameter("@TYPE", 4));// for artWork Type
                command2.ExecuteNonQuery();
            }
            OdbcCommand command3 = new OdbcCommand(@"insert into dbo.ImagesRequest ( 
                                                                                    ImageID,
                                                                                    RefID,
                                                                                    TYPE )
	                                                                            values (?,?,?)", con1);
            command3.Parameters.Add(new OdbcParameter("@ImageID", moicImg));
            command3.Parameters.Add(new OdbcParameter("@RefID", id.ToString()));
            command3.Parameters.Add(new OdbcParameter("@TYPE", 41));// for Moic image in artWork Type
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
        foreach (Tuple<string, string> element in getTypes())
        {
            typeID.Items.Add(new ListItem(element.Item2, element.Item1));
        }
        foreach (Tuple<string, string> element in Helper.getCities())
        {
            deptCity.Items.Add(new ListItem(element.Item2, element.Item1));
        }

    }

    public static List<Tuple<string, string>> getTypes()
    {
        OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
        con1.Open();
        OdbcCommand command = new OdbcCommand("SELECT TypeID,TypeName FROM ArtworkType order by TypeName ", con1);
        DataSet results = new DataSet();
        OdbcDataAdapter usersAdapter = new OdbcDataAdapter(command);
        usersAdapter.Fill(results);
        DataTable dt = results.Tables[0];
        List<Tuple<string, string>> list = new List<Tuple<string, string>>();
        list.Add(Tuple.Create<string, string>("", ""));
        foreach (DataRow row in dt.Rows)
        {
            list.Add(Tuple.Create<string, string>(row["TypeID"].ToString(), row["TypeName"].ToString()));
        }
        return list;

    }




    public void loadPrevData()
    {
        OdbcConnection con1 = new OdbcConnection(ConfigurationManager.AppSettings["connectionStringReal"]);
        con1.Open();
        OdbcCommand command = new OdbcCommand("SELECT * FROM dbo.ArtWorkRequest where id = ? ", con1);
        command.Parameters.Add(new OdbcParameter("@id", ViewState["id"].ToString()));
        DataSet results = new DataSet();
        OdbcDataAdapter usersAdapter = new OdbcDataAdapter(command);
        usersAdapter.Fill(results);
        DataTable dt = results.Tables[0];
        foreach (DataRow row in dt.Rows)
        {
            txtTitle.Value = row["title"].ToString();
            txtArtist.Value = row["artist"].ToString();
            txtArtist1.Value = row["artist1"].ToString();
            txtArtist2.Value = row["artist2"].ToString();
            ListItem li = hijry.Items.FindByText(row["prodHDate"].ToString());
            li.Selected = true;
            li = gerg.Items.FindByText(row["prodADate"].ToString());
            li.Selected = true;
            li = typeID.Items.FindByValue(row["typeID"].ToString());
            li.Selected = true;
            txtMaterial.Value = row["material"].ToString();
            txtDimentions.Value = row["dimentions"].ToString();
            txtepisodeNo.Value = row["episodeNo"].ToString();
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
        OdbcCommand command = new OdbcCommand("SELECT * FROM dbo.ArtWorkRequest where id = ? and (note ='' or note is null) ", con1);
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