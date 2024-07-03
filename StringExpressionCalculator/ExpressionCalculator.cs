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
        {
            return 0;
        }

        return Convert.ToDecimal(expression);
    }
}
