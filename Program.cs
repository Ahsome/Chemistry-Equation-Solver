using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemistryEquationSolver
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine("What equation would you like to check/balance?");
            var Equation = new Equation(Console.ReadLine());

            Console.Clear();

            while (true)
            {
                Console.WriteLine("What would you like to do?\n - CHECK\n - BALANCE (NOT IMPLEMENTED)");

                if (Console.ReadLine() == "CHECK")
                {
                    Console.Clear();
                    if (Equation.CheckBalanced())
                    {
                        Console.WriteLine("HELL YEAH, BALANCING!");

                    }

                    else
                    {
                        Console.WriteLine("Not balanced, dufus");
                    }
                    Console.ReadKey(true);
                    break;
                }
            }

        }
    }
}
