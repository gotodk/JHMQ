using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 这是模板注释
/// </summary>
public partial class CreatingFileMod_listonlyformod : System.Web.UI.Page
{
    #region 必备的公共变量
    /// <summary>
    /// 列表配置
    /// </summary>
    public DataSet dsFPZ = null;
    /// <summary>
    /// 其他辅助配置
    /// </summary>
    public Hashtable htPP = new Hashtable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //列表配置主键识别号
        string FID = "[*[待替换主键]*]";
        #region 必备的配置代码
        //获取列表配置
        dsFPZ = CallIPCPB.Get_FormsListDB(FID);
        htPP = FUPpublic.initPP_list(Request, dsFPZ);
        //给控件传值
        wuc_content_onlygrid._dsFPZ = dsFPZ;
        wuc_content_onlygrid._htPP = htPP;
        wuc_script_onlygrid._dsFPZ = dsFPZ;
        wuc_script_onlygrid._htPP = htPP;
        #endregion
    }
}