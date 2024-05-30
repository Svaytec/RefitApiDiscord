using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apilib;
using apilib.Модели;
using Refit;

namespace apilib
{
    public interface DiscordApi
    {
        [Get("/users/@me/guilds")]
        Task<List<Serv>> GetCurrentUserGuildsAsync([Header("Authorization")] string authorization);

        [Get("/guilds/{guildId}/channels")]
        Task<List<Channels>> GetGuildChannelsAsync([Header("Authorization")] string authorization, [AliasAs("guildId")] string guildId);

        [Get("/channels/{channelId}/messages")]
        Task<List<Messages>> GetChannelMessagesAsync([Header("Authorization")] string authorization, [AliasAs("channelId")] string channelId, [Query] int limit);

        [Put("/channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")]
        Task AddReactionAsync([Header("Authorization")] string authorization, [AliasAs("channelId")] string channelId, [AliasAs("messageId")] string messageId, [AliasAs("emoji")] string emoji);
    }
}