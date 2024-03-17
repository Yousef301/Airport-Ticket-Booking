using Microsoft.Extensions.Configuration;

namespace Airport_Ticket_Booking.Helpers;

public class CustomConfigurationManager
{
    private readonly IConfiguration _configuration;

    public CustomConfigurationManager(string configFile)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile(configFile, optional: true, reloadOnChange: true);

        _configuration = builder.Build();
    }

    public string GetValue(string key)
    {
        return _configuration[key];
    }
}