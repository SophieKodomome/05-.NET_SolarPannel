using System.Collections.Generic;
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
                            int id=reader.GetInt32(0);
                            string name=reader.GetString(1);
                            DateTime startDate=reader.GetDateTime(2);
                            DateTime endDate=reader.GetDateTime(3);

                            Semester semester =  new Semester()
                                .addId(id)
                                .addName(name)
                                .addStartDate(startDate)
                                .addEndDate(endDate);

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

        public static List<HourlyEfficiency> getListHourlyEfficiency(NpgsqlConnection connection)
        {
            List<HourlyEfficiency> hourlies = new List<HourlyEfficiency>();

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
                            int id=reader.GetInt32(0);
                            string name=reader.GetString(1);
                            int startHour=reader.GetInt32(2);
                            int endHour=reader.GetInt32(3);

                            HourlyEfficiency hourlyEfficiency =  new HourlyEfficiency()
                                .addIdSemester(id)
                                .addName(name)
                                .addStartHour(startHour)
                                .addEndHour(endHour);

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
    }
}