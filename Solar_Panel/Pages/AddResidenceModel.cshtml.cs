using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Solar_Panel.Pages;

public class AddResidenceModel : PageModel
{
    private readonly ILogger<AddResidenceModel> _logger;

    public AddResidenceModel(ILogger<AddResidenceModel> logger)
    {
        _logger = logger;
        
    }

    public void OnGet()
    {
    }
}
