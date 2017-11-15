using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore;
using LuisBot.Helpers;
using LuisBot.Helpers.Extension;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {

        private const string EMPLOYEE = "BlackthornVision.Employee";

        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(ConfigurationManager.AppSettings["LuisAppId"], ConfigurationManager.AppSettings["LuisAPIKey"])))
        {
        }


        [LuisIntent("Greetings")]
        public async Task HelloIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(Strings.RandomGreeting).ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Чорт пойме, що ти мелеш. І *{result.Query}* і *{result.Query}*. Ото молодь пішла"); //
            context.Wait(MessageReceived);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "MyIntent" with the name of your newly created intent in the following handler
        [LuisIntent("Login")]
        public async Task LoginIntent(IDialogContext context, LuisResult result)
        {
            var name = result.GetField(EMPLOYEE);
            await context.PostAsync($"А, так би й зразу сказав, що ти {name}").ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("Help")]
        public async Task HelpIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(Strings.Help).ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("NextEvent")]
        public async Task NextEventIntent(IDialogContext context, LuisResult result)
        {

            var events = new List<Event>()
            {
                new Event()
                {
                    Date = new DateTime(2017, 11, 16),
                    Name = "Чатботи, чатботи",
                    Message = "Думаю, що Юрко вам уже все показує",
                    Url = "http://blackthorn-vision.com/case-studies/web-management/",
                    CardImageUrl = "https://did-panas.azurewebsites.net/Assets/bots.png",
                },
                new Event()
                {
                    Date = new DateTime(2017,12,16),
                    Name = "Корпоратівка!",
                    Message = "Ото дід нап'ється!",
                    Url = "http://blackthorn-vision.com/case-studies/charting-library/",
                    CardImageUrl = "https://did-panas.azurewebsites.net/Assets/corporativ.png",
                },
                new Event()
                {
                    Date = new DateTime(2017,12,18),
                    Name = "Миколая",
                    Message = "Ой хто-хто Миколая любить",
                    Url = "http://blackthorn-vision.com/case-studies/befit-and-caltrain/",
                    CardImageUrl = "https://did-panas.azurewebsites.net/Assets/mykolaya.png",
                },
                new Event()
                {
                    Date = new DateTime(2017,12,31),
                    Name = "Новий 2018!",
                    Message = "Жовта земляна собака",
                    Url = "http://blackthorn-vision.com/case-studies/befit-and-caltrain/",
                    CardImageUrl = "https://did-panas.azurewebsites.net/Assets/new-year.png",
                },
            };

            var cards = events.Select(wm => createEventCard(wm).ToAttachment()).ToList();

            await context.PostCardsAsync(cards, "Події").ConfigureAwait(false);
            context.Wait(MessageReceived);
        }

        [LuisIntent("WorkingHoursPass")]
        public async Task WorkingHoursPassIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Та скільки то ти попрацював? Тю. Та хіба жто робота, хіба ногами бовтаєш.").ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("WorkingHoursRemaining")]
        public async Task WorkingHoursRemainingIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Та ще пахати і пахати. 112 годин радісної праці!").ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("ConfirmEvent")]
        public async Task ConfirmEventIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"ОК, записано! Чекаємо з хорошшим настроєм і чайочком").ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("RejectEvent")]
        public async Task RejectEventIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Жаль, ну якщо передумаєш то кажи. Будемо тримати для тебе місце.").ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }


        private static HeroCard createEventCard(Event ev)
        {
            var openWm = new CardAction(CardActionType.OPEN_URL, "Зацінити", value: ev.Url);
            var register = new CardAction(CardActionType.IM_BACK, "Буду", value: $"Буду радий відвідати {ev.Name}");
            var reject = new CardAction(CardActionType.IM_BACK, "Не буду", value: $"{ev.Name} це не для мене.");
            var card = new HeroCard(ev.Name)
            {
                Images = new List<CardImage> { new CardImage(ev.CardImageUrl, $"Хочу зацінити {ev.Name}", openWm) },
                Buttons = new List<CardAction> { openWm, register, reject },
                Text = new StringBuilder()
                    .AppendLine($"{ev.Message} \n")
                    .AppendLine($"*  Дата  : **{ev.Date.ToShortDateString()}** ")
                    .AppendLine($"*  Статус : **Не підтверджено** ")
                    .ToString()
            };
            return card;
        }



    }
}