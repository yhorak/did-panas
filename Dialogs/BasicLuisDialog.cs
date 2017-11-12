using System;
using System.Configuration;
using System.Threading.Tasks;
using LuisBot.Helpers;
using LuisBot.Helpers.Extension;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

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
            await context.PostAsync($"������ ��� �����, � �� ��� �����?").ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"���� �����, �� �� �����. � *{result.Query}* � *{result.Query}*. ��� ������ ����"); //
            context.Wait(MessageReceived);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "MyIntent" with the name of your newly created intent in the following handler
        [LuisIntent("Login")]
        public async Task LoginIntent(IDialogContext context, LuisResult result)
        {
            var name = result.GetField(EMPLOYEE);
            await context.PostAsync($"�, ��� �� � ����� ������, �� ��: {name}").ConfigureAwait(false); //
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
            var message = @"

              *  **16.11.2017** ���� ��� ��� ���� �������!
              *  **16.12.2017** ������� �����������. ��� �� ���������!

            ";
            await context.PostAsync(message).ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("WorkingHoursPass")]
        public async Task WorkingHoursPassIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"�� ������ �� �� ����������? ��. �� ���� ��� ������, ���� ������ ������.").ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("WorkingHoursRemaining")]
        public async Task WorkingHoursRemainingIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"�� �� ������ � ������. 112 ����� ������ �����!").ConfigureAwait(false); //
            context.Wait(MessageReceived);
        }



    }
}