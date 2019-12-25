using System.Collections.Generic;

namespace ChemistryEquationSolver
{
    internal static class Helper
    {
        public static void AddToProperty(int value, string key, Dictionary<string, int> property)
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

        public static bool IsEqual(Dictionary<string, int> dict, Dictionary<string, int> dict2)
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