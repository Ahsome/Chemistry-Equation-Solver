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
                    if (IsEqual(Equation.ReactantElements, Equation.ProductsElements))
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

        private static bool IsEqual(Dictionary<string, int> dict, Dictionary<string, int> dict2)
        {
            bool equal = false;
            if (dict.Count == dict2.Count) // Require equal count.
            {
                equal = true;
                foreach (var pair in dict)
                {
                    int value;
                    if (dict2.TryGetValue(pair.Key, out value))
                    {
                        // Require value be equal.
                        if (value != pair.Value)
                        {
                            equal = false;
                            break;
                        }
                    }
                    else
                    {
                        // Require key be present.
                        equal = false;
                        break;
                    }
                }
            }
            return equal;
        }
    }
}
