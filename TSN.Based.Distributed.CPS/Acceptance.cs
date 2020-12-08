using System;

namespace TSN.Based.Distributed.CPS
{
    public class Acceptance
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="energy"></param>
        /// <param name="adjEnergy"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static double Acceptance_Function(double energy, double adjEnergy, double temp)
        {
            if (adjEnergy < energy) return 1.0;
            else
                return Math.Exp((energy - adjEnergy) / temp);
        }

    }
}
