/*
һ��������Ϣ��
��ԴЭ�飺https://github.com/xucongli1989/XCLNetTools/blob/master/LICENSE
��Ŀ��ַ��https://github.com/xucongli1989/XCLNetTools
Create By: XCL @ 2012

 */

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XCLNetTools.FileHandler
{
    /// <summary>
    /// �ļ�Ŀ¼������
    /// </summary>
    public static class FileDirectory
    {
        #region Ŀ¼����

        /// <summary>
        /// ���Ŀ¼�Ƿ�Ϊ��Ŀ¼����û���ļ��У�Ҳû���ļ���
        /// </summary>
        /// <param name="path">Ŀ¼·��</param>
        /// <returns>true:��Ŀ¼��false:�ǿ�Ŀ¼</returns>
        public static bool IsEmpty(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return true;
            }
            var files = Directory.GetFiles(path);
            var dirs = Directory.GetDirectories(path);
            return (null == files || files.Length == 0) && (null == dirs || dirs.Length == 0);
        }

        /// <summary>
        /// �ж�Ŀ¼�Ƿ����
        /// </summary>
        /// <param name="directoryName">Ŀ¼·��</param>
        /// <returns>true�����ڣ�false��������</returns>
        public static bool DirectoryExists(string directoryName)
        {
            return Directory.Exists(directoryName);
        }

        /// <summary>
        /// ����Ŀ¼
        /// </summary>
        /// <param name="directoryName">Ŀ¼��</param>
        /// <returns>����boolean,true:Ŀ¼�����ɹ�, false:Ŀ¼����ʧ��</returns>
        public static bool MakeDirectory(string directoryName)
        {
            try
            {
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ɾ��ָ����Ŀ¼
        /// </summary>
        /// <param name="directoryName">Ŀ¼��</param>
        /// <returns>true��ɾ���ɹ���false��ɾ��ʧ��</returns>
        public static bool RMDIR(string directoryName)
        {
            DirectoryInfo di = new DirectoryInfo(directoryName);
            if (di.Exists == false)
            {
                return false;
            }
            else
            {
                di.Delete(true);
                return true;
            }
        }

        /// <summary>
        /// ɾ��Ŀ¼��ɾ�����µ���Ŀ¼�����ļ�
        /// </summary>
        /// <param name="directoryName">Ŀ¼��</param>
        /// <returns>true:ɾ���ɹ�,false:ɾ��ʧ��</returns>
        public static bool DelTree(string directoryName)
        {
            if (DirectoryExists(directoryName))
            {
                Directory.Delete(directoryName, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ���ָ��Ŀ¼
        /// </summary>
        /// <param name="rootPath">Ҫ��յ�Ŀ¼</param>
        /// <returns>�Ƿ�����ɹ�</returns>
        public static bool ClearDirectory(string rootPath)
        {
            //ɾ����Ŀ¼
            string[] subPaths = System.IO.Directory.GetDirectories(rootPath);
            foreach (string path in subPaths)
            {
                DelTree(path);
            }
            //ɾ���ļ�
            string[] files = XCLNetTools.FileHandler.ComFile.GetFolderFiles(rootPath);
            if (null != files && files.Length > 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    XCLNetTools.FileHandler.ComFile.DeleteFile(files[i]);
                }
            }
            return true;
        }

        /// <summary>
        /// ��ȡָ��Ŀ¼�µ������ļ����ļ�����Ϣ
        /// </summary>
        /// <param name="dirPath">Ҫ��ȡ��Ϣ��Ŀ¼·��</param>
        /// <param name="rootPath">��·�������ø�ֵ�󣬷��ص���Ϣʵ���н���������ڸø�·�������·����Ϣ��</param>
        /// <param name="webRootPath">web��·�����������ɸ��ļ����ļ��е�web·�������磺http://www.a.com/web/</param>
        /// <returns>�ļ���Ϣlist</returns>
        public static List<XCLNetTools.Entity.FileInfoEntity> GetFileList(string dirPath, string rootPath = "", string webRootPath = "")
        {
            if (!string.IsNullOrEmpty(dirPath))
            {
                dirPath = XCLNetTools.FileHandler.ComFile.MapPath(dirPath);
            }
            if (!string.IsNullOrEmpty(rootPath))
            {
                rootPath = XCLNetTools.FileHandler.ComFile.MapPath(rootPath);
            }

            if (string.IsNullOrEmpty(dirPath) || FileDirectory.IsEmpty(dirPath))
            {
                return null;
            }
            int idx = 1;

            List<XCLNetTools.Entity.FileInfoEntity> result = new List<Entity.FileInfoEntity>();
            XCLNetTools.Entity.FileInfoEntity tempFileInfoEntity = null;
            //�ļ���
            var directories = System.IO.Directory.EnumerateDirectories(dirPath);
            if (null != directories && directories.Any())
            {
                directories.ToList().ForEach(k =>
                {
                    var dir = new System.IO.DirectoryInfo(k);
                    if (dir.Exists)
                    {
                        tempFileInfoEntity = new Entity.FileInfoEntity();
                        tempFileInfoEntity.ID = idx++;
                        tempFileInfoEntity.Name = dir.Name;
                        tempFileInfoEntity.IsFolder = true;
                        tempFileInfoEntity.Path = k;
                        tempFileInfoEntity.RootPath = rootPath;
                        tempFileInfoEntity.RelativePath = ComFile.GetUrlRelativePath(rootPath, k);
                        tempFileInfoEntity.WebPath = webRootPath.TrimEnd('/') + "/" + tempFileInfoEntity.RelativePath;
                        tempFileInfoEntity.ModifyTime = dir.LastWriteTime;
                        tempFileInfoEntity.CreateTime = dir.CreationTime;
                        result.Add(tempFileInfoEntity);
                    }
                });
            }

            //�ļ�
            string[] files = XCLNetTools.FileHandler.ComFile.GetFolderFiles(dirPath);
            if (null != files && files.Length > 0)
            {
                files.ToList().ForEach(k =>
                {
                    var file = new System.IO.FileInfo(k);
                    if (file.Exists)
                    {
                        tempFileInfoEntity = new Entity.FileInfoEntity();
                        tempFileInfoEntity.ID = idx++;
                        tempFileInfoEntity.Name = file.Name;
                        tempFileInfoEntity.IsFolder = false;
                        tempFileInfoEntity.Path = k;
                        tempFileInfoEntity.RootPath = rootPath;
                        tempFileInfoEntity.RelativePath = ComFile.GetUrlRelativePath(rootPath, k);
                        tempFileInfoEntity.WebPath = webRootPath.TrimEnd('/') + "/" + tempFileInfoEntity.RelativePath;
                        tempFileInfoEntity.ModifyTime = file.LastWriteTime;
                        tempFileInfoEntity.CreateTime = file.CreationTime;
                        tempFileInfoEntity.Size = file.Length;
                        tempFileInfoEntity.ExtName = (file.Extension ?? "").Trim('.');
                        result.Add(tempFileInfoEntity);
                    }
                });
            }

            return result;
        }

        #endregion Ŀ¼����

        #region �ļ�����

        /// <summary>
        /// ����һ���ļ�
        /// </summary>
        /// <param name="filePathName">Ŀ¼��</param>
        /// <returns>true:�����ɹ�,false:����ʧ��</returns>
        public static bool CreateTextFile(string filePathName)
        {
            FileInfo info = new FileInfo(filePathName);
            if (info.Exists)
            {
                return true;
            }
            using (var fs = info.Create())
            {
                if (null != fs)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ���ļ���׷������
        /// </summary>
        /// <param name="filePathName">�ļ���</param>
        /// <param name="writeWord">׷������</param>
        public static void AppendText(string filePathName, string writeWord)
        {
            AppendText(filePathName, writeWord, System.Text.Encoding.Default);
        }

        /// <summary>
        /// ���ļ���׷������
        /// </summary>
        /// <param name="filePathName">�ļ���</param>
        /// <param name="writeWord">׷�ӵ�����</param>
        /// <param name="encode">����</param>
        public static void AppendText(string filePathName, string writeWord, System.Text.Encoding encode)
        {
            //�����ļ�
            CreateTextFile(filePathName);
            //�õ�ԭ���ļ�������
            using (FileStream fileRead = new FileStream(filePathName, FileMode.Open, FileAccess.ReadWrite))
            using (StreamReader fileReadWord = new StreamReader(fileRead, encode))
            using (StreamWriter fileWrite = new StreamWriter(fileRead, encode))
            {
                string oldString = fileReadWord.ReadToEnd().ToString();
                oldString = oldString + writeWord;
                fileWrite.Write(oldString);
            }
        }

        /// <summary>
        /// ��ȡ�ļ�������
        /// </summary>
        /// <param name="filePathName">�ļ���</param>
        /// <returns>�ļ�����</returns>
        public static string ReadFileData(string filePathName)
        {
            string str = "";
            using (FileStream fileRead = new FileStream(filePathName, FileMode.Open, FileAccess.Read))
            using (StreamReader fileReadWord = new StreamReader(fileRead, System.Text.Encoding.Default))
            {
                str = fileReadWord.ReadToEnd().ToString();
            }
            return str;
        }

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="absoluteFilePath">�ļ����Ե�ַ</param>
        /// <returns>true:ɾ���ļ��ɹ�,false:ɾ���ļ�ʧ��</returns>
        public static bool FileDelete(string absoluteFilePath)
        {
            try
            {
                FileInfo objFile = new FileInfo(absoluteFilePath);
                if (objFile.Exists)//�������
                {
                    //ɾ���ļ�.
                    objFile.Delete();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        #endregion �ļ�����
    }
}