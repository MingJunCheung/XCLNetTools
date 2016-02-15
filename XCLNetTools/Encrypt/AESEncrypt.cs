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
using System.Security.Cryptography;
using System.Text;

namespace XCLNetTools.Encrypt
{
    /// <summary>
    /// AES加密解密类
    /// </summary>
    public class AESEncrypt
    {
        private const string CRYPTO_IV = "XCLNETTOOLS";
        private const string CRYPTO_KEY = "XCLNETTOOLS";
        private const int CRYPTO_KEY_LENGTH = 32;
        private const int CRYPTO_IV_LENGTH = 16;

        private AesCryptoServiceProvider m_aesCryptoServiceProvider;
        private bool m_containKey;

        /// <summary>
        /// True：密文中包含密钥
        /// False：密文中不包含密钥
        /// </summary>
        public bool ContainKey
        {
            get { return m_containKey; }
            set { m_containKey = value; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public AESEncrypt()
        {
            m_aesCryptoServiceProvider = new AesCryptoServiceProvider();
            m_containKey = true;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="containKey">密文中是否包含密钥</param>
        public AESEncrypt(bool containKey)
            : this()
        {
            m_containKey = containKey;
        }

        /// <summary>
        /// 指定密钥对明文进行AES加密
        /// </summary>
        /// <param name="s_crypto">明文</param>
        /// <param name="s_key">加密密钥</param>
        /// <returns>密文</returns>
        public string Encrypt(string s_crypto, string s_key)
        {
            try
            {
                byte[] key = new byte[CRYPTO_KEY_LENGTH], iv = new byte[CRYPTO_IV_LENGTH];
                byte[] temp = string2Byte(s_key);
                if (temp.Length > key.Length)
                {
                    throw new Exception("Key不能超过32字节！");
                }
                key = string2Byte(s_key.PadRight(key.Length));
                iv = string2Byte(CRYPTO_IV.PadRight(iv.Length));
                return Encrypt(s_crypto, key, iv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 动态生成密钥，并对明文进行AES加密
        /// </summary>
        /// <param name="s_crypto">明文</param>
        /// <returns>密文</returns>
        public string Encrypt(string s_crypto)
        {
            byte[] key = new byte[CRYPTO_KEY_LENGTH], iv = new byte[CRYPTO_IV_LENGTH];
            m_aesCryptoServiceProvider.GenerateKey();
            key = m_aesCryptoServiceProvider.Key;
            iv = string2Byte(CRYPTO_IV.PadRight(iv.Length));
            return Encrypt(s_crypto, key, iv);
        }

        /// <summary>
        /// 从密文中解析出密钥，并对密文进行解密
        /// </summary>
        /// <param name="s_encrypted">密文</param>
        /// <returns>明文</returns>
        public string Decrypt(string s_encrypted)
        {
            string s_key = string.Empty;
            byte[] key = new byte[CRYPTO_KEY_LENGTH], iv = new byte[CRYPTO_IV_LENGTH];

            if (s_encrypted.Length <= CRYPTO_KEY_LENGTH * 2)
            {
                throw new Exception("无效的密文！");
            }
            if (m_containKey)
            {
                s_key = s_encrypted.Substring(0, CRYPTO_KEY_LENGTH * 2);
                s_encrypted = s_encrypted.Substring(CRYPTO_KEY_LENGTH * 2);
            }
            key = hexString2Byte(s_key);
            iv = string2Byte(CRYPTO_IV.PadRight(iv.Length));
            return Decrypt(s_encrypted, key, iv);
        }

        /// <summary>
        /// 指定密钥，并对密文进行解密
        /// </summary>
        /// <param name="s_encrypted">密文</param>
        /// <param name="s_key">密钥</param>
        /// <returns>明文</returns>
        public string Decrypt(string s_encrypted, string s_key)
        {
            byte[] key = new byte[CRYPTO_KEY_LENGTH], iv = new byte[CRYPTO_IV_LENGTH];

            byte[] temp = string2Byte(s_key);
            if (temp.Length > key.Length)
            {
                throw new Exception("Key不能超过32字节！");
            }
            key = string2Byte(s_key.PadRight(key.Length));
            iv = string2Byte(CRYPTO_IV.PadRight(iv.Length));
            if (m_containKey)
            {
                s_encrypted = s_encrypted.Substring(CRYPTO_KEY_LENGTH * 2);
            }
            return Decrypt(s_encrypted, key, iv);
        }

        #region 私有方法

        /// <summary>
        /// 加密
        /// </summary>
        private string Encrypt(string s_crypto, byte[] key, byte[] iv)
        {
            string s_encryped = string.Empty;
            byte[] crypto, encrypted;
            ICryptoTransform ct;
            try
            {
                crypto = string2Byte(s_crypto);
                m_aesCryptoServiceProvider.Key = key;
                m_aesCryptoServiceProvider.IV = iv;
                ct = m_aesCryptoServiceProvider.CreateEncryptor();
                encrypted = ct.TransformFinalBlock(crypto, 0, crypto.Length);
                if (m_containKey)
                {
                    s_encryped += byte2HexString(key);
                }
                s_encryped += byte2HexString(encrypted);
                return s_encryped;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        private string Decrypt(string s_encrypted, byte[] key, byte[] iv)
        {
            string s_decrypted = string.Empty;
            byte[] encrypted, decrypted;
            ICryptoTransform ct;

            try
            {
                encrypted = hexString2Byte(s_encrypted);
                m_aesCryptoServiceProvider.Key = key;
                m_aesCryptoServiceProvider.IV = iv;
                ct = m_aesCryptoServiceProvider.CreateDecryptor();
                decrypted = ct.TransformFinalBlock(encrypted, 0, encrypted.Length);
                s_decrypted += byte2String(decrypted);
                return s_decrypted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// byte转16进制
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns>16进制</returns>
        private string byte2HexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 16进制转byte
        /// </summary>
        /// <param name="hex">16进制</param>
        /// <returns>byte数组</returns>
        private byte[] hexString2Byte(string hex)
        {
            int len = hex.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                bytes[i] = (byte)(Convert.ToInt32(hex.Substring(i * 2, 2), 16));
            }
            return bytes;
        }

        /// <summary>
        /// 字符串转byte
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>byte数组</returns>
        private byte[] string2Byte(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// byte转字符串
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns>字符串</returns>
        private string byte2String(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        #endregion 私有方法
    }
}