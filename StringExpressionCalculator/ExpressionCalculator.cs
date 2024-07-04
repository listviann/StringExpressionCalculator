using System.Text.RegularExpressions;

namespace StringExpressionCalculator;

public class ExpressionCalculator
{
    public static decimal Calculate(string expression) => new ExpressionEvaluator().Evaluate(expression);
}
