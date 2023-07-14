using System.Globalization;
using MatrixModel;
using MatrxCalculatorLib;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var A = new Matrix(3, 3);
            var B = new Matrix(1, 1);
			var C = new Matrix(3, 3);
            var listOperand = new List<Matrix> { A, B, C };
            var listOperator = new List<char> { '*', '*' };
			var calculator = new Calculator(listOperand, listOperator, false);
            int j = 0;
            for(int i = 0; i < listOperand.Count; i++)
            {
                if (i == 0)
                {
                    if (calculator.HasBeginWithSign)
                    {
                        Console.WriteLine(listOperator[j]);
                        Console.WriteLine(listOperand[i]);
                        j++;
                    }
                    else
                        Console.WriteLine(listOperand[i]);
                }
                else
                {
					Console.WriteLine(listOperator[j]);
					Console.WriteLine(listOperand[i]);
					j++;
				}
			}
            calculator.Execute();
			Console.WriteLine(calculator.Result);
		}
    }
}