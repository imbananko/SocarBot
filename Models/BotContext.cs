using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocarWebPage.Models
{
    public class BotContext : DbContext
    {
        public BotContext() : base("name=SocarDBConnection")
        {
        }
        public DbSet<BotModel> TelegramBots { get; set; }
    }
}