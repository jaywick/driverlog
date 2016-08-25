using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteAndroid))]
public class SQLiteAndroid : ISQLite
{
    public SQLiteAndroid()
    {
    }

    public SQLiteConnection Connect(string filename)
    {
        var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        return new SQLiteConnection(Path.Combine(folder, filename));
    }
}