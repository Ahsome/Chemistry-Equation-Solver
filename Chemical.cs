using System;
using System.Collections.Generic;
using System.Text;

namespace ChemistryEquationSolver
{
    internal class Chemical
    {
        public Chemical(string chemicalInformation)
        {
            SetFullValue(chemicalInformation);
            SetCoefficient(chemicalInformation);
            SetElements(chemicalInformation);
            SetValue(chemicalInformation);
        }

        public int Coefficient { get; private set; }

        public Dictionary<string, int> Elements { get; } = new Dictionary<string, int>();

        public string FullValue { get; private set; }

        public Dictionary<string, int> TotalElements
        {
            get
            {
                var totalElements = new Dictionary<string, int>();
                foreach (var value in Elements.Keys)
                {
                    totalElements.Add(value, Elements[value] * Coefficient);
                }

                return totalElements;
            }
        }

        public string Value { get; private set; }

        private string RemoveCoefficient(string chemicalInformation)
        {
            string chemInfoNoCoefficient = chemicalInformation;
            if (char.IsDigit(chemicalInformation[0]))
            {
                chemInfoNoCoefficient = chemInfoNoCoefficient.Substring(Coefficient.ToString().Length);
            }

            return chemInfoNoCoefficient;
        }

        private void SetCoefficient(string chemicalInformation)
        {
            string coefficientString = "";
            foreach (var character in chemicalInformation)
            {
                if (char.IsDigit(character))
                {
                    coefficientString += character;
                }
                else
                {
                    break;
                }
            }

            if (!coefficientString.Equals(""))
            {
                Coefficient = int.Parse(coefficientString);
            }
            else
            {
                Coefficient = 1;
            }
        }

        private void SetElements(string chemicalInformation)
        {
            string chemInfoNoCoefficient = RemoveCoefficient(chemicalInformation);

            StringBuilder elements = new StringBuilder();
            foreach (char c in chemInfoNoCoefficient)
            {
                if (char.IsUpper(c) && elements.Length > 0)
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
                    if (char.IsLetter(c))
                    {
                        element += c;
                    }
                    else if (Char.IsNumber(c))
                    {
                        number += c;
                    }
                }

                int value = 1;
                if (!number.Equals(""))
                {
                    value = int.Parse(number);
                }
                Helper.AddToProperty(value, element, Elements);
            }
        }

        private void SetFullValue(string chemicalInformation)
        {
            FullValue = chemicalInformation;
        }

        private void SetValue(string chemicalInformation)
        {
            Value = RemoveCoefficient(chemicalInformation);
        }
    }
}