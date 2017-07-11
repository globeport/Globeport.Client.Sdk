using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Globeport.Client.Sdk.Extensions
{
    public static class TaskExtensions
    {
        public async static void Run(this Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch
            {
            }
        }

        public async static Task RunAsync(this Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch
            {
            }
        }
    }
}
