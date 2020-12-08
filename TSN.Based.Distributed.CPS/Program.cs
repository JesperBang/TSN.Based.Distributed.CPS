using System;


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
                Console.WriteLine("Please select test file: Press 1 for small.xml, 2 for medium.xml, 3 for large.xml or 4 for huge.xml");
                try
                {
                    num = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input!!!");
                    num = 0;
                }

                if ((num <= 0) || (num >= 5))
                {
                    Console.WriteLine("Input must be 1, 2, 3 or 4");
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
