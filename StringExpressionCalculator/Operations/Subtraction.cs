namespace StringExpressionCalculator.Operations;

public class Subtraction : IOperation
{
    public void Execute(Stack<decimal> stack, decimal number) => stack.Push(-number);
}
