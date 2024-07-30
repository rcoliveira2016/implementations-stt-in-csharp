
using Microsoft.Extensions.Configuration;
using VostSTT;

class Program {
    public static void Main() {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        new TranscreverAudio(config).Transcrever();
    }
}
