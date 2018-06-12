using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using SocarWebPage.Models;

namespace SocarWebPage.Controllers
{
    public class HomeController : Controller
    {
        BotContext db = new BotContext();

        public ActionResult Index()
        {
            IEnumerable<BotModel> bots = db.TelegramBots;
            Console.WriteLine(bots.Count());
            ViewBag.Bots = bots;
            return View();
        }

        [HttpPost]
        public void UpdateBotStatus(String channelName, String isChecked)
        {
            db.TelegramBots.First(b => channelName.Contains(b.ChannelName)).IsActive = isChecked == "true";
            db.SaveChangesAsync();
        }
    }
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString FormatDate(this HtmlHelper helper, DateTime? dateTime)
        {
            if (dateTime == null) return new MvcHtmlString("");
            String result = "Recently";
            var diff = DateTime.Now - dateTime;

            if (diff?.TotalMinutes < 14 * 24 * 60)
                result = "Less than 2 weeks ago";
            if (diff?.TotalMinutes < 7 * 24 * 60)
                result = "Less than a week ago";
            if (diff?.TotalMinutes < 2 * 24 * 60)
                result = "Less than 2 days ago";
            if (diff?.TotalMinutes < 24 * 60)
                result = "Less than a day ago";
            if (diff?.TotalMinutes < 12 * 60)
                result = "About 12 hours ago";
            if (diff?.TotalMinutes < 6 * 60)
                result = "About 6 hours ago";
            if (diff?.TotalMinutes < 3 * 60)
                result = "About 3 hours ago";
            if (diff?.TotalMinutes < 60)
                result = "About 1 hour ago";
            if (diff?.TotalMinutes < 30)
                result = "About half an hour ago";
            if (diff?.TotalMinutes < 10)
                result = "Less than 10 minutes ago";
            if (diff?.TotalMinutes < 5)
                result = "Less than 5 minutes ago";
            if (diff?.TotalMinutes < 1)
                result = "Less than a minute ago";
            return new MvcHtmlString(result);
        }
    }
}