using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;
using LineBOT.Models;
using System.Collections.Generic;

namespace LineBOT.Controllers
{
    public class CallbackController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult POST()
        {
            string postData = string.Empty;
            string ChannelAccessToken = "TD9YQDq4woBQXd91J11K2V08pAoesJQ/SxaOAaT5I7cNQhW8ZYd6Y8Bcs5mB8o6X4xbsl9as8k+FoiRUj7oda0DQ8sMv1NzyPDNEF+3Cfb4pbnDmIRE8cH0uPDz+m5KNz4/bFKxJECSHRgJ8aWUDiAdB04t89/1O/w1cDnyilFU=";

            //try
            //{
                //http Post RawData(should be JSON)
                postData = Request.Content.ReadAsStringAsync().Result;
                //JSON
                ReceievedMessage rmsg = Parsing(postData);

            //string Message;
            //Message = "해인 :" + rmsg.events[0].message.text;
            //ReplyMessageText(rmsg.events[0].replyToken, Message, ChannelAccessToken);

            if (rmsg.events[0].message.type.ToLower().Equals("text"))
            {
                if (rmsg.events[0].message.text.Equals("우리집"))
                {
                    ReplyMessageLocation(rmsg.events[0].replyToken, rmsg.events[0].message, ChannelAccessToken);
                }
                else if (rmsg.events[0].message.text.Equals("해인이"))
                {
                    ReplyMessageImage(rmsg.events[0].replyToken, rmsg.events[0].message, ChannelAccessToken);
                }
                else
                {
                    string Message;
                    Message = "해인 :" + rmsg.events[0].message.text;
                    ReplyMessageText(rmsg.events[0].replyToken, Message, ChannelAccessToken);
                }
            }
            else if (rmsg.events[0].message.type.ToLower().Equals("sticker"))
            {
                ReplyMessageSticker(rmsg.events[0].replyToken, rmsg.events[0].message, ChannelAccessToken);
            }
            //API OK
            return Ok();
            //}
            //catch (Exception ex)
            //{
            //    return Ok();
            //}
        }

        public static ReceievedMessage Parsing(string RawData)
        {
            return JsonConvert.DeserializeObject<ReceievedMessage>(RawData);
        }

        public static string ReplyMessageText(string ReplyToken, string pMessage, string ChannelAccessToken)
        {
            string str2;
            string s = "\r\n{{\r\n    \"replyToken\":\"{0}\",\r\n    \"messages\":[\r\n        {{\r\n            \"type\":\"text\",\r\n            \"text\":\"{1}\"\r\n        }}\r\n    ]\r\n}}";

            try
            {
                pMessage = pMessage.Replace("\n", @"\n");
                pMessage = pMessage.Replace("\r", @"\r");
                pMessage = pMessage.Replace("\"", "'");

                List<Message_Text> lst = new List<Message_Text>();
                lst.Add(new Message_Text
                {
                    type = "text",
                    text = pMessage
                });

                string json = JsonConvert.SerializeObject(new
                {
                    replyToken = ReplyToken,
                    messages = lst
                });

                
                s = string.Format(s.Replace("'", "\""), ReplyToken, pMessage);
                WebClient client = new WebClient();
                client.Headers.Clear();
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                byte[] buffer2 = client.UploadData("https://api.line.me/v2/bot/message/reply", bytes);
                str2 = Encoding.UTF8.GetString(buffer2);
            }
            catch (WebException exception)
            {
                using (StreamReader reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    string str3 = reader.ReadToEnd();
                    throw new Exception("ReplyMessage API ERROR: " + str3, exception);
                }
            }
            return str2;
        }

        public static string ReplyMessageSticker(string ReplyToken, Message msg, string ChannelAccessToken)
        {
            string str2;
            string s = "\r\n{{\r\n    'replyToken':'{0}',\r\n    'messages':[\r\n        {{\r\n            'type':'"+msg.type+ "',\r\n            'packageId':'" + msg.packageId + "',\r\n            'stickerId':'" + msg.stickerId + "'\r\n        }}\r\n    ]\r\n}}";
            try
            {
                //Message = Message.Replace("\n", @"\n");
                //Message = Message.Replace("\r", @"\r");
                //Message = Message.Replace("\"", "'");
                s = string.Format(s.Replace("'", "\""), ReplyToken);
                WebClient client = new WebClient();
                client.Headers.Clear();
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                byte[] buffer2 = client.UploadData("https://api.line.me/v2/bot/message/reply", bytes);
                str2 = Encoding.UTF8.GetString(buffer2);
            }
            catch (WebException exception)
            {
                using (StreamReader reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    string str3 = reader.ReadToEnd();
                    throw new Exception("ReplyMessage API ERROR: " + str3, exception);
                }
            }
            return str2;
        }

        public static string ReplyMessageLocation(string ReplyToken, Message msg, string ChannelAccessToken)
        {
            string str2;
            string s = "\r\n{{\r\n    'replyToken':'{0}',\r\n    'messages':[\r\n        {{\r\n            'type':'location',\r\n            'title':'my location',\r\n            'address':'대한민국 서울특별시 성북구 월곡1동 77-698',\r\n            'latitude':37.607181,\r\n            'longitude':127.038474\r\n        }}\r\n    ]\r\n}}";
            try
            {
                //Message = Message.Replace("\n", @"\n");
                //Message = Message.Replace("\r", @"\r");
                //Message = Message.Replace("\"", "'");
                s = string.Format(s.Replace("'", "\""), ReplyToken);
                WebClient client = new WebClient();
                client.Headers.Clear();
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                byte[] buffer2 = client.UploadData("https://api.line.me/v2/bot/message/reply", bytes);
                str2 = Encoding.UTF8.GetString(buffer2);
            }
            catch (WebException exception)
            {
                using (StreamReader reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    string str3 = reader.ReadToEnd();
                    throw new Exception("ReplyMessage API ERROR: " + str3, exception);
                }
            }
            return str2;
        }

        public static string ReplyMessageImage(string ReplyToken, Message msg, string ChannelAccessToken)
        {
            string str2;
            string s = "\r\n{{\r\n    'replyToken':'{0}',\r\n    'messages':[\r\n        {{\r\n            'type':'image',\r\n            'originalContentUrl':'https://avatars1.githubusercontent.com/u/24517577?v=3&u=d63df664290f4a0ac66840c6092da326a55c6fc7&s=400',\r\n            'previewImageUrl':'https://avatars3.githubusercontent.com/u/24517672?v=3&s=200'\r\n        }}\r\n    ]\r\n}}";
            try
            {
                //Message = Message.Replace("\n", @"\n");
                //Message = Message.Replace("\r", @"\r");
                //Message = Message.Replace("\"", "'");
                s = string.Format(s.Replace("'", "\""), ReplyToken);
                WebClient client = new WebClient();
                client.Headers.Clear();
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                byte[] buffer2 = client.UploadData("https://api.line.me/v2/bot/message/reply", bytes);
                str2 = Encoding.UTF8.GetString(buffer2);
            }
            catch (WebException exception)
            {
                using (StreamReader reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    string str3 = reader.ReadToEnd();
                    throw new Exception("ReplyMessage API ERROR: " + str3, exception);
                }
            }
            return str2;
        }

        public static string ReplyMessageAUSure(string ReplyToken, Message msg, string ChannelAccessToken)
        {
            string str2;
            string s = "\r\n{{\r\n    'replyToken':'{0}',\r\n    'messages':[\r\n        {{\r\n            'type':'image',\r\n            'originalContentUrl':'https://avatars1.githubusercontent.com/u/24517577?v=3&u=d63df664290f4a0ac66840c6092da326a55c6fc7&s=400',\r\n            'previewImageUrl':'https://avatars3.githubusercontent.com/u/24517672?v=3&s=200'\r\n        }}\r\n    ]\r\n}}";
            try
            {
                //Message = Message.Replace("\n", @"\n");
                //Message = Message.Replace("\r", @"\r");
                //Message = Message.Replace("\"", "'");
                s = string.Format(s.Replace("'", "\""), ReplyToken);
                WebClient client = new WebClient();
                client.Headers.Clear();
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                byte[] buffer2 = client.UploadData("https://api.line.me/v2/bot/message/reply", bytes);
                str2 = Encoding.UTF8.GetString(buffer2);
            }
            catch (WebException exception)
            {
                using (StreamReader reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    string str3 = reader.ReadToEnd();
                    throw new Exception("ReplyMessage API ERROR: " + str3, exception);
                }
            }
            return str2;
        }


        //public async Task<HttpResponseMessage> Post()
        //{
        //    var contentString = await Request.Content.ReadAsStringAsync();

        //    dynamic contentObj = JsonConvert.DeserializeObject(contentString);
        //    var result = contentObj.result[0];

        //    var client = new HttpClient();
        //    try
        //    {
        //        //client.DefaultRequestHeaders
        //        //  .Add("X-Line-ChannelID", "1492600672");
        //        //client.DefaultRequestHeaders
        //        //  .Add("X-Line-ChannelSecret", "4224ebd9f5d466296648d20e42821b23");
        //        client.DefaultRequestHeaders
        //          .Add("X-Line-Signature", "NqSOGthaSwce5atbPzDgk7hRksW4g9ibUHtRpp848aimbxkgG2pYB8WUsnKp1CaW4xbsl9as8k+FoiRUj7oda0DQ8sMv1NzyPDNEF+3Cfb6c9mb1YuiiKE4qqh1hXjt89F4tgA/iA9FN/Ci2unQEUQdB04t89/1O/w1cDnyilFU=");


        //        var res = await client.PostAsJsonAsync("https://trialbot-api.line.me/v1/events",
        //            new
        //            {
        //                to = new[] { result.content.from },
        //                toChannel = "1383378250",
        //                eventType = "138311608800106203",
        //                content = new
        //                {
        //                    contentType = 1,
        //                    toType = 1,
        //                    text = $"「{result.content.text}」"
        //                }
        //            });

        //        System.Diagnostics.Debug.WriteLine(await res.Content.ReadAsStringAsync());
        //        return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        //    }
        //    catch (Exception e)
        //    {
        //        System.Diagnostics.Debug.WriteLine(e);
        //        return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
        //    }
        //}
    }
}
