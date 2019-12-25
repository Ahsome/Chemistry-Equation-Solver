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
        }

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

        public bool CheckBalanced()
        {
            var reactantsElements = new Dictionary<string, int>();
            var productsElements = new Dictionary<string, int>();

            foreach (var reactant in Reactants)
            {
                foreach (var element in reactant.TotalElements.Keys)
                {
                    AddToProperty(reactant.TotalElements[element], element, reactantsElements);
                }
            }

            foreach (var product in Products)
            {
                foreach (var element in product.TotalElements.Keys)
                {
                    AddToProperty(product.TotalElements[element], element, productsElements);
                }
            }

            return IsEqual(reactantsElements, productsElements);
        }

        private void AddToProperty(int value, string key, Dictionary<string, int> property)
        {
            if (property.TryGetValue(key, out int previousValue))
            {
                property[key] = previousValue + value;
            }
            else
            {
                property.Add(key, value);
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
