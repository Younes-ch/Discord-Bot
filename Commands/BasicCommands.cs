using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        [Command("ping"), Aliases("ms")]
        [Description("-Displays your current client ping.")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"Ping: {ctx.Client.Ping}ms");
        }

        [Command("random"), Description("Picks a random number within the specified range"), Aliases("rand")]
        public async Task Random(CommandContext ctx, [Description("Min Value")] int min, [Description("Max Value")] int max)
        {
            await ctx.TriggerTypingAsync();

            var rnd = new Random();
            var emoji = DiscordEmoji.FromName(ctx.Client, ":thinking:");

            await ctx.RespondAsync($"The random generated number is: {rnd.Next(min, max + 1)} {emoji}");
        }

        [Command("add")]
        [Description("-Returns the sum of the two given numbers.")]
        public async Task Add(CommandContext ctx, [Description("1st Number")] int numberOne, [Description("2nd Number")] int numberTwo)
        {
            await ctx.TriggerTypingAsync();
            var emoji = DiscordEmoji.FromName(ctx.Client, ":1234:");
            await ctx.Channel.SendMessageAsync($"{numberOne} + {numberTwo} = {numberOne + numberTwo} {emoji}");
        }

        [Command("subs")]
        [Description("-Subtracts two given numbers.")]
        public async Task Substract(CommandContext ctx, [Description("1st Number")] int numberOne, [Description("2nd Number")] int numberTwo)
        {
            await ctx.TriggerTypingAsync();
            var emoji = DiscordEmoji.FromName(ctx.Client, ":1234:");
            await ctx.Channel.SendMessageAsync($"{numberOne} - {numberTwo} = {numberOne - numberTwo} {emoji}");
        }

        [Command("sum")]
        [Description("-Returns the sums of all given numbers."), ]
        public async Task Sum(CommandContext ctx, [Description("Integers to sum")] params double[] args)
        {
            await ctx.TriggerTypingAsync();

            var sum = args.Sum();
            var emoji = DiscordEmoji.FromName(ctx.Client, ":1234:");

            await ctx.RespondAsync($"The sum of these numbers is {sum}  {emoji}");
        }

        [Command("avatar")]
        [Description("-Returns the avatar of the user mentioned or yours in case no one was tagged.")]
        public async Task Avatar(CommandContext ctx, DiscordMember member = null, ushort size = 256)
        {
            await ctx.TriggerTypingAsync();

            if (member == null)
            {
                var avatarEmbed = new DiscordEmbedBuilder
                {
                    Color = ctx.Member.Color,
                    Url = ctx.Member.AvatarUrl,
                    ImageUrl = ctx.Member.GetAvatarUrl(ImageFormat.Png, size),
                    Title = $"{ctx.Member.Username}'s avatar"
                };
                await ctx.Channel.SendMessageAsync(embed: avatarEmbed).ConfigureAwait(false);
            }
            else
            {
                var avatarEmbed = new DiscordEmbedBuilder
                {
                    Color = member.Color,
                    Url = member.AvatarUrl,
                    ImageUrl = member.GetAvatarUrl(ImageFormat.Png, size),
                    Title = $"{member.Username}'s avatar"
                };
                await ctx.Channel.SendMessageAsync(embed: avatarEmbed).ConfigureAwait(false);
            }
                
        }

        [Command("say")]
        [Description("-Sends a message under the Bot\'s name.")]
        public async Task Say(CommandContext ctx, [RemainingText , Description("The message you want to send")] string content)
        {
            await ctx.TriggerTypingAsync();
            await ctx.Channel.SendMessageAsync(content);
            await ctx.Message.DeleteAsync().ConfigureAwait(false);
        }

        [Command("invite"), Description("-Generates an invite link.")]
        public async Task Invite(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Yellow,
                Title = "Add me to your server!",
                Url = "https://discord.com/oauth2/authorize?client_id=828940570964656139&permissions=1544023121&scope=bot",
                Footer = new DiscordEmbedBuilder.EmbedFooter 
                { 
                    Text = "Creator Ninja.#3069",
                    IconUrl = Bot.myCurrentAvatarUrl
                },
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Bot.currentBotAvatarUrl },
            };

            await ctx.RespondAsync(embed);
        }

        [Command("about"), Description("-Shows details related to the bot.")]
        public async Task About(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            var emoji = DiscordEmoji.FromName(ctx.Client, ":point_down:");

            var embed = new DiscordEmbedBuilder
            {
                Color = ctx.Member.Color,
                Title = "Github link",
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = "https://cdn.discordapp.com/avatars/387798722827780108/243d829b599d25cc29079c64498818a0.png?size=128",
                    Name = $"{emoji} {emoji} {emoji}"
                },
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail 
                {
                    Url = Bot.currentBotAvatarUrl
                },
                Url = "https://github.com/Younes-ch/Ustaz-Bot",
                Footer = new DiscordEmbedBuilder.EmbedFooter 
                { 
                    Text = "Creator Ninja.#3069",
                    IconUrl = Bot.myCurrentAvatarUrl
                },
            };

            embed.AddField("About:", "This bot is made for learning purposes only.", true);

            await ctx.RespondAsync(embed);
        }
    }
}
