using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    [Description("-(Group of commands) Administrative commands.")]
    public class UseFulCommands : BaseCommandModule
    {
        [Command("nick"), Description("-Gives someone a new nickname (the member must be lower than the bot in the hierarchy)."), RequirePermissions(Permissions.ManageNicknames)]
        public async Task ChangeNickname(CommandContext ctx, [Description("Member to change the nickname for.")] DiscordMember member, [RemainingText, Description("new_nickname")] string new_nickname)
        {
            await ctx.TriggerTypingAsync();

            try
            {
                await member.ModifyAsync(x =>
                {
                    x.Nickname = new_nickname;
                    x.AuditLogReason = $"Changed by {ctx.User.Username}#{ctx.Member.Discriminator} ({ctx.User.Id})";
                });

                var emoji = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");

                var embed = new DiscordEmbedBuilder()
                {
                    Color = DiscordColor.Green,
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                    {
                        Url = member.GetAvatarUrl(ImageFormat.Png, 128)
                    },
                    Title = "Access granted!",
                    Description = $"{member.Mention}'s nickname changed to \"**{new_nickname}**\" successfully! {emoji}",
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        IconUrl = ctx.Member.GetAvatarUrl(ImageFormat.Png, 128),
                        Text = $"Changed by {ctx.Member.Username}#{ctx.Member.Discriminator}"
                    }
                };

                await ctx.RespondAsync(embed);
            }
            catch (Exception)
            {
                var stopSignEmoji = DiscordEmoji.FromName(ctx.Client, ":no_entry:");
                var stopHandEmoji = DiscordEmoji.FromName(ctx.Client, ":raised_hand:");

                var embed = new DiscordEmbedBuilder
                {
                    Title = $"Acces Denied! {stopSignEmoji}",
                    Description = $"I do not have permission to execute this command on someone higher than me! {stopHandEmoji}",
                    Color = DiscordColor.Red
                };

                await ctx.RespondAsync(embed);
            }
        }
        

        [Command("delete"), Description("-Deletes the channel where the message was sent."), RequirePermissions(Permissions.ManageChannels)]
        public async Task Delete(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            try
            {
                await ctx.Channel.DeleteAsync();

                var emoji = DiscordEmoji.FromName(ctx.Client, ":ok_hand:");

                await ctx.RespondAsync(emoji);
            }
            catch (Exception)
            {
                var stopSignEmoji = DiscordEmoji.FromName(ctx.Client, ":no_entry:");
                var stopHandEmoji = DiscordEmoji.FromName(ctx.Client, ":raised_hand:");

                var embed = new DiscordEmbedBuilder
                {
                    Title = $"Acces Denied! {stopSignEmoji}",
                    Description = $"I do not have the permission to execute this command! {stopHandEmoji}",
                    Color = DiscordColor.Red
                };

                await ctx.RespondAsync(embed);
            }

        }

        [Command("CreateT"), Description("-Creates a new text Channel."), RequirePermissions(Permissions.ManageChannels)]
        public async Task CreateT(CommandContext ctx, [RemainingText, Description("Name of the channel")] string name)
        {
            await ctx.TriggerTypingAsync();

            try
            {
                await ctx.Guild.CreateChannelAsync(name, ChannelType.Text);
                var emoji = DiscordEmoji.FromName(ctx.Client, ":ok_hand:");

                await ctx.RespondAsync(emoji);
            }
            catch (Exception)
            {
                var stopSignEmoji = DiscordEmoji.FromName(ctx.Client, ":no_entry:");
                var stopHandEmoji = DiscordEmoji.FromName(ctx.Client, ":raised_hand:");

                var embed = new DiscordEmbedBuilder
                {
                    Title = $"Acces Denied! {stopSignEmoji}",
                    Description = $"I do not have the permission to execute this command! {stopHandEmoji}",
                    Color = DiscordColor.Red
                };

                await ctx.RespondAsync(embed);
            }
        }


        [Command("CreateV"), Description("-Creates a new voice channel."), RequirePermissions(Permissions.ManageChannels)]
        public async Task CreateV(CommandContext ctx, [RemainingText, Description("Name of the channel")] string name)
        {
            await ctx.TriggerTypingAsync();

            try
            {
                await ctx.Guild.CreateVoiceChannelAsync(name);
                var emoji = DiscordEmoji.FromName(ctx.Client, ":ok_hand:");

                await ctx.RespondAsync(emoji);
            }
            catch (Exception)
            {
                var stopSignEmoji = DiscordEmoji.FromName(ctx.Client, ":no_entry:");
                var stopHandEmoji = DiscordEmoji.FromName(ctx.Client, ":raised_hand:");

                var embed = new DiscordEmbedBuilder
                {
                    Title = $"Acces Denied! {stopSignEmoji}",
                    Description = $"I do not have the permission to execute this command! {stopHandEmoji}",
                    Color = DiscordColor.Red
                };

                await ctx.RespondAsync(embed);
            }
        }


        [Command("CreateC"), Description("-Creates a new Category for channels."), RequirePermissions(Permissions.ManageChannels)]
        public async Task CreateC(CommandContext ctx, [RemainingText, Description("Name of the Category")] string name)
        {
            await ctx.TriggerTypingAsync();

            try
            {
                await ctx.Guild.CreateChannelCategoryAsync(name);
                var emoji = DiscordEmoji.FromName(ctx.Client, ":ok_hand:");

                await ctx.RespondAsync(emoji);
            }
            catch (Exception)
            {
                var stopSignEmoji = DiscordEmoji.FromName(ctx.Client, ":no_entry:");
                var stopHandEmoji = DiscordEmoji.FromName(ctx.Client, ":raised_hand:");

                var embed = new DiscordEmbedBuilder
                {
                    Title = $"Acces Denied! {stopSignEmoji}",
                    Description = $"I do not have the permission to execute this command! {stopHandEmoji}",
                    Color = DiscordColor.Red
                };

                await ctx.RespondAsync(embed);
            }
        }
    }
}
