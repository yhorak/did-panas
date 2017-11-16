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

            await context.PostCardsAsync(cards, "").ConfigureAwait(false);
            context.Wait(MessageReceived);
        }

        [LuisIntent("WorkingHours")]
        public async Task WorkingHoursPassIntent(IDialogContext context, LuisResult result)
        {
            await context.PostCardAsync(createAnimationCard().ToAttachment()).ConfigureAwait(false); //
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

        [LuisIntent("ShowProfile")]
        public async Task ShowProfile(IDialogContext context, LuisResult result)
        {
            var profiles = new List<Profile>()
            {
                new Profile()
                {
                    Name = "Юрко",
                    Birthday = new DateTime(1988,03,18),
                    Vacation = 4,
                    SickLeave = "Хворів, 5 днів",
                    Ensurance = 1234.55,
                    Url = "http://blackthorn-vision.com/case-studies/web-management/",
                    CardImageUrl = "https://did-panas.azurewebsites.net/Assets/sickly.jpg",
                },
                
            };

            var cards = profiles.Select(wm => createProfileCard(wm).ToAttachment()).ToList();

            await context.PostCardsAsync(cards, "").ConfigureAwait(false);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Vote")]
        public async Task Vote(IDialogContext context, LuisResult result)
        {
            var events = new List<Event>()
            {
                new Event()
                {
                    Name = "Фото 1",
                    Url = "http://blackthorn-vision.com/case-studies/web-management/",
                    CardImageUrl = "https://did-panas.azurewebsites.net/Assets/bots.png",
                },
                new Event()
                {
                    Name = "Фото 2!",
                    Url = "http://blackthorn-vision.com/case-studies/charting-library/",
                    CardImageUrl = "https://did-panas.azurewebsites.net/Assets/corporativ.png",
                },
                new Event()
                {
                    Name = "Фото 3",
                    Url = "http://blackthorn-vision.com/case-studies/befit-and-caltrain/",
                    CardImageUrl = "https://did-panas.azurewebsites.net/Assets/mykolaya.png",
                },
            };

            var cards = events.Select(wm => createVoteCard(wm).ToAttachment()).ToList();

            await context.PostCardsAsync(cards, "").ConfigureAwait(false);
            context.Wait(MessageReceived);
        }

        [LuisIntent("VoteResult")]
        public async Task VoteResult(IDialogContext context, LuisResult result)
        {

            await context.PostAsync(Strings.VoteResult).ConfigureAwait(false);
            context.Wait(MessageReceived);
        }

        private static HeroCard createEventCard(Event ev)
        {
            var openWm = new CardAction(CardActionType.OPEN_URL, "Зацінити", value: ev.Url);
            // var okAct = new CardAction(CardActionType.IM_BACK, "Буду точно", value: $"{ev.Name}  ");
            var reject = new CardAction(CardActionType.IM_BACK, "Запишіть", value: $" {ev.Name} ? Чекайте на мене");
            var reject2 = new CardAction(CardActionType.IM_BACK, "Не буду", value: $"{ev.Name} не для мене.");
            var card = new HeroCard(ev.Name, tap: new CardAction(CardActionType.IM_BACK, value:ev.Url))
            {
                Images = new List<CardImage> { new CardImage(ev.CardImageUrl, $"Хочу зацінити {ev.Name}", openWm) },
                Buttons = new List<CardAction> { openWm , reject, reject2 },
                Text = new StringBuilder()
                    .AppendLine($"{ev.Message} \n")
                    .AppendLine($"*  Дата  : **{ev.Date.ToShortDateString()}** ")
                    .AppendLine($"*  Статус : **Не підтверджено** ")
                    .ToString()
            };
            return card;
        }

        private static HeroCard createVoteCard(Event it)
        {
            var vote = new CardAction(CardActionType.IM_BACK, "Я голосую за ", value: $"{it.Name}");
            var card = new HeroCard(it.Name, tap: new CardAction(CardActionType.IM_BACK, value: it.Url))
            {
                Images = new List<CardImage> { new CardImage(it.CardImageUrl, $"Кандидат {it.Name}") },
                Buttons = new List<CardAction> { vote },
                Text = new StringBuilder()
                    .AppendLine($"{it.Name}")
                    .ToString()
            };
            return card;
        }

        private static HeroCard createProfileCard(Profile it)
        {
            var openProfile = new CardAction(CardActionType.OPEN_URL, "Відкрити в браузері", value: it.Url);
            var card = new HeroCard(it.Name)
            {
                Images = new List<CardImage> { new CardImage(it.CardImageUrl, $"Відкрити в браузері", openProfile) },
                Buttons = new List<CardAction> { openProfile },
                Text = new StringBuilder()
                    .AppendLine($"*  День Народження : **{it.Birthday.ToShortDateString()}** ")
                    .AppendLine($"*  Відпустка       : **{it.Vacation}** дні")
                    .AppendLine($"*  Лікарняні       : **{it.SickLeave}** ")
                    .AppendLine($"*  Страхівка       : **{it.Ensurance}** грн ")
                    .ToString()
            };
            return card;
        }


        private static AnimationCard createAnimationCard()
        {
            var card = new AnimationCard("Огляд робочих годин", "Листопад 2017" )
            {
                Image = new ThumbnailUrl(""),
                Media = new List<MediaUrl>()
                {
                    new MediaUrl("https://did-panas.azurewebsites.net/Assets/hard_work.gif", "image/gif")
                },
                Text = new StringBuilder()
                    .AppendLine($"* Всього       : *176* ")
                    .AppendLine($"* На сьогодні  : *96* ")
                    .AppendLine($"* Залишилось   : *80* ")
                    .AppendLine($"* Рейт         : *середній* ")
                    .AppendLine($"* Овертайми    : *за бажанням* ")
                    .ToString()

            };
            return card;
        }

 

    }
}