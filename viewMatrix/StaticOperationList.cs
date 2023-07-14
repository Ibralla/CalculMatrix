using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixModel;
using MatrxCalculatorLib;

namespace viewMatrix
{
	public static class StaticOperationList
	{
		public static List<Matrix> MatrixList = new List<Matrix>();
		public static List<Matrix> OperandList = new List<Matrix>();
		public static List<char> OperatorList = new List<char>();
		public static Calculator Calculator = new Calculator();
		public static bool showError = false;
		public static string error = string.Empty;

		public static void UpdateOperation(List<string> list)
		{
			OperandList.Clear();
			int i = 0;
			int j = 0;
			foreach (var item in list)
			{
				switch (item)
				{
					case "matrice":
						OperandList.Add(MatrixList[i]);
						i++;
						break;
					case "determinant":
						OperandList.Add(MatrixList[i].GaussJordanEliminationDeterminant());
						i++;
						break;
					case "inverse":
						try
						{
							OperandList.Add(MatrixList[i].GaussJordanEliminationIverse());
							showError = false;
						}
						catch (Exception e)
						{
							showError = true;
							error = e.Message;
							StaticOperationList.OperandList.Add(MatrixList[i]);
						}
						
						i++;
						break;
					case "plus":
						break;
					case "multiplication":
						
						break;
					case "moins":
						
						break;
				}
}
		}
	}
}
