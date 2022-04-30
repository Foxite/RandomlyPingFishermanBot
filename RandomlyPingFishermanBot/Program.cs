using DSharpPlus;
using DSharpPlus.Entities;

string botToken = Environment.GetEnvironmentVariable("BOT_TOKEN")!;
ulong targetId = ulong.Parse(Environment.GetEnvironmentVariable("TARGET_ID")!);
ulong guildId = ulong.Parse(Environment.GetEnvironmentVariable("GUILD_ID")!);

var random = new Random();
var discord = new DiscordClient(new DiscordConfiguration() {
	Token = botToken
});

await discord.ConnectAsync();

while (true) {
	await Task.Delay(TimeSpan.FromHours(random.NextDouble() * 22 + 2));
	
	DiscordGuild guild = await discord.GetGuildAsync(guildId);
	List<DiscordChannel> channels = guild.Channels.Values.ToList();
	DiscordChannel channel = channels[random.Next(0, channels.Count)];
	await channel.SendMessageAsync($"<@{targetId}>");
}
