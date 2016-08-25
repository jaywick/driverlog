using System.Threading.Tasks;

namespace DriverLog.Pages
{
    /// <summary>
    /// Interface to expose an Awaiter which are used to <code>await</code> the result of a page. Must be implemented on a Page.
    /// </summary>
    public interface IResultPage<TResult>
    {
        /// <summary>
        /// Awaiters are used to <code>await</code> the result of a IResultPage
        /// </summary>
        TaskCompletionSource<TResult> Awaiter { get; }
    }
}