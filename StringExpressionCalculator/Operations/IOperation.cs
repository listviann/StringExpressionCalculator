namespace StringExpressionCalculator.Operations;

public interface IOperation
{
    void Execute(Stack<decimal> stack, decimal number);
}
