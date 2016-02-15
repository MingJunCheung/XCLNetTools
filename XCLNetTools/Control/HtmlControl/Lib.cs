﻿/*
一：基本信息：
开源协议：https://github.com/xucongli1989/XCLNetTools/blob/master/LICENSE
项目地址：https://github.com/xucongli1989/XCLNetTools
Create By: XCL @ 2012

二：贡献者：
1：xucongli1989（https://github.com/xucongli1989）电子邮件：80213876@qq.com

三：更新：
当前版本：v2.2
更新时间：2016-02

四：更新内容：
1：更新表单获取的参数类型
2：更改Message/JsonMsg类的目录
3：删除多余的方法
4：修复一处未dispose问题
5：整理部分代码
6：添加 MethodResult.cs
7：获取枚举list时可以使用byte/short等
 */


using System;
using System.Collections.Generic;
using System.Text;
using XCLNetTools.Entity;

namespace XCLNetTools.Control.HtmlControl
{
    /// <summary>
    /// 原生html控件操作类
    /// </summary>
    public class Lib
    {
        /// <summary>
        /// 将枚举转为select的options
        /// <param name="t">枚举type</param>
        /// <param name="options">选项</param>
        /// <returns>option字符串</returns>
        /// </summary>
        public static string GetOptions(Type t, SetOptionEntity options = null)
        {
            StringBuilder str = new StringBuilder();
            if (null != options && options.IsNeedPleaseSelect)
            {
                str.Append("<option value=''>--请选择--</option>");
            }
            var lst = XCLNetTools.Enum.EnumHelper.GetEnumFieldModelList(t);
            if (null != lst && lst.Count > 0)
            {
                lst.ForEach(m =>
                {
                    if (null != options)
                    {
                        str.AppendFormat("<option value='{0}' {2}>{1}</option>", m.Text, m.Description, string.Equals(options.DefaultValue, m.Text, StringComparison.OrdinalIgnoreCase) ? " selected='selected' " : "");
                    }
                    else
                    {
                        str.AppendFormat("<option value='{0}'>{1}</option>", m.Text, m.Description);
                    }
                });
            }
            return str.ToString();
        }

        /// <summary>
        /// 将Dictionary转为select 的options
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="options">选项</param>
        /// <returns>option字符串</returns>
        public static string GetOptions(Dictionary<string, string> dataSource, SetOptionEntity options = null)
        {
            StringBuilder str = new StringBuilder();
            if (null != options && options.IsNeedPleaseSelect)
            {
                str.Append("<option value=''>--请选择--</option>");
            }
            if (null != dataSource && dataSource.Count > 0)
            {
                foreach (var m in dataSource)
                {
                    if (null != options)
                    {
                        str.AppendFormat("<option value='{0}' {2}>{1}</option>", m.Value, m.Key, string.Equals(options.DefaultValue, m.Value, StringComparison.OrdinalIgnoreCase) ? " selected='selected' " : "");
                    }
                    else
                    {
                        str.AppendFormat("<option value='{0}'>{1}</option>", m.Value, m.Key);
                    }
                }
            }
            return str.ToString();
        }
    }
}