using DiscordBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Bot
    {
        //Update these on change
        public static string myCurrentAvatarUrl = "https://cdn.discordapp.com/avatars/387798722827780108/7536028bc305d6c3b5a870385ee13df3.png?size=128";
        public static string currentBotAvatarUrl = "https://cdn.discordapp.com/avatars/828940570964656139/ffb3bcc787967a62ee1e509a3861ba80.png?size=128";


        public readonly EventId BotEventId = new EventId(42, "Ustaz-Bot");
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }

        public async Task RunAsync()
        {

            var fileContent = await File.ReadAllTextAsync("config.json");
            var configJson = JsonSerializer.Deserialize<ConfigJson>(fileContent);


            var botConfig = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                LogTimestampFormat = "MMM dd yyyy - hh:mm:ss tt"
            };

            Client = new DiscordClient(botConfig);

            // Debug Purposes..
            Client.Ready += (Client, e) =>
            {
                Client.Logger.LogInformation(BotEventId, "Client is ready to process events.");
                return Task.CompletedTask;
            };

            Client.GuildAvailable += (Client, e) =>
            {
                Client.Logger.LogInformation(BotEventId, $"Guild available: {e.Guild.Name} && Owner: {e.Guild.Owner.Username}");
                return Task.CompletedTask;
            };

            Client.ClientErrored += (Client, e) =>
            {
                Client.Logger.LogInformation(BotEventId, e.Exception, "Exception occured");
                return Task.CompletedTask;
            };

            Client.MessageCreated += (Client, e) =>
            {
                Client.Logger.LogInformation(BotEventId, $"Server: {e.Guild.Name}, Channel: #{e.Channel.Name}, Message Content: \"{e.Message.Content}\" (Sent by {e.Author.Username})");
                return Task.CompletedTask;
            };

            var config = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
            };

            Commands = Client.UseCommandsNext(config);

            Commands.SetHelpFormatter<CustomHelp>();
            Commands.RegisterCommands<BasicCommands>();
            Commands.RegisterCommands<UseFulCommands>();
            
            // Debug purposes..
            Commands.CommandExecuted += (Commands, e) =>
            {
                e.Context.Client.Logger.LogInformation(BotEventId, $"{e.Context.User.Username} successfully executed {e.Command.QualifiedName}");
                return Task.CompletedTask;
            };

            

            Client.UseInteractivity(new InteractivityConfiguration
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromSeconds(30)
            });

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}
