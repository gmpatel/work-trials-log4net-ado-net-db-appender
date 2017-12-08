using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationLog4NetTest
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                int i = 99;
                int j = 0;
                int k = Divide(i, j);
            }
            catch (Exception exception)
            {
                Logging.Logger.Instance().Error(exception.Message, exception);
            }

            Logging.Logger.Instance().Debug("log Debug");
            Logging.Logger.Instance().Info("log Info");
            Logging.Logger.Instance().Warn("log Warn");
            Logging.Logger.Instance().Error("log Error");
            Logging.Logger.Instance().Fatal("log Fatal");

            Console.ReadKey();
        }

        private static int Divide(int i, int j)
        {
            return i/j;
        }
    }
}