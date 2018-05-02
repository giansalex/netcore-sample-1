namespace MainLibrary.Core
{
    public class SumOperation : ICalculator
    {
        public decimal Operate(decimal a, decimal b)
        {
            return a + b;
        }
    }
}