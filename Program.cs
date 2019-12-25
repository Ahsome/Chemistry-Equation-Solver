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

        private static int[] ElementNumbers(string[] substances)
        {
            int[] numberOfElements = new int[118];
            var elementNumber = new Dictionary<string, int>
            {
                {"H", 0},
                {"He", 1},
                {"Li", 2},
                {"Be", 3},
                {"B", 4},
                {"C", 5},
                {"N", 6},
                {"O", 7},
                {"F", 8},
                {"Ne",9},
                {"Na",10},
                {"Mg",11},
                {"Al",12},
                {"Si",13},
                {"P",14},
                {"S",15},
                {"Cl",16},
                {"Ar",17},
                {"K",18},
                {"Ca",19},
                {"Sc",20},
                {"Ti",21},
                {"V", 22},
                {"Cr", 23},
                {"Mn", 24},
                {"Fe", 25},
                {"Co", 26},
                {"Ni", 27},
                {"Cu", 28},
                {"Zn", 29},
                {"Ga", 30},
                {"Ge",31},
                {"As",32},
                {"Se",33},
                {"Br",34},
                {"Kr",35},
                {"Rb",36},
                {"Sr",37},
                {"Y",38},
                {"Zr",39},
                {"Nb",40},
                {"Mo",41},
                {"Tc",42},
                {"Ru",43},
                {"Rh",44},
                {"Pd", 45},
                {"Ag", 46},
                {"Cd", 47},
                {"In", 48},
                {"Sn", 49},
                {"Sb", 50},
                {"Te", 51},
                {"I", 52},
                {"Xe", 53},
                {"Cs",54},
                {"Ba",55},
                {"La",56},
                {"Ce",57},
                {"Pr",58},
                {"Nd",59},
                {"Pm",60},
                {"Sm",61},
                {"Eu",62},
                {"Gd",63},
                {"Tb",64},
                {"Dy",65},
                {"Ho",66},
                {"Er", 67},
                {"Tm", 68},
                {"Yb", 69},
                {"Lu", 70},
                {"Hf", 71},
                {"Ta", 72},
                {"W", 73},
                {"Re", 74},
                {"Os", 75},
                {"Ir",76},
                {"Pt",77},
                {"Au",78},
                {"Hg",79},
                {"Tl",80},
                {"Pb",81},
                {"Bi",82},
                {"Po",83},
                {"At",84},
                {"Rn",85},
                {"Fr",86},
                {"Ra",87},
                {"Ac",88},
                {"Th",89},
                {"Pa",90},
                {"U", 91},
                {"Np",92},
                {"Pu",93},
                {"Am",94},
                {"Cm",95},
                {"Bk",96},
                {"Cf",97},
                {"Es",98},
                {"Fm",99},
                {"Md",100},
                {"No",101},
                {"Lr",102},
                {"Rf",103},
                {"Db",104},
                {"Sg", 105},
                {"Bh",106},
                {"Hs",107},
                {"Mt",108},
                {"Ds",109},
                {"Rg",110},
                {"Cn",111},
                {"Uut",112},
                {"Fl",113},
                {"Uup",114},
                {"Lv",115},
                {"Uus",116},
                {"Uuo",117},
            };

            foreach (var substance in substances)
            {
                int coefficient;
                if (!Char.IsNumber(substance[0]))
                {
                    coefficient = 1;
                }
                else
                {
                    string coefficientString = "";
                    foreach (var react in substance)
                    {
                        if (Char.IsNumber(react))
                        {
                            coefficientString += react;
                        }
                        else
                        {
                            break;
                        }
                    }
                    coefficient = int.Parse(coefficientString);
                }

                string reactantNoCoefficient = substance;
                if (coefficient != 1)
                {
                    reactantNoCoefficient = String.Format("{0}",
                        substance.Substring(coefficient.ToString().Length,
                            substance.Length - coefficient.ToString().Length));
                }

                string currentElement = "";
                string numberOfElement = "";


                foreach (var subst in reactantNoCoefficient)
                {
                repeatIfEnd:
                    if (Char.IsLetter(subst) && currentElement == String.Empty)
                    {
                        currentElement += subst;
                        continue;
                    }
                    else if (Char.IsLetter(subst) && Char.IsLower(subst))
                    {
                        currentElement += subst;
                        continue;
                    }
                    else if (Char.IsNumber(subst))
                    {
                        numberOfElement += subst;
                        continue;
                    }
                    else if (Char.IsLetter(subst) && numberOfElement == String.Empty)
                    {
                        numberOfElement = "1";
                    }
                    int elementId = elementNumber[currentElement];
                    numberOfElements[elementId] += int.Parse(numberOfElement) * coefficient;
                    currentElement = String.Empty;
                    numberOfElement = String.Empty;
                    goto repeatIfEnd;
                }

                if (Char.IsLetter(reactantNoCoefficient[reactantNoCoefficient.Length - 1]) && numberOfElement == String.Empty)
                {
                    numberOfElement = "1";
                }

                int outElementId = elementNumber[currentElement];
                numberOfElements[outElementId] += int.Parse(numberOfElement) * coefficient;
            }
            return numberOfElements;
        }

        //        private static void BalanceEquation(int n)
        //        {
        //            float[][] a = new float[n][]
        //            {
        //                new float[n] {2.0f, 1.0f},
        //                new float[n] {3.0f, -1.0f}
        //            };
        //
        //            float[] b = new float[n]
        //            {
        //                7.0f,
        //                8.0f
        //            };
        //
        //            float[] x = new float[n];
        //
        //            //
        //            for (int i = 0; i < n - 1; i++)
        //            {
        //                for (int j = i + 1; j < n; j++)
        //                {
        //                    float s = a[j][i] / a[i][i];
        //                    for (int k = i; k < n; k++)
        //                    {
        //                        a[j][k] -= a[i][k] * s;
        //                    }
        //                    b[j] -= b[i] * s;
        //                }
        //            }
        //
        //            //
        //            for (int i = n - 1; i >= 0; i--)
        //            {
        //                b[i] /= a[i][i];
        //                a[i][i] /= a[i][i];
        //                for (int j = i - 1; j >= 0; j--)
        //                {
        //                    float s = a[j][i] / a[i][i];
        //                    a[j][i] -= s;
        //                    b[j] -= b[i] * s;
        //                }
        //            }
        //
        //            /*
        //            for (int i = 0; i < N; i++)
        //            {
        //                x[i] = b[i] / A[i][i];
        //            }
        //            */
        //            x = Enumerable.Range(0, n).Select(i => b[i] / a[i][i]).ToArray();
        //
        //            //Console.WriteLine(string.Join("\n", x.Select(v => string.Format("{0}", v))));
        //        }
    }
}
