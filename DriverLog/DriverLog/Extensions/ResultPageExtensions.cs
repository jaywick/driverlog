using DriverLog.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Extensions
{
    // fyi: extension methods are a c# feature which lets us 'add-on' methods that get added onto a type
    // designated by first argument of the method declared here.
    //
    // For example if we wanted stringVariable.IsThisJustASpace() we can declare
    // public static bool IsThisJustASpace(this string target) { return target == " " }
    // and anything that imports the extension method's class will have access to these methods!
    public static class ResultPageExtensions
    {
        /// <summary>
        /// Uses the IResultPage's Awaiter property to allow us to <code>await</code> from the result.
        /// </summary>
        public static async Task<TResult> GetResultAsync<TResult>(this IResultPage<TResult> page)
        {
            return await page.Awaiter.Task;
        }
    }
}
