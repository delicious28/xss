using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class UserInfo
{
    private string ip;              //用户IP

    public string Ip
    {
        get { return ip; }
        set { ip = value; }
    }

    private DateTime requestTime;   //请求时间

    public DateTime RequestTime
    {
        get { return requestTime; }
        set { requestTime = value; }
    }
    private string userHostName;   //客户端电脑名

    public string UserHostName
    {
        get { return userHostName; }
        set { userHostName = value; }
    }

    private Dictionary<string, string> cookies; //用户cookies

    public Dictionary<string, string> Cookies
    {
        get { return cookies; }
        set { cookies = value; }
    }

    private Dictionary<string, string> localstrongs; //用户localstrong

    public Dictionary<string, string> Localstrongs
    {
        get { return localstrongs; }
        set { localstrongs = value; }
    }
}
