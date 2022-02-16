using DotStar;
using nanoFramework.Hardware.Esp32;
using System.Drawing;
using System;
using System.Threading;

namespace ColorCircle
{
    public class Program
    {
        private static DotStarLed led;
        private static Random random = new Random();
        private static int colorIndex = 0;

        public static void Main()
        {
            Configuration.SetPinFunction(12, DeviceFunction.SPI1_CLOCK);
            Configuration.SetPinFunction(2, DeviceFunction.SPI1_MOSI);
            Configuration.SetPinFunction(19, DeviceFunction.SPI1_MISO);

            led = new DotStarLed();

            while (true)
            {
                led.SetColor(DotstarColorWheel(128, colorIndex++));

                Thread.Sleep(20);
            }
        }   

        public static Color DotstarColorWheel(int alpha, int wheel_pos)
        {
            wheel_pos = wheel_pos % 255;

            if (wheel_pos < 85)
                return Color.FromArgb(alpha, 255 - wheel_pos * 3, 0, wheel_pos * 3);
            else if (wheel_pos < 170)
            {
                wheel_pos -= 85;
                return Color.FromArgb(alpha, 0, wheel_pos * 3, 255 - wheel_pos * 3);
            }
            else
            {
                wheel_pos -= 170;
                return Color.FromArgb(alpha, wheel_pos * 3, 255 - wheel_pos * 3, 0);
            }
        }
    }
}
