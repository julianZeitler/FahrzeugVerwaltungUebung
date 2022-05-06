using MySqlConnector;

namespace FahrzeugDatenbank
{
    public class FahrzeugRepository
    {
        private string _connectionString;

        public FahrzeugRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public List<FahrzeugDTO> HoleAlleFahrzeuge()
        {
            using var datenbankVerbindung = new MySqlConnection(_connectionString);
            datenbankVerbindung.Open();

            const string query = "SELECT id, fahrzeug_name, fahrzeug_typ FROM fahrzeuge";
            using var kommando = new MySqlCommand(query, datenbankVerbindung);
            var reader = kommando.ExecuteReader();

            List<FahrzeugDTO> fahrzeuge = new();

            while(reader.Read())
            {
                var fahrzeug = new FahrzeugDTO();
                fahrzeug.Id = reader.GetInt32(0);
                fahrzeug.Name = reader.GetString(1);
                fahrzeug.Typ = reader.GetString(2);

                fahrzeuge.Add(fahrzeug);
            }

            return fahrzeuge;
        }

        public void FuegeFahrzeugEin(string name, string type)
        {
            using var datenbankVerbindung = new MySqlConnection(_connectionString);
            datenbankVerbindung.Open();
            const string query = $"INSERT INTO fahrzeuge (fahrzeug_name, fahrzeug_typ) VALUES (@name,@type)";
            using var kommando = new MySqlCommand(query, datenbankVerbindung);
            kommando.Parameters.AddWithValue("@name", name);
            kommando.Parameters.AddWithValue("@type", type);
            kommando.ExecuteNonQuery();
        }

    }

    public class FahrzeugDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Typ { get; set; }
    }
}