using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace puppeteer_core
{
  class Program
  {
    static void Main(string[] args)
    {
      MainAsync().GetAwaiter().GetResult();
    }

    static async Task MainAsync()
    {
      var options = new LaunchOptions
      {
        ExecutablePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
        Headless = true
      };
      //await Downloader.CreateDefault().DownloadRevisionAsync(Downloader.DefaultRevision);
      using (var browser = await Puppeteer.LaunchAsync(options, Downloader.DefaultRevision))
      {
        using (var page = await browser.NewPageAsync())
        {
          await page.GoToAsync("http://www.baidu.com");
          await page.PdfAsync("D:/1.pdf", new PdfOptions()
          {
            PrintBackground = true,
            Landscape = true,
            Format = PaperFormat.A4
          });
        }
      }
    }
  }
}
