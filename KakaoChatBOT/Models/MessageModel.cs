
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KakaoChatBOT.Models
{
    public class RecvMessageModel
    {
        // Properties
        public string user_key { get; set; }
        public string type { get; set; }
        public string content { get; set; }
    }

    public class ResponseMessageModel
    {
        // Properties
        public MessageModel message { get; set; }
        public KeyBoardModel keyboard { get; set; }
    }

    public class MessageModel
    {
        // Properties
        public string text { get; set; }
        public PhotoModel photo { get; set; }
        public MessageButtonModel message_button { get; set; }
    }

    public class PhotoModel
    {
        // Properties
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class MessageButtonModel
    {
        // Properties
        public string label { get; set; }
        public string url { get; set; }
    }
}