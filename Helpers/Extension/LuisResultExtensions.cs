
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataStore;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace LuisBot.Helpers.Extension
{
    public static class LuisResultExtensions
    {
        public static string GetField(this LuisResult result, string field)
        {
            return mapParameter(result.Entities, field);
        }

        private static string mapParameter(IList<EntityRecommendation> entities, string type)
        {
            return entities.FirstOrDefault(it => it.Type == type)?.Entity;
        }

        public static IMessageActivity CreateCardResponse(this IDialogContext context, Attachment attachment, string text = null)
        {
            var msg = context.MakeMessage();
            msg.Text = text;
            msg.Attachments = new List<Attachment> { attachment };
            return msg;
        }

        public static Task PostCardAsync(this IDialogContext context, Attachment attachment, string text = null)
        {
            return context.PostAsync(context.CreateCardResponse(attachment, text));
        }

        public static IMessageActivity CreateCardsResponse(this IDialogContext context, List<Attachment> attachments, string text = null, AttachmentLayout layout = AttachmentLayout.carousel)
        {
            var msg = context.MakeMessage();
            msg.Text = text;
            msg.Attachments = attachments;
            msg.AttachmentLayout = layout.ToString();
            return msg;
        }

        public static Task PostCardsAsync(this IDialogContext context, List<Attachment> attachments, string text = null, AttachmentLayout layout = AttachmentLayout.carousel)
        {
            return context.PostAsync(context.CreateCardsResponse(attachments, text, layout));
        }
    }
}