using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;

 
 /// <summary>
 /// 这是模板注释
 /// </summary>
public class NoReSetAR_modfile
{


    /// <summary>
    /// 二次处理数据
    /// </summary>
    /// <param name="oldDS">原始获取到的分页数据</param>
    /// <param name="dic_mysearchtop">快捷搜索条件</param>
    /// <param name="parameter_forUI">原始传入的条件</param>
    /// <returns></returns>
    public DataSet NRS_AR_other(DataSet oldDS, Dictionary<string, string> dic_mysearchtop, DataTable parameter_forUI)
    {
        DataSet NewDS = null;
        NewDS = oldDS.Copy();
        //这是图形报表例子
        //DataSet dstemp = jsontodatatable.re_chart("柱状图数据", "Scity", null, null, "Stime", "Sint", oldDS.Tables["主要数据"],"yes");
        DataSet dstemp = jsontodatatable.re_chart("曲线图数据", "Scity", null, null, "Stime", "Sint", oldDS.Tables["主要数据"], "yes");
        // DataSet dstemp = jsontodatatable.re_chart("饼图数据", "Sname", "测试数据", null, "Sint", null, oldDS.Tables["主要数据"], "yes");
        for (int i = 0; i < dstemp.Tables.Count; i++)
        {
            NewDS.Tables.Add(dstemp.Tables[i].Copy());
        }
        return NewDS;

    }

}
 
