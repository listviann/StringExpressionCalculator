using StringExpressionCalculator.Operations;
using StringExpressionCalculator.Validation;

namespace StringExpressionCalculator;

public class ExpressionEvaluator
{
    private readonly Dictionary<char, IOperation> _operations = new Dictionary<char, IOperation>
    {
        { '+', new Addition() },
        { '-', new Subtraction() },
        { '*', new Multiplication() },
        { '/', new Division() },
    };

    public decimal Evaluate(string expression)
    {
        var braceValidator = new BraceValidator();
        
        if (!braceValidator.Validate(expression))
            throw new ArgumentException("Invalid expression: Braces are not balanced.");
        

        var stack = new Stack<decimal>();
        decimal number = 0;
        char sign = '+';
        int decimalEncountered = 0;

        for (int i = 0; i < expression.Length; i++)
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
                while (j < expression.Length && braces > 0)
                {
                    if (expression[j] == '(') braces++;
                    if (expression[j] == ')') braces--;
                    j++;
                }

                number = Evaluate(expression.Substring(i + 1, j - i - 2));
                i = j - 1;
                decimalEncountered = 0;
            }
            if ((!char.IsDigit(c) && c != ' ' && c != '.') || i == expression.Length - 1)
            {
                ApplyOperation(stack, sign, number);
                sign = c;
                number = 0;
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

    private void ApplyOperation(Stack<decimal> stack, char op, decimal number)
    {
        if (_operations.TryGetValue(op, out IOperation? operation))
            operation.Execute(stack, number);
    }
}
