using System;
using System.Collections.Generic;

namespace Evaluate.Logic
{
    // Optional exercise: Use interface
    public interface IExpressionEvaluator
    {
        IDictionary<string, int> Variables { get; }

        void SetVariable(string name, int value);

        int Evaluate(string formula);
    }
}
