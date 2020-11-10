using System;
using System.Collections.Generic;
using System.Text;

namespace TSN.Based.Distributed.CPS
{
    public class Acceptance
    {

        public static double Acceptance_Function(double energy, double adjEnergy, double temp)
        {
            if (adjEnergy > energy) return 1.0;
            else
                return Math.Exp((energy - adjEnergy) / temp);
        }

    }
}
