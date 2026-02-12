using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program
{
    private static readonly string apiKey = "YOUR_API_KEY_HERE";
    static async Task Main(string[] args)
    {
        Console.Write("Uzun metninizi veya makalenizi giriniz: ");
        string input;
        input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine();
            Console.WriteLine("Girmiş olduğunuz metin AI tarafından özetleniyor...");
            Console.WriteLine();

            string shortSummary = await SummarizeText(input, "short");
            string mediumSummary = await SummarizeText(input, "medium");
            string detailedSummary = await SummarizeText(input, "detailed");

            Console.WriteLine("Özetler");
            Console.WriteLine("------------------------");
            Console.WriteLine($" ** Kısa Özet: ** {shortSummary}");
            Console.WriteLine("------------------------");
            Console.WriteLine($" ** Orta Uzunlukta Özet: ** {mediumSummary}");
            Console.WriteLine("------------------------");
            Console.WriteLine($" ** Detaylı Özet: ** {detailedSummary}");
        }



        async Task<string> SummarizeText(string text, string level)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                string instruction = level switch
                {
                    "short" => "Summarize this text in 1-2 sentences.",
                    "medium" => "Summarize this text in 3-5 sentences.",
                    "detailed" => "Summarize this text in a detailed but concise manner.",
                    _ => "Summerize this text."
                };

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                    new {role="system",content="You are an AI that summarize text info different leves: short, medium and detailed."},
                    new {role="user",content=$"{instruction}\n\n{text}"}
                }
                };

                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    return result.choices[0].message.content.ToString();
                }
                else
                {
                    Console.WriteLine($"Hata: {responseJson}");
                    return "Hata!";
                }
            }
        }
    }
}
//Yapay zekâ, günümüzde teknolojik dönüşümün en güçlü itici unsurlarından biri hâline gelmiştir.Eskiden yalnızca teorik çalışmalarla sınırlı olan yapay zekâ, artık günlük hayatın merkezindedir.Akıllı telefonlardan arama motorlarına kadar pek çok sistem yapay zekâ ile çalışmaktadır.Sağlık sektöründe hastalık teşhisi ve tedavi planlamasında önemli rol oynamaktadır.Eğitim alanında kişiselleştirilmiş öğrenme modelleri sunarak verimliliği artırmaktadır.Finans sektöründe dolandırıcılık tespiti ve risk analizi için yaygın olarak kullanılmaktadır.Yapay zekâ, büyük veri analizi sayesinde insanın fark edemeyeceği örüntüleri yakalayabilmektedir.Doğal dil işleme teknolojileri insan–makine etkileşimini daha doğal hâle getirmiştir.Görüntü işleme sistemleri güvenlik ve otomasyon alanlarında büyük ilerleme sağlamıştır.Buna rağmen yapay zekânın etik boyutu önemli tartışmaları da beraberinde getirmektedir.Veri gizliliği ve güvenliği günümüzde en çok sorgulanan konular arasındadır.Ayrıca yapay zekânın iş gücü üzerindeki etkileri endişe yaratmaktadır.Bazı meslekler dönüşürken bazıları tamamen ortadan kalkma riski taşımaktadır.Bu durum yeni meslek alanlarının ortaya çıkmasını da hızlandırmaktadır.Yapay zekâ kararlarının şeffaf olmaması güven sorunlarına yol açabilmektedir.Bu nedenle açıklanabilir yapay zekâ kavramı giderek önem kazanmaktadır.Devletler ve kurumlar yapay zekâ için yasal düzenlemeler geliştirmeye başlamıştır.Amaç, teknolojik ilerleme ile toplumsal faydayı dengede tutmaktır.Doğru kullanıldığında yapay zekâ insan yaşamını ciddi biçimde kolaylaştırabilir.Gelecekte yapay zekânın insanla iş birliği içinde çalışması kaçınılmaz görünmektedir.