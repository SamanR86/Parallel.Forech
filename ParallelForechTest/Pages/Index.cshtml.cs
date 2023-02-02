using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ParallelForechTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public string Message { get; set; }
        public string sumDelay { get; set; }
        public string RunTime { get; set; }
        public async Task OnGet()
        {
            List<int> items = new List<int>
            { 1, 2, 3, 4, 5};

            int result=0;
            int total=0;
            var startTime = DateTime.Now;


            //foreach (var item in items)
            //{
            //    result = await delayMakerWithRandomSeconds();
            //    total = result + total;
            //    Message = Message + item;
            //}

            //Parallel.ForEach(items, async (item) =>
            //{
            //    result = await delayMakerWithRandomSeconds();
            //    total = result + total;
            //    Message = Message + item;
            //});

            await Parallel.ForEachAsync(items, async (item, token) =>
            {
                result = await delayMakerWithRandomSeconds();
                total = result + total;
                Message = Message + item;
            });

            var diff = DateTime.Now - startTime;
            RunTime = diff.Seconds.ToString();
            sumDelay = (total/1000).ToString();
        }   

        public async Task<int> delayMakerWithRandomSeconds()
        {
            Random rand = new Random();
            var result = rand.Next(10000);
            Thread.Sleep(result);
            return result;

        }

    }
}