using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        private int maxPower = 700;

        public PowerTube(IOutput output)
        {
            myOutput = output;
        }

        public void SetUpMaxPower(int maxPow)
        {
            maxPower = maxPow;
            myOutput.OutputLine("Powertube set up with a maxpower of: " + maxPow);
        }

        public void TurnOn(int power)
        {
            if (power < 1 || maxPower < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and " + maxPower + " (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}