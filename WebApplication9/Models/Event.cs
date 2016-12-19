
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Event
    {
        public Message message {  get;  set; }
        public string replyToken {  get;  set; }
        public Source source {  get;  set; }
        public long timestamp {  get;  set; }
        public string type {  get;  set; }

    }
}