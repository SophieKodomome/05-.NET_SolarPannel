using connect;
using house;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using util;

namespace Solar_Panel.Pages;

public class TraitementResidenceModel : PageModel
{
    private readonly ILogger<TraitementResidenceModel> _logger;

    public TraitementResidenceModel(ILogger<TraitementResidenceModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    
        Console.WriteLine("Residence Model");
        string adress= TempData["adress"].ToString();

        string device= TempData["device"].ToString();
        string power= TempData["power"].ToString();
        string startHour= TempData["start_hour"].ToString();
        string endHour= TempData["end_hour"].ToString();

        char[] delimiter = new char[]{'-'};
        string[] arrayDevices=device.Split(delimiter);
        string[] arrayPowers=power.Split(delimiter);
        string[] arrayStartHour=startHour.Split(delimiter);
        string[] arrayEndHour=endHour.Split(delimiter);

        Residence currentResidence= new Residence();

        PSQLCon pSQLCon = new PSQLCon();

        using (var connection = new NpgsqlConnection(pSQLCon.ConnectionString))
        {
            DAO.insertResidence(adress,connection);

            currentResidence = DAO.getResidencebyName(connection,adress);

            for (int i = 0; i < arrayDevices.Length; i++)
            {
                DAO.insertDevice(
                    currentResidence.Id,
                    arrayDevices[i],
                    arrayPowers[i],
                    arrayStartHour[i],
                    arrayEndHour[i],
                    new NpgsqlConnection(new PSQLCon().ConnectionString));
            }
        }

        

    }
}