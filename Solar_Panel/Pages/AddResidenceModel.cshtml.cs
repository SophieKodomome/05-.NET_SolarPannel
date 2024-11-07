using Microsoft.AspNetCore.Mvc;
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
    public IActionResult OnPost()
    {
        string adresse = Request.Form["residence"];

        List<string> devices = Request.Form["material"].ToList();
        List<string> powers = Request.Form["power"].ToList();
        List<string> startHours = Request.Form["start_hour"].ToList();
        List<string> endHours = Request.Form["end_hour"].ToList();

        string separator = "-";

        string concatmaterials = string.Join(separator, devices);
        string concatpowers = string.Join(separator, powers);
        string concatstartHours = string.Join(separator, startHours); 
        string concatendHours = string.Join(separator, endHours);


        TempData["adress"] = adresse;
        TempData["device"] = concatmaterials;
        TempData["power"] = concatpowers;
        TempData["start_hour"] = concatstartHours;
        TempData["end_hour"] = concatendHours;

        return RedirectToPage("/TraitementResidence");
    }
}