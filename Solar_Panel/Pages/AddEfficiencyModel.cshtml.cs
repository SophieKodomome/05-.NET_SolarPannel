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
    public Semester SelectedSemester{get;set;}

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

    public IActionResult OnPost()
    {
        string[] arraySymptomLabel = Request.Form["symptom"];
        string[] arraySymptomValues = Request.Form["severity"];
        string[] arraySymptomId = Request.Form["symptomId"];

        //Console.WriteLine("size "+arraySymptomLabel.Length);
        string separator = "-";

        string concatSymptomValues = string.Join(separator, arraySymptomValues);
        string concatSymptomlabel = string.Join(separator, arraySymptomLabel);
        string concatSymptomId = string.Join(separator, arraySymptomId);

        TempData["symptom"] = concatSymptomlabel;
        TempData["symptomId"] = concatSymptomId;
        TempData["severity"] = concatSymptomValues;
        return RedirectToPage("/Traitement");
    }
}
