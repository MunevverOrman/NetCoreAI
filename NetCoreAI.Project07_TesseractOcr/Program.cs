using Tesseract;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Karaketr okuması yapılacak resim yolunu giriniz: ");
        string input = Console.ReadLine();

     
        string imagePath = CleanPath(input);

        if (!File.Exists(imagePath) && File.Exists(imagePath + ".jpeg"))
        {
            imagePath = imagePath + ".jpeg";
        }

        Console.WriteLine($"İşlenecek dosya yolu: [{imagePath}]");

        string tessDataPath = @"C:\tessdata";

        try
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Hata: Dosya hala bulunamadı! Lütfen dosya adının '1.jpeg.jpeg' olup olmadığını kontrol edin.");
            }
            else
            {
                using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            Console.WriteLine("\n--- OKUNAN METİN ---");
                            Console.WriteLine(page.GetText());
                            Console.WriteLine("--------------------\n");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        }

        Console.WriteLine("Çıkmak için Enter'a basın...");
        Console.ReadLine();
    }


    static string CleanPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) return "";


        string temp = path.Trim().Replace("\"", "");

        
        int index = temp.IndexOf(@"C:\", StringComparison.OrdinalIgnoreCase);
        if (index != -1) temp = temp.Substring(index);

        return new string(temp.Where(c => !char.IsControl(c) && (int)c < 128).ToArray()).Trim();
    }
}