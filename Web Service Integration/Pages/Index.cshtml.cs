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
        public void OnGet()
        {
            BestPerformer = "GOOGLE";
            BestGrowth = 100;
        }
    }
}
