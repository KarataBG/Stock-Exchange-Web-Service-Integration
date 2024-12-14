using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_Service_Integration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public string BestPerformer { get; private set; }
        public decimal BestGrowth { get; private set; }

        public Dictionary<string, decimal> GrowthData { get; private set; } = new Dictionary<string, decimal>();

        public void OnGet()
        {
            BestPerformer = "GOOGLE";
            BestGrowth = 99999999;

            GrowthData.Add("1", 333);
            GrowthData.Add("2", 25235);
            GrowthData.Add("3", 7457487);
            GrowthData.Add("4", 3573);
            GrowthData.Add("5", 1230979);

            Page();
        }
    }
}
