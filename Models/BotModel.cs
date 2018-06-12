using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocarWebPage.Models
{
    [Table("telegram_bot")]
    public class BotModel
    {
        [Key] [Column("bot_name")] public string BotName { get; set; }
        [Column("last_post_id")] public int LastPostId { get; set; }
        [Column("is_active")] public bool IsActive { get; set; }
        [Column("last_post_date")] public DateTime? LastPostDate { get; set; }
        [Column("last_check_date")] public DateTime? LastCheckDate { get; set; }
        [Column("channel_ref")] public String ChannelRef { get; set; }
        [Column("channel_name")] public String ChannelName { get; set; }
    }
}