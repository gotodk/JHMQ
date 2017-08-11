using FMipcClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ajax_pp_do : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //处理ajax请求
        string ajaxrun = "";
        if (Request["ajaxrun"] == null || Request["ajaxrun"].ToString().Trim() == "")
        {
            return;
        }
        if (Request["jkname"] == null || Request["jkname"].ToString().Trim() == "")
        {
            return;
        }
        string jkname = Request["jkname"].ToString();
        ajaxrun = Request["ajaxrun"].ToString();

        if (ajaxrun == "save")
        {/*
            string show = "<br>Form:<br>";
            for (int i = 0; i < Request.Form.Count; i++)
            {

                show = show + Request.Form.Keys[i].ToString() + " = " + Request.Form[i].ToString() + "<br>";
            }
            show = show + "<br>QueryString:<br>";
            for (int i = 0; i < Request.QueryString.Count; i++)
            {

                show = show + Request.QueryString.Keys[i].ToString() + " = " + Request.QueryString[i].ToString() + "<br>";
            }

            Response.Write(show);//向客户端输出字符串
          */


      
            
            
     
            //调用执行方法获取数据
            DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);

            //对《处理结构代码文件生成》接口，需要特殊处理，生成文件，将生成结果强加到提交参数中
            if (jkname == "处理结构代码文件生成")
            {

                string cljieguo = "<span class='red'>ERR:</span>界面文件路径写入错误";
                //接收转换参数
                Hashtable ht_forUI = new Hashtable();
                for (int i = 0; i < dt_request.Rows.Count; i++)
                {
                    ht_forUI[dt_request.Rows[i]["参数名"].ToString()] = dt_request.Rows[i]["参数值"].ToString();
                }
                //读取模板;
                string modstr_aspx = File.ReadAllText(Server.MapPath(ht_forUI["uimod"].ToString()).ToString(), Encoding.UTF8);
                string modstr_cs = File.ReadAllText(Server.MapPath(ht_forUI["uimod"].ToString().Trim() + ".cs").ToString(), Encoding.UTF8);
                //替换关键字
                string peizhizhujian = ht_forUI["peizhizhujian"].ToString();
                string mbleixing = ht_forUI["mbleixing"].ToString();
                string mbl_n = "";
                if (mbleixing == "通用列表")
                {
                    mbl_n = "listonlyformod";
                }
                if (mbleixing == "一般表单")
                {
                    mbl_n = "formonlyformod";
                }
                string uinewfile = ht_forUI["uinewfile"].ToString();
                string unf = Server.MapPath(uinewfile);//新文件物理路径


                modstr_aspx = modstr_aspx.Replace("CodeFile=\"" + mbl_n + ".aspx.cs\"", "CodeFile=\"" + Path.GetFileNameWithoutExtension(unf) + ".aspx.cs\"");
                string mkj = uinewfile.Substring(0, uinewfile.Length - 4 - 1).Replace("/", "_");
                modstr_aspx = modstr_aspx.Replace("Inherits=\"CreatingFileMod_" + mbl_n + "\"", "Inherits=\"" + mkj + "\"");

                modstr_cs = modstr_cs.Replace("public partial class CreatingFileMod_" + mbl_n + "", "public partial class " + mkj);
                modstr_cs = modstr_cs.Replace("[*[待替换主键]*]", peizhizhujian);

                //保存到文件(检查文件是否存在)
                if (File.Exists(unf))
                {
                    //文件存在，提示
                    cljieguo = "<span class='red'>ERR:</span>界面文件路径已存在文件，不能写入！";
                }
                else
                {
                    try {
                        //文件不存在，写入
                        Directory.CreateDirectory(Path.GetDirectoryName(unf));
                        File.WriteAllText(unf, modstr_aspx, Encoding.UTF8);
                        File.WriteAllText(unf + ".cs", modstr_cs, Encoding.UTF8);
                        cljieguo = "OK:界面文件路径新文件成功从模板创建！";
                    }
                    catch (Exception ex)
                    {
                        string aa = ex.ToString();
                    }
                 
                }

                //保存结果添加到dt_request
                dt_request.Rows.Add(new string[] { "UIjieguo", cljieguo, "自动添加" });
            }

            object[] re_dsi = IPC.Call(jkname, new object[] { dt_request });
            if (re_dsi[0].ToString() == "ok")
            {
     
                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                DataSet dsreturn = (DataSet)re_dsi[1];
                Response.Write(dsreturn.Tables["返回值单条"].Rows[0]["提示文本"].ToString());

            }
            else
            {
                Response.Write(re_dsi[1].ToString());//向客户端输出错误字符串
          
            }





        }
        if (ajaxrun == "del")
        {

 

            //调用执行方法获取数据
            DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
            object[] re_dsi = IPC.Call(jkname, new object[] { dt_request });
            if (re_dsi[0].ToString() == "ok")
            {

                ;
            }
            else
            {
                Response.Write(re_dsi[1].ToString());//向客户端输出错误字符串

            }
        }
        if (ajaxrun == "info")
        {

 
            string idforedit = "";
            if (Request["idforedit"] != null && Request["idforedit"].ToString().Trim() != "")
            {
                idforedit = Request["idforedit"].ToString();
            }
            else
            {
                //没有id传进来返回空内容
                Response.Write("");
                return;
            }

 
            //调用执行方法获取数据
            DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
            object[] re_dsi = IPC.Call(jkname, new object[] { dt_request });
            if (re_dsi[0].ToString() == "ok")
            {

                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                DataSet dsreturn = (DataSet)re_dsi[1];


                //转换xml

                TextWriter tw = new StringWriter();
                dsreturn.WriteXml(tw);
                string twstr = tw.ToString();
       
                StringWriter writer = new StringWriter();
                string xmlstr = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                    + twstr;
                Response.ContentType = "text/xml";
                Response.Charset = "UTF-8";
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(xmlstr);
                doc.Save(Response.OutputStream);
                Response.End(); 

            }
            else
            {
                Response.Write("");
                return;

            }

            
     
        }
    }
}