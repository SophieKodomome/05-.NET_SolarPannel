using connect;
using efficiency;
using house;
using material;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using util;

namespace Solar_Panel.Pages;

public class ListResidenceModel : PageModel
{
    private readonly ILogger<ListResidenceModel> _logger;
    public List<Residence> residences= new List<Residence>();
    
    public List<Semester> semesters= new List<Semester>();

    public ListResidenceModel(ILogger<ListResidenceModel> logger)
    {
        _logger = logger;
        
    }

    public void OnGet()
    {
        PSQLCon pSQLCon = new PSQLCon();

        using (var connection = new NpgsqlConnection(pSQLCon.ConnectionString))
        {
            residences= DAO.getListResidence(connection);
            List<Device> devices=DAO.getListDevice(connection);

            for (int i = 0; i < residences.Count; i++)
            {
                List<Device> selectedDevices = new List<Device>();

                for (int j = 0; j < devices.Count; j++)
                {
                    if (residences[i].Id==devices[j].IdResidence)
                    {
                        selectedDevices.Add(devices[j]);
                    }
                    residences[i].addDevices(selectedDevices);
                }
            }

            semesters= DAO.getListSemester(connection);
            List<HourlyEfficiency> hourlyEfficiencies=DAO.getListHourlyEfficiency(connection);

            for (int i = 0; i < semesters.Count; i++)
            {
                List<HourlyEfficiency> selectedHours=new List<HourlyEfficiency>();
                for (int j = 0; j < hourlyEfficiencies.Count; j++)
                {
                    if (semesters[i].Id==hourlyEfficiencies[j].IdSemester)
                    {
                        selectedHours.Add(hourlyEfficiencies[j]);
                    }
                    semesters[i].addHours(selectedHours);
                }
            }
        }

        DAO.getConsumption(residences[0].Devices,semesters[0].Hours);


    }
}
