using connect;
using efficiency;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using util;

namespace Solar_Panel.Pages;

public class AddEfficiencyModel : PageModel
{
    private readonly ILogger<AddEfficiencyModel> _logger;
    public List<Semester> semesters = new List<Semester>();
    public Semester SelectedSemester { get; set; }

    public AddEfficiencyModel(ILogger<AddEfficiencyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        PSQLCon pSQLCon = new PSQLCon();
        using (var connection = new NpgsqlConnection(pSQLCon.ConnectionString))
        {
            semesters = DAO.getListSemester(connection);
            List<HourlyEfficiency> hourlyEfficiencies = DAO.getListHourlyEfficiency(connection);
        }
    }

    public IActionResult OnPostTraitementSemester()
    {
        // Handle form submission for "Ajouter semestre"
        string responseSemester = Request.Form["semestre"];
        string responseStartDate = Request.Form["start_date"];
        string responseEndDate = Request.Form["end_date"];

        TempData["semestre"] = responseSemester;
        TempData["start_date"] = responseStartDate;
        TempData["end_date"] = responseEndDate;

        return RedirectToPage("/TraitementSemester");
    }

    public IActionResult OnPostTraitementEfficiency()
    {
        // Handle form submission for "Ajouter efficacité journalière"
        string responseEfficiency = Request.Form["efficacite"];
        string responseSemesterId = Request.Form["SelectedSemesterId"];
        string responseStartHour = Request.Form["start_hour"];
        string responseEndHour = Request.Form["end_hour"];

        TempData["efficacite"] = responseEfficiency;
        TempData["semesterId"] = responseSemesterId;
        TempData["start_hour"] = responseStartHour;
        TempData["end_hour"] = responseEndHour;

        return RedirectToPage("/TraitementEfficiency");
    }
}

