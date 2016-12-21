using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;
using Telegram.Bot.Types;
using File = System.IO.File;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

namespace TelegramBOT.Controllers
{
    static class Bot
    {
        public static readonly Api Api = new Api("316912100:AAGmelO9PwAsMm37fgSgDZAGPtrDxjfZgeU");
    }

    public class WebHookController : ApiController
    {
        public async Task<IHttpActionResult> Post(Update update)
        {
            var message = update.Message;

            Console.WriteLine("Received Message from {0}", message.Chat.Id);

            if (message.Type == MessageType.TextMessage)
            {
                if(message.Text.Equals("바보"))
                    await Bot.Api.SendTextMessage(message.Chat.Id, "웃기시네");
                else
                    // Echo each Message
                    await Bot.Api.SendTextMessage(message.Chat.Id, message.Text);
            }
            else if (message.Type == MessageType.PhotoMessage)
            {
                // Download Photo
                var file = await Bot.Api.GetFile(message.Photo.LastOrDefault()?.FileId);

                var filename = file.FileId + "." + file.FilePath.Split('.').Last();

                using (var saveImageStream = File.Open(filename, FileMode.Create))
                {
                    await file.FileStream.CopyToAsync(saveImageStream);
                }

                await Bot.Api.SendTextMessage(message.Chat.Id, "Thx for the Pics");
            }

            return Ok();
        }
    }
}
