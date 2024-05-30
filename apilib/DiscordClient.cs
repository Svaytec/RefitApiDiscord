using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apilib.Модели;
using apilib;
using Refit;
using System.Threading.Channels;

public class DiscordClient
{
    private readonly DiscordApi Api;
    private readonly string _token;

    public DiscordClient(string baseUrl, string token)
    {
        Api = RestService.For<DiscordApi>(baseUrl);
        _token = token;
    }

    public async Task<List<Serv>> GetCurrentUserGuildsAsync()
    {
        return await Api.GetCurrentUserGuildsAsync($"{_token}");
    }

    public async Task<List<Channels>> GetGuildChannelsAsync(string guildId)
    {
        return await Api.GetGuildChannelsAsync($"{_token}", guildId);
    }

    public async Task<List<Messages>> GetChannelMessagesAsync(string channelId, int limit)
    {
        return await Api.GetChannelMessagesAsync($"{_token}", channelId, limit);
    }

    public async Task AddReactionAsync(string channelId, string messageId, string emoji)
    {
        await Api.AddReactionAsync($"{_token}", channelId, messageId, emoji);
    }

    public async Task AddReactionsAsync(string channelId, string[] messageIds, string emoji)
    {
        foreach (var messageId in messageIds)
        {
            await Task.Delay(1000);
            await Api.AddReactionAsync($"{_token}", channelId, messageId, emoji);
        }
    }
}