using JianZhong.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace JianZhong.Business
{
    public static class AdminHelper
    {
        public const string endpoint = "http://jianzhonghuwebapi.net";
        public static List<ImgWallForm> GetUploadData()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://jianzhonghuwebapi.net/api/image/");
            request.Method = "GET";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string result = readStream.ReadToEnd();
            var images = JsonConvert.DeserializeObject<List<ImgWallForm>>(result);
            return images;
        }

        public static bool PostUploadData(ImgWallForm imageData, HttpPostedFileBase image)
        {
            imageData.imagepath = image.FileName;
            imageData.mimetype = image.ContentType;
            imageData.size = image.ContentLength;

            //save image to disk
            if (image != null && image.ContentLength > 0)
            {
                var stream = image.InputStream;
                var fileName = image.FileName;
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), fileName);
                using (var fileStream = File.Create(path))
                {
                    stream.CopyTo(fileStream);
                }
            }

            string output = JsonConvert.SerializeObject(imageData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://jianzhonghuwebapi.net/api/image/");
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(output);
                streamWriter.Flush();
            }
            try
            {
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}