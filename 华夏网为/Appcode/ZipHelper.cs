using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace 华夏网为.Appcode
{
    class ZipHelper
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationZipFilePath"></param>
        public static void CreateZip(string sourceFilePath, string destinationZipFilePath)
        {
            if (sourceFilePath[sourceFilePath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                sourceFilePath += System.IO.Path.DirectorySeparatorChar;

            ZipOutputStream zipStream = new ZipOutputStream(File.Create(destinationZipFilePath));
            zipStream.SetLevel(6);  // 压缩级别 0-9
            CreateZipFiles(sourceFilePath, zipStream, sourceFilePath);

            zipStream.Finish();
            zipStream.Close();
        }

        /// <summary>
        /// 递归压缩文件
        /// </summary>
        /// <param name="sourceFilePath">待压缩的文件或文件夹路径</param>
        /// <param name="zipStream">打包结果的zip文件路径（类似 D:\WorkSpace\a.zip）,全路径包括文件名和.zip扩展名</param>
        /// <param name="staticFile"></param>
        private static void CreateZipFiles(string sourceFilePath, ZipOutputStream zipStream, string staticFile)
        {

            Crc32 crc = new Crc32();
            string[] filesArray = Directory.GetFileSystemEntries(sourceFilePath);
            foreach (string file in filesArray)
            {
                if (Directory.Exists(file))                     //如果当前是文件夹，递归
                {
                    CreateZipFiles(file, zipStream, staticFile);
                }

                else                                            //如果是文件，开始压缩
                {
                    FileStream fileStream = File.OpenRead(file);
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    string tempFile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempFile);

                    entry.DateTime = DateTime.Now;
                    entry.Size = fileStream.Length;
                    fileStream.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zipStream.PutNextEntry(entry);

                    zipStream.Write(buffer, 0, buffer.Length);
                }
            }
        }
        /// <summary>   
        /// 压缩文件夹    
        /// </summary>   
        /// <param name="folderToZip">要压缩的文件夹路径</param>   
        /// <param name="zipedFile">压缩文件完整路径</param>   
        /// <param name="password">密码</param>   
        /// <returns>是否压缩成功</returns>   
        public static bool ZipDirectory(string folderToZip, string zipedFile, string password)
        {
            bool result = false;
            if (!Directory.Exists(folderToZip))
                return result;

            ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipedFile));
            zipStream.SetLevel(6);
            if (!string.IsNullOrEmpty(password))

                zipStream.Password = password;

            CreateZipFiles(folderToZip, zipStream, zipedFile);

            zipStream.Finish();
            zipStream.Close();

            return result;
        }


        /// <summary>
        /// ZIP压缩单个文件
        /// </summary>
        /// <param name="sFileToZip">需要压缩的文件（绝对路径）</param>
        /// <param name="sZippedPath">压缩后的文件路径（绝对路径）</param>
        /// <param name="sZippedFileName">压缩后的文件名称（文件名，默认 同源文件同名）</param>
        /// <param name="nCompressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="nBufferSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <param name="bEncrypt">是否加密（默认 加密）</param>
        /// <param name="sPassword">密码（设置加密时生效。默认密码为"123"）</param>
        public static string ZipFile(string sFileToZip, string sZippedPath, string sZippedFileName = "", int nCompressionLevel = 5, int nBufferSize = 2048, bool bEncrypt = false, string sPassword = "123")
        {
            if (!File.Exists(sFileToZip))
            {
                return null;
            }
            string sZipFileName = string.IsNullOrEmpty(sZippedFileName) ? sZippedPath + "\\" + new FileInfo(sFileToZip).Name.Substring(0, new FileInfo(sFileToZip).Name.LastIndexOf('.')) + ".zip" : sZippedPath + "\\" + sZippedFileName + ".zip";
            using (FileStream aZipFile = File.Create(sZipFileName))
            {
                using (ZipOutputStream aZipStream = new ZipOutputStream(aZipFile))
                {
                    using (FileStream aStreamToZip = new FileStream(sFileToZip, FileMode.Open, FileAccess.Read))
                    {
                        string sFileName = sFileToZip.Substring(sFileToZip.LastIndexOf("\\") + 1);
                        ZipEntry ZipEntry = new ZipEntry(sFileName);
                        if (bEncrypt)
                        {
                            aZipStream.Password = sPassword;
                        }
                        aZipStream.PutNextEntry(ZipEntry);
                        aZipStream.SetLevel(nCompressionLevel);
                        byte[] buffer = new byte[nBufferSize];
                        int sizeRead = 0;
                        try
                        {
                            do
                            {
                                sizeRead = aStreamToZip.Read(buffer, 0, buffer.Length);
                                aZipStream.Write(buffer, 0, sizeRead);
                            }
                            while (sizeRead > 0);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        aStreamToZip.Close();
                    }
                    aZipStream.Finish();
                    aZipStream.Close();
                }
                aZipFile.Close();
            }
            return sZipFileName;
        }

    }
}