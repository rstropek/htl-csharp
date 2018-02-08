using System;
using System.Collections.Generic;
using System.Linq;

// Optional exercise: Implement evaluation logic in separate library

namespace Evaluate.Logic
{
    public class ExpressionEvaluator : IExpressionEvaluator
    {
        private Dictionary<string, int> variables = new Dictionary<string, int>();

        private int GetValue(string subExpression)
        {
            if (Int32.TryParse(subExpression, out var result))
            {
                return result;
            }
            else
            {
                return variables[subExpression];
            }
        }

        public IDictionary<string, int> Variables => variables;

        public void SetVariable(string name, int value) => variables[name] = value;

        // Note: Positive, if understanding of LINQ is shown here
        public int Evaluate(string formula) => formula.Split('+').Select(v => GetValue(v)).Sum();
    }
}
