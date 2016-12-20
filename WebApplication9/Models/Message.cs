
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBOT.Models
{
    public class Message
    {
        // Properties
        public string address { get; set; }
        public string id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string packageId { get; set; }
        public string stickerId { get; set; }
    }

    public class Message_Text
    {
        // Properties
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Message_Sticker
    {
        // Properties
        public string type { get; set; }
        public string packageId { get; set; }
        public string stickerId { get; set; }
    }

    public class Message_Location
    {
        // Properties
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }

    public class Message_Image
    {
        // Properties
        public string originalContentUrl { get; set; }
        public double previewImageUrl { get; set; }
        public string type { get; set; }
    }
}