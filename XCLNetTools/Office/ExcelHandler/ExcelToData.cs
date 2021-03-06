﻿/*
一：基本信息：
开源协议：https://github.com/xucongli1989/XCLNetTools/blob/master/LICENSE
项目地址：https://github.com/xucongli1989/XCLNetTools
Create By: XCL @ 2012

 */

using Aspose.Cells;
using System.Data;

namespace XCLNetTools.Office.ExcelHandler
{
    /// <summary>
    /// excel读取类
    /// </summary>
    public static class ExcelToData
    {
        /// <summary>
        /// 单个工作薄读入（第一个可见的sheet）
        /// <param name="excelfilePath">文件路径</param>
        /// <returns>DataTable</returns>
        /// </summary>
        public static DataTable ReadExcelToTable(string excelfilePath)
        {
            Workbook workbook = new Workbook(excelfilePath);
            Worksheet worksheet = null;
            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                worksheet = workbook.Worksheets[i];
                if (worksheet.IsVisible)
                {
                    break;
                }
            }
            DataTable dataTable = new DataTable();
            if (null != worksheet && worksheet.Cells.MaxRow > -1 && worksheet.Cells.MaxColumn > -1)
            {
                dataTable = worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1);
            }
            return dataTable;
        }

        /// <summary>
        /// 将多个工作薄导入到DS中（所有可见的sheet）
        /// <param name="excelfilePath">文件路径</param>
        /// <returns>DataSet</returns>
        /// </summary>
        public static DataSet ReadExcelToDataSet(string excelfilePath)
        {
            DataSet ds = new DataSet();
            Workbook workbook = new Workbook(excelfilePath);
            Worksheet worksheet = null;
            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                worksheet = workbook.Worksheets[i];
                if (worksheet.IsVisible && worksheet.Cells.MaxRow > -1 && worksheet.Cells.MaxColumn > -1)
                {
                    var dataTable = worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1);
                    dataTable.TableName = string.Format("dt{0}", i);
                    ds.Tables.Add(dataTable);
                }
            }
            return ds;
        }
    }
}