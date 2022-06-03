using DSharpPlus;
using DSharpPlus.Entities;

string botToken = Environment.GetEnvironmentVariable("BOT_TOKEN")!;
ulong targetId = ulong.Parse(Environment.GetEnvironmentVariable("TARGET_ID")!);

var random = new Random();
var discord = new DiscordClient(new DiscordConfiguration() {
	Token = botToken
});

await discord.ConnectAsync();

while (true) {
	await Task.Delay(TimeSpan.FromHours(random.NextDouble() * 48 + 24));

	// List all channels in all guilds that the target can access (but not necessarily read history) and the bot can send messages in
	var channels = new List<DiscordChannel>();
	foreach (DiscordGuild guild in discord.Guilds.Values) {
		DiscordMember? targetMember = await guild.GetMemberAsync(targetId);
		if (targetMember != null) {
			foreach (DiscordChannel channel in guild.Channels.Values) {
				if ((channel.PermissionsFor(guild.CurrentMember) & Permissions.SendMessages) != 0 &&
					(channel.PermissionsFor(targetMember) & Permissions.AccessChannels) != 0) {
					channels.Add(channel);
				}
			}
		}
	}

	// Crash if there aren't any channels
	DiscordChannel selectedChannel = channels[random.Next(0, channels.Count)];
	await selectedChannel.SendMessageAsync($"<@{targetId}>");
}
