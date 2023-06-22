using System.Text;
using System.Text.Json;

namespace DiscordWebhookSender
{
    class Program
    {

        static void SetForegroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        static void ResetForegroundColor()
        {
            Console.ResetColor();
        }

        static async Task Main(string[] args)
        {
            using var client = new HttpClient();

            Console.Clear();
            SetForegroundColor(ConsoleColor.Blue);
            Console.WriteLine(@"  ███████╗██████╗  █████╗ ███╗   ███╗███╗   ███╗███████╗██████╗ 
  ██╔════╝██╔══██╗██╔══██╗████╗ ████║████╗ ████║██╔════╝██╔══██╗
  ███████╗██████╔╝███████║██╔████╔██║██╔████╔██║█████╗  ██████╔╝
  ╚════██║██╔═══╝ ██╔══██║██║╚██╔╝██║██║╚██╔╝██║██╔══╝  ██╔══██╗
  ███████║██║     ██║  ██║██║ ╚═╝ ██║██║ ╚═╝ ██║███████╗██║  ██║
  ╚══════╝╚═╝     ╚═╝  ╚═╝╚═╝     ╚═╝╚═╝     ╚═╝╚══════╝╚═╝  ╚═╝");
            ResetForegroundColor();
            Console.WriteLine();
            SetForegroundColor(ConsoleColor.Red);
            Console.WriteLine("Created by 69UnderWater#0545");
            ResetForegroundColor();
            Console.WriteLine();

            Console.WriteLine("Enter the Discord webhook URL:");
            var webhookUrl = Console.ReadLine();

            //Console.WriteLine("Enter the message to send:");
            //var message = Console.ReadLine();

            Console.WriteLine("Enter the number of times to send the message:");
            var countString = Console.ReadLine();

            if (!int.TryParse(countString, out var count))
            {
                Console.WriteLine("Invalid number entered.");
                return;
            }

            // Create an embed object
            var embed = new
            {
                title = "Discord Webhook Spammer",
                description = "Project only for educational purposes",
                color = 16711680, // Red color
                //image = new
                //{
                //    url = "https://img.freepik.com/vektoren-kostenlos/einzelner-baum-mit-gruenen-blaettern-auf-transparentem-hintergrund_1308-68031.jpg?w=2000"
                //},
                thumbnail = new 
                {
                    url = "https://img.freepik.com/vektoren-kostenlos/einzelner-baum-mit-gruenen-blaettern-auf-transparentem-hintergrund_1308-68031.jpg?w=2000"
                },
                //fields = new[]
                //        {
                //            new
                //            {
                //                name = "Field 1",
                //                value = "Value 1",
                //                inline = true
                //            },
                //            new
                //            {
                //                name = "Field 2",
                //                value = "Value 2",
                //                inline = true
                //            }
                //        }
            };

            // Add the embed object to the payload object
            var payload = new
            {
                //content = message,
                embeds = new[] { embed }
            };

            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { IgnoreNullValues = true });

            for (var i = 0; i < count; i++)
            {
                try
                {
                    var response = await client.PostAsync(webhookUrl, new StringContent(json, Encoding.UTF8, "application/json"), CancellationToken.None);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error sending message. Response status code: {response.StatusCode}");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return;
                }
            }

            Console.WriteLine("Messages sent successfully.");
        }
    }
}

// cmd to compile the files in one file
// dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true