using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apilib;
using apilib.Модели;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.Write("Введите ваш токен: ");
        var token = Console.ReadLine();
        var baseUrl = "https://discord.com/api/v9";
        var discordClient = new DiscordClient(baseUrl, token);

        var guilds = await discordClient.GetCurrentUserGuildsAsync();
        Console.WriteLine("Ваши сервера:");
        for (int i = 0; i < guilds.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {guilds[i].Name} (ID: {guilds[i].Id})");
        }

        Console.Write("Выберите сервер по номеру: ");
        if (int.TryParse(Console.ReadLine(), out int serverIndex) && serverIndex > 0 && serverIndex <= guilds.Count)
        {
            var selectedGuild = guilds[serverIndex - 1];
            Console.WriteLine($"Вы выбрали сервер: {selectedGuild.Name}");

            var channels = await discordClient.GetGuildChannelsAsync(selectedGuild.Id);
            Console.WriteLine("Каналы на сервере:");
            for (int i = 0; i < channels.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {channels[i].Name} (ID: {channels[i].Id})");
            }

            Console.Write("Выберите канал по номеру: ");
            if (int.TryParse(Console.ReadLine(), out int channelIndex) && channelIndex > 0 && channelIndex <= channels.Count)
            {
                var selectedChannel = channels[channelIndex - 1];
                Console.WriteLine($"Вы выбрали канал: {selectedChannel.Name}");

                Console.Write("Введите количество сообщений для получения: ");
                if (int.TryParse(Console.ReadLine(), out int messageCount) && messageCount > 0)
                {
                    var messages = await discordClient.GetChannelMessagesAsync(selectedChannel.Id, messageCount);
                    Console.WriteLine($"Последние {messageCount} сообщений из канала {selectedChannel.Name}:");
                    foreach (var message in messages)
                    {
                        Console.WriteLine($"{message.Author}: {message.Content} (ID: {message.Id})");
                    }
                    Console.Write("Введите ID сообщения/сообщений (через пробел) для добавления реакции: ");
                    var input = Console.ReadLine();
                    string[] inputParts = input.Split(' ');
                    string[] messageIds = new string[inputParts.Length];
                    for (int i = 0; i < inputParts.Length; i++)
                    {
                        messageIds[i] = inputParts[i];
                    }
                    var emoji = "👽";

                    foreach (var messageId in messageIds)
                    {
                        await Task.Delay(1000);
                        await discordClient.AddReactionsAsync(selectedChannel.Id, messageIds, emoji);
                        break;
                    }
                    Console.WriteLine("Добавлена реакция на сообщение/сообщения.");
                }
                else
                {
                    Console.WriteLine("Неверное количество сообщений.");
                }
            }
            else
            {
                Console.WriteLine("Неверный выбор канала.");
            }
        }
        else
        {
            Console.WriteLine("Неверный выбор сервера.");
        }
    }
}