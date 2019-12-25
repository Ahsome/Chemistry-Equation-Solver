using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemistryEquationSolver
{
    class Equation
    {
        public Equation(string equationString)
        {
            AddChemicals(equationString);
            SetValue(equationString);
        }

        private void SetValue(string equationString)
        {
            Value = equationString;
        }

        public override string ToString()
        {
            return Value;
        }

        public string Value { get; private set; }

        private void AddChemicals(string equationString)
        {
            string[] reactants = ExtractReactants(equationString);
            string[] products = ExtractProducts(equationString);

            foreach (var reactant in reactants)
            {
                var reactantChemical = new Chemical(reactant);
                Reactants.Add(reactantChemical);
            }

            foreach (var product in products)
            {
                var productChemical = new Chemical(product);
                Products.Add(productChemical);
            }
        }

        private string[] ExtractProducts(string equationString)
        {
            return equationString.Split('=')[1].Split('+');
        }

        private string[] ExtractReactants(string equationString)
        {
            return equationString.Split('=')[0].Split('+');
        }

        public List<Chemical> Reactants { get; private set; } = new List<Chemical>();
        public List<Chemical> Products { get; private set; } = new List<Chemical>();
        public List<Chemical> TotalChemicals
        {
            get
            {
                var totalChemicals = new List<Chemical>();
                totalChemicals.AddRange(Reactants);
                totalChemicals.AddRange(Products);
                return totalChemicals;
            }
        }

        public bool CheckBalanced()
        {
            var reactantsElements = new Dictionary<string, int>();
            var productsElements = new Dictionary<string, int>();

            foreach (var reactant in Reactants)
            {
                foreach (var element in reactant.TotalElements.Keys)
                {
                    Helper.AddToProperty(reactant.TotalElements[element], element, reactantsElements);
                }
            }

            foreach (var product in Products)
            {
                foreach (var element in product.TotalElements.Keys)
                {
                    Helper.AddToProperty(product.TotalElements[element], element, productsElements);
                }
            }

            return Helper.IsEqual(reactantsElements, productsElements);
        }

        public string BalancedEquation()
        {
            var newCoefficients = CalculateNewCoefficients();
            var newEquationString = new StringBuilder();

            for (int i = 0; i < TotalChemicals.Count; i++)
            {
                if (newCoefficients[i] != 1)
                {
                    newEquationString.Append(newCoefficients[i]);
                }
                newEquationString.Append(TotalChemicals[i].Value);

                if (i == Reactants.Count - 1)
                {
                    newEquationString.Append("=");
                }
                else
                {
                    newEquationString.Append("+");
                }
            }

            newEquationString.Length--;
            return newEquationString.ToString();

            //TODO: Used when solving same number of equations and variables
            //var x = A.SolveIterative(b, new MlkBiCgStab());
        }

        private Vector<double> CalculateNewCoefficients()
        {
            string[] allElements = GetAllElements();
            var elementCoefficients = new double[allElements.Length + 1, TotalChemicals.Count];
            for (int i = 0; i < allElements.Length - 1; i++)
            {
                for (int j = 0; j < TotalChemicals.Count; j++)
                {
                    if (TotalChemicals.ElementAt(j).Elements.ContainsKey(allElements[i]))
                    {
                        TotalChemicals.ElementAt(j).Elements.TryGetValue(allElements[i], out int value);
                        if (j < Reactants.Count)
                        {
                            elementCoefficients[i, j] += value;
                        }
                        else
                        {
                            elementCoefficients[i, j] -= value;
                        }
                    }
                }
            }

            elementCoefficients[elementCoefficients.GetLength(0) - 1, 0] = 1;
            var vector = new double[elementCoefficients.GetLength(0)];
            vector[vector.Length - 1] = 1;

            var A = Matrix<double>.Build.DenseOfArray(elementCoefficients);
            var b = Vector<double>.Build.Dense(vector);

            var ATA = A.Transpose() * A;
            var ATb = A.Transpose() * b;
            var C = ATA.Inverse();
            var ans = C * ATb;

            double smallest = double.MaxValue;
            foreach (var a in ans)
            {
                if (a < smallest)
                {
                    smallest = a;
                }
            }

            for (int i = 0; i < ans.Count; i++)
            {
                ans[i] = Math.Round(ans[i]/smallest);
            }

            return ans;
        }

        private string[] GetAllElements()
        {
            var allElements = new List<String>();

            foreach (var reactant in Reactants)
            {
                foreach (var element in reactant.Elements.Keys)
                {
                    if (!allElements.Contains(element))
                    {
                        allElements.Add(element);
                    }
                }
            }

            foreach (var product in Products)
            {
                foreach (var element in product.Elements.Keys)
                {
                    if (!allElements.Contains(element))
                    {
                        allElements.Add(element);
                    }
                }
            }

            return allElements.ToArray();
        }
    }
}
