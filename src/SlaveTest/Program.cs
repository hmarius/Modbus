using System;
using System.Threading;
using System.Threading.Tasks;
using AMWD.Modbus.Tcp.Server;

namespace SlaveTest
{
    class SlaveTest
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static Task MainAsync()
        {
            using (var mbs = new ModbusServer())
            {
                mbs.AddDevice(1);
                mbs.Initialization.GetAwaiter().GetResult();
                Console.WriteLine("Server started");
                while (true)
                {
                    var register = mbs.GetHoldingRegister(1, 0);
                    if(register.Value == 99)
                        break;
                    Console.WriteLine($"Holding register 0: {register}");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Server closed");
            }
            return Task.CompletedTask;
        }
    }
}