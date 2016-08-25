using System.Threading.Tasks;

public interface INotifier
{
    Task<INotification> Notify(string title, string message);
}

public interface INotification
{
    int ID { get; set; }
    void Dismiss();
}

