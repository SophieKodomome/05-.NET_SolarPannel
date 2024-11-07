using connect;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using util;

namespace Solar_Panel.Pages;

public class TraitementEfficiencyModel : PageModel
{
    private readonly ILogger<TraitementEfficiencyModel> _logger;

    public TraitementEfficiencyModel(ILogger<TraitementEfficiencyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        PSQLCon pSQLCon = new PSQLCon();

        using (var connection = new NpgsqlConnection(pSQLCon.ConnectionString))
        {
            if (TempData["efficacite"] != null)
            {
                string efficiency = TempData["efficacite"].ToString();
                string semesterId = TempData["semesterId"].ToString();
                string start_hour = TempData["start_hour"].ToString();
                string end_hour = TempData["end_hour"].ToString();

                DAO.insertEfficiency(efficiency, semesterId, start_hour, end_hour, connection);
            }
        }

    }
}