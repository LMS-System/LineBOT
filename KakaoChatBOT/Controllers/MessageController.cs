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
using System.Net.Http.Formatting;
using System.Web.Http.Cors;

namespace KakaoChatBOT.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MessageController : ApiController
    {
        public ResponseMessageModel POST()
        {
            ResponseMessageModel result = new ResponseMessageModel();
            KeyBoardModel kmodel = new KeyBoardModel();
            MessageModel model = new MessageModel();
            PhotoModel pmodel = new PhotoModel();
            MessageButtonModel mbmodel = new MessageButtonModel();

            try
            {
                string postData = Request.Content.ReadAsStringAsync().Result;

                RecvMessageModel rcvModel = JsonConvert.DeserializeObject<RecvMessageModel>(postData);
                kmodel.type = "text";

                if (rcvModel.type.Equals("photo"))
                {
                    model.text = "요 사진을 보내셨네요";
                    pmodel.url = rcvModel.content;
                    model.photo = pmodel;
                }
                else if (rcvModel.content.Equals("집구하기"))
                {

                    model.text = "아래 링크를 참조";
                    mbmodel.label = "집보러가자";
                    mbmodel.url = "https://hogangnono.com/";
                    model.message_button = mbmodel;
                }
                else
                {
                    model.text = rcvModel.content;
                    Random rnd = new Random();
                    string str = rnd.Next(1, 7).ToString();
                    pmodel.url = "http://jun-bot.eastasia.cloudapp.azure.com/" + str + ".png";
                    model.photo = pmodel;
                }

                //model.text = rcvModel.content;

                result.message = model;
                result.keyboard = kmodel;

            } catch(Exception ex)
            {
                return result;
            }

            return result;
        }
    }
}
