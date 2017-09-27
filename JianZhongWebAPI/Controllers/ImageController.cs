using JianZhongWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JianZhongWebAPI.Controllers
{
    public class ImageController : ApiController
    {
        // GET api/<controller>
        public List<ImgWallForm> Get()
        {
            List<ImgWallForm> list = new List<ImgWallForm>();

            string query =
                "SELECT id, title, description, imagepath, mimetype, size" +
                " FROM dbo.ImgWallForm";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ZhongJianHUDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check is the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                ImgWallForm imgWallForm = new ImgWallForm();
                                imgWallForm.id = reader.GetInt32(reader.GetOrdinal("id"));
                                imgWallForm.title = reader.GetString(reader.GetOrdinal("title"));
                                imgWallForm.description = reader.GetString(reader.GetOrdinal("description"));
                                imgWallForm.imagepath = reader.GetString(reader.GetOrdinal("imagePath"));
                                imgWallForm.mimetype = reader.GetString(reader.GetOrdinal("mimetype"));
                                imgWallForm.size = reader.GetInt32(reader.GetOrdinal("size"));
                                list.Add(imgWallForm);
                            }
                        }
                    }
                }
            }
            return list;
        }

        // POST api/<controller>
        [HttpPost]
        public bool Post([FromBody]ImgWallForm value)
        {   
            ImgWallForm imageData = value;
            //call api/save metadata to the database
            try
            {
                string query = "INSERT dbo.ImgWallForm (title, description, imagepath, mimetype, size) VALUES (@title, @description, @imagepath, @mimetype, @size)";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZhongJianHUDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@title", imageData.title);
                        cmd.Parameters.AddWithValue("@description", imageData.description);
                        cmd.Parameters.AddWithValue("@imagepath", imageData.imagepath);
                        cmd.Parameters.AddWithValue("@mimetype", imageData.mimetype);
                        cmd.Parameters.AddWithValue("@size", imageData.size);
                        int result = cmd.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                        {
                            return false;
                        }

                    }
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