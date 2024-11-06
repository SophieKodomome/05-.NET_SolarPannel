using connect;
using efficiency;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using util;

namespace Solar_Panel.Pages;

public class ListEfficiencyModel : PageModel
{
    private readonly ILogger<ListEfficiencyModel> _logger;
    public List<Semester> semesters= new List<Semester>();

    public ListEfficiencyModel(ILogger<ListEfficiencyModel> logger)
    {
        _logger = logger;
        
    }

    public void OnGet()
    {
        PSQLCon pSQLCon = new PSQLCon();

        using (var connection = new NpgsqlConnection(pSQLCon.ConnectionString))
        {
            semesters= DAO.getListSemester(connection);
            List<HourlyEfficiency> hourlyEfficiencies=DAO.getListHourlyEfficiency(connection);

            for (int i = 0; i < semesters.Count; i++)
            {
                for (int j = 0; j < hourlyEfficiencies.Count; j++)
                {
                    if (semesters[i].Id==hourlyEfficiencies[j].IdSemester)
                    {
                        semesters[i].addHours(hourlyEfficiencies);
                    }
                }
            }
        }
    }
}
