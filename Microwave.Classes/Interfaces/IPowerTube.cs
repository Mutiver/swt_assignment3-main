﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Interfaces
{
    public interface IPowerTube
    {
        void SetUpMaxPower(int power);

        void TurnOn(int power);

        void TurnOff();
    }
}
