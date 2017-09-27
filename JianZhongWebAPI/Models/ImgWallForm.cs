using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JianZhongWebAPI.Models
{
    public class ImgWallForm
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string imagepath { get; set; }
        public string mimetype { get; set; }
        public int size { get; set; }
    }
}