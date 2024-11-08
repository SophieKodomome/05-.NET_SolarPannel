using efficiency;
using house;
using material;
using Npgsql;

namespace util
{
    public class DAO
    {

        public static int[] GetDaytimeConsumption(List<Device> devices, List<HourlyEfficiency> hourlyEfficiencies)
        {
            int[] hourlyConsumption = new int[24];

            for (int hour = 0; hour < hourlyConsumption.Length; hour++)
            {
                foreach (var hourlyEfficiency in hourlyEfficiencies)
                {
                    // Process only hours with positive efficiency
                    if (hourlyEfficiency.PercentileEfficiency > 0 &&
                        ((hourlyEfficiency.StartHour <= hour && hourlyEfficiency.EndHour > hour) ||  // Efficiency within the same day
                         (hourlyEfficiency.StartHour > hourlyEfficiency.EndHour &&                  // Efficiency wrapping past midnight
                          (hour >= hourlyEfficiency.StartHour || hour < hourlyEfficiency.EndHour))))
                    {
                        foreach (var device in devices)
                        {
                            bool isDeviceActive = false;

                            // Case 1: Device active within the same day
                            if (device.StartHour < device.EndHour &&
                                device.StartHour <= hour && device.EndHour > hour)
                            {
                                isDeviceActive = true;
                            }
                            // Case 2: Device active across midnight
                            else if (device.StartHour > device.EndHour &&
                                     (hour >= device.StartHour || hour < device.EndHour))
                            {
                                isDeviceActive = true;
                            }

                            if (isDeviceActive)
                            {
                                hourlyConsumption[hour] += device.Power;
                            }
                            Console.WriteLine(device.IdResidence);
                        }

                        // Scale consumption by efficiency percentage
                        //hourlyConsumption[hour] = (int)((hourlyConsumption[hour] * (hourlyEfficiency.PercentileEfficiency / 100.0)))*2;
                        hourlyConsumption[hour] = hourlyConsumption[hour];
                        Console.WriteLine($"Hour {hour}: Scaled Consumption = {hourlyConsumption[hour]}");
                    }
                }
            }

            return hourlyConsumption;
        }
        public static int GetHighestConsumption(int[] consumptionArray)
        {
            int maxConsumption = 0;

            for (int i = 0; i < consumptionArray.Length; i++)
            {
                if (consumptionArray[i] > maxConsumption)
                {
                    maxConsumption = consumptionArray[i];
                }
            }

            return maxConsumption;
        }

        public static int GetTotalNightlyConsumption(List<Device> devices, List<HourlyEfficiency> hourlyEfficiencies)
        {
            int totalNightlyConsumption = 0;

            for (int hour = 0; hour < 24; hour++)
            {
                foreach (var hourlyEfficiency in hourlyEfficiencies)
                {
                    // Process only hours with zero efficiency (night hours)
                    if (hourlyEfficiency.PercentileEfficiency == 0 &&
                        ((hourlyEfficiency.StartHour <= hour && hourlyEfficiency.EndHour > hour) ||  // Night within the same day
                         (hourlyEfficiency.StartHour > hourlyEfficiency.EndHour &&                   // Night wrapping past midnight
                          (hour >= hourlyEfficiency.StartHour || hour < hourlyEfficiency.EndHour))))
                    {
                        foreach (var device in devices)
                        {
                            bool isDeviceActiveAtNight = false;

                            // Case 1: Device active within the same day
                            if (device.StartHour < device.EndHour &&
                                device.StartHour <= hour && device.EndHour > hour)
                            {
                                isDeviceActiveAtNight = true;
                            }
                            // Case 2: Device active across midnight
                            else if (device.StartHour > device.EndHour &&
                                     (hour >= device.StartHour || hour < device.EndHour))
                            {
                                isDeviceActiveAtNight = true;
                            }

                            if (isDeviceActiveAtNight)
                            {
                                totalNightlyConsumption += device.Power;
                                Console.WriteLine($"Night Hour {hour}: Device {device.Name} adds {device.Power} to total nightly consumption.");
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Total Nightly Consumption with Zero Efficiency: " + totalNightlyConsumption);
            return totalNightlyConsumption;
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