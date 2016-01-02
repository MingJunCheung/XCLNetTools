﻿/*
一：基本信息：
开源协议：https://github.com/xucongli1989/XCLNetTools/blob/master/LICENSE
项目地址：https://github.com/xucongli1989/XCLNetTools
Create By: XCL @ 2012

二：贡献者：
1：xucongli1989（https://github.com/xucongli1989）电子邮件：80213876@qq.com

三：更新：
当前版本：v2.1
更新时间：2016-01-01

四：更新内容：
1：将原先基础数据转换方法转移到Common/DataTypeConvert下面
2：其它逻辑优化，如表单参数获取等
3：首次开放所有源代码
 */



using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace XCLNetTools.Generic
{
    /// <summary>
    /// List操作类
    /// </summary>
    public class ListHelper
    {
        /// <summary>
        /// 根据步长，将一个总List拆分为多个子List
        /// </summary>
        /// <param name="step">每个子list最多的项数</param>
        /// <param name="lst">主list</param>
        public static List<List<T>> SplitListByStep<T>(int step, List<T> lst)
        {
            List<List<T>> newList = null;
            if (null != lst && lst.Count > 0)
            {
                newList = new List<List<T>>();
                int max = lst.Count;
                int times = (int)Math.Ceiling(max * 1.00 / step);
                for (int i = 1; i <= times; i++)
                {
                    int c = step;
                    if (i == times && ((max % step) != 0))
                    {
                        c = max % step;
                    }
                    newList.Add(lst.GetRange(step * (i - 1), c));
                }
            }
            return newList;
        }

        /// <summary>
        /// 将list中的项拼接字符串
        /// </summary>
        /// <param name="lst">要操作的list</param>
        /// <param name="splitChar">分隔符</param>
        public static string GetStringByList<T>(List<T> lst, string splitChar)
        {
            string str = string.Empty;
            if (null != lst && lst.Count > 0)
            {
                str = string.Join(splitChar, lst.ConvertAll(k => k.ToString()).ToArray());
            }
            return str;
        }

        /// <summary>
        /// 将List转换成DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            System.ComponentModel.PropertyDescriptorCollection properties = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                System.ComponentModel.PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }

        /// <summary>
        /// 将dataTable转为list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            if (null == dt || dt.Rows.Count == 0)
            {
                return null;
            }

            // 定义集合
            IList<T> ts = new List<T>();

            // 获得此模型的类型
            Type type = typeof(T);

            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();

                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;

                    // 检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }

                ts.Add(t);
            }

            return ts;
        }
    }
}