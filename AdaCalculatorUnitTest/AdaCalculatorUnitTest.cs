using AdaCalculator;
using Moq;
using Shouldly;

namespace AdaCalculator.Test
{
    public class AdaCalculatorUnitTest
    {
        private readonly CalculatorMachine _sut;
        private readonly CalculatorMachine _sutMock;
        private readonly Mock<ICalculator> _calculatorMock;

        public AdaCalculatorUnitTest()
        {
            _sut = new CalculatorMachine();
            _calculatorMock = new Mock<ICalculator>();
            _sutMock = new CalculatorMachine(_calculatorMock.Object);
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

        [Fact]
        public void CalculatorMachine_Sum_ShouldBeCorrectMock()
        {
            _calculatorMock.Setup(x => x.Calculate("sum", 777, 710)).Returns(("sum", 1487));
            var result = _sutMock.Calculate("sum", 777, 710);
            _calculatorMock.Verify(x => x.Calculate("sum", 777, 710), Times.Once);

            Assert.Equal(("sum", 1487), result);
        }

        [Fact]
        public void CalculatorMachine_Subtract_ShouldBeCorrectMock()
        {
            _calculatorMock.Setup(x => x.Calculate("subtract", 777, 710)).Returns(("subtract", 67));
            var result = _sutMock.Calculate("subtract", 777, 710);
            _calculatorMock.Verify(x => x.Calculate("subtract", 777, 710), Times.Once);

            Assert.Equal(("subtract", 67), result);
        }

        [Fact]
        public void CalculatorMachine_Multiply_ShouldBeCorrectMock()
        {
            _calculatorMock.Setup(x => x.Calculate("multiply", 3, 2)).Returns(("multiply", 6));
            var result = _sutMock.Calculate("multiply", 3, 2);
            _calculatorMock.Verify(x => x.Calculate("multiply", 3, 2), Times.Once);

            Assert.Equal(("multiply", 6), result);
        }

        [Fact]
        public void CalculatorMachine_Divide_ShouldBeCorrectMock()
        {
            _calculatorMock.Setup(x => x.Calculate("divide", 6, 2)).Returns(("divide", 3));
            var result = _sutMock.Calculate("divide", 6, 2);
            _calculatorMock.Verify(x => x.Calculate("divide", 6, 2), Times.Once);

            Assert.Equal(("divide", 3), result);
        }
    }
}