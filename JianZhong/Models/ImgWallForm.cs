using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JianZhong.Models
{
    public class ImgWallForm
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool finalizedSubmission { get; set; }
        public string hostedPage { get; set; }
        public string submitTime { get; set; }
        public string submitUser { get; set; }
        public bool? approved { get; set; }
        public string approvedName
        {
            get
            {
                if (approved != null)
                {
                    if (approved == true)
                    {
                        return "APPROVED";
                    }
                    else
                    {
                        return "DENIED";
                    }
                }
                else
                {
                    return "PENDING";
                }
            }
        }
        public string imagepath { get; set; }
    }
}