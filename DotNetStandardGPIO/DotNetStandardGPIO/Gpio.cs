using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetStandardGPIO
{
    public static class Gpio
    {
        public static void GpioOn(this int gpioId, bool enable = false)
        {
            if (CreateGpio(gpioId, false))
                File.WriteAllText("/sys/class/gpio/gpio" + gpioId + "/value", enable ? "1" : "0");
        }

        private static bool CreateGpio(int gpioId, bool gpioIn)
        {
            if (Directory.Exists("/sys/class/gpio/gpio" + gpioId)) return true;

            try
            {
                File.WriteAllText("/sys/class/gpio/export", gpioId.ToString());
                if (!gpioIn)
                {
                    File.WriteAllText("/sys/class/gpio/gpio" + gpioId + "/direction", "out");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot export or specify GPIO" + gpioId + " as IN/OUT, are you root? " + e);
                return false;
            }
        }
    }
}
