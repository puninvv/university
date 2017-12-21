using Newtonsoft.Json;
using SmartPocket.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartPocket.Dialogs
{
    public static class IUserDialogExtensions
    {
        public static void SendSupportedCommandsMessage(this IUserDialog _dialog, Message _message, ITelegramBotClient _bot)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Без обид, не понял чего вы хотели:(");
            sb.AppendLine("Я поддерживаю следующий список команд:");

            foreach (var command in _dialog.SupportedCommands)
                sb.Append(command.Command).Append(" - ").AppendLine(command.Info);

            _bot.SendTextMessageAsync(_message.Chat.Id, sb.ToString());
        }


        public static string SerializeToJson(this IUserDialog _dialog)
        {
            var tmpList = new List<IUserDialog>() { _dialog };
            return JsonConvert.SerializeObject(tmpList, Formatting.Indented, JsonSerializerSettingsSingletone.Instance);
        }

        public static IUserDialog DeserializeObject(string serializeObject)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<IUserDialog>>(serializeObject, JsonSerializerSettingsSingletone.Instance)[0];
            }
            catch (Exception ex)
            {
                Logger.Log.Error("error", ex);
            }

            return null;
        }
    }
}
