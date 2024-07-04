namespace StringExpressionCalculator.Operations;

public class Addition : IOperation
{
    public void Execute(Stack<decimal> stack, decimal number) => stack.Push(number);
}
