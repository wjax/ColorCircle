using System;
using System.Device.Gpio;
using System.Device.Spi;
using System.Drawing;
using Iot.Device.Apa102;
using nanoFramework.Hardware.Esp32;

namespace DotStar
{
    public class DotStarLed : IDisposable
    {
        private SpiDevice spiDevice;
        private Apa102 apa102;
        private GpioController gpioController;
        private GpioPin _pwr;

        public DotStarLed()
        {
            gpioController = new GpioController();
            _pwr = gpioController.OpenPin(13);
            _pwr.SetPinMode(PinMode.Output);
            _pwr.Write(PinValue.Low);

            spiDevice = SpiDevice.Create(new SpiConnectionSettings(1,1)
            {
                ClockFrequency = 1_000_000,
                DataFlow = DataFlow.MsbFirst,
                Mode = SpiMode.Mode0, // ensure data is ready at clock rising edge
            });
            apa102 = new Apa102(spiDevice, 1);
            
        }

        public void SetColor(Color color)
        {
            var pixels = apa102.Pixels;
            for (var i = 0; i < apa102.Pixels.Length; i++)
            {
                pixels[i] = color;
            }

            apa102.Flush();
        }

        public void SetPower(bool power)
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
