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
using System.Runtime.InteropServices;
using System.Text;

namespace XCLNetTools.FileHandler
{
    /// <summary>
    /// INI文件读写类。
    /// </summary>
    public class INIFile
    {
        private INIFile()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iniPath">文件路径</param>
        public INIFile(string iniPath)
        {
            this.Path = iniPath;
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);

        /// <summary>
        /// 写INI文件
        /// </summary>
        public void IniWriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.Path);
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <returns>value值</returns>
        public string IniReadValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            return temp.ToString();
        }

        /// <summary>
        /// 读ini
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <returns>结果byte[]</returns>
        public byte[] IniReadValues(string section, string key)
        {
            byte[] temp = new byte[255];
            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            return temp;
        }

        /// <summary>
        /// 删除ini文件下所有段落
        /// </summary>
        public void ClearAllSection()
        {
            IniWriteValue(null, null, null);
        }

        /// <summary>
        /// 删除ini文件下section段落下的所有键
        /// </summary>
        public void ClearSection(string section)
        {
            IniWriteValue(section, null, null);
        }
    }
}