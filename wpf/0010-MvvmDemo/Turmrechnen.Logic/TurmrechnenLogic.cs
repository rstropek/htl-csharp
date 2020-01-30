using System.Collections.Generic;

namespace MvvmDemo
{
    public class ResultItem
    {
        public int SourceValue { get; set; }
        public char Operator { get; set; }
        public int Operand { get; set; }
        public int Result { get; set; }

        public override string ToString() =>
            $"{SourceValue} {Operator} {Operand} = {Result}";
    }

    public class TurmrechnenLogic
    {
        public IEnumerable<ResultItem> Calculate(int baseValue, int height)
        {
            var currentValue = baseValue;
            for(var i = 0; i < height; i++)
            {
                yield return new ResultItem
                {
                    SourceValue = currentValue,
                    Operator = '*',
                    Operand = 2 + i,
                    Result = currentValue *= (2 + i)
                };
            }
            for (var i = 0; i < height; i++)
            {
                yield return new ResultItem
                {
                    SourceValue = currentValue,
                    Operator = '/',
                    Operand = 2 + i,
                    Result = currentValue /= (2 + i)
                };
            }
        }
    }
}
