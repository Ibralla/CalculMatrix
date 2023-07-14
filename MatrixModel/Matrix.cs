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

		public Matrix GaussJordanEliminationDeterminant()
		{
            int rowPermuteNumber = 0;

            var matrix = this.Copy();

			double multiple;
			double pivot;
			double tempVar;
			var result = new Matrix(1, 1);
            result.Matrice[0,0] = 1;
			int k = 0; // l'indice de ligne du maximum
			int r = -1; // r est l'indice de ligne du dernier pivot trouvé
			for (int j = 0; j < ColumnNumber; j++) // j décrit tous les indices de colonnes
			{
				k = r + 1;
				for (int i = r + 1; i < RowNumber - 1; i++)
				{
					if (Math.Abs(matrix.Matrice[i + 1, j]) > Math.Abs(matrix.Matrice[i, j]))
						k = i + 1;
				}
				pivot = matrix.Matrice[k, j]; // A[k,j] est le pivot
                result.Matrice[0, 0] = result.Matrice[0, 0] * pivot;

				if (pivot != 0)
				{
					r++;
					// A[k,j] est le pivot
					for (int col = 0; col < ColumnNumber; col++) // Diviser la ligne k par pivot
					{
						matrix.Matrice[k, col] = matrix.Matrice[k, col] / pivot;
					}
					if (k != r)
					{
                        rowPermuteNumber++;
						// Échanger les lignes k et r
						for (int col = 0; col < ColumnNumber; col++)
						{
							tempVar = matrix.Matrice[k, col];
							matrix.Matrice[k, col] = matrix.Matrice[r, col];
							matrix.Matrice[r, col] = tempVar;
						}

					}
					// On simplifie les autres lignes
					for (int i = 0; i < RowNumber; i++)
					{
						multiple = matrix.Matrice[i, j];

						if (i != r)
						{
							// Soustraire à la ligne i la ligne r multipliée par A[i,j] (de façon à annuler A[i,j])
							for (int col = 0; col < ColumnNumber; col++)
							{
								matrix.Matrice[i, col] = matrix.Matrice[i, col] -
									matrix.Matrice[r, col] * multiple;
							}
						}
					}
				}
			}
            result.Matrice[0, 0] = Math.Pow(-1, rowPermuteNumber) * result.Matrice[0, 0];
            return result;
		}

		public Matrix GaussJordanEliminationIverse()
        {
			var matrix = this.Copy();
			double multiple;
            double pivot;
            double tempVar;
            var result = Identity(new Matrix(RowNumber, ColumnNumber));
            int k = 0; // l'indice de ligne du maximum
			int r = -1; // r est l'indice de ligne du dernier pivot trouvé
            if (this.GaussJordanEliminationDeterminant().Matrice[0, 0] == 0)
                throw new Exception("Matrix is not Inversible");

            for(int j=0; j<ColumnNumber; j++) // j décrit tous les indices de colonnes
			{
                k = r + 1;
                for(int i=r+1; i<RowNumber - 1; i++)
                {
                    if (Math.Abs(matrix.Matrice[i + 1, j]) > Math.Abs(matrix.Matrice[i, j]))
                        k = i + 1;
				}
                pivot = matrix.Matrice[k, j]; // A[k,j] est le pivot

				if (pivot != 0) 
				{
                    r++;
					// A[k,j] est le pivot
                    for(int col = 0; col<ColumnNumber; col++) // Diviser la ligne k par pivot
					{
						matrix.Matrice[k, col] = matrix.Matrice[k, col] / pivot;
						result.Matrice[k, col] = result.Matrice[k, col] / pivot;
					}
                    if(k != r)
                    {
						// Échanger les lignes k et r
						for (int col = 0; col < ColumnNumber; col++)
						{
                            tempVar = matrix.Matrice[k, col];
							matrix.Matrice[k, col] = matrix.Matrice[r, col];
							matrix.Matrice[r, col] = tempVar;

							tempVar = result.Matrice[k, col];
							result.Matrice[k, col] = result.Matrice[r, col];
							result.Matrice[r, col] = tempVar;
						}
                        
					}
					// On simplifie les autres lignes
                    for(int i=0; i<RowNumber; i++)
                    {
                        multiple = matrix.Matrice[i, j];

						if (i != r)
                        {
							// Soustraire à la ligne i la ligne r multipliée par A[i,j] (de façon à annuler A[i,j])
							for (int col = 0; col < ColumnNumber; col++)
							{
								matrix.Matrice[i, col] = matrix.Matrice[i, col] -
									matrix.Matrice[r, col] * multiple;
								result.Matrice[i, col] = result.Matrice[i, col] -
									result.Matrice[r, col] * multiple;
							}
						}
					}
				}
			}
			return result;
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

        public Matrix Identity(Matrix matrix)
        {
            var result = new Matrix(matrix.RowNumber, matrix.ColumnNumber);
            for(int i=0; i<result.RowNumber; i++)
            {
				for (int j = 0; j < result.RowNumber; j++)
                {
                    if(i != j)
                        result.Matrice[i, j] = 0;
                    else
						result.Matrice[i, j] = 1;
				}
            }
            return result;
        }

        private Matrix Copy()
        {
            Matrix result = new Matrix(RowNumber, ColumnNumber);
			for (int i = 0; i < RowNumber; i++)
            {
				for (int j = 0; j < RowNumber; j++)
                {
                    result.Matrice[i,j] = Matrice[i, j];
                }
			}
			return result;
		}
        
        #endregion
    }
}