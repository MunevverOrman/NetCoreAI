using Newtonsoft.Json;
using System.Text;

class Program
{
    private static async Task Main(string[] args)
    {
        Console.Write("Lütfen Çevirmek İstediğiniz Cümleyi Giriniz:");
        string inputText = Console.ReadLine();

        string apiKey = "YOUR_API_KEY_HERE";

        string translatedText=await TranslateTextToEnglish(inputText, apiKey);

        if (!string.IsNullOrEmpty(translatedText))
        {
            Console.WriteLine();
            Console.Write($"Çeviri(İngilizce): {translatedText}");
            Console.WriteLine();

        }
        else
        {
            Console.Write("Beklenmeyen bir hata oluştu.");
        
        }

    }

    private static async Task<string> TranslateTextToEnglish(string text, string apiKey)
    {
        using (HttpClient httpClient = new HttpClient()) 
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                  new {role="system",content="You are a helpful translator."},
                  new {role="user",content=$"Please translate this text to english: {text}"}

                }
            };

            string jsonBody=JsonConvert.SerializeObject(requestBody);
            var content=new StringContent(jsonBody,Encoding.UTF8,"application/json");

            try
            {
                HttpResponseMessage responseMessage=await httpClient.PostAsync("https://api.openai.com/v1/chat/completions",content);
                string responseString=await responseMessage.Content.ReadAsStringAsync();

                dynamic responseObject=JsonConvert.DeserializeObject(responseString);
                string translation=responseObject.choices[0].message.content;

                return translation;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu {ex.Message}");
                return null;


            }

        }


}
}