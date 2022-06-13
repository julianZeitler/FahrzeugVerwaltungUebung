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

        private void ErstelleDatenbank(string connectionString)
        {
            using var datenbankVerbindung = new MySqlConnection(connectionString);
            datenbankVerbindung.Open();

            const string query = @"CREATE DATABASE IF NOT EXISTS FahrzeugDB;
            GRANT ALL PRIVILEGES ON FahrzeugDB.* TO 'root'@'%' IDENTIFIED BY 'admin';
            FLUSH PRIVILEGES;
            USE FahrzeugDB;
            DROP TABLE IF EXISTS fahrzeuge;
            CREATE TABLE fahrzeuge(
            id INT NOT NULL AUTO_INCREMENT,
            fahrzeug_name VARCHAR(100) NOT NULL,
            fahrzeug_typ VARCHAR(100) NOT NULL,
            PRIMARY KEY(id)
            ); ";

            using var kommando = new MySqlCommand(query, datenbankVerbindung);
            kommando.ExecuteNonQuery();
        }

        public List<FahrzeugDTO> HoleAlleFahrzeuge()
        {
            try
            {
                using var datenbankVerbindung = new MySqlConnection(_connectionString);
                datenbankVerbindung.Open();

                const string query = "SELECT id, fahrzeug_name, fahrzeug_typ FROM fahrzeuge";
                using var kommando = new MySqlCommand(query, datenbankVerbindung);
                var reader = kommando.ExecuteReader();

                List<FahrzeugDTO> fahrzeuge = new();

                while (reader.Read())
                {
                    var fahrzeug = new FahrzeugDTO();
                    fahrzeug.Id = reader.GetInt32(0);
                    fahrzeug.Name = reader.GetString(1);
                    fahrzeug.Typ = reader.GetString(2);

                    fahrzeuge.Add(fahrzeug);
                }

                return fahrzeuge;
            } catch (Exception ex)
            {
                ErstelleDatenbank("Server=localhost;User ID=root;Password=admin;");
                using var datenbankVerbindung = new MySqlConnection(_connectionString);
                datenbankVerbindung.Open();

                const string query = "SELECT id, fahrzeug_name, fahrzeug_typ FROM fahrzeuge";
                using var kommando = new MySqlCommand(query, datenbankVerbindung);
                var reader = kommando.ExecuteReader();

                List<FahrzeugDTO> fahrzeuge = new();

                while (reader.Read())
                {
                    var fahrzeug = new FahrzeugDTO();
                    fahrzeug.Id = reader.GetInt32(0);
                    fahrzeug.Name = reader.GetString(1);
                    fahrzeug.Typ = reader.GetString(2);

                    fahrzeuge.Add(fahrzeug);
                }

                return fahrzeuge;
            }
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

        public void LoescheFahrzeug(int id)
        {
            using var datenbankVerbindung = new MySqlConnection(_connectionString);
            datenbankVerbindung.Open();
            const string query = $"DELETE FROM fahrzeuge WHERE id=@id";
            using var kommando = new MySqlCommand(query, datenbankVerbindung);
            kommando.Parameters.AddWithValue("@id", id);
            kommando.ExecuteNonQuery();
        }

        public FahrzeugDTO holeFahrzeug(int id)
        {
            using var datenbankVerbindung = new MySqlConnection(_connectionString);
            datenbankVerbindung.Open();

            const string query = "SELECT fahrzeug_name, fahrzeug_typ FROM fahrzeuge WHERE id=@id";
            using var kommando = new MySqlCommand(query, datenbankVerbindung);
            kommando.Parameters.AddWithValue("@id", id);
            var reader = kommando.ExecuteReader();
            var fahrzeug = new FahrzeugDTO();

            reader.Read();
            fahrzeug.Id = id;
            fahrzeug.Name = reader.GetString(0);
            fahrzeug.Typ = reader.GetString(1);
            
            return fahrzeug;
        }

        public void AktualisiereFahrzeug(int id, string name, string typ)
        {
            using var datenbankVerbindung = new MySqlConnection(_connectionString);
            datenbankVerbindung.Open();

            const string query = "UPDATE fahrzeuge SET fahrzeug_name=@name, fahrzeug_typ=@typ WHERE id=@id";
            using var kommando = new MySqlCommand(query, datenbankVerbindung);
            kommando.Parameters.AddWithValue("@id", id);
            kommando.Parameters.AddWithValue("@name", name);
            kommando.Parameters.AddWithValue("@typ", typ);
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