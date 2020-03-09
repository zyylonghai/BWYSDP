using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SDPCRL.CORE.FileUtils
{
    public class FTPHelp
    {
        //private static string FTPCONSTR = "ftp://192.168.0.103:321/";//FTP的服务器地址，格式为ftp://192.168.1.234:8021/。ip地址和端口换成自己的，这些建议写在配置文件中，方便修改
        //private static string FTPUSERNAME = "ftpzyy";//FTP服务器的用户名
        //private static string FTPPASSWORD = "123456";//FTP服务器的密码
        private string _host = string.Empty;
        private string _UserNm = string.Empty;
        private string _Pwd = string.Empty;

        public List<string> ErrorMsg = null;

        public FTPHelp(string host,string usernm,string pwd)
        {
            this._host = host;
            this._UserNm = usernm;
            this._Pwd = pwd;
            ErrorMsg = new List<string>();
        }

        /// <summary>
        /// 上传文件到远程ftp
        /// </summary>
        /// <param name="ftpPath">ftp上的文件路径</param>
        /// <param name="path">本地的文件目录</param>
        /// <param name="id">文件名</param>
        /// <returns></returns>
        public bool UploadFile(string ftpPath, string localpath)
        {
            //string erroinfo = "";
            FileInfo f = new FileInfo(localpath);
            localpath = localpath.Replace("\\", "/");
            //bool b = MakeDir(ftpPath);
            if (!MakeDir(ftpPath))
            {
                return false;
            }
            //localpath = FTPCONSTR + ftpPath;
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpPath+"/"+f.Name));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(this._UserNm, this._Pwd);
            reqFtp.KeepAlive = false;
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.ContentLength = f.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = f.OpenRead();
            try
            {
                Stream strm = reqFtp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
                //this.ErrorMsg.Add("完成");
                //error = "完成";
                return true;
            }
            catch (Exception ex)
            {
                this.ErrorMsg.Add(string.Format("因{0},无法完成上传{1}", ex.Message));
                //error = string.Format("因{0},无法完成上传", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 批量上传文件到远程ftp
        /// </summary>
        /// <param name="ftpPath"></param>
        /// <param name="localPaths"></param>
        /// <returns></returns>
        public void UploadFileBatch(string ftpPath, List<string> localPaths)
        {
            foreach (string localpath in localPaths)
            {
                UploadFile(ftpPath, localpath);
            }
        }

        /// <summary>
        ///在ftp服务器上创建文件目录
        /// </summary>
        /// <param name="dirName">文件目录</param>
        /// <returns></returns>
        public  bool MakeDir(string dirName)
        {
            try
            {
                bool b = RemoteFtpDirExists(dirName);
                if (b)
                {
                    return true;
                }
                string url = dirName;
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFtp.UseBinary = true;
                // reqFtp.KeepAlive = false;
                reqFtp.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFtp.Credentials = new NetworkCredential(this._UserNm, this._Pwd);
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                //errorinfo = string.Format("因{0},无法下载", ex.Message);
                return false;
            }

        }
        /// <summary>
        /// 判断ftp上的文件目录是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public  bool RemoteFtpDirExists(string path)
        {

            //path = path;
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(this._UserNm, this._Pwd);
            reqFtp.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse resFtp = null;
            try
            {
                resFtp = (FtpWebResponse)reqFtp.GetResponse();
                FtpStatusCode code = resFtp.StatusCode;//OpeningData
                resFtp.Close();
                return true;
            }
            catch
            {
                if (resFtp != null)
                {
                    resFtp.Close();
                }
                return false;
            }
        }

        /// <summary>
        /// 获得文件大小
        /// </summary>
        /// <param name="url">FTP文件的完全路径</param>
        /// <returns></returns>
        public long GetFileSize(string url)
        {

            long fileSize = 0;
            try
            {
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFtp.UseBinary = true;
                reqFtp.Credentials = new NetworkCredential(this._UserNm, this._Pwd);
                reqFtp.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                fileSize = response.ContentLength;

                response.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return fileSize;
        }

        /// <summary>
        /// 从ftp服务器删除文件的功能
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool DeleteFile(string fileName)
        {
            try
            {
                string url =this._host + fileName;
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFtp.UseBinary = true;
                reqFtp.KeepAlive = false;
                reqFtp.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFtp.Credentials = new NetworkCredential(this._UserNm, this._Pwd);
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                //errorinfo = string.Format("因{0},无法下载", ex.Message);
                return false;
            }
        }
    }
}
