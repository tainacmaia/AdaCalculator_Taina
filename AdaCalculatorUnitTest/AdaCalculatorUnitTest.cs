using AdaCalculator;
using Moq;
using Shouldly;

namespace AdaCalculator.Test
{
    public class AdaCalculatorUnitTest
    {
        private readonly CalculatorMachine _sut;
        private readonly CalculatorMachine _sutMock;
        private readonly Mock<ICalculator> calculatorMock;

        public AdaCalculatorUnitTest()
        {
            _sut = new CalculatorMachine();
            calculatorMock = new Mock<ICalculator>();
            _sutMock = new CalculatorMachine(calculatorMock.Object);
        }

        [Theory]
        [InlineData("sum", 777, 710, 1487)]
        [InlineData("sum", 777, -710, 67)]
        [InlineData("sum", -777, 710, -67)]
        [InlineData("sum", -12, -15, -27)]
        [InlineData("sum", 33, 0, 33)]
        public void CalculatorMachine_Sum_ShouldBeValid(string operation, double number1, double number2, double result)
        {
            var calculation = _sut.Calculate(operation, number1, number2);
            calculation.ShouldBe((operation, result));
        }

        [Theory]
        [InlineData("subtract", 777, 710, 67)]
        [InlineData("subtract", 777, -710, 1487)]
        [InlineData("subtract", -777, 710, -1487)]
        [InlineData("subtract", -12, -15, 3)]
        [InlineData("subtract", 33, 0, 33)]
        [InlineData("subtract", 0, 33, -33)]
        public void CalculatorMachine_Subtract_ShouldBeValid(string operation, double number1, double number2, double result)
        {
            var calculation = _sut.Calculate(operation, number1, number2);
            calculation.ShouldBe((operation, result));
        }

        [Theory]
        [InlineData("multiply", 2, 3, 6)]
        [InlineData("multiply", 2, -3, -6)]
        [InlineData("multiply", -2, -3, 6)]
        [InlineData("multiply", -2, 3, -6)]
        [InlineData("multiply", 0, 3, 0)]
        [InlineData("multiply", 3, 0, 0)]
        public void CalculatorMachine_Multiply_ShouldBeValid(string operation, double number1, double number2, double result)
        {
            var calculation = _sut.Calculate(operation, number1, number2);
            calculation.ShouldBe((operation, result));
        }

        [Theory]
        [InlineData("divide", 3, 1, 3)]
        [InlineData("divide", 3, 3, 1)]
        [InlineData("divide", 0, 1, 0)]
        [InlineData("divide", 3, -1, -3)]
        [InlineData("divide", 1, 0, double.PositiveInfinity)]
        public void CalculatorMachine_Divide_ShouldBeValid(string operation, double number1, double number2, double result)
        {
            var calculation = _sut.Calculate(operation, number1, number2);
            calculation.ShouldBe((operation, result));
        }

        [Theory]
        [InlineData("sum", 777, 710, 1487)]
        [InlineData("sum", 777, -710, 67)]
        [InlineData("sum", -777, 710, -67)]
        [InlineData("sum", -12, -15, -27)]
        [InlineData("sum", 33, 0, 33)]
        public void CalculatorMachine_Sum_ShouldBeValidMock(string operation, double number1, double number2, double result)
        {
            calculatorMock.Setup(x => x.Calculate(operation, number1, number2)).Returns((operation, result));
            _sutMock.Calculate(operation, number1, number2);
            calculatorMock.Verify(x => x.Calculate(operation, number1, number2), Times.Once);
        }

        [Theory]
        [InlineData("subtract", 777, 710, 67)]
        [InlineData("subtract", 777, -710, 1487)]
        [InlineData("subtract", -777, 710, -1487)]
        [InlineData("subtract", -12, -15, 3)]
        [InlineData("subtract", 33, 0, 33)]
        [InlineData("subtract", 0, 33, -33)]
        public void CalculatorMachine_Subtract_ShouldBeValidMock(string operation, double number1, double number2, double result)
        {
            calculatorMock.Setup(x => x.Calculate(operation, number1, number2)).Returns((operation, result));
            _sutMock.Calculate(operation, number1, number2);
            calculatorMock.Verify(x => x.Calculate(operation, number1, number2), Times.Once);
        }

        [Theory]
        [InlineData("multiply", 2, 3, 6)]
        [InlineData("multiply", 2, -3, -6)]
        [InlineData("multiply", -2, -3, 6)]
        [InlineData("multiply", -2, 3, -6)]
        [InlineData("multiply", 0, 3, 0)]
        [InlineData("multiply", 3, 0, 0)]
        public void CalculatorMachine_Multiply_ShouldBeValidMock(string operation, double number1, double number2, double result)
        {
            calculatorMock.Setup(x => x.Calculate(operation, number1, number2)).Returns((operation, result));
            _sutMock.Calculate(operation, number1, number2);
            calculatorMock.Verify(x => x.Calculate(operation, number1, number2), Times.Once);
        }

        [Theory]
        [InlineData("divide", 3, 1, 3)]
        [InlineData("divide", 3, 3, 1)]
        [InlineData("divide", 0, 1, 0)]
        [InlineData("divide", 3, -1, -3)]
        [InlineData("divide", 0, 0, double.NaN)]
        [InlineData("divide", 1, 0, double.PositiveInfinity)]
        public void CalculatorMachine_Divide_ShouldBeValidMock(string operation, double number1, double number2, double result)
        {
            calculatorMock.Setup(x => x.Calculate(operation, number1, number2)).Returns((operation, result));
            _sutMock.Calculate(operation, number1, number2);
            calculatorMock.Verify(x => x.Calculate(operation, number1, number2), Times.Once);
        }
    }
}