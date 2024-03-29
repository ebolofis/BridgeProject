﻿using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using Hit.Services.Models.Enums;
using System.Threading;

namespace Hit.Services.Helpers.Classes.Classes
{
    public class FileHelpers
    {

        /// <summary>
        /// Read an ecnrypted file and return the contents on the clear. 
        /// On error throw exception
        /// </summary>
        /// <param name="path">file's path</param>
        /// <returns></returns>
        public string ReadEncryptedFile(string path)
        {
            string contents;
            using (StreamReader r = new StreamReader(path))
            {
                contents = r.ReadToEnd();
            }
            EncryptionHelper eh = new EncryptionHelper();
            return eh.Decrypt(contents);
        }


    
        /// <summary>
        /// Read a  file and return the contents as one string. 
        /// On error throw exception
        /// </summary>
        /// <param name="path">file's path</param>
        /// <returns></returns>
        public string ReadFile(string path,Encoding encoding=null)
        {
            if (encoding == null) encoding = Encoding.Default;
            string contents;
            using (StreamReader r = new StreamReader(path, encoding))
            {
                contents = r.ReadToEnd();
            }
            return contents;
        }

        /// <summary>
        /// Read a text file and return the file lines as list of strings. 
        /// </summary>
        /// <param name="path">file's full path</param>
        /// <param name="comment">if a line starts with this character then this line is considered as comments and it will NOT be included into the returned list of lines</param>
        /// <returns></returns>
        public List<string> ReadFileLines(string path, string comment=null, Encoding encoding = null)
        {
            List<string> lines = new List<string>();
            if (encoding == null) encoding = Encoding.Default;
            foreach (string line in File.ReadAllLines(path, encoding))
            {
                if (comment != null && comment != "" && !line.StartsWith(comment))
                    lines.Add(line);
            }
            return lines;
        }


        /// <summary>
        /// Enumarate files from a Source Directory. Return the list of files match the searchPattern
        /// </summary>
        /// <param name="sourceDirectory">the Source Directoryto  enumerate from</param>
        /// <param name="searchPattern">file's search patterm, ex: *.txt</param>
        /// <param name="searchFileOption">TopDirectoryOnly (default), or AllDirectories</param>
        /// <returns>Return the list of files match the searchPattern</returns>
        public List<string> EnumerateFiles(string sourceDirectory,string searchPattern, SearchFileOption searchFileOption = SearchFileOption.TopDirectoryOnly)
        {
            SearchOption searchOption;
            if (searchFileOption == SearchFileOption.TopDirectoryOnly)
                searchOption = SearchOption.TopDirectoryOnly;
            else
                searchOption = SearchOption.AllDirectories;

            List<string> files= Directory.EnumerateFiles(sourceDirectory, searchPattern, searchOption).ToList<string>();
            if (files == null)
                return new List<string>();
            else
                return files;
        }


        /// <summary>
        /// Write a T model as json to a file
        /// </summary>
        /// <typeparam name="T">model</typeparam>
        /// <param name="path">files's path</param>
        /// <param name="model">model</param>
        /// <param name="isEnctypted">if true then encrypt json before saving to file</param>
        public void WriteToFile<T>(string path, T model,bool isEnctypted)
        {
            EncryptionHelper eh = new EncryptionHelper();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            if (isEnctypted) json = eh.Encrypt(json);
            System.IO.File.WriteAllText(path, json);
        }

        /// <summary>
        /// check write permissions in a folder. If user has no write permisions it throws an UnauthorizedAccessException 
        /// </summary>
        /// <param name="directory"></param>
        public void CheckWritePermissions(string directory)
        {
            try
            {
                string file = directory + @"\textfile";
                using (FileStream fs = File.Create(
             Path.Combine(
                 directory,
                 Path.GetRandomFileName()
             ),
             1,
             FileOptions.DeleteOnClose))
                { }
            }
            catch (Exception ex)
            {
                UnauthorizedAccessException uex = new UnauthorizedAccessException("Hit Services has No write permisions to folder: "+ directory,ex);
                throw uex;
            }
        }

        /// <summary>
        /// Simulate the process of Writing a T model as json to a file. Return the content of the file but DO NOT save to file.
        /// </summary>
        /// <typeparam name="T">model</typeparam>
        /// <param name="path">files's path</param>
        /// <param name="model">model</param>
        /// <param name="isEnctypted">if true then encrypt json before saving to file</param>
        public string SimulateWriteToFile<T>(string path, T model, bool isEnctypted)
        {
            EncryptionHelper eh = new EncryptionHelper();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            if (isEnctypted) json = eh.Encrypt(json);
            return json;
        }

        /// <summary>
        /// Write a string  to a file. Also replase path's &lt;TIMESTAMP&gt; with a proper value (DateTime.Now). 
        /// </summary>
        /// <param name="path">files's path</param>
        /// <param name="text">test to write to file</param>
        /// <param name="isEnctypted">if true then encrypt test before saving to file</param>
        /// <param name="timeStamp">Timestamp's format.</param>
        public void WriteTextToFile(string path, string text, bool isEnctypted,string timeStamp="yyyyMMddHHmmss")
        {
            EncryptionHelper eh = new EncryptionHelper();
            if (isEnctypted) text = eh.Encrypt(text);

            if (path.Contains("<TIMESTAMP>"))
            {
                path= path.Replace("<TIMESTAMP>", DateTime.Now.ToString(timeStamp));
            }
            System.IO.File.WriteAllText(path, text);
        }

        /// <summary>
        /// Write an html string  to a PDF file. Also replase path's &lt;TIMESTAMP&gt; with a proper value (DateTime.Now). 
        /// </summary>
        /// <param name="path">files's path</param>
        /// <param name="html">html text to write to file</param>
        /// <param name="css">css text to write to file</param>
        /// <param name="timeStamp">Timestamp's format.</param>
        public void WriteHtmlToPdf(string path, string html,string css,  string timeStamp = "yyyyMMddHHmmss")
        {
            //Install 2 NuGet packages iTextSharp and itextsharp.xmlworker

            if (path.Contains("<TIMESTAMP>"))
            {
                path = path.Replace("<TIMESTAMP>", DateTime.Now.ToString(timeStamp));
            }

            byte[] pdf; // result will be here

            using (var memoryStream = new MemoryStream())
            {
                var document = new Document(PageSize.A4, 50, 50, 60, 60);
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                {
                    using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                    }
                }

                document.Close();

                pdf = memoryStream.ToArray();
            }

            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                fs.Write(pdf, 0, pdf.Length);
            }

            //// write to pdf file
            //StringReader sr = new StringReader(text);
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //    pdfDoc.Open();

            //    htmlparser.Parse(sr);
            //    pdfDoc.Close();

            //    byte[] bytes = memoryStream.ToArray();
            //    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            //    {
            //        fs.Write(bytes, 0, bytes.Length);
            //    }
            //    memoryStream.Close();
            //}
        }



        /// <summary>
        ///return full path for folder \Json. 
        /// </summary>
        /// <returns></returns>
        public string GetJsonPath()
        {
            string pasePath = getBasePath();
            DirectoryInfo fi;
            string newPath = null;

            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\Json\"));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }

            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\..\HitServices_WebApi\Json\"));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @".\Json\"));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }


            return null;
        }



        /// <summary>
        /// return full path for a Settings file (relative path: .....\HitServices_WebApi\Settings)
        /// </summary>
        /// <param name="filename">file name (with or without path)</param>
        /// <returns></returns>
        public string GetSettingsPath(string filename)
        {
            FileInfo fi;
            string newPath = null;

            try
            {
                fi = new FileInfo(filename);//filename is full path
                if (fi.Exists) return fi.FullName;
            }
            catch { }

            string pasePath = getBasePath();
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\Settings\" + filename));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\..\HitServices_WebApi\Settings\" + filename));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @".\Settings\" + filename));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\Settings\" + filename));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }

            return "Settings_NOT_FOUND("+ filename + ")";
        }

        /// <summary>
        /// return the full path for a Settings folder (relative path: .....\HitServices_WebApi\Settings)
        /// </summary>
        /// <returns></returns>
        public string GetSettingsPath()
        {
            DirectoryInfo fi;
            string newPath = null;

            string pasePath = getBasePath();
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\Settings\" ));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\..\HitServices_WebApi\Settings\" ));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch 
            { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @".\Settings\" ));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\Settings\" ));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }

            return "Settings_FOLDER_NOT_FOUND";
        }


        /// <summary>
        /// return full path for Scripts file (relative path: .....\HitServices_WebApi\Scripts)
        /// </summary>
        /// <param name="filename">file name (with or without path)</param>
        /// <returns></returns>
        public string GetScriptsPath(string filename)
        {

            string pasePath = getBasePath();
            FileInfo fi;
            string newPath = null;

            try
            {
                fi = new FileInfo(filename);//filename is full path
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\AppScripts\" + filename));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\..\HitServices_WebApi\AppScripts\" + filename));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch 
            { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @".\AppScripts\" + filename));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\AppScripts\" + filename));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }

            return "Scripts_NOT_FOUND(" + filename + ")";
        }

        /// <summary>
        /// return full path for Scripts folder (relative path: .....\HitServices_WebApi\Scripts)
        /// </summary>
        /// <param name="filename">file name (with or without path)</param>
        /// <returns></returns>
        public string GetScriptsPath()
        {

            string pasePath = getBasePath();
            DirectoryInfo fi;
            string newPath = null;

          
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\AppScripts\"));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\..\HitServices_WebApi\AppScripts\"));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @".\AppScripts\"));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\AppScripts\"));
                fi = new DirectoryInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }

            return "Scripts_Folder_NOT_FOUND";
        }


        /// <summary>
        /// return the full path for HitServicesWA.dll
        /// </summary>
        /// <returns></returns>
        public string GetHitServicesWAPath()
        {
            string pasePath = getBasePath();
            FileInfo fi;
            string newPath = null;

            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @".\HitServicesWA.dll"));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }

            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\..\HitServices_WebApi\bin\HitServicesWA.dll"));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }
           


            return "HitServicesWA.dll_NOT_FOUND";
        }

        /// <summary>
        /// return full path for Scripts folder (relative path: .....\HitServices_Scheduler\ExtJobs)
        /// </summary>
        /// <returns></returns>
        public List<string> GetExternalJobsPaths()
        {

            string pasePath = getBasePath();
            string newPath = null;
            string fileType = "dll";
            List<string> FilePathsList = new List<string>();

            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\ExtJobs\"));
                DirectoryInfo d = new DirectoryInfo(newPath);
                if (d.Exists)
                {
                    FileInfo[] fileList = d.GetFiles("*." + fileType, SearchOption.AllDirectories);
                    foreach (FileInfo file in fileList)
                    {
                        FilePathsList.Add(file.FullName);
                    }
                    return FilePathsList;
                }
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\..\HitServices_WebApi\ExtJobs\"));
                DirectoryInfo d = new DirectoryInfo(newPath);
                if (d.Exists)
                {
                    FileInfo[] fileList = d.GetFiles("*." + fileType, SearchOption.AllDirectories);

                    foreach (FileInfo file in fileList)
                    {
                        FilePathsList.Add(file.FullName);
                    }
                    return FilePathsList;
                }
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @".\ExtJobs\"));
                DirectoryInfo d = new DirectoryInfo(newPath);
                if (d.Exists)
                {
                    FileInfo[] fileList = d.GetFiles("*." + fileType, SearchOption.AllDirectories);

                    foreach (FileInfo file in fileList)
                    {
                        FilePathsList.Add(file.FullName);
                    }
                    return FilePathsList;
                }
            }
            catch { }
            try
            {
                newPath = Path.GetFullPath(Path.Combine(pasePath, @"..\..\ExtJobs\"));
                DirectoryInfo d = new DirectoryInfo(newPath);
                if (d.Exists)
                {
                    FileInfo[] fileList = d.GetFiles("*." + fileType, SearchOption.AllDirectories);

                    foreach (FileInfo file in fileList)
                    {
                        FilePathsList.Add(file.FullName);
                    }
                    return FilePathsList;
                }
            }
            catch { }
            return FilePathsList;
        }

        /// <summary>
        ///  return the full path for HitServices_Core.dll
        /// </summary>
        /// <returns></returns>
        public string GetExtPath()
        {
            string basePath = getBasePath();
            FileInfo fi;
            string newPath = null;

            try
            {
                newPath = Path.GetFullPath(Path.Combine(basePath, @".\HitServices_Core.dll"));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }

            try
            {
                newPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\HitServices_Core\bin\Debug\HitServices_Core.dll"));
                fi = new FileInfo(newPath);
                if (fi.Exists) return fi.FullName;
            }
            catch { }

            return "HitServices_Core.dll_NOT_FOUND";
        }


        /// <summary>
        /// Get application's base path
        /// </summary>
        /// <returns></returns>
        public string getBasePath()
        {
            //as window service
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services\HitServices_Scheduler");
                if (key != null)
                {
                    Object o = key.GetValue("ImagePath");
                    if (o != null)
                    {
                        FileInfo fi = new FileInfo(o as String);
                        return fi.DirectoryName;
                    }
                }
            }
            catch { }

            // as webapi or test project
            return AppDomain.CurrentDomain.BaseDirectory;

        }


        /// <summary>
        /// Read a Csv File
        /// </summary>
        /// <param name="path">file's path</param>
        /// <param name="delimeter">delimeter</param>
        /// <param name="hasHeader">true id file has header</param>
        /// <param name="headerlist">list of header's names (Required if hasHeader=false)</param>
        /// <param name="encoding">encoding (default: UTF8)</param>
        /// <returns></returns>
        public List<IDictionary<string, dynamic>> ReadCsvFile(string path, string delimeter,bool hasHeader,List<string> headerlist=null, Encoding encoding= null)
        {
            if (hasHeader == false && (headerlist == null || headerlist.Count==0))
                throw new Exception("Csv file must have header as first line Or header must be provided manually from configuration.");

            if (encoding==null) encoding= Encoding.UTF8;
            List<IDictionary<string, dynamic>> dict = null;
            IEnumerable<dynamic> records = null;
            ConvertDynamicHelper dynamicCast = new ConvertDynamicHelper();
            using (TextReader fileReader = File.OpenText(path))
            {
                var csv = new CsvReader(fileReader);
                csv.Configuration.HasHeaderRecord = hasHeader;
                csv.Configuration.Delimiter = delimeter;
                csv.Configuration.Encoding = encoding;
                if (hasHeader)
                {//  file has  header
                    records = csv.GetRecords<dynamic>();
                    return dynamicCast.ToListDictionary(records);
                }
                else
                {//  file has no header
                    dict = new List<IDictionary<string, dynamic>>();
                    while (csv.Read())
                    {
                        var record = csv.GetRecord<dynamic>();
                        dict.Add(dynamicCast.ToDictionary(record, headerlist));
                    }
                    return dict;
                }
            }
        }
    }
}
