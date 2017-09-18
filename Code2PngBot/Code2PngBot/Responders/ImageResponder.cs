using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using ColorCode;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace Code2PngBot.Responders
{
    public class ImageResponder : IResponder
    {
        public async void SendResponce(Message _msg, TelegramBotClient _bot)
        {
            var lng = GetLanguage(_msg.Text);
            if (lng == null)
            {
                await _bot.SendTextMessageAsync(_msg.Chat.Id, "Sorry, i can not support your language now.\nContact punin.v.v@gmail.com with your request :3");
                return;
            }

            try
            {

                var source = _msg.Text.Remove(0, lng.Item2);
                var colorizedCode = new CodeColorizer().Colorize(source, lng.Item1);

                var image = TheArtOfDev.HtmlRenderer.WinForms.HtmlRender.RenderToImageGdiPlus(colorizedCode);

                var tmpFileName = Guid.NewGuid().ToString() + ".png";
                image.Save(tmpFileName, ImageFormat.Png);

                using (var fileStream = System.IO.File.OpenRead(tmpFileName))
                {
                    FileToSend fts = new FileToSend();
                    fts.Content = fileStream;
                    fts.Filename = tmpFileName;

                    await _bot.SendPhotoAsync(_msg.Chat.Id, fts, "Enjoy!", replyToMessageId: _msg.MessageId);
                }

                System.IO.File.Delete(tmpFileName);
            }
            catch (Exception ex)
            {
                await _bot.SendTextMessageAsync(_msg.Chat.Id, "Sorry, i can not answer to your request:(\nContact @puninvv");
                Logger.Instance.Write(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

        public Tuple<ILanguage, int> GetLanguage(string _input)
        {
            if (_input.StartsWith("/cs"))
                return new Tuple<ILanguage, int>(Languages.CSharp, 3);
            if (_input.StartsWith("/cpp"))
                return new Tuple<ILanguage, int>(Languages.Cpp, 4);
            if (_input.StartsWith("/java"))
                return new Tuple<ILanguage, int>(Languages.Java,5);
            if (_input.StartsWith("/sql"))
                return new Tuple<ILanguage, int>(Languages.Sql, 4);
            if (_input.StartsWith("/xml"))
                return new Tuple<ILanguage, int>(Languages.Xml, 4);
            return null;
        }
    }
}
