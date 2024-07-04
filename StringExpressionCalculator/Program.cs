using StringExpressionCalculator;

while (true)
{
    Console.WriteLine("Enter an arithmetic expression:\n(enter \"exit\" to quit):");
    var expression = Console.ReadLine();

    if (expression!.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
        break;   

    try
    {
        var result = ExpressionCalculator.Calculate(expression!);
        Console.WriteLine(result);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine(ex.Message);
    }
}