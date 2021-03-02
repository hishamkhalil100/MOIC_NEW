using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using context = System.Web.HttpContext;  
/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    // to retreve connection string from web.config app settings 
    public static string connectionString = ConfigurationManager.AppSettings["connectionStringReal"];
    private static String exepurl;
   
    public Helper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    // to initialize conniction with webservece with deafult proxy settings 
    public static void setConnectionProxy()
    {
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        var proxy = new WebProxy(ConfigurationManager.AppSettings["proxy"], true);
        proxy.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["proxyUserName"], ConfigurationManager.AppSettings["proxyPassword"]);
        WebRequest.DefaultWebProxy = proxy;
    }
    public static List<int> getHijryDDL()
    {
        DateHG cal = new DateHG();
        string ddlHtml = string.Empty;
        int year = cal.getHyear();
        List<int> list = new List<int>();
        for (int i = 0; i <= 100; i++)
        {
            list.Add(year - i);
        }
        return list;
    }
    public static List<int> getGerDDL()
    {

        string ddlHtml = string.Empty;
        int year = DateTime.Now.Year;
        List<int> list = new List<int>();
        for (int i = 0; i <= 100; i++)
        {
            list.Add(year - i);
        }
        return list;
    }
    public static List<Tuple<string, string>> getPrintedWorkTypes()
    {
        List<Tuple<string, string>> list = new List<Tuple<string, string>>();
        list.Add(Tuple.Create<string, string>("", ""));
        list.Add(Tuple.Create<string, string>("2", "كتاب"));
        list.Add(Tuple.Create<string, string>("3", "كتيب"));
        list.Add(Tuple.Create<string, string>("4", "مطوية"));
        //list.Add(Tuple.Create<string, string>("5", "رسالة جامعية"));
        list.Add(Tuple.Create<string, string>("6", "مطبوع دوري"));
        list.Add(Tuple.Create<string, string>("7", "خريطة"));
        list.Add(Tuple.Create<string, string>("8", "شجرة أنساب"));
        list.Add(Tuple.Create<string, string>("9", "بطاقات"));
        list.Add(Tuple.Create<string, string>("10", "لوحة"));
        list.Add(Tuple.Create<string, string>("11", "مفكرة"));
        list.Add(Tuple.Create<string, string>("12", "العاب و تمارين تعليمية"));
        return list;
    }
    public static List<Tuple<string, string>> getCities()
    {
        OdbcConnection con1 = new OdbcConnection(connectionString);
        con1.Open();
        OdbcCommand command = new OdbcCommand("SELECT CityId,AName FROM cities order by CityID", con1);
        DataSet results = new DataSet();
        OdbcDataAdapter usersAdapter = new OdbcDataAdapter(command);
        usersAdapter.Fill(results);
        DataTable dt = results.Tables[0];
        List<Tuple<string, string>> list = new List<Tuple<string, string>>();
        list.Add(Tuple.Create<string, string>("", ""));
        foreach (DataRow row in dt.Rows)
        {
            list.Add(Tuple.Create<string, string>(row["CityId"].ToString(), row["AName"].ToString()));
        }
        return list;

    }

    public static string invertDate(string date)
    {
        string[] arr = date.Split('/');
        string invDate = string.Empty;
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            if (i == 0)
                invDate += arr[i];
            else
                invDate += arr[i] + "/";

        }
        return invDate.Replace(" ", "");
    }
    public static string correctDate(string date)
    {
        try
        {
            string[] arr = date.Split('/');
            string invDate = string.Empty;

            //day arr[1]
            if (arr[1].Length == 1)
                invDate += "0" + arr[1];
            else
            {
                invDate += arr[1];
            }
            // month arr[0]
            if (arr[0].Length == 1)
                invDate += "0" + arr[0] + "/";
            else
            {
                invDate += arr[0] + "/";
            }
            // year arr[2]
            invDate += arr[2] + "/";
            return invDate.Replace(" ", "");
        }
        catch (Exception ex)
        {
            return "";
        }


    }
    public static void logError(Exception ex)
    {


        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        message += string.Format("Message: {0}", ex.Message);
        message += Environment.NewLine;
        message += string.Format("StackTrace: {0}", ex.StackTrace);
        message += Environment.NewLine;
        message += string.Format("Source: {0}", ex.Source);
        message += Environment.NewLine;
        message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        message += Environment.NewLine;
        string path = "C:/MOIC_NEW/assets/log.txt";
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(message);
            writer.Close();
        }

        exepurl = context.Current.Request.Url.ToString();  
        OdbcConnection con1 = new OdbcConnection(connectionString);
        con1.Open();

        OdbcCommand command = new OdbcCommand(@"insert into dbo.LogErrors ( ExceptionMsg, ExceptionType, ExceptionURL, ApplicationName )
	    values ( ?, ?,  ?, ? )", con1);
        if (ex.Message.ToString().Length > 254)
        {
            command.Parameters.Add(new OdbcParameter("@ExceptionMsg", ex.Message.ToString().Substring(0, 254)));
        }
        else
        {
            command.Parameters.Add(new OdbcParameter("@ExceptionMsg", ex.Message.ToString()));
        }
        
        command.Parameters.Add(new OdbcParameter("@ExceptionType", ex.GetType().Name.ToString()));
        //command.Parameters.Add(new OdbcParameter("@ExceptionSource", line.ToString()));
        command.Parameters.Add(new OdbcParameter("@ExceptionURL",exepurl));
        command.Parameters.Add(new OdbcParameter("@ApplicationName", "MOIC"));
        command.ExecuteNonQuery();
       
    }
    public string Encrypt(string clearText)
    {
        string EncryptionKey = "KFNLP@$$W0rdMOICP@$$Word";
        byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
        using (System.Security.Cryptography.Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x49, 0x65, 0x64, 0x76, 0x65, 0x64, 0x76, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (System.IO.MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = "KFNLP@$$W0rdMOICP@$$Word";
        cipherText = cipherText.Replace(" ", "+");
        if(!cipherText.EndsWith("="))
        {
            cipherText = cipherText + "=";
        }
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x49, 0x65, 0x64, 0x76, 0x65, 0x64, 0x76, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = System.Text.Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}