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

    public ListResidenceModel(ILogger<ListResidenceModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        PSQLCon pSQLCon = new PSQLCon();

        using (var connection = new NpgsqlConnection(pSQLCon.ConnectionString))
        {
            // Retrieve semesters and hourly efficiencies
            List<Semester> semesters = DAO.getListSemester(connection);
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
                }
                semesters[i].addHours(selectedHours);  // Move addHours call outside inner loop
            }

            List<SolarPanel> solarPanels=DAO.GetSolarPanels(connection);
            List<Battery> batteries=DAO.GetBatteries(connection);
            // Retrieve residences and devices
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
                }
                residences[i].addDevices(selectedDevices);

                // Calculate day and night consumption
                int[] dayConsumption = DAO.GetDaytimeConsumption(residences[i].Devices, semesters[2].Hours);
                int nightConsumption = DAO.GetTotalNightlyConsumption(residences[i].Devices, semesters[2].Hours);
                int highestConsumption=DAO.GetHighestConsumption(dayConsumption);
                int highestConsumptionHour = DAO.GetHighestConsumptionHour(dayConsumption);
                // Get hourly efficiency for the highest consumption hour
                HourlyEfficiency highestConsumptionEfficiency = DAO.GetHourlyEfficiencyForHour(highestConsumptionHour, semesters[0].Hours);

                Bill bill= new Bill().AddHighestConsumption(highestConsumption)
                                    .AddDayTimeHighestConsumption(highestConsumptionHour)
                                    .AddNightTimeConsumption(nightConsumption)
                                    .AddDayTimePowerNeed(((highestConsumption*highestConsumptionEfficiency.PercentileEfficiency)/100)*2)
                                    .AddSolarPanel(solarPanels[1])
                                    .AddBattery(batteries[3])
                                    .SetTotalPrice()
                                    .SetInstallationFee()
                                    .SetMonthlyPrice();
                for (int k=0;k<dayConsumption.Length;k++)
                {
                    bill.HourlyConsumption.Add(dayConsumption[k]);
                    bill.Resoldrest.Add(dayConsumption[k]);

                }
                
                bill.SetPriceResoldRest();
                residences[i].addBill(bill);

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
        }
    }
}
