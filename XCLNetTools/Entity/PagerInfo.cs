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

namespace XCLNetTools.Entity
{
    /// <summary>
    /// 分页信息model
    /// </summary>
    public class PagerInfo
    {
        private PagerInfo()
        {
        }

        /// <summary>
        /// 分页信息 构造函数
        /// </summary>
        public PagerInfo(int pageIndex, int pageSize, int recordCount)
        {
            this.PageIndex = pageIndex <= 0 ? 1 : pageIndex;
            this.PageSize = pageSize <= 0 ? 10 : pageSize;
            this.RecordCount = recordCount <= 0 ? 0 : recordCount;

            if (this.RecordCount > 0)
            {
                this.PageCount = (int)Math.Ceiling((1.0 * this.RecordCount) / this.PageSize);
                this.PageIndex = this.PageIndex > this.PageCount ? this.PageCount : this.PageIndex;
                this.CurrentPageRecordCount = this.PageIndex == this.PageCount && this.RecordCount % this.PageSize > 0 ? this.RecordCount % this.PageSize : this.PageSize;
                this.StartIndex = (this.PageIndex - 1) * this.PageSize + 1;
                this.EndIndex = this.StartIndex + this.CurrentPageRecordCount - 1;
            }
        }

        /// <summary>
        /// 当前页码（第一页为1），默认为1
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页最多显示的记录数，默认为10
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前记录总数
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 当前总页数
        /// </summary>
        public int PageCount { get; private set; }

        /// <summary>
        /// 当前页的实际记录条数
        /// </summary>
        public int CurrentPageRecordCount { get; private set; }

        /// <summary>
        /// 当前页的第一条记录的序号
        /// </summary>
        public int StartIndex { get; private set; }

        /// <summary>
        /// 当前页的最后一条记录的序号
        /// </summary>
        public int EndIndex { get; private set; }

        /// <summary>
        /// controller
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// action
        /// </summary>
        public string ActionName { get; set; }
    }
}