using System.Text.RegularExpressions;

namespace StringExpressionCalculator.Validation;

public partial class BraceValidator
{
    public bool Validate(string expression)
    {
        string braces = BraceRegex().Replace(expression, "");

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

        return stack.Count == 0;
    }

    [GeneratedRegex(@"[^\(\)]")]
    private static partial Regex BraceRegex();
}