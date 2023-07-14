using MatrixModel;

namespace MatrxCalculatorLib
{
	public class Calculator
	{
		#region Fields
		List<Matrix> _operandList;
		Matrix? _result;
		List<char> _operatorsList;
		bool _hasBeginWithSign;
		#endregion

		#region Constructors
		public Calculator()
		{
			_operandList = new List<Matrix>();
			_operatorsList = new List<char>();
		}
		public Calculator(List<Matrix> operandList, List<char> operatorsList, bool hasBeginWithSign) : this()
		{
			OperandList = operandList;
			OperatorsList = operatorsList;
			HasBeginWithSign = hasBeginWithSign;
		}
		#endregion

		#region Properties
		public Matrix? Result { get => _result; set => _result = value; }
		public List<char> OperatorsList { get => _operatorsList; set => _operatorsList = value; }
		public List<Matrix> OperandList { get => _operandList; set => _operandList = value; }
		public bool HasBeginWithSign { get => _hasBeginWithSign; set => _hasBeginWithSign = value; }
		#endregion

		#region Public Methodes
		public void Execute()
		{
			if (OperandList != null && OperandList.Count > 0)
			{
				int j = 0;
				for (int i = 0; i < OperandList.Count; i++)
				{
					if (i == 0)
					{
						Result = OperandList.ElementAt(i);
						j = i;
						if (HasBeginWithSign)
						{
							var scalar = new Matrix(1, 1);
							scalar.Matrice[0, 0] = -1;
							Result = scalar * Result;
							j++;
						}
					}
					else
					{
						if (Result != null)
						{
							switch (OperatorsList.ElementAt(j))
							{
								case '+':
									Result = Result + OperandList.ElementAt(i);
									break;
								case '-':
									Result = Result - OperandList.ElementAt(i);
									break;
								case '*':
									Result = Result * OperandList.ElementAt(i);
									break;
							}
							j++;
						}
					}
				}
			}
		}
		#endregion
	}
}