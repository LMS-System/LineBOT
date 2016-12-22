using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KakaoChatBOT.Models
{
    public class KeyBoardModel
    {
        public string type { get; set; }

        public List<string> buttons { get; set; }
    }
}