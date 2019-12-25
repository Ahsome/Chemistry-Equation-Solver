using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemistryEquationSolver
{
    class Chemical
    {

        public Chemical(string chemicalInformation)
        {
            SetFullValue(chemicalInformation);
            SetCoefficient(chemicalInformation);
            SetElements(chemicalInformation);
            SetValue(chemicalInformation);
        }

        private void SetValue(string chemicalInformation)
        {
            Value = RemoveCoefficient(chemicalInformation);
        }

        private string RemoveCoefficient(string chemicalInformation)
        {
            string chemInfoNoCoefficient = chemicalInformation;
            if (Char.IsDigit(chemicalInformation[0]))
            {
                chemInfoNoCoefficient = chemInfoNoCoefficient.Substring(Coefficient.ToString().Length);
            }

            return chemInfoNoCoefficient;
        }

        private void SetFullValue(string chemicalInformation)
        {
            FullValue = chemicalInformation;
        }

        private void SetCoefficient(string chemicalInformation)
        {
            string coefficientString = "";
            foreach (var character in chemicalInformation)
            {
                if (Char.IsDigit(character))
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

                int value = 1;
                    if (!number.Equals(""))
                {
                    value = int.Parse(number);
                }
                Helper.AddToProperty(value, element, Elements);
            }
        }

        public Dictionary<string, int> Elements { get; private set; } = new Dictionary<string, int>();
        public Dictionary<string, int> TotalElements
        {
            get
            {
                var totalElements = new Dictionary<string, int>();
                foreach (var value in Elements.Keys)
                {
                    totalElements.Add(value, Elements[value]*Coefficient);
                }

                return totalElements;
            }
        }
        public int Coefficient { get; private set; }
        public string Value { get; private set; }
        public string FullValue { get; private set; }
    }
}
