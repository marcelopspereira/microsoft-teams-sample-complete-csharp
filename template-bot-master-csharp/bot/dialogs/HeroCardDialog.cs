﻿using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Teams.TemplateBotCSharp.Properties;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace Microsoft.Teams.Tutorial.CSharp
{
    /// <summary>
    /// This is Begin Dialog Class. Main purpose of this class is to notify users that Child dialog has been called 
    /// and its a Basic example to call Child dialog from Root Dialog.
    /// </summary>

    [Serializable]
    public class HeroCardDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var message = context.MakeMessage();
            var attachment = GetHeroCard();

            message.Attachments.Add(attachment);

            await context.PostAsync((message));

            context.Done<object>(null);
        }

        private static Attachment GetHeroCard()
        {
            var baseUri = ConfigurationManager.AppSettings["BaseUri"].ToString();
            var heroCard = new HeroCard
            {
                Title = Strings.HeroCardTitle,
                Subtitle = Strings.HeroCardSubTitle,
                Text = Strings.HeroCardTextMsg,
                Images = new List<CardImage> { new CardImage(baseUri + "/public/assets/computer_people.jpg") },
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.OpenUrl, Strings.HeroCardButtonCaption, value: "https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-add-rich-card-attachments"),
                    new CardAction(ActionTypes.MessageBack, Strings.MessageBackCardButtonCaption, value: "{\"" + Strings.cmdValueMessageBack + "\": \"" + Strings.cmdValueMessageBack+ "\"}", text:"hello", displayText:"I clicked the button to send 'hello'"),
                    new CardAction(ActionTypes.Signin, "Sign In", value: baseUri + "/authentication/simple-start.aspx?width=5000&height=5000")
                }
            };

            return heroCard.ToAttachment();
        }
    }
}