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



using Aspose.Cells;
using System.Collections.Generic;
using System.Data;

namespace XCLNetTools.DataHandler
{
    /// <summary>
    /// 导出参数类
    /// </summary>
    public class OutPutParamClass
    {
        /// <summary>
        /// 表名（主要是便于在xml字段名list中找到该节点信息）,对应dataSet中的table
        /// </summary>
        public string[] tableName { get; set; }

        /// <summary>
        /// 导出类，包含新旧字段名（为null时，则保持ds中的相应的列名）
        /// </summary>
        public List<OutPutClass> outPutClass { get; set; }

        /// <summary>
        /// 要导出的DataSet
        /// </summary>
        public DataSet ds { get; set; }

        /// <summary>
        /// 导出的EXCEL文件的名字
        /// </summary>
        public string fileTitle { get; set; }

        /// <summary>
        /// excel中第一行的标题
        /// </summary>
        public string[] conTitle { get; set; }

        private bool _autoDownLoad = true;

        /// <summary>
        /// 是否自动下载
        /// </summary>
        public bool AutoDownLoad
        {
            get { return _autoDownLoad; }
            set { _autoDownLoad = value; }
        }

        /// <summary>
        /// 自定义文件名（保存后的完整路径名）
        /// </summary>
        public string CustomFileName
        {
            get;
            set;
        }

        /// <summary>
        /// 自定义保存时，文件保存的格式
        /// </summary>
        public Aspose.Cells.SaveFormat SaveFormat
        {
            get;
            set;
        }

        private int _firstRowIndex = 1;

        /// <summary>
        /// 填充的数据起始行索引号（0为第一行）
        /// </summary>
        public int FirstRowIndex
        {
            get { return _firstRowIndex; }
            set { _firstRowIndex = value; }
        }

        private int _firstColumnIndex = 0;

        /// <summary>
        /// 填充的数据起始列索引号（0为第一行）
        /// </summary>
        public int FirstColumnIndex
        {
            get { return _firstColumnIndex; }
            set { _firstColumnIndex = value; }
        }

        private bool _isShowCustomLine = true;

        /// <summary>
        /// 是否显示自定义文字行（就是第一行的导出信息）
        /// </summary>
        public bool IsShowCustomLine
        {
            get { return _isShowCustomLine; }
            set { _isShowCustomLine = value; }
        }

        private bool _isShowFieldLine = true;

        /// <summary>
        /// 是否显示字段行
        /// </summary>
        public bool IsShowFieldLine
        {
            get { return _isShowFieldLine; }
            set { _isShowFieldLine = value; }
        }

        /// <summary>
        /// 指定被操作的工作薄文件
        /// （用于向已有文件中写入数据并导出的情况）
        /// </summary>
        public string WorkBookFilePath
        {
            get;
            set;
        }

        /// <summary>
        /// 获取当前正在操作的WorkBook
        /// </summary>
        public Workbook GetWorkBook
        {
            get;
            set;
        }
    }
}