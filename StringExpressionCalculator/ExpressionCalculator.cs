using System.Text.RegularExpressions;

namespace StringExpressionCalculator;

public class ExpressionCalculator
{
    public static bool ValidateBraces(string expression)
    {
        string braces = Regex.Replace(expression, @"[^\(\)]", "");

        var stack = new Stack<char>();

        for (int i = 0; i < braces.Length; i++)
        {
            if (braces[i] == '(')
                stack.Push(braces[i]);

            else if (stack.Count > 0 && braces[i] == ')' && stack.Peek() == '(')
                stack.Pop();

            else
                return false;
        }

        if (stack.Count == 0)
            return true;

        return false;
    }

    public static decimal Calculate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            return 0;



        var stack = new Stack<decimal>();

        decimal number = 0;
        char sign = '+';
        int n = expression.Length;
        int decimalEncountered = 0;

        for (int i = 0; i < n; i++)
        {
            char c = expression[i];
            if (char.IsDigit(c))
            {
                if (decimalEncountered > 0)
                {
                    number += (c - '0') / Convert.ToDecimal(Math.Pow(10, decimalEncountered));
                    decimalEncountered++;
                }
                else
                {
                    number = number * 10 + (c - '0');
                }
            }
            else if (c == '.')
            {
                decimalEncountered = 1;
            }

            if (c == '(')
            {
                int j = i + 1, braces = 1;
                while (j < n && braces > 0)
                {
                    if (expression[j] == '(')
                        braces++;
                    if (expression[j] == ')')
                        braces--;

                    j++;
                }

                number = Calculate(expression.Substring(i + 1, j - i - 2));
                i = j - 1;
                decimalEncountered = 0;
            }
            if ((!char.IsDigit(c) && c != ' ' && c != '.') || i == n - 1)
            {
                if (sign == '+')
                {
                    stack.Push(number);
                }
                if (sign == '-')
                {
                    stack.Push(-number);
                }
                if (sign == '*')
                {
                    stack.Push(stack.Pop() * number);
                }
                if (sign == '/')
                {
                    stack.Push(stack.Pop() / number);
                }
                number = 0;
                sign = c;
                decimalEncountered = 0;
            }
        }

        decimal result = 0;
        while (stack.Count != 0)
        {
            result += stack.Pop();
        }
        return Math.Round(result, 2, MidpointRounding.AwayFromZero);
    }
}
