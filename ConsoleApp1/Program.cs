using System.Globalization;
using MatrixModel;
using MatrxCalculatorLib;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[,] mat = { { 0, 1, 2 }, { 1, 1, 2 }, { 0, 2, 3 } };

			Console.WriteLine(new Matrix(mat));
			Console.WriteLine("Determinant: " + new Matrix(mat).GaussJordanEliminationDeterminant());
			Console.WriteLine("\n\n\n");
			Console.WriteLine("Inverse: " + new Matrix(mat).GaussJordanEliminationIverse());
		}
    }
}