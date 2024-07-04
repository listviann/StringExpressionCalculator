using System.Linq.Expressions;

namespace StringExpressionCalculator.Tests;

public class ExpressionCalculatorTests
{
    [Test]
    public void Calculate_NoDigitsInExpressionAsParam_ReturnsZero()
    {
        Assert.That(ExpressionCalculator.Calculate("+"), Is.EqualTo(0));
    }

    [Test]
    public void Calculate_StringWithSingleNumberAsParam_ReturnsTheNumber()
    {
        Assert.That(ExpressionCalculator.Calculate("12"), Is.EqualTo(12));
    }

    [TestCase("1+(2-3")]
    [TestCase("1+2-3)")]
    [TestCase("2*((5+5*2)/3+(6/(2+8)")]
    [TestCase(")))))(1+(4+5+2)-3)+(6.1+8)(((")]
    public void Calculate_StringWithIncorrectBracesOrder_ThrowsArgumentException(string expression)
    {
        Assert.That(() => ExpressionCalculator.Calculate(expression), 
            Throws.Exception.TypeOf<ArgumentException>());
    }

    [TestCase("1+2-3", 0)]
    [TestCase("2*(5+5*2)/3+(6/2+8)", 21)]
    [TestCase("(1+(4+5+2)-3)+(6.1+8)", 23.1)]
    [TestCase("3+2*2", 7)]
    [TestCase("3/2", 1.5)]
    [TestCase("3+5/2", 5.5)]
    public void Calculate_StringExpression_ReturnsResult(string expression, decimal result)
    {
        Assert.That(ExpressionCalculator.Calculate(expression), Is.EqualTo(result));
    }
}
