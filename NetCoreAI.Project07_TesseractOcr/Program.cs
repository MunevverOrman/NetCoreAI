using Tesseract;

class Program
{

    static void Main(string[] args)
{
    Console.Write("Karaketr okuması yapılacak resim yolunu giriniz: ");
    string imagePath = Console.ReadLine();
	Console.WriteLine();

    string tessDataPath = @"C:\tessdata"; // Tesseract veri dosyalarının yolu

	try
	{
		using (var engine=new TesseractEngine(tessDataPath,"eng",EngineMode.Default))
		{
			using (var img=Pix.LoadFromFile(imagePath))
			{
				using (var page=engine.Process(img))
				{
					string text=page.GetText();
					Console.WriteLine("Resimden Okunan Metin:");
					Console.WriteLine(text);
				}
			}
			}
    
	}
	catch (Exception ex)
	{

		Console.WriteLine($"Bir Hata Oluştu. Lütfen Resim Yolunu ve Tesseract Veri Dosyalarının Yolunu Kontrol Ediniz.{ex.Message}");
	}
		Console.ReadLine();

    }
}