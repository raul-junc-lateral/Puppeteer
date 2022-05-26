using PuppeteerSharp;
using System;

namespace PuppeteerPlayground
{
    internal class Program
    {
        private static Browser ChromeDriver { get; set; }
        private static Page page { get; set; }
        private static string Url = @"file://D:/Personal Repos(raul-junc-lateral)/Puppeteer/PuppeteerPlayground/default.htm";

        static async Task Main(string[] args)
        {
            try
            {
                await DoStuff();
            }
            catch(Exception ex)
            {

            }
        }

        static async Task DoStuff()
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var launchOptions = new LaunchOptions
            {
                Headless = false,
                DefaultViewport = null
            };
            launchOptions.Args = new[] { "--disable-web-security", "--disable-features=IsolateOrigins,site-per-process" };
            ChromeDriver = await Puppeteer.LaunchAsync(launchOptions);

            page = await ChromeDriver.NewPageAsync();
            await page.GoToAsync(Url, new NavigationOptions { WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Networkidle0 } });
            var selectorIFrame = "#twitter_iframe";
            var frameElement1 = await page.WaitForSelectorAsync(selectorIFrame);
            var frame1 = await frameElement1.ContentFrameAsync();
            var frameContent1 = await frame1.GetContentAsync();
        }
    }
}