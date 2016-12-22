using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;
using KakaoChatBOT.Models;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace KakaoChatBOT.Controllers
{
    public class KeyboardController : ApiController
    {
        [AllowAnonymous]
        public KeyBoardModel GET()
        {
            KeyBoardModel view = new KeyBoardModel();

            string postData = string.Empty;



            List<string> lst = new List<string>();
            lst.Add("버튼1");
            lst.Add("선택2");
            lst.Add("선택3");

            view.type = "buttons";
            view.buttons = lst;

            //string json = JsonConvert.SerializeObject(new
            //{
            //    type = "buttons",
            //    buttons = lst
            //});

            //try
            //{
            //http Post RawData(should be JSON)
            //postData = Request.Content.ReadAsStringAsync().Result;


            return view;

            //API OK
            //return Ok();
            //}
            //catch (Exception ex)
            //{
            //    return Ok();
            //}
        }
    }
}
