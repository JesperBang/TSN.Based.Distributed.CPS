using System;
using System.Collections.Generic;
using System.Data;
using TSN.Based.Distributed.CPS.Models;


namespace TSN.Based.Distributed.CPS
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 1;
            bool input = false;

            while (!input)
            {
                Console.WriteLine("Please select test file: Press 1 for small.xml or 2 for medium.xml");
                try
                {
                    num = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input!!!");
                    num = 0;
                }

                if ((num <= 0) || (num >= 3))
                {
                    Console.WriteLine("Input must be 1 or 2");
                }
                else
                {
                    input = true;
                }
            }
            new SimulatedAnnealing(num);
            Console.WriteLine($"Completed! The generated solution can be found in the project folder");
        }
    }
}
