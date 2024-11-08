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
    public List<Residence> residences = new List<Residence>();
    public List<Semester> semesters = new List<Semester>();

    public ListResidenceModel(ILogger<ListResidenceModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        PSQLCon pSQLCon = new PSQLCon();

        using (var connection = new NpgsqlConnection(pSQLCon.ConnectionString))
        {
            residences = DAO.getListResidence(connection);
            List<Device> devices = DAO.getListDevice(connection);

            for (int i = 0; i < residences.Count; i++)
            {
                List<Device> selectedDevices = new List<Device>();

                for (int j = 0; j < devices.Count; j++)
                {
                    if (residences[i].Id == devices[j].IdResidence)
                    {
                        selectedDevices.Add(devices[j]);
                    }
                    residences[i].addDevices(selectedDevices);
                }
            }

            semesters = DAO.getListSemester(connection);
            List<HourlyEfficiency> hourlyEfficiencies = DAO.getListHourlyEfficiency(connection);

            for (int i = 0; i < semesters.Count; i++)
            {
                List<HourlyEfficiency> selectedHours = new List<HourlyEfficiency>();
                for (int j = 0; j < hourlyEfficiencies.Count; j++)
                {
                    if (semesters[i].Id == hourlyEfficiencies[j].IdSemester)
                    {
                        selectedHours.Add(hourlyEfficiencies[j]);
                    }
                    semesters[i].addHours(selectedHours);
                }
            }
        }

        int[] dayConsumption = DAO.GetDaytimeConsumption(residences[0].Devices, semesters[0].Hours);
        int nightConsumption = DAO.GetTotalNightlyConsumption(residences[0].Devices, semesters[0].Hours);

        // Get the hour with the highest consumption
        int highestConsumptionHour = GetHighestConsumptionHour(dayConsumption);

        // Find the corresponding hourly efficiency
        HourlyEfficiency highestConsumptionEfficiency = GetHourlyEfficiencyForHour(highestConsumptionHour, semesters[0].Hours);

        Console.WriteLine($"The hour with the highest consumption is: {highestConsumptionHour} with {dayConsumption[highestConsumptionHour]} units.");
        if (highestConsumptionEfficiency != null)
        {
            Console.WriteLine($"The efficiency for this hour is {highestConsumptionEfficiency.PercentileEfficiency}% " +
                              $"from {highestConsumptionEfficiency.StartHour} to {highestConsumptionEfficiency.EndHour}.");
        }
        else
        {
            Console.WriteLine("No efficiency data found for this hour.");
        }
    }

    // Existing function to get the hour with the highest consumption
    public static int GetHighestConsumptionHour(int[] dayConsumption)
    {
        int maxConsumption = 0;
        int maxHour = 0;

        for (int hour = 0; hour < dayConsumption.Length; hour++)
        {
            if (dayConsumption[hour] > maxConsumption)
            {
                maxConsumption = dayConsumption[hour];
                maxHour = hour;
            }
        }

        return maxHour;
    }

    // New function to find the hourly efficiency for a specific hour
    public static HourlyEfficiency GetHourlyEfficiencyForHour(int hour, List<HourlyEfficiency> hourlyEfficiencies)
    {
        foreach (var efficiency in hourlyEfficiencies)
        {
            if (efficiency.StartHour <= efficiency.EndHour && hour >= efficiency.StartHour && hour < efficiency.EndHour)
            {
                return efficiency;
            }
            else if (efficiency.StartHour > efficiency.EndHour && (hour >= efficiency.StartHour || hour < efficiency.EndHour))
            {
                return efficiency;
            }
        }
        return null;
    }
}
