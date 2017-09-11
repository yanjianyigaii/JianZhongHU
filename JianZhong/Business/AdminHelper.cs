using JianZhong.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JianZhong.Business
{
    public static class AdminHelper
    {
        public static List<ImgWallForm> GetUploadData()
        {
            List<ImgWallForm> list = new List<ImgWallForm>();

            string query =
                "SELECT id, title, description, submitTime, submitUser, approved, imagePath" +
                " FROM dbo.ImgWallForm";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectDB"].ConnectionString))
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
                                imgWallForm.approved = reader.GetBoolean(reader.GetOrdinal("approved"));
                                imgWallForm.imagepath = reader.GetString(reader.GetOrdinal("imagePath"));
                                list.Add(imgWallForm);
                            }
                        }
                    }
                }
            }
            

            return list;
        }
    }
}