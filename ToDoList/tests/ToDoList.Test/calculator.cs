namespace ToDoList.Test;

using System;
using Microsoft.VisualBasic;

public class UnitTest1
{
    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(2, 2, 1)]
    public void TestDeleni(int dividend, int divisor, int expectedResult)
    {
        //Arrange
        var calculator = new Calculator();
        //Act
        var result = calculator.Divide(dividend, divisor);
        //Assert
        Assert.Equal(result, expectedResult);


    }

    [Fact]
    public void TestDeleniNulou()
    {
        var kalkulacka = new Calculator();

        Assert.Throws<DivideByZeroException>(() => kalkulacka.Divide(10, 0));
    }
}

public class Calculator
{
    public float Divide(float dividend, float divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException();
        }

        return dividend / divisor;
    }
}
