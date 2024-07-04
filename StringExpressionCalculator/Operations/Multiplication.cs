namespace StringExpressionCalculator.Operations;

public class Multiplication : IOperation
{
    public void Execute(Stack<decimal> stack, decimal number) => stack.Push(stack.Pop() * number);
}
