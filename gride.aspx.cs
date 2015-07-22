using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SQLite;

public partial class gride : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = "select * from userInfo order by id desc limit 200";
            SQLiteDatabase db = new SQLiteDatabase();
            var datatable = db.GetDataTable(sql);
            rptUserInfo.DataSource = datatable;
            rptUserInfo.DataBind();
        }
    }
}