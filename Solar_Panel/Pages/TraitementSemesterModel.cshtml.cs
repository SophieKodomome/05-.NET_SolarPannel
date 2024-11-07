using connect;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using util;

namespace Solar_Panel.Pages;

public class TraitementSemesterModel : PageModel
{
    private readonly ILogger<TraitementSemesterModel> _logger;

    public TraitementSemesterModel(ILogger<TraitementSemesterModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        PSQLCon pSQLCon = new PSQLCon();

        using (var connection = new NpgsqlConnection(pSQLCon.ConnectionString))
        {
        if(TempData["semestre"] != null)
        {
            
            string semestre = TempData["semestre"].ToString();
            string startDate = TempData["start_date"].ToString();
            string endDate = TempData["end_date"].ToString();

            DAO.insertSemester(semestre,startDate,endDate,connection);
        }
        }

    }
}