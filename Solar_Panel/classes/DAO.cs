using efficiency;
using house;
using Npgsql;

namespace util
{
    public class DAO
    {
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
                    string insertCommand = "INSERT INTO Efficiency (id_semester, start_hour, end_hour,percentile_efficiency) VALUES (@semesterId,@startHour,@endHour,@efficiency)";

                    using (var command = new NpgsqlCommand(insertCommand, connect))
                    {
                        // Set parameter values to prevent SQL injection
                        command.Parameters.AddWithValue("semesterId", semesterId);
                        command.Parameters.AddWithValue("startHour", startHour);
                        command.Parameters.AddWithValue("endHour", endHour);
                        command.Parameters.AddWithValue("efficiency", efficiency);

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