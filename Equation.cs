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
            AddReactantElements();
            AddProductElements();
        }

        private void AddChemicals(string equationString)
        {
            string[] reactants = ExtractReactants(equationString);
            string[] products = ExtractProducts(equationString);

            foreach (var reactant in reactants)
            {
                var chemInfo = ExtractChemicalInformation(reactant);
                AddToProperty(chemInfo.Coefficient, chemInfo.Name, Reactants);
            }

            foreach (var product in products)
            {
                var chemInfo = ExtractChemicalInformation(product);
                AddToProperty(chemInfo.Coefficient, chemInfo.Name, Reactants);
            }
        }

        private (string Name, int Coefficient) ExtractChemicalInformation(string chemical)
        {
            int coefficient = GetCoefficient(chemical);
            string name = GetName(chemical, coefficient);

            return (name, coefficient);
        }

        private static string GetName(string chemical, int coefficient)
        {
            string name = chemical;
            if (coefficient != 1)
            {
                name = chemical.Substring(coefficient.ToString().Length);
            }

            return name;
        }

        private void AddProductElements()
        {
            AddElements(Products, ProductsElements);
        }

        private void AddReactantElements()
        {
            AddElements(Reactants, ReactantElements);
        }

        private void AddElements(Dictionary<string, int> chemicals, Dictionary<string, int> elementProperty)
        {
            foreach (var chemical in chemicals.Keys)
            {
                StringBuilder elements = new StringBuilder();
                foreach (char c in chemical)
                {
                    if (Char.IsUpper(c) && elements.Length > 0)
                    {
                        elements.Append(' ');
                    };
                    elements.Append(c);
                }

                foreach (var elementWithNum in elements.ToString().Split(' '))
                {
                    string element = "";
                    string number = "";
                    foreach (var c in elementWithNum.ToString())
                    {
                        if (Char.IsLetter(c))
                        {
                            element += c;
                        }
                        else if (Char.IsNumber(c))
                        {
                            number += c;
                        }
                    }

                    int value;
                    chemicals.TryGetValue(chemical, out value);

                    if (!number.Equals(""))
                    {
                        value *= int.Parse(number);
                    }

                    AddToProperty(value, element, elementProperty);
                }
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

        public Dictionary<string, int> Reactants { get; private set; } = new Dictionary<string, int>();
        public Dictionary<string, int> Products { get; private set; } = new Dictionary<string, int>();
        public Dictionary<string, int> ReactantElements { get; private set; } = new Dictionary<string, int>();
        public Dictionary<string, int> ProductsElements { get; private set; } = new Dictionary<string, int>();

        private int GetCoefficient(string reactant)
        {
            string coefficient = "";
            foreach (var character in reactant)
            {
                if (Char.IsDigit(character))
                {
                    coefficient += character;
                }
                else
                {
                    break;
                }
            }
            
            if (!coefficient.Equals(""))
            {
                return int.Parse(coefficient);
            }
            else
            {
                return 1;
            }
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
    }
}
