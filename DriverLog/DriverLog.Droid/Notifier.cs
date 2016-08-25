using Android.App;
using Android.Content;
using DriverLog.Droid;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;

[assembly: Dependency(typeof(AndroidNotifier))]
public class AndroidNotifier : INotifier
{
    public Task<INotification> Notify(string title, string message)
    {
        return Task.Run(() =>
        {
            var notification = new Notification.Builder(StaticContext.Current)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.ic_directions_car_black_24dp)
                .SetOngoing(true)
                .Build();

            var notificationManager = (NotificationManager)StaticContext.Current.GetSystemService(Context.NotificationService);

            const int id = 0;
            notificationManager.Notify(id, notification);

            return (INotification)new AndroidNotification(id);
        });
    }
}

public class AndroidNotification : INotification
{
    public int ID { get; set; }

    public AndroidNotification(int id)
    {
        ID = id;
    }

    public void Dismiss()
    {
        var notificationManager = (NotificationManager)StaticContext.Current.GetSystemService(Context.NotificationService);
        notificationManager.Cancel(ID);
    }
}