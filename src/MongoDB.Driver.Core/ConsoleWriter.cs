using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Driver
{
    /// <summary>
    /// Implements a Non-thread blocking console writer to help with debugging multithreading / locking issues
    /// </summary>
    public static class ConsoleWriter
    {
        private static BlockingCollection<string> blockingCollection = new BlockingCollection<string>();

        static ConsoleWriter()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    System.Console.WriteLine(blockingCollection.Take());
                }

            });
        }

        /// <summary>
        /// Sends a message to be processed on the ConsoleWriter thread's output queue
        /// </summary>
        /// <param name="value"></param>
        public static void WriteLine(string value)
        {
            blockingCollection.Add(value);
        }

    }
}
