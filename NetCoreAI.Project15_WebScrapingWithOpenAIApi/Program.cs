using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Text;
using System.Web.Helpers;

class Program
{
    private static readonly string apiKey = "YOUR_API_KEY_HERE";
   public static async Task Main(string[] args)
    {
        Console.Write("Lütfen analiz yapmak istediğiniz web sayfa url'ini giriniz: ");
        string inputUrl;
        inputUrl = Console.ReadLine();

        Console.WriteLine("Web sayfası içeriği analiz ediliyor...");
        Console.WriteLine("Web sayfası içeriği:");



        static string ExtractTextFromHtml(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var bodyText=doc.DocumentNode.SelectSingleNode("//body").InnerText;
            return bodyText ?? "Sayfa içeriği okunamadı.";

        }

        static async Task AnalyzeWithAI(string text, string sourceType)
        {
            using (HttpClient client = new HttpClient())
            {
                               client.DefaultRequestHeaders.Add("Authorization",$"Bearar {apiKey}");
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = $"Sen bir yapay zek asistanısın . Kullanıcının gönderdiği metni analiz eder ve Türkçe olarak özetlersin.Yanıtlarını sadece Türkçe ver." },
                        new { role = "user", content = $"Analyze the summarize {sourceType} \n\n{text}" }
                    }
                };

                string json=JsonConvert.SerializeObject(requestBody);
                HttpContent content= new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response=await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseJson=await response.Content.ReadAsStringAsync();

                if(response.IsSuccessStatusCode)
                {
                    var result=JsonConvert.DeserializeObject<dynamic>(responseJson);
                    Console.WriteLine($"\n AI Analizi Sonucu:\n{result.choices[0].message.content}");
                }
                else
                {
                   
                    Console.WriteLine($"Hata Mesajı: {responseJson}");
                }
        }
        }


    }

    private static void PostAsync(string v, HttpContent content)
    {
        throw new NotImplementedException();
    }
}