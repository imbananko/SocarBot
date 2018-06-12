using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Quartz;
using SocarWebPage.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SocarWebPage.WebJob
{

    public class Updater : IJob
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("---");

        private static readonly string baseFeedURL = "http://www.socar.az/socar/en/feed";
        public async Task Execute(IJobExecutionContext context)
        {
            var botName = Bot.GetMeAsync().Result.Username;

            using (var db = new BotContext())
            {
                var dbBot = db.TelegramBots.FirstOrDefault(b => b.BotName == botName);

                if (dbBot == null || !dbBot.IsActive) return;

                var lastArticleId = GetLastAritleRssId();

                if (dbBot.LastPostId != lastArticleId)
                {
                    var article = GetArticleByRssId(lastArticleId);
                    Task.WaitAll(PostToChannel($"{article.Title.Text} {article.Id}"));
                    dbBot.LastPostId = lastArticleId;
                    dbBot.LastPostDate = DateTime.Now;
                }

                dbBot.LastCheckDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        private static Task PostToChannel(string message)
        {
            return Bot.SendTextMessageAsync(new ChatId("@socar_news"), message);
        }

        private static SyndicationItem GetArticleByRssId(int id)
        {
            var reader = XmlReader.Create(baseFeedURL);
            return SyndicationFeed.Load(reader).Items.FirstOrDefault(i => i.Id.EndsWith(id.ToString()));
        }

        private static int GetLastAritleRssId()
        {
            var reader = XmlReader.Create(baseFeedURL);
            return int.Parse(SyndicationFeed.Load(reader).Items.First().Id.Split('/').Last());
        }
    }
}