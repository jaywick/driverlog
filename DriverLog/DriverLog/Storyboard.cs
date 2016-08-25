using DriverLog.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using System.Threading.Tasks;
using DriverLog.Extensions;

namespace DriverLog
{
    /// <summary>
    /// Wrapper around Navigation class to simplify switching between Pages
    /// </summary>
    public class Storyboard
    {
        private App _app;
        private INavigation _navigation;

        public Storyboard(App app)
        {
            _app = app;
        }

        /// <summary>
        /// Goes to the first page of the application
        /// </summary>
        public void Start(Page page)
        {
            _app.MainPage = new NavigationPage(page);
            _navigation = _app.MainPage.Navigation;
        }

        /// <summary>
        /// Switches to another page
        /// </summary>
        public async Task ShowAsync(Page page)
        {
            // add to navigation stack
            await _navigation.PushAsync(page, true);
        }

        /// <summary>
        /// Switches back to previous Page which called ShowAsync
        /// </summary>
        public async Task Return()
        {
            // go back in navigation stack
            await _navigation.PopAsync();
        }

        /// <summary>
        /// Switches to a Page which is to send back a result of type TResult
        /// </summary>
        public async Task<TResult> PromptAsync<TResult>(IResultPage<TResult> page)
        {
            // wrap new page in NavigationPage so it appears like pages launched from ShowAsync
            var wrappedPage = new NavigationPage((Page)page);

            await _navigation.PushModalAsync(wrappedPage, true);
            return await page.GetResultAsync();
        }

        /// <summary>
        /// Switches back to Page which called PromptAsync() and sends back the result
        /// </summary>
        public void Return<TResult>(TResult value, IResultPage<TResult> sender)
        {
            if (sender.Awaiter.Task.IsCompleted || sender.Awaiter.Task.IsCanceled || sender.Awaiter.Task.IsFaulted)
                return; // already called Return, perhaps error or user double clicked before visually returned back to previous page

            if (!(sender is Page))
                throw new InvalidOperationException($"Storyboard.Return cannot be called with sender not being a page. Was {sender.GetType().Name}");

            sender.Awaiter.SetResult(value);

            // go back in navigation stack
            _navigation.PopModalAsync();
        }
    }
}
