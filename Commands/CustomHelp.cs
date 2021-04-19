using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Builders;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class CustomHelp : BaseHelpFormatter
    {
        protected DiscordEmbedBuilder embed;

        //Constructor
        public CustomHelp(CommandContext ctx) : base(ctx)
        {
            var emoji = DiscordEmoji.FromName(ctx.Client, ":point_right:");
            embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Yellow,
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = "https://cdn.discordapp.com/avatars/387798722827780108/243d829b599d25cc29079c64498818a0.png?size=128",
                    Name = $"{emoji} Github link",
                    Url = "https://github.com/Younes-ch/Ustaz-Bot"
                },
                Title = "Commands:",
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    IconUrl = Bot.myCurrentAvatarUrl,
                    Text = "Creator Ninja.#3069"
                }
            };
        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            if (command.Aliases?.Any() == true)
                embed.AddField("Aliases:", string.Join(", ", command.Aliases.Select(Formatter.InlineCode)), false);
            if (command.Overloads?.Any() == true)
            {
                var sb = new StringBuilder();
                embed.AddField(command.Name + ':', $"``_{command.Name}``: {command.Description}");

                foreach (var ovl in command.Overloads.OrderByDescending(x => x.Priority))
                {
                    foreach (var arg in ovl.Arguments)
                        sb.Append(arg.IsOptional || arg.IsCatchAll ? " [" : " <").Append(arg.Name).Append(" (").Append(CommandsNext.GetUserFriendlyTypeName(arg.Type)).Append("): ").Append(arg.Description ?? "No description provided.").Append(arg.IsCatchAll ? "..." : "").Append(arg.IsOptional || arg.IsCatchAll ? ']' : '>');

                    if (sb.Length != 0)
                        embed.AddField("Arguments:", sb.ToString().Trim(), false);
                }

            }
            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> cmds)
        {
            foreach (var cmd in cmds)
            {
                embed.AddField(cmd.Name + ':', $"``_{cmd.Name}``: {cmd.Description}");
            }

            return this;
        }

        public override CommandHelpMessage Build()
        {
            return new CommandHelpMessage(embed: embed);
        }
    }
}
