using System;
using System.Collections.Generic;
using connect;
using efficiency;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Npgsql;
using util;

namespace Solar_Panel.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        
    }

    public void OnGet()
    {
        PSQLCon pSQLCon = new PSQLCon();

        using (var connection = new NpgsqlConnection(pSQLCon.ConnectionString))
        {
            List<Semester> semesters= DAO.getListSemester(connection);
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
