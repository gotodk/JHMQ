﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageTop : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dibu.InnerText = ConfigurationManager.AppSettings["SYSname"].ToString();
        Span1.InnerText = ConfigurationManager.AppSettings["SYSname"].ToString();
    }
 
}
