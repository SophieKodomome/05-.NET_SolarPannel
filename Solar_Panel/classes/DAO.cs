using efficiency;
using house;
using material;
using Npgsql;

namespace util
{
    public class DAO
    {

        public static void getConsumption(List<Device> devices, List<HourlyEfficiency> hourlyEfficiencies)
        {
            int[] hourlyConsumption = new int[24];
            int totalNightlyConsumption = 0; // Variable to accumulate nightly consumption with zero efficiency

            for (int hour = 0; hour < hourlyConsumption.Length; hour++)
            {
                foreach (var hourlyEfficiency in hourlyEfficiencies)
                {
                    // Check if current hour falls within the efficiency period (same day or wrapping past midnight)
                    bool isEfficiencyActive = hourlyEfficiency.PercentileEfficiency > 0 &&
                        ((hourlyEfficiency.StartHour <= hour && hourlyEfficiency.EndHour > hour) ||
                         (hourlyEfficiency.StartHour > hourlyEfficiency.EndHour &&
                          (hour >= hourlyEfficiency.StartHour || hour < hourlyEfficiency.EndHour)));

                    // Calculate consumption based on efficiency
                    if (isEfficiencyActive)
                    {
                        foreach (var device in devices)
                        {
                            bool isDeviceActive = false;

                            // Device active within the same day
                            if (device.StartHour < device.EndHour &&
                                device.StartHour <= hour && device.EndHour > hour)
                            {
                                isDeviceActive = true;
                            }
                            // Device active across midnight
                            else if (device.StartHour > device.EndHour &&
                                     (hour >= device.StartHour || hour < device.EndHour))
                            {
                                isDeviceActive = true;
                            }

                            if (isDeviceActive)
                            {
                                hourlyConsumption[hour] += device.Power;
                            }
                        }

                        // Apply efficiency scaling for the hour after summing all devices
                        hourlyConsumption[hour] = (int)(hourlyConsumption[hour] * (hourlyEfficiency.PercentileEfficiency / 100.0));
                        Console.WriteLine("Consumption: " + hourlyConsumption[hour]);
                    }
                    else
                    {
                        // Calculate totalNightlyConsumption when efficiency is zero and within defined "night" hours
                        if (hourlyEfficiency.PercentileEfficiency == 0)
                        {
                            bool isNightHour = (hourlyEfficiency.StartHour <= hour && hourlyEfficiency.EndHour > hour) ||
                                               (hourlyEfficiency.StartHour > hourlyEfficiency.EndHour &&
                                               (hour >= hourlyEfficiency.StartHour || hour < hourlyEfficiency.EndHour));

                            if (isNightHour)
                            {
                                foreach (var device in devices)
                                {
                                    bool isDeviceActiveAtNight = false;

                                    // Device active within the same day
                                    if (device.StartHour < device.EndHour &&
                                        device.StartHour <= hour && device.EndHour > hour)
                                    {
                                        isDeviceActiveAtNight = true;
                                    }
                                    // Device active across midnight
                                    else if (device.StartHour > device.EndHour &&
                                             (hour >= device.StartHour || hour < device.EndHour))
                                    {
                                        isDeviceActiveAtNight = true;
                                    }

                                    if (isDeviceActiveAtNight)
                                    {
                                        totalNightlyConsumption += device.Power;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Total Nightly Consumption with Zero Efficiency: " + totalNightlyConsumption);
        }
        public static List<Semester> getListSemester(NpgsqlConnection connection)
        {
            List<Semester> semesters = new List<Semester>();

            try
            {
                connection.Open();

                string selectCommand = "SELECT * FROM Semester";

                using (var command = new NpgsqlCommand(selectCommand, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime startDate = reader.GetDateTime(2);
                            DateTime endDate = reader.GetDateTime(3);

                            Semester semester = new Semester()
                                .addId(id)
                                .addName(name)
                                .addStartDate(ConvertToDateOnly(startDate))
                                .addEndDate(ConvertToDateOnly(endDate));

                            semesters.Add(semester);
                        }
                    }
                }
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return semesters;
        }

        public static List<Residence> getListResidence(NpgsqlConnection connection)
        {
            List<Residence> semesters = new List<Residence>();

            try
            {
                connection.Open();

                string selectCommand = "SELECT * FROM residence";

                using (var command = new NpgsqlCommand(selectCommand, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);

                            Residence semester = new Residence()
                                .addId(id)
                                .addName(name);

                            semesters.Add(semester);
                        }
                    }
                }
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return semesters;
        }

        public static List<Device> getListDevice(NpgsqlConnection connection)
        {
            List<Device> devices = new List<Device>();

            try
            {
                connection.Open();

                string selectCommand = "SELECT * FROM residence_device";

                using (var command = new NpgsqlCommand(selectCommand, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int idResidence = reader.GetInt32(1);
                            string name = reader.GetString(2);
                            int consumption = reader.GetInt32(3);
                            int startHour = reader.GetInt32(4);
                            int startDate = reader.GetInt32(5);

                            Device device = new Device()
                                .addId(id)
                                .addIdResidence(idResidence)
                                .addName(name)
                                .addPower(consumption)
                                .addStartHour(startHour)
                                .addEndHour(startDate);

                            devices.Add(device);
                        }
                    }
                }
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return devices;
        }

        public static DateOnly ConvertToDateOnly(DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }

        public static List<HourlyEfficiency> getListHourlyEfficiency(NpgsqlConnection connection)
        {
            List<HourlyEfficiency> hourlies = new List<HourlyEfficiency>();

            try
            {
                connection.Open();

                string selectCommand = "SELECT * FROM hourly_efficiency";

                using (var command = new NpgsqlCommand(selectCommand, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int IdSemester = reader.GetInt32(1);
                            int startHour = reader.GetInt32(2);
                            int endHour = reader.GetInt32(3);
                            int efficiency = reader.GetInt32(4);

                            HourlyEfficiency hourlyEfficiency = new HourlyEfficiency()
                                .addId(id)
                                .addIdSemester(IdSemester)
                                .addStartHour(startHour)
                                .addEndHour(endHour)
                                .addPercentileEfficiency(efficiency);

                            hourlies.Add(hourlyEfficiency);
                        }
                    }
                }
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return hourlies;
        }

        public static Residence getResidencebyName(NpgsqlConnection connection, string address)
        {
            Residence residence = new Residence();

            try
            {
                connection.Open();

                string selectCommand = "SELECT * FROM residence WHERE address=@address";

                using (var command = new NpgsqlCommand(selectCommand, connection))
                {
                    command.Parameters.AddWithValue("address", address);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            residence.addId(id).addName(name);
                        }
                    }
                }
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return residence;
        }
        public static void insertSemester(string name, string startDate, string endDate, NpgsqlConnection connection)
        {

            using (var connect = new NpgsqlConnection(connection.ConnectionString))
            {
                try
                {
                    connect.Open();

                    // SQL command to insert a new semester
                    string insertCommand = "INSERT INTO Semester (name, start_date, end_date) VALUES (@name,@startDate,@endDate)";

                    using (var command = new NpgsqlCommand(insertCommand, connect))
                    {
                        // Set parameter values to prevent SQL injection
                        command.Parameters.AddWithValue("name", name);
                        command.Parameters.AddWithValue("startDate", DateTime.Parse(startDate));
                        command.Parameters.AddWithValue("endDate", DateTime.Parse(endDate));

                        // Execute the command
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting semester: " + ex.Message);
                    // Handle exceptions or logging as needed
                }
                finally
                {
                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }

        public static void insertEfficiency(string efficiency, string semesterId, string startHour, string endHour, NpgsqlConnection connection)
        {

            using (var connect = new NpgsqlConnection(connection.ConnectionString))
            {
                try
                {
                    connect.Open();

                    // SQL command to insert a new semester
                    string insertCommand = "INSERT INTO hourly_efficiency (id_semester, start_hour, end_hour,percentile_efficiency) VALUES (" + semesterId + "," + startHour + "," + endHour + "," + efficiency + ")";

                    using (var command = new NpgsqlCommand(insertCommand, connect))
                    {
                        // Execute the command
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting efficiency: " + ex.Message);
                    // Handle exceptions or logging as needed
                }
                finally
                {
                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }

        public static void insertDevice(int idResidence, string device, string consumption, string startHour, string endHour, NpgsqlConnection connection)
        {

            using (var connect = new NpgsqlConnection(connection.ConnectionString))
            {
                try
                {
                    connect.Open();

                    // SQL command to insert a new semester
                    string insertCommand = "INSERT INTO residence_device (id_residence,device,consumption, start_hour, end_hour) VALUES (@idResidence,@device,@consumption,@startHour,@endHour)";

                    using (var command = new NpgsqlCommand(insertCommand, connect))
                    {
                        // Set parameter values to prevent SQL injection
                        command.Parameters.AddWithValue("idResidence", idResidence);
                        command.Parameters.AddWithValue("device", device);
                        command.Parameters.AddWithValue("startHour", int.Parse(startHour));
                        command.Parameters.AddWithValue("endHour", int.Parse(endHour));
                        command.Parameters.AddWithValue("consumption", int.Parse(consumption));

                        // Execute the command
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting device: " + ex.Message);
                }
                finally
                {
                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }

        public static void insertResidence(string adresse, NpgsqlConnection connection)
        {
            try
            {
                connection.Open();
                string insertResidence = "INSERT INTO residence (address) VALUES (@adresse)";

                using (var command = new NpgsqlCommand(insertResidence, connection))
                {
                    command.Parameters.AddWithValue("adresse", adresse);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting Residence: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}