using Google.Cloud.Vision.V1;

class Program
    {
    static void Main(string[] args)
    {
        Console.WriteLine("Resim Yolunu Giriniz:");
        Console.WriteLine();
        string imagePath = Console.ReadLine();

        string credentialPath = @"C:\Users\LENOVO26AGUS022\Desktop\my-project-ai-485704-ba63b9ed408d.json";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

        try
        {
            var client=ImageAnnotatorClient.Create();
            var image = Image.FromFile(imagePath);
            var response = client.DetectText(image);
            Console.WriteLine("Resimdeki Metin:");
            Console.WriteLine();
            foreach (var annotation in response)
            {
               if (!string.IsNullOrEmpty(annotation.Description))
                {
                    Console.WriteLine(annotation.Description);
                }
            }

        }
        catch (Exception ex)
        {

           Console.WriteLine($"Hata Oluştu  {ex.Message}");
        }
    }
}