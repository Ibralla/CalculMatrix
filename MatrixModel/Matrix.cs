using System.Security.Cryptography;
using System.Text;

namespace MatrixModel
{
    public class Matrix
    {
        #region Fields
        int _rowNumber;
        int _columnNumber;
        double[,] _matrix;
		#endregion

		#region Properties
		public int RowNumber { get => _rowNumber; set => _rowNumber = value; }
		public int ColumnNumber { get => _columnNumber; set => _columnNumber = value; }
		public double[,] Matrice 
        {
            get => _matrix;
            set => _matrix = value; 
        }
		#endregion

		#region Constructors
		public Matrix()
        {
            _rowNumber = 3;
            _columnNumber = 3;
            _matrix = new double[3,3];
            InitializeMatrix();
        }
        public Matrix(int rowNumber, int columnNumber) : this()
        {
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
            Matrice = new double[rowNumber, columnNumber];
            InitializeMatrix();
        }

        public Matrix(double[,] matrix) : this()
        {
            _rowNumber = matrix.GetLength(0);
			_columnNumber = matrix.GetLength(1);
            _matrix = matrix;
		}
        #endregion

        #region Public Methodes
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    builder.Append(Matrice[i, j] + " ");
                }
                builder.Append('\n');
            }
            return builder.ToString();
        }

        //public Matrix Det(Matrix matrix)
        //{

        //}
        #endregion

        #region Public Static Methodes
        public static Matrix operator +(Matrix matrixA, Matrix matrixB)
        {
            if((matrixA.RowNumber == matrixB.RowNumber)
                && (matrixA.ColumnNumber == matrixB.ColumnNumber))
            {
                var result = new Matrix(matrixA.RowNumber, matrixA.ColumnNumber);
                for (int i = 0; i < result.RowNumber; i++)
                {
                    for (int j = 0; j < result.ColumnNumber; j++)
                    {
                        result.Matrice[i,j] = matrixA.Matrice[i,j] + matrixB.Matrice[i,j];
                    }
                }
                return result;
            }
            throw new Exception("the dimension of matrix is not compatible");
        }

        public static Matrix operator -(Matrix matrixA, Matrix matrixB)
        {
            if ((matrixA.RowNumber == matrixB.RowNumber)
                && (matrixA.ColumnNumber == matrixB.ColumnNumber))
            {
                var result = new Matrix(matrixA.RowNumber, matrixA.ColumnNumber);
                for (int i = 0; i < result.RowNumber; i++)
                {
                    for (int j = 0; j < result.ColumnNumber; j++)
                    {
                        result.Matrice[i, j] = matrixA.Matrice[i, j] - matrixB.Matrice[i, j];
                    }
                }
                return result;
            }
            throw new Exception("the dimension of matrix is not compatible");
        }

        public static Matrix operator *(Matrix matrixA, Matrix matrixB)
        {
            Matrix? result;
			// Multiplication entre scalaire
			if ((matrixA.ColumnNumber == 1 && matrixA._rowNumber == 1)
					&& (matrixB.ColumnNumber == 1 && matrixB._rowNumber == 1))
			{
                result = new Matrix(1, 1);
                result.Matrice[0, 0] = matrixA.Matrice[0, 0] * matrixB.Matrice[0, 0];
			}
			// Multiplication entre matrice
			else if (matrixA.ColumnNumber == matrixB.RowNumber)
            {
                result = new Matrix(matrixA.RowNumber, matrixB.ColumnNumber);
                for (int i = 0; i < matrixA.RowNumber; i++)
                {
                    for (int j = 0; j < matrixB.ColumnNumber; j++)
                    {
                        result.Matrice[i, j] = 0;
                        for (int k = 0; k < matrixB.RowNumber; k++)
                        {
                            result.Matrice[i, j] += matrixA.Matrice[i, k] * matrixB.Matrice[k, j];
                        }
                    }
                }
            }
			// Multiplication entre scalaire et matrice
			else if (matrixA.ColumnNumber == 1 && matrixA._rowNumber == 1)
            {
                result = new Matrix(matrixB._rowNumber, matrixB._columnNumber);
                double scalaire = matrixA.Matrice[0, 0];
                result = scalaire * matrixB;
            }
			else if (matrixB.ColumnNumber == 1 && matrixB._rowNumber == 1)
			{
				result = new Matrix(matrixA._rowNumber, matrixA._columnNumber);
				double scalaire = matrixB.Matrice[0, 0];
				result = scalaire * matrixA;
			}
			else
				throw new Exception("the dimension of matrix is not compatible");
			return result;
		}

        public static Matrix operator *(double scalaire, Matrix matrixA)
        {
            var result = new Matrix(matrixA.RowNumber, matrixA.ColumnNumber);
            for (int i = 0; i < result.RowNumber; i++)
            {
                for (int j = 0; j < result.ColumnNumber; j++)
                {
                    result.Matrice[i, j] = scalaire * matrixA.Matrice[i, j];
                }
            }
            return result;
        }
        #endregion

        #region Helpers
        private void InitializeMatrix()
        {
            for(int i=0; i<RowNumber; i++)
            {
                for(int j=0; j<ColumnNumber; j++)
                {
                    Matrice[i, j] = 0;
                }
            }
        }
        #endregion
    }
}