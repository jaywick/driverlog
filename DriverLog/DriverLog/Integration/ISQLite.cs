using SQLite;

public interface ISQLite
{
    SQLiteConnection Connect(string filename);
}