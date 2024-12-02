using Microsoft.AspNetCore.Authentication.OAuth;

internal class DiscordAuthenticationEvents : OAuthEvents
{
    public Func<object, Task> OnCreatingTicket { get; set; }
}