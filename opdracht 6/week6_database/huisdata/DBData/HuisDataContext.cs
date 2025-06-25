using Microsoft.Data.Sqlite;
using huisdata.Models;
namespace huisdata.DBData;

public class HuisDataContext
{
    public string DBDPath { get; }

    public HuisDataContext()
    {
        string location = typeof(HuisDataContext).Assembly.Location;
        string folder = new FileInfo(location).Directory.FullName;
        DBDPath = Path.Join(folder, "huisdata.db");
        if (CreateDatabaseIfNotExists())
        {
            AddVerbruik(new EnergyVerbruik()
            {
                VerbruikKwh = 100,
                Datum = DateTime.Now.AddDays(-2)
            });
        }
    }
    public void AddVerbruik(EnergyVerbruik item)
    { 
        using (SqliteConnection connection = new SqliteConnection("Data Source=" + DBDPath))
        {
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO EnergyVerbruik (VerbruikKwh, Datum)
                VALUES (@verbruikKwh, @datum);"
          ;
            command.Parameters.Add(new SqliteParameter("@verbruikKwh"));
            command.Parameters.Add("@datum", item.Datum.ToString("yyyy-MM-dd HH:mm:ss"));
            command.ExecuteNonQuery();
        }
    }

    public List<EnergyVerbruik> GetEnergyVerbruik()
    {
        List<EnergyVerbruik> items = new List<EnergyVerbruik>();
        using (SqliteConnection connection = new SqliteConnection("Data Source=" + DBDPath))
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * from EnergyVerbruik ORDER BY Datum DESC;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    EnergyVerbruik item = new EnergyVerbruik()
                    {
                        Datum = DateTime.Parse(reader["Datum"] as string),
                        VerbruikKwh = (long)reader["VerbruikKwh"],
                    };
                    items.Add(item);
                }
            }
            return items;
        }
    }

    private bool CreateDatabaseIfNotExists()
    {
        if (File.Exists(DBDPath) == false)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=" + DBDPath))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE  EnergyVerbruik (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        VerbruikKwh INTEGER NOT NULL,
                        Datum TEXT NOT NULL,
                    );
                ";
                command.ExecuteNonQuery();
            }
            return true;
        }
        return false;
    }
}
