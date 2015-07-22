using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SQLite;

public partial class frame_cookieInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id = 0;
            if (Request["id"] != null)
            {
                int.TryParse(Request["id"], out id);

                if (id > 0)
                {
                    string sql = "select * from usercookie where infoId=" + id + " limit 200";
                    SQLiteDatabase db = new SQLiteDatabase();
                    var datatable = db.GetDataTable(sql);
                    rptCookieInfo.DataSource = datatable;
                    rptCookieInfo.DataBind();
                }
            }
        }
    }
}