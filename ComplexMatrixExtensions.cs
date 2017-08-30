// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComplexMatrixExtensions.cs" company="Azven">
//   (C) Steven Digby
// </copyright>
// <summary>
//   The matrix extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Matrix2
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Numerics;

    using Matrix1;

    // using System.Drawing;

    /// <summary>The matrix extensions.</summary>
    public static class ComplexMatrixExtensions
    {
        /// <summary>The rows.</summary>
        private const int Rows = 0;

        /// <summary>The columns.</summary>
        private const int Columns = 1;

        /// <summary>The angle units.</summary>
        public enum AngleUnits
        {
            /// <summary>The degrees.</summary>
            Degrees,

            /// <summary>The radians.</summary>
            Radians
        }

        /// <summary>The triangular type.</summary>
        public enum TriangularType
        {
            /// <summary>The lower.</summary>
            LowerTriangular,

            /// <summary>The upper triangular.</summary>
            UpperTriangular,

            /// <summary>The diagonal matrix.</summary>
            DiagonalMatrix,

            /// <summary>The identity matrix.</summary>
            IdentityMatrix,

            /// <summary>The null.</summary>
            NullMatrix,

            /// <summary>The not trangular.</summary>
            NotTrangular
        }

        /// <summary>The add matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="targetArray">The target array.</param>
        /// <returns>The resulting sum array.</returns>
        /// <exception cref="Exception">Same dimensions exception</exception>
        public static Complex[,] AddMatrix(this Complex[,] sourceArray, Complex[,] targetArray)
        {
            if ((sourceArray.GetUpperBound(Rows) != targetArray.GetUpperBound(Rows))
                || (sourceArray.GetUpperBound(Columns) != targetArray.GetUpperBound(Columns)))
            {
                throw new Exception("Addition and subtraction can only occur when the matices have the same dimensions.");
            }

            var resultArray = new Complex[sourceArray.GetUpperBound(Rows) + 1, sourceArray.GetUpperBound(Columns) + 1];

            for (var row = 0; row <= sourceArray.GetUpperBound(Rows); row++)
            {
                for (var col = 0; col <= sourceArray.GetUpperBound(Columns); col++)
                {
                    resultArray[row, col] = sourceArray[row, col].AddComplex(targetArray[row, col]);
                }
            }

            return resultArray;
        }

        /// <summary>The subtract matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="targetArray">The target array.</param>
        /// <returns>The resulting subtraction array.</returns>
        /// <exception cref="Exception">Same dimensions exception</exception>
        public static Complex[,] SubtractMatrix(this Complex[,] sourceArray, Complex[,] targetArray)
        {
            if ((sourceArray.GetUpperBound(Rows) != targetArray.GetUpperBound(Rows))
                || (sourceArray.GetUpperBound(Columns) != targetArray.GetUpperBound(Columns)))
            {
                throw new Exception("Addition and subtraction can only occur when the matices have the same dimensions.");
            }

            var resultArray = new Complex[sourceArray.GetUpperBound(Rows) + 1, sourceArray.GetUpperBound(Columns) + 1];

            for (var row = 0; row <= sourceArray.GetUpperBound(Rows); row++)
            {
                for (var col = 0; col <= sourceArray.GetUpperBound(Columns); col++)
                {
                    resultArray[row, col] = sourceArray[row, col].SubtractComplex(targetArray[row, col]);
                }
            }

            return resultArray;
        }

        /// <summary>The multiply matrix by constant.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="constant">The constant.</param>
        /// <returns>A complex matrix with each element multiplied by a real constant.</returns>
        public static Complex[,] MultiplyMatrixByConstant(this Complex[,] sourceArray, double constant)
        {
            var resultArray = new Complex[sourceArray.GetUpperBound(Rows) + 1, sourceArray.GetUpperBound(Columns) + 1];

            for (var row = 0; row <= sourceArray.GetUpperBound(Rows); row++)
            {
                for (var col = 0; col <= sourceArray.GetUpperBound(Columns); col++)
                {
                    resultArray[row, col] = sourceArray[row, col].MultiplyBy(new Complex(constant, 0));
                }
            }

            return resultArray;
        }

        /// <summary>The multiply matrix by constant.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="constant">The constant.</param>
        /// <returns>A complex matrix with each element multiplied by a complex constant.</returns>
        public static Complex[,] MultiplyMatrixByConstant(this Complex[,] sourceArray, Complex constant)
        {
            var resultArray = new Complex[sourceArray.GetUpperBound(Rows) + 1, sourceArray.GetUpperBound(Columns) + 1];

            for (var row = 0; row <= sourceArray.GetUpperBound(Rows); row++)
            {
                for (var col = 0; col <= sourceArray.GetUpperBound(Columns); col++)
                {
                    resultArray[row, col] = sourceArray[row, col].MultiplyBy(constant);
                }
            }

            return resultArray;
        }        

        /// <summary>Multiply two complex matrices.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="targetArray">The target array.</param>
        /// <returns>The dot product of the two matrices.</returns>
        /// <exception cref="Exception">Row and Column mismatch.</exception>
        public static Complex[,] MultiplyMatrix(this Complex[,] sourceArray, Complex[,] targetArray)
        {
            if (sourceArray.GetUpperBound(Columns) != targetArray.GetUpperBound(Rows))
            {
                throw new Exception(
                    "Matrix multiplication A·B : There must be the same number or rows in Matrix B as there are columns in Matrix A.");
            }

            var resultArray = new Complex[sourceArray.GetUpperBound(Rows) + 1, targetArray.GetUpperBound(Columns) + 1];

            for (var row = 0; row <= sourceArray.GetUpperBound(Rows); row++)
            {
                for (var col = 0; col <= targetArray.GetUpperBound(Columns); col++)
                {
                    Complex element = 0;
                    for (var index = 0; index <= targetArray.GetUpperBound(Rows); index++)
                    {
                        element = element.AddComplex(sourceArray[row, index].MultiplyBy(targetArray[index, col]));
                    }

                    resultArray[row, col] = element;
                }
            }

            return resultArray;
        }

        /// <summary>The negate function.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>For each complex element (A + iB) this returns (-A - iB).</returns>
        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1512:SingleLineCommentsMustNotBeFollowedByBlankLine", Justification = "Reviewed. Suppression is OK here.")]
        public static Complex[,] Negate(this Complex[,] sourceArray)
        {
            // Note : this is functionally the same as
            //        return sourceArray.MultiplyMatrixByConstant(-1)            

            var resultArray = new Complex[sourceArray.GetUpperBound(Rows) + 1, sourceArray.GetUpperBound(Columns) + 1];

            for (var row = 0; row <= sourceArray.GetUpperBound(Rows); row++)
            {
                for (var col = 0; col <= sourceArray.GetUpperBound(Columns); col++)
                {
                    resultArray[row, col] = sourceArray[row, col].Negate();
                }
            }

            return resultArray;
        }

        /// <summary>The zero matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns the zero or null comple matrix of the same size as source. 
        /// Use thisArray.RealConvertToDoubleMatrix() to convert to a double array.</returns>
        public static Complex[,] ZeroMatrix(this Complex[,] sourceArray)
        {
            // Use thisArray.RealConvertToDoubleMatrix() to convert to a double array            
            var rowCount = sourceArray.GetUpperBound(Rows) + 1;
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            var resultArray = new Complex[rowCount, colCount];

            for (var r = 0; r < rowCount; r++)
            {
                for (var c = 0; c < colCount; c++)
                {
                    resultArray[r, c] = Complex.Zero;
                }
            }

            return resultArray;
        }

        /// <summary>The identity matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns the complex identity matrix of the same size as source. 
        /// Use thisArray.RealConvertToDoubleMatrix() to convert to a double array.</returns>
        public static Complex[,] IdentityMatrix(this Complex[,] sourceArray)
        {
            // Use thisArray.RealConvertToDoubleMatrix() to convert to a double array
            if (IsNotSquare(sourceArray))
            {
                throw new Exception("Only a square matrix can be an identity matrix.");
            }

            var s = sourceArray.GetUpperBound(Rows) + 1;
            var resultArray = new Complex[s, s];

            for (var r = 0; r < s; r++)
            {
                for (var c = 0; c < s; c++)
                {
                    resultArray[r, c] = new Complex(r == c ? 1 : 0, 0);
                }
            }

            return resultArray;
        }

        /// <summary>The get row.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="row">The row.</param>
        /// <returns>The row of complex numbers from the matrix.</returns>
        public static Complex[] GetRow(this Complex[,] sourceArray, int row)
        {
            var resultArray = new Complex[sourceArray.GetUpperBound(Columns) + 1];

            for (var col = 0; col <= sourceArray.GetUpperBound(Columns); col++)
            {
                resultArray[col] = sourceArray[row, col];
            }

            return resultArray;
        }

        /// <summary>The get column.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="column">The column.</param>
        /// <returns>The column of complex numbers from the matrix.</returns>
        public static Complex[] GetColumn(this Complex[,] sourceArray, int column)
        {
            var resultArray = new Complex[sourceArray.GetUpperBound(Rows) + 1];

            for (var row = 0; row <= sourceArray.GetUpperBound(Columns); row++)
            {
                resultArray[row] = sourceArray[row, column];
            }

            return resultArray;
        }

        /// <summary>The to 1D matrix or vector.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>A 1D column from a 2D matrix.</returns>
        public static Complex[] ToVector(this Complex[,] sourceArray)
        {
            return sourceArray.GetColumn(0);
        }

        /// <summary>The get an element from the 2D matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>A single complex element from out of the matrix.</returns>
        public static Complex GetElement(this Complex[,] sourceArray, int row, int column)
        {
            return sourceArray[row, column];
        }

        /// <summary>The get an element from the vector.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="row">The row.</param>
        /// <returns>A single complex element from out of the matrix.</returns>
        public static Complex GetElement(this Complex[,] sourceArray, int row)
        {
            return sourceArray[row, 0];
        }

        /// <summary>Convert a vector to a Rx1 matrix.</summary>
        /// <param name="vector">The vector.</param>
        /// <returns>A Row x 1 matrix. M[3] becomes M[3, 0].</returns>
        public static Complex[,] To2DMatrix(this Complex[] vector)
        {
            var resultArray = new Complex[vector.GetUpperBound(0) + 1, 1];
            for (var row = 0; row <= vector.GetUpperBound(0); row++)
            {
                resultArray[row, 0] = vector[row];
            }

            return resultArray;
        }        

        /// <summary>The power function for a complex matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="index">The index.</param>
        /// <returns>The matrix raised to the power of some numerical index.</returns>
        public static Complex[,] Pow(this Complex[,] sourceArray, double index)
        {
            var sign = Math.Sign(index);

            if (Math.Abs(index + 1) < 1E-10)
            {
                // Special case: if index = -1 we can return the inverse matrix.
                // We don't allow any other negative powers. Instead the user must 
                // find the inverse matrix and raise to a positive power.
                return sourceArray.InverseMatrix();
            }

            if (sign < 0)
            {
                throw new Exception("The index must be a positive double. If the index is negative find the positive power first then use InverseMatrix.");
            }

            // Raise the matrix M to the power of some index
            if (Math.Abs(index) < 1E-10)
            {                
                // M ^ 0 = I : Any matrix to the power of zero is an Identity matrix
                return sourceArray.IdentityMatrix();
            }

            if (sourceArray.IsZeroMatrix() || sourceArray.IsIdentity())
            {
                // O ^ index == O and I ^ index = I
                return sourceArray;
            }            

            int integerIndex;

            var rowCount = sourceArray.GetUpperBound(Rows) + 1;

            if (IsSquare(sourceArray) && sourceArray.IsDiagonal())
            {
                // If the matrix is diagonal and square
                var returnArray = sourceArray.IdentityMatrix();
                for (var i = 0; i < rowCount; i++)
                {
                    returnArray[i, i] = sourceArray[i, i].Pow(index);
                }

                return returnArray;
            }

            if (int.TryParse(index.ToString(CultureInfo.InvariantCulture), out integerIndex))
            {
                return sourceArray.Pow(integerIndex);
            }

            // We can use another method for non-integer powers but this method is currently limited to 2x2 matrices
            if (IsSquare2X2(sourceArray))
            {
                if (sourceArray.IsDiagonalizable())
                {
                    Complex[,] pMatrix;
                    Complex[,] diagonal;
                    Complex[,] pInvers;

                    try
                    {
                        sourceArray.Diagonalize(out pMatrix, out diagonal, out pInvers);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Exception during Diagonalize.", ex);
                    }

                    var d = diagonal.Pow(index);

                    return pMatrix.MultiplyMatrix(d).MultiplyMatrix(pInvers);
                }
            }

            throw new Exception("The current code cannot find the power function you need to complete this action.");
        }

        /// <summary>The power function for a complex matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="complexIndex">The complex index.</param>
        /// <returns>The matrix raised to the power of a complex index.</returns>
        /// <exception cref="Exception">only square, diagonal matrices</exception>
        public static Complex[,] Pow(this Complex[,] sourceArray, Complex complexIndex)
        {            
            if (!sourceArray.IsDiagonal())
            {
                throw new Exception("The current code only allows complex indecies on square, diagonal matrices.");
            }

            // If the matrix is diagonal and square
            var rowCount = sourceArray.GetUpperBound(Rows) + 1;

            var returnArray = sourceArray.IdentityMatrix();
            for (var i = 0; i < rowCount; i++)
            {
                returnArray[i, i] = sourceArray[i, i].Pow(complexIndex);
            }

            return returnArray;
        }

        /// <summary>The power function for a complex matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="index">The integer index.</param>
        /// <returns>The matrix raised to the power of some integer index.</returns>
        public static Complex[,] Pow(this Complex[,] sourceArray, int index)
        {
            var sign = Math.Sign(index);

            if (sign < 0)
            {
                throw new Exception("The index must be a positive integer. If the index is negative find the positive power first then use InverseMatrix.");
            }

            index = Math.Abs(index);

            if (index == 0)
            {
                return sourceArray.IdentityMatrix();
            }

            if (index == 1)
            {
                return sourceArray;
            }

            var cumulativeValue = sourceArray.IdentityMatrix();
            for (var i = 1; i <= index; i++)
            {
                cumulativeValue = cumulativeValue.MultiplyMatrix(sourceArray);
            }

            return cumulativeValue;
        }

        /// <summary>The roots of a 2x2 matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The a list of roots.</returns>
        /// <exception cref="Exception">Exception during Diagonalize</exception>
        public static List<Complex[,]> SquareRoots(this Complex[,] sourceArray)
        {
            var returnList = new List<Complex[,]>();

            if (IsNotSquare2X2(sourceArray))
            {
                throw new Exception("You can only find the square roots of 2x2 matrices.");
            }

            if (!sourceArray.IsDiagonalizable())
            {
                throw new Exception("The matrix cannot be diagonized.");
            }

            try
            {
                Complex[,] pMatrix;
                Complex[,] diagonal;
                Complex[,] pInverse;

                if (sourceArray.Diagonalize(out pMatrix, out diagonal, out pInverse))
                {
                    var d1 = diagonal.Pow(0.5); // Raise diagonal to the power of ½.

                    var d2 = (Complex[,])d1.Clone();
                    d2[0, 0] = -d2[0, 0]; // By switching a sign we can create another root.

                    var d3 = (Complex[,])d1.Clone();
                    d3[1, 1] = -d3[1, 1]; // Switching sign on the other diagonal creates another root.

                    var d4 = (Complex[,])d1.Clone();
                    d4[0, 0] = -d4[0, 0]; // Switching sign on the both diagonal elements
                    d4[1, 1] = -d4[1, 1]; // creates the fourth root.

                    returnList.Add(pMatrix.MultiplyMatrix(d1).MultiplyMatrix(pInverse));
                    returnList.Add(pMatrix.MultiplyMatrix(d2).MultiplyMatrix(pInverse));
                    returnList.Add(pMatrix.MultiplyMatrix(d3).MultiplyMatrix(pInverse));
                    returnList.Add(pMatrix.MultiplyMatrix(d4).MultiplyMatrix(pInverse));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception while calculating Square Roots.", ex);
            }

            return returnList;
        }

        /// <summary>The function to divide one matrix by another. To divide a matrix by a constant use MultiplyMatrixByConstant.</summary>
        /// <param name="numerator">The numerator matrix.</param>
        /// <param name="denominator">The denominator matrix.</param>
        /// <returns>The two resulting matrices of the division (although they may be the same).</returns>
        /// <exception cref="Exception">Cannot find an inverse for the denominator.</exception>
        public static List<Complex[,]> DivideMatrix(this Complex[,] numerator, Complex[,] denominator)
        {
            // If M = A / B, then M = A·B^-1 AND M = B^-1·A (two results).
            // So to divide numerator by denominator we must find the inverse matrix of the denominator
            // then multiply by the numerator.
            try
            {
                var inverseDenominator = denominator.InverseMatrix();

                var resultOne = inverseDenominator.MultiplyMatrix(numerator);

                var resultTwo = numerator.MultiplyMatrix(inverseDenominator);

                return new List<Complex[,]> { resultOne, resultTwo };
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot find an inverse for the denominator. Division is not possible.", ex);
            }
        }                      

        /// <summary>The is equal.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="targetArray">The target array.</param>
        /// <returns>True when each complex element in sourceArray is the same value as in targetArray.</returns>
        public static bool IsEqual(this Complex[,] sourceArray, Complex[,] targetArray)
        {
            for (var r = 0; r <= sourceArray.GetUpperBound(Rows); r++)
            {
                for (var c = 0; c <= sourceArray.GetUpperBound(Columns); c++)
                {
                    if (!sourceArray[r, c].EqualTo(targetArray[r, c]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>The matrix is an identity matrix (true/false).</summary>
        /// <param name="sourceArray">Any Complex[,] array.</param>
        /// <returns>The complex identity matrix (each element is either (0 + i0) or (1 + i0).</returns>
        public static bool IsIdentity(this Complex[,] sourceArray)
        {
            return IsSquare(sourceArray) && sourceArray.IsEqual(sourceArray.IdentityMatrix());
        }

        /// <summary>True when the matrix is a zero matrix. IsNullMatrix is a synonym for IsZeroMatrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns true when the matrix is a zero matrix.</returns>
        public static bool IsNullMatrix(this Complex[,] sourceArray)
        {
            return sourceArray.IsZeroMatrix();
        }

        /// <summary>True when the matrix is a zero matrix. IsZeroMatrix is a synonym for IsNullMatrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns true when the matrix is a zero matrix.</returns>
        public static bool IsZeroMatrix(this Complex[,] sourceArray)
        {
            // Create an identity matrix the same size as the source matrix            
            var zeroMatrix = sourceArray.ZeroMatrix();

            // Compare the source matrix to the identity matrix and return the results;
            return sourceArray.IsEqual(zeroMatrix);
        }

        /// <summary>Complex matrix is a unitary matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns true if the inverse of the source matrix equals the conjugate-transpose of the source matrix.</returns>
        public static bool IsUnitaryMatrix(this Complex[,] sourceArray)
        {
            if (IsNotSquare(sourceArray))
            {
                return false; // only a square matrix can be a unitary matrix
            }
           
            try
            {
                // Create an inverse to the source matrix             
                var inverseMatrix = sourceArray.InverseMatrix();

                // Compare the conjugate transpose of the source matrix to the inverse matrix
                return inverseMatrix.Equals(sourceArray.ConjugateTranspose());
            }
            catch (Exception)
            {
                // Typically the inner exception will be "The inverse is undefined (determinant is zero)" 
                // from the InverseMatrix function.
                return false;
            }            
        }

        /// <summary>The complex matrix is an hermitian matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns true if the source matrix equals the conjugate-transpose of itself.</returns>
        public static bool IsHermitian(this Complex[,] sourceArray)
        {
            if (IsNotSquare(sourceArray))
            {
                return false; // only a square matrix can be an Hermitian matrix
            }

            // A matrix, M, is Hermitian if the conjugate-transpose of M = M
            return sourceArray.IsEqual(sourceArray.ConjugateTranspose());
        }

        /// <summary>The complex matrix is a skew-hermitian matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns true if the source matrix equals the negative of its conjugate transpose.</returns>
        public static bool IsSkewHermitian(this Complex[,] sourceArray)
        {
            if (IsNotSquare(sourceArray))
            {
                return false; // only a square matrix can be an Hermitian matrix
            }

            // A matrix, M, is Skew-Hermitian if the negative conjugate-transpose of M = M
            return sourceArray.IsEqual(sourceArray.ConjugateTranspose().Negate());
        }

        /// <summary>The complex matrix is an anti-hermitian matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns true if the source matrix equals the negative of its conjugate transpose.</returns>
        public static bool IsAntiHermitian(this Complex[,] sourceArray)
        {
            return sourceArray.IsSkewHermitian();
        }

        /// <summary>The is diagonal.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="rectangularDiagonalMatrix">The rectangular diagonal matrix.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsDiagonal(this Complex[,] sourceArray, bool rectangularDiagonalMatrix = false)
        {
            var rowCount = sourceArray.GetUpperBound(Rows) + 1;
            var colCount = sourceArray.GetUpperBound(Columns) + 1;
            
            // The term diagonal matrix may sometimes refer to a rectangular diagonal matrix, 
            // which is an m-by-n matrix with all the entries not on the diagonal, being zero.
            if (rectangularDiagonalMatrix == false && IsNotSquare(sourceArray))
            {
                return false; // only a square matrix can be a Diagonal matrix when rectangularDiagonalMatrix is false.
            }
            
            for (var r = 0; r < rowCount; r++)
            {
                for (var c = 0; c < colCount; c++)
                {
                    // When r <> c, these are the off-diagonal elements
                    if (r != c)
                    {
                        if (sourceArray[r, c] != Complex.Zero)
                        {
                            // if ANY off-diagonal element is not zero then this is not a diagonal matrix.
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>The scalar value, s, of a scalar matrix, M, where M = sI.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The complex value which is the scalar value of the scalar matrix.</returns>
        /// <exception cref="Exception">This matrix is not a scalar matrix</exception>
        public static Complex ScalarValue(this Complex[,] sourceArray)
        {
            if (sourceArray.IsScalarMatrix())
            {
                return sourceArray.GetElement(0, 0);
            }

            throw new Exception("This matrix is not a scalar matrix. No scalar value could be determined.");
        }

        /// <summary>Identifies a scalar matrix. A square scalar matrix is a constant multiplied by the Identity.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="rectangularMatrix">A square matrix (false; default) or a rectangular matrix (true).</param>
        /// <returns>The true if te matrix is a scalar matrix>.</returns>
        public static bool IsScalarMatrix(this Complex[,] sourceArray, bool rectangularMatrix = false)
        {
            if (!sourceArray.IsDiagonal(rectangularMatrix))
            {
                return false;
            }

            var rowCount = sourceArray.GetUpperBound(Rows) + 1;
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            // itemCount is the smaller of rowCount and colCount 
            var itemCount = rowCount < colCount ? rowCount : colCount;

            var complexElement = sourceArray.GetElement(0, 0);

            // Note: row starts at 1
            for (var item = 1; item < itemCount; item++)
            {
                if (!sourceArray[item, item].EqualTo(complexElement))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>The diagonalize.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="leftMatrix">The left matrix.</param>
        /// <param name="diagonalMatrix">The diagonal matrix.</param>
        /// <param name="rightMatrix">The right matrix.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool Diagonalize(
            this Complex[,] sourceArray,
             out Complex[,] leftMatrix,
             out Complex[,] diagonalMatrix,
             out Complex[,] rightMatrix)
        {
            try
            {
                // Get the Eigen values of the source
                var eValues = sourceArray.EigenValues2X2();

                // Create diagonal matrix
                diagonalMatrix = sourceArray.ZeroMatrix(); 
                diagonalMatrix[0, 0] = eValues[0];
                diagonalMatrix[1, 1] = eValues[1];

                // Get the Eigen vectors of the source
                var eVectors = sourceArray.EigenVectors2X2();

                // Create the left matrix
                leftMatrix = eVectors[0].AugmentMatrix(eVectors[1]);

                // Create the right matrix ( leftMatrix.InverseMatrix()  )
                rightMatrix = leftMatrix.InverseMatrix();

                // If there have been no exception then return true; otherwise false
                return true;
            }
            catch (Exception)
            {
                leftMatrix = sourceArray.ZeroMatrix();
                diagonalMatrix = sourceArray.ZeroMatrix();
                rightMatrix = sourceArray.ZeroMatrix();
                return false;
            }
        }        

        /// <summary>The is diagonalizablel.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsDiagonalizable(this Complex[,] sourceArray)
        {
            Complex[,] leftMatrix;

            Complex[,] diagonalMatrix;

            Complex[,] rightMatrix;

            return sourceArray.Diagonalize(out leftMatrix, out diagonalMatrix, out rightMatrix);
        }

        /// <summary>The is involution matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns true if the matrix is equal to its inverse.</returns>
        public static bool IsInvolutionMatrix(this Complex[,] sourceArray)
        {
            // An involutory matrix, is a matrix that is its own inverse.
            return sourceArray.InverseMatrix().IsEqual(sourceArray);
        }

        /// <summary>The is nil potent for specified index.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="degreeOrIndex">The degree or index.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsNilPotentForSpecifiedIndex(this Complex[,] sourceArray, int degreeOrIndex)
        {
            return sourceArray.Pow(degreeOrIndex).IsNullMatrix();
        }

        /// <summary>Is the matrix nil potent for an index range.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="toIndex">The to index.</param>
        /// <param name="fromIndex">The from index.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsNilPotentForIndexRange(this Complex[,] sourceArray, int toIndex, int fromIndex = 1)
        {
            var isNil = true;
            for (var n = fromIndex; n <= toIndex; n++)
            {
                isNil = isNil && sourceArray.Pow(n).IsNullMatrix();
            }

            return isNil;
        }

        /// <summary>The nil-potent function.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>True, False or undetermined. Undertermined means that there may be a higher power of M that is null.</returns>
        public static bool? IsNilPotent(this Complex[,] sourceArray)
        {
            if (IsNotSquare(sourceArray))
            {
                return false; // only a square matrix can be a Nil-Potent matrix
            }

            if (sourceArray.IsTriangularMatrix() == TriangularType.NullMatrix)
            {
                return true;
            }

            if (sourceArray.IsTriangularMatrix() == TriangularType.LowerTriangular)
            {
                return true;
            }

            if (sourceArray.IsTriangularMatrix() == TriangularType.UpperTriangular)
            {
                return true;
            }

            if (sourceArray.MultiplyMatrix(sourceArray).IsZeroMatrix())
            {
                return true;
            }

            return null;
        }

        /// <summary>This returns a flag identifying if the matrix is upper-triangular or lower-triangular. In addition it will identify Zero, Diagonal or Identity matrices.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The <see cref="TriangularType"/>.</returns>
        public static TriangularType IsTriangularMatrix(this Complex[,] sourceArray)
        {
            // TriangularMatrix
            if (IsNotSquare(sourceArray))
            {
                return TriangularType.NotTrangular; // only a square matrix can be a Triangular matrix
            }

            if (sourceArray.IsZeroMatrix())
            {                
                return TriangularType.NullMatrix;
            }

            if (sourceArray.IsDiagonal())
            {
                return TriangularType.DiagonalMatrix;
            }

            if (sourceArray.IsIdentity())
            {
                return TriangularType.IdentityMatrix;
            }

            var s = sourceArray.GetUpperBound(Rows) + 1;

            // Check Upper...
            var checkUpper = true;

            // Note: row starts at 1
            for (var r = 0; r < s; r++)
            {
                for (var c = 0; c <= r; c++)
                {
                    if (sourceArray[r, c] != Complex.Zero)
                    {
                        checkUpper = false;
                        break;
                    }
                }

                if (checkUpper == false)
                {
                    break;
                }
            }

            if (checkUpper)
            {
                return TriangularType.UpperTriangular;
            }

            // Check Lower...
            var checkLower = true;

            for (var r = 0; r < s; r++)
            {
                // Note: column starts at r+1
                for (var c = r; c < s; c++)
                {
                    if (sourceArray[r, c] != Complex.Zero)
                    {
                        checkLower = false;
                        break;
                    }
                }

                if (checkLower == false)
                {
                    break;
                }
            }

            if (checkLower)
            {
                return TriangularType.LowerTriangular;
            }

            return TriangularType.NotTrangular;
        }

        /// <summary>The complex sum of the elements on the main diagonal of a matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        /// <exception cref="Exception">Trace can only be calculated from a square matrix</exception>
        public static Complex Trace(this Complex[,] sourceArray)
        {
            if (IsNotSquare(sourceArray))
            {
                throw new Exception("A trace can only be calculated from a square matrix.");
            }

            var itemCount = sourceArray.GetUpperBound(Rows) + 1;

            var trace = Complex.Zero;
            for (var i = 0; i < itemCount; i++)
            {
                trace = trace.AddComplex(sourceArray[i, i]);
            }

            return trace;
        }

        /// <summary>The determinant.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The <see cref="int"/>.</returns>
        /// <exception cref="Exception">A matrix determinant can only be calculated for a 2x2 or a 3x3 matrix</exception>
        public static Complex Determinant(this Complex[,] sourceArray)
        {
            if (IsNotSquare(sourceArray))
            {
                throw new Exception("Only a square matrix can have a determinant.");
            }

            var count = sourceArray.GetUpperBound(Rows) + 1;

            // Determinantn calls Determinant2, so if source is a 2x2 complex matrix then go straight to Determinant2
            return count == 2 ? Determinant2(sourceArray) : Determinantn(sourceArray);
        }

        /// <summary>The transpose.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The Transpose matrix of the source matrix.</returns>
        public static Complex[,] Transpose(this Complex[,] sourceArray)
        {
            var resultArray = new Complex[sourceArray.GetUpperBound(Columns) + 1, sourceArray.GetUpperBound(Rows) + 1];

            for (var r = 0; r <= sourceArray.GetUpperBound(Rows); r++)
            {
                for (var c = 0; c <= sourceArray.GetUpperBound(Columns); c++)
                {
                    resultArray[c, r] = sourceArray[r, c];
                }
            }

            return resultArray;
        }

        /// <summary>The complex conjugate of a complex matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The Conjugate matrix of the source matrix.</returns>
        public static Complex[,] Conjugate(this Complex[,] sourceArray)
        {
            var resultArray = new Complex[sourceArray.GetUpperBound(Columns) + 1, sourceArray.GetUpperBound(Rows) + 1];

            for (var r = 0; r <= sourceArray.GetUpperBound(Rows); r++)
            {
                for (var c = 0; c <= sourceArray.GetUpperBound(Columns); c++)
                {
                    resultArray[r, c] = sourceArray[r, c].Conjugate();
                }
            }

            return resultArray;
        }

        /// <summary>The conjugate transpose matrix. TransposeConjugate and ConjugateTranspose are synonyms.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The Conjugate-Transpose matrix of the source matrix.</returns>
        public static Complex[,] ConjugateTranspose(this Complex[,] sourceArray)
        {
            // The conjugate and transpose operations commute, so             
            // sourceArray.Transpose().Conjugate() = sourceArray.Conjugate().Transpose()
            return sourceArray.Conjugate().Transpose();
        }

        /// <summary>The transpose conjugate matrix. TransposeConjugate and ConjugateTranspose are synonyms.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The Transpose-Conjugate matrix of the source matrix.</returns>
        public static Complex[,] TransposeConjugate(this Complex[,] sourceArray)
        {
            // TransposeConjugate and ConjugateTranspose are synonyms
            return sourceArray.ConjugateTranspose();
        }

        /// <summary>The Adjoint (aka Adjunct) matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The Adjoint (aka Adjunct) if the transpose of the co-factor matrix.</returns>
        public static Complex[,] Adjoint(this Complex[,] sourceArray)
        {
            return sourceArray.Adjunct();
        }

        /// <summary>The adjunct (aka adjoint) matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The Adjunct (aka Adjoint) if the transpose of the co-factor matrix.</returns>
        public static Complex[,] Adjunct(this Complex[,] sourceArray)
        {
            var count = sourceArray.GetUpperBound(Rows) + 1;

            if (IsNotSquare(sourceArray))
            {
                throw new Exception("Only a square matrix can have an adjunt matrix.");
            }

            if (count != 2)
            {
                return sourceArray.CoFactorMatrix().Transpose();
            }

            var resultArray = new Complex[2, 2];

            resultArray[0, 0] = sourceArray[1, 1];
            resultArray[1, 0] = sourceArray[1, 0].Minus();
            resultArray[0, 1] = sourceArray[0, 1].Minus();
            resultArray[1, 1] = sourceArray[0, 0];

            return resultArray;
        }

        /// <summary>The co-factor matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The matrix of co-factors.</returns>
        public static Complex[,] CoFactorMatrix(this Complex[,] sourceArray)
        {
            var count = sourceArray.GetUpperBound(Rows) + 1;

            if (IsNotSquare(sourceArray))
            {
                throw new Exception("Only a square matrix can have an cofactor matrix.");
            }

            var resultArray = new Complex[count, count];

            for (var r = 0; r < count; r++)
            {
                var negative = r % 2 != 0;
                for (var c = 0; c < count; c++)
                {
                    resultArray[r, c] = sourceArray.SubMatrix(r, c).Determinant();
                    if (negative)
                    {
                        resultArray[r, c] = resultArray[r, c].Minus();
                    }

                    negative = negative == false;
                }
            }

            return resultArray;
        }

        /// <summary>Creates a sub-matrix which is one row and one column smaller than the source matrix. For example a 4x4 matrix becomes a 3x3 matrix. This is done by ignoring one row and one column.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="elementRow">The row (zero based) to be removed.</param>
        /// <param name="elementColumn">The column (zero based) to be removed.</param>
        /// <returns>The a sub-matrix of the source matrix.</returns>
        /// <exception cref="Exception">Exceptions for size.</exception>
        public static Complex[,] SubMatrix(this Complex[,] sourceArray, int elementRow, int elementColumn)
        {
            var rowCount = sourceArray.GetUpperBound(Rows) + 1;
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            if (IsNotSquare(sourceArray))
            {
                throw new Exception("A sub-matrix can only be created from a square matrix.");
            }

            if (rowCount < 3)
            {
                throw new Exception("A sub-matrix can only be created from a matrix greater or equal to 3x3.");
            }

            if ((elementRow >= rowCount) || (elementRow < 0))
            {
                throw new Exception("Selected row to remove is out of range (should be 0 to " + (rowCount - 1) + ").");
            }

            if ((elementColumn >= colCount) || (elementColumn < 0))
            {
                throw new Exception("Selected column to remove is out of range (should be 0 to " + (colCount - 1) + ").");
            }

            var resultArray = new Complex[rowCount - 1, colCount - 1];

            var rr = 0;
            var cc = 0;

            for (var r = 0; r < rowCount; r++)
            {
                if (r == elementRow)
                {
                    continue;
                }

                for (var c = 0; c < colCount; c++)
                {
                    if (c == elementColumn)
                    {
                        continue;
                    }
                    
                    resultArray[rr, cc] = sourceArray[r, c];

                    cc++;
                    if (cc > colCount - 2)
                    {
                        cc = 0;
                        rr++;
                    }
                }
            }

            return resultArray;
        }

        /// <summary>Creates a minor matrix. A minor matrix is one row and one column smaller than the source matrix. For example a 4x4 matrix becomes a 3x3 matrix. This is done by ignoring the top row (row 0) and a selected column.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="columnToRemove">The column to remove.</param>
        /// <returns>The minor matrix of the source matrix.</returns>
        /// <exception cref="Exception">Exceptions for size conditions.</exception>
        public static Complex[,] MinorMatrix(this Complex[,] sourceArray, int columnToRemove)
        {
            // Creates a minor matrix 
            // A minor matrix is one row and one column smaller than the source matrix.
            // For example a 4x4 matrix becomes a 3x3 matrix.
            // This is done by ignoring the top row (row 0) and a selected column (0 to colCount-1).            
            var rowCount = sourceArray.GetUpperBound(Rows) + 1;
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            if (IsNotSquare(sourceArray))
            {
                throw new Exception("A Minor matrix can only be created from a square matrix.");
            }

            if (rowCount < 3)
            {
                throw new Exception("A Minor matrix can only be created from a matrix greater or equal to 3x3.");
            }

            if ((columnToRemove > colCount) || (columnToRemove < 0))
            {
                throw new Exception("Selected column to remove is out of range.");
            }

            return sourceArray.SubMatrix(0, columnToRemove);
        }

        /// <summary>The inverse matrix of the source matrix. A matrix multiplied by its own inverse is an identity matrix.</summary>
        /// <param name="sourceArray">The source matrix.</param>
        /// <returns>The functional inverse matrix of the source matrix.</returns>
        /// <exception cref="Exception">Exception for size.</exception>
        public static Complex[,] InverseMatrix(this Complex[,] sourceArray)
        {
            // A^-1 = 1/|A| x adj(A)
            if (IsNotSquare(sourceArray))
            {
                throw new Exception("Only a square matrix can have an inverse matrix.");
            }

            var detSource = sourceArray.Determinant();

            if (detSource == Complex.Zero)
            {
                throw new Exception("The inverse is undefined (determinant is zero).");
            }

            // The inverse of A = 1/|A| x adj(A)
            return sourceArray.Adjunct().MultiplyMatrixByConstant(detSource.Reciprocal());
        }

        /// <summary>Augment a matrix with the identity of the same size. M = { A|I }.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>For a (n x n) matrix returns a (n x 2n) matrix where the right hand side is the appropriate Identity matrix.</returns>
        public static Complex[,] AugmentMatrixWithIdentity(this Complex[,] sourceArray)
        {
            return sourceArray.JoinMatrixToIdentity();
        }

        /// <summary>Join a matrix to the identity of the same size. M = { A|I }.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>For a (n x n) matrix returns a (n x 2n) matrix where the right hand side is the appropriate Identity matrix.</returns>
        /// <exception cref="Exception">Only a square matrix can have a Join matrix</exception>
        public static Complex[,] JoinMatrixToIdentity(this Complex[,] sourceArray)
        {
            if (IsNotSquare(sourceArray))
            {
                throw new Exception("Only a square matrix can have a augmented matrix.");
            }

            return sourceArray.AugmentMatrix(sourceArray.IdentityMatrix());
        }

        /// <summary>The right matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>For a (m x n) matrix returns a (m x n/2) matrix where the returned matrix is the right hand side of the source matrix.</returns>
        /// <exception cref="Exception">Must be even column number exception</exception>
        public static Complex[,] RightMatrix(this Complex[,] sourceArray)
        {
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            if (colCount % 2 != 0)
            {
                throw new Exception("To return a right-matrix there must be an even number of columns.");
            }

            var rowCount = sourceArray.GetUpperBound(Rows) + 1;

            var halfColCount = colCount / 2;

            var resultArray = new Complex[rowCount, halfColCount];

            for (var r = 0; r < rowCount; r++)
            {
                for (var c = 0; c < halfColCount; c++)
                {
                    resultArray[r, c] = sourceArray[r, halfColCount + c];
                }
            }

            return sourceArray;            
        }

        /// <summary>The left matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>For a (m x n) matrix returns a (m x n/2) matrix where the returned matrix is the left hand side of the source matrix.</returns>
        /// <exception cref="Exception">Must be even column number exception</exception>
        public static Complex[,] LeftMatrix(this Complex[,] sourceArray)
        {
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            if (colCount % 2 != 0)
            {
                throw new Exception("To return a left-matrix there must be an even number of columns.");
            }

            var rowCount = sourceArray.GetUpperBound(Rows) + 1;

            var halfColCount = colCount / 2;

            var resultArray = new Complex[rowCount, halfColCount];

            for (var r = 0; r < rowCount; r++)
            {
                for (var c = 0; c < halfColCount; c++)
                {
                    resultArray[r, c] = sourceArray[r, c];
                }
            }

            return sourceArray;
        }

        /// <summary>The 2D rotation matrix.</summary>
        /// <param name="angle">The angle to rotate.</param>
        /// <param name="units">The units.</param>
        /// <returns>The matrix to apply to rotate a point in 2D about the origin.</returns>
        public static double[,] RotationMatrix(this double angle, AngleUnits units = AngleUnits.Degrees)
        {
            var radians = angle * (units == AngleUnits.Degrees ? Math.PI / 180D : 1D);

            var resultArray = new double[2, 2];
            resultArray[0, 0] = Math.Cos(radians);
            resultArray[0, 1] = Math.Sin(radians);
            resultArray[1, 0] = -Math.Sin(radians);
            resultArray[1, 1] = Math.Cos(radians);

            return resultArray;
        }

        /// <summary>Finds the Eigen value of a 2x2 matrix.</summary>
        /// <param name="sourceArray">The 2x2 source array.</param>
        /// <returns>The the two Eigen values of a 2x2 matrix.</returns>
        public static List<Complex> EigenValues2X2(this Complex[,] sourceArray)
        {
            if (IsNotSquare2X2(sourceArray))
            {
                throw new Exception("The current code can only find Eigen values for a 2x2 matrix.");
            }

            // Finds the Eigen value of a 2x2 matrix
            var b = sourceArray.GetElement(0, 0).AddComplex(sourceArray.GetElement(1, 1)).Negate(); // -sourceArray.Trace()
            var c = sourceArray.Determinant2();

            // Sqrt(b^2 - 4ac)
            var t1 = b.MultiplyBy(b).SubtractComplex(c.MultiplyBy(4D)).Pow(0.5);

            // [-b + Sqrt(b^2 - 4ac)]  / 2
            var lambda1 = b.Minus().AddComplex(t1).DivideBy(2D);

            // [-b - Sqrt(b^2 - 4ac)]  / 2
            var lambda2 = b.Minus().SubtractComplex(t1).DivideBy(2D);

            return new List<Complex> { lambda1, lambda2 };
        }

        /// <summary>The Eigen vectors of a 2x2 matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The two 2x1 vector matrices of a 2x2 matrix.</returns>
        public static List<Complex[,]> EigenVectors2X2(this Complex[,] sourceArray)
        {
            if (IsNotSquare2X2(sourceArray))
            {
                throw new Exception("The current code can only find Eigen vectors for a 2x2 matrix.");
            }

            // Declare the two vectors that we are going to return
            // Note that these are 2x1 arrays. Use ToVector to convert to a 1D vector.
            var eVector1 = new Complex[2, 1];
            var eVector2 = new Complex[2, 1];

            // Get elements b and c
            var elementB = sourceArray[0, 1];
            var elementC = sourceArray[1, 0];

            // If b and c are zero...
            if (elementB.IsZero() && elementC.IsZero())
            {
                eVector1[0, 0] = new Complex(1, 0);
                eVector1[1, 0] = 0;

                eVector2[0, 0] = 0;
                eVector2[1, 0] = new Complex(1, 0);

                return new List<Complex[,]> { eVector1, eVector2 };
            }

            // Get elements a and d
            var elementA = sourceArray[0, 0];
            var elementD = sourceArray[1, 1];

            // Get the Eigen values
            var lambdas = sourceArray.EigenValues2X2();

            // If c is NOT zero...
            if (!elementC.IsZero())
            {
                eVector1[0, 0] = lambdas[0].SubtractComplex(elementD);
                eVector1[1, 0] = elementC;

                eVector2[0, 0] = lambdas[1].SubtractComplex(elementD);
                eVector2[1, 0] = elementC;
            }

            // If b is NOT zero...
            if (!elementB.IsZero())
            {
                eVector1[0, 0] = elementB;
                eVector1[1, 0] = lambdas[0].SubtractComplex(elementA);

                eVector2[0, 0] = elementB;
                eVector2[1, 0] = lambdas[1].SubtractComplex(elementA);
            }

            double gcd;
            eVector1 = eVector1.ReduceMatrixToLowestTerms(out gcd);
            eVector2 = eVector2.ReduceMatrixToLowestTerms(out gcd);

            // Return the vectors as a list
            return new List<Complex[,]> { eVector1, eVector2 };
        }

        /// <summary>Augment a matrix with another. M = { A | B }.</summary>
        /// <param name="matrixToBeAugmented">The matrix to be augmented.</param>
        /// <param name="matrixToAugmentWith">The matrix to augment with.</param>
        /// <returns>The augmented matrix.</returns>
        public static Complex[,] AugmentMatrix(this Complex[,] matrixToBeAugmented, Complex[,] matrixToAugmentWith)
        {
            return matrixToBeAugmented.RightJoinTwoMatrices(matrixToAugmentWith);
        }

        /// <summary>Join a second complex matrix on to the right of the first.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="matrixToRightJoin">The matrix to right join.</param>
        /// <returns>The joined matrix.</returns>
        /// <exception cref="Exception">same number of rows</exception>
        public static Complex[,] RightJoinTwoMatrices(this Complex[,] sourceArray, Complex[,] matrixToRightJoin)
        {
            var rowCount = sourceArray.GetUpperBound(Rows) + 1;
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            if (rowCount != matrixToRightJoin.GetUpperBound(Rows) + 1)
            {
                throw new Exception("Matrix to join must have the same number of rows.");
            }

            var colCountM = matrixToRightJoin.GetUpperBound(Columns) + 1;

            var returnArray = new Complex[rowCount, colCount + colCountM];

            for (var r = 0; r < rowCount; r++)
            {
                // Put the sour array on the left
                for (var c = 0; c < colCount; c++)
                {
                    returnArray[r, c] = sourceArray[r, c];
                }

                // Put the other matrix on the right
                for (var c = 0; c < colCountM; c++)
                {
                    returnArray[r, colCount + c] = matrixToRightJoin[r, c];
                }
            }

            return returnArray;
        }

        /// <summary>Convert a string of the form '[ i, 0 ; 2 + i, 3 - i2 ]' to a complex matrix.</summary>
        /// <param name="stringMatrix">The string matrix.</param>
        /// <returns>The a complex matrix of the appropriate dimensions.</returns>
        /// <exception cref="Exception">Cannot convert string to a complex matrix.</exception>
        public static Complex[,] ConvertStringToMatrix(this string stringMatrix)
        {
            // Strings of the form, "[ i, 0 ; 2 + i, 3 - i2 ]" are converted to matrices.
            // Square and round brackets are allowed but ignored.
            // Semi-colons are row delimiters.
            // Commas are value delimiters.
            stringMatrix = stringMatrix.Replace("]", string.Empty).Replace("[", string.Empty).Trim();
            stringMatrix = stringMatrix.Replace(")", string.Empty).Replace("(", string.Empty).Trim();

            var rowsOfValues = stringMatrix.Split(';').ToList();

            var rowCount = rowsOfValues.Count();
            var colCount = rowsOfValues[0].Split(',').Count();

            var returnArray = new Complex[rowCount, colCount];

            for (var r = 0; r < rowCount; r++)
            {
                var values = rowsOfValues[r].Split(',');

                if (values.Count() != colCount)
                {
                    throw new Exception("Cannot convert string to " + (rowCount == colCount ? "square" : "rectangular") + " matrix. Please check row: [" + rowsOfValues[r] + "]");
                }

                for (var c = 0; c < colCount; c++)
                {
                    var item = values[c].Trim();

                    Complex complex;

                    if (!item.TryParse(out complex))
                    {
                        throw new Exception("Cannot convert string to a complex matrix due to invalid value (" + item + ").");
                    }

                    returnArray[r, c] = complex;
                }
            }

            return returnArray;
        }

        /// <summary>The reduce matrix to lowest terms.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="greatestCommonDivisor">The greatest common divisor.</param>
        /// <returns>Returns the reduced matrix and the Greatest Common Divisor</returns>
        public static Complex[,] ReduceMatrixToLowestTerms(this Complex[,] sourceArray, out double greatestCommonDivisor)
        {
            greatestCommonDivisor = 1;

            // Are all the elements integers?
            if (AllElementsAreIntegers(sourceArray))
            {
                // List all the prime numbers of the lowest element.
                var low = sourceArray.FindLowestElementInMatrix();

                if (Math.Abs((int)(low - 1)) < 1E-10)
                {
                    // If one of the elements is 1 there won't be a Greatest Common Divisor > 1
                    return sourceArray;
                }

                var primes = ListOfPrimeFactorsOfN(low);

                foreach (var p in primes)
                {
                    var rowCount = sourceArray.GetUpperBound(Rows) + 1;
                    var colCount = sourceArray.GetUpperBound(Columns) + 1;

                    // Test all the elements for divisability.
                    var canBeDividedByP = true;

                    while (canBeDividedByP)
                    {
                        for (var r = 0; r < rowCount; r++)
                        {
                            for (var c = 0; c < colCount; c++)
                            {
                                var real = sourceArray[r, c].Real / p;
                                if (Math.Abs(real - Math.Floor(real)) > 1E-10)
                                {
                                    canBeDividedByP = false;
                                    break;
                                }

                                var imaginary = sourceArray[r, c].Imaginary / p;
                                if (Math.Abs(imaginary - Math.Floor(imaginary)) > 1E-10)
                                {
                                    canBeDividedByP = false;
                                    break;
                                }
                            }

                            if (!canBeDividedByP)
                            {
                                break;
                            }
                        }

                        // If the current prime number can divide all the elements then...
                        if (canBeDividedByP)
                        {
                            // Multiply all the strored prime numbers together and return the product (greatestCommonDivisor).
                            greatestCommonDivisor = greatestCommonDivisor * p;

                            // Divide all the element by the current prime number.
                            for (var r = 0; r < rowCount; r++)
                            {
                                for (var c = 0; c < colCount; c++)
                                {
                                    sourceArray[r, c] = sourceArray[r, c] / new Complex(p, 0);
                                }
                            }
                        }
                    }
                }
            }

            return sourceArray; // Return the divided through matrix.
        }

        /// <summary>The find lowest element in matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double FindLowestElementInMatrix(this Complex[,] sourceArray)
        {
            var rowCount = sourceArray.GetUpperBound(Rows) + 1;
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            var value = double.MaxValue;

            for (var r = 0; r < rowCount; r++)
            {
                for (var c = 0; c < colCount; c++)
                {
                    if (Math.Abs(sourceArray[r, c].Real) > 0 && Math.Abs(sourceArray[r, c].Real) < value)
                    {
                        value = Math.Abs(sourceArray[r, c].Real);
                    }

                    if (Math.Abs(sourceArray[r, c].Imaginary) > 0 && Math.Abs(sourceArray[r, c].Imaginary) < value)
                    {
                        value = Math.Abs(sourceArray[r, c].Imaginary);
                    }
                }
            }

            return value;
        }

        /// <summary>The determinant of a 2x2 complex matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The complex determinant of a 2x2 complex matrix.</returns>
        private static Complex Determinant2(this Complex[,] sourceArray)
        {
            var determinant2X2Part1 = sourceArray[0, 0].MultiplyBy(sourceArray[1, 1]);
            var determinant2X2Part2 = sourceArray[0, 1].MultiplyBy(sourceArray[1, 0]);

            return determinant2X2Part1.SubtractComplex(determinant2X2Part2);
        }

        /// <summary>The determinant of a (n x n) complex matrix (where n > 1).</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The complex determinant of a complex matrix.</returns>
        private static Complex Determinantn(this Complex[,] sourceArray)
        {
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            // We have previously checked that sourceArray is a square matrix
            if (colCount < 2)
            {
                // 1 x 1 matrix. Determinant is the one value.
                return sourceArray.GetElement(0, 0);
            }

            if (colCount == 2)
            {
                // 2x2 matrix. Use the Determinant2 function
                return Determinant2(sourceArray);
            }

            // Recurse through all the sub-matrices until we have a 2x2 matrix. Then use the Determinant2
            // function to solve this. Add/subtract all the determinants on the way.
            var determinant = Complex.Zero;

            for (var c = 0; c < colCount; c++)
            {
                // If  sourceArray[0, c] is zero then we don't have to extract the minor matrix or 
                // calculate its determinant.
                if (sourceArray[0, c] != Complex.Zero)
                {
                    var det = Determinantn(sourceArray.MinorMatrix(c)).MultiplyBy(sourceArray[0, c]);

                    // Add or subtract (as appropraite) the determinant to/from the running total.
                    determinant = determinant.AddComplex((c % 2 == 1) ? det.Minus() : det);
                }                                
            }

            return determinant;
        }

        /// <summary>The all elements, including both parts of each complex number, are integers.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool AllElementsAreIntegers(Complex[,] sourceArray)
        {
            var rowCount = sourceArray.GetUpperBound(Rows) + 1;
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            for (var r = 0; r < rowCount; r++)
            {
                for (var c = 0; c < colCount; c++)
                {
                    if (Math.Abs(sourceArray[r, c].Real - Math.Floor(sourceArray[r, c].Real)) > 1E-10 
                        ||
                        Math.Abs(sourceArray[r, c].Imaginary - Math.Floor(sourceArray[r, c].Imaginary)) > 1E-10)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>The list of prime numbers less than, or equal to, n.</summary>
        /// <param name="n">The limit of primes.</param>
        /// <returns>A list of primes.</returns>
        /// <exception cref="ArgumentOutOfRangeException">A prime number has to be a positive number greater than 1</exception>
        private static IEnumerable<double> ListOfPrimeNumbersLessThanN(double n)
        {
            if (n < 2)
            {
                throw new ArgumentOutOfRangeException("n", "A prime number has to be a positive number greater than 1.");
            }

            var primes = new List<double> { 2D };

            if (Math.Abs(n - 2) < 1E-10)
            {
                return primes;
            }

            var primeness = new Primeness();

            var i = 3D;
            while (i <= n)
            {
                if (primeness.IsPrime((int)i))
                {
                    primes.Add(i);
                }

                i = i + 2;
            }

            return primes;
        }

        /// <summary>The list of prime factors of n.</summary>
        /// <param name="n">The n.</param>
        /// <returns>Returns the list of prime factors of n.</returns>
        private static IEnumerable<double> ListOfPrimeFactorsOfN(double n)
        {
            var primes = ListOfPrimeNumbersLessThanN(n);
            
            var primeFactors = new List<double>();

            foreach (var p in primes)
            {
                if (p > n)
                {
                    break;
                }

                var notAlreadyAdded = true;
                while (true)
                {
                    if (Math.Abs((n / p) - Math.Floor(n / p)) < 1E-10)
                    {
                        // p is a factor of n
                        n = n / p;
                        if (notAlreadyAdded)
                        {
                            primeFactors.Add(p);
                        }
                    }
                    else
                    {
                        break;
                    }

                    notAlreadyAdded = false;
                }
            }

            return primeFactors;
        }

        /// <summary>Is the matrix a n x n square matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns true if square; else false.</returns>
        private static bool IsSquare(Complex[,] sourceArray)
        {
            var rowCount = sourceArray.GetUpperBound(Rows) + 1;
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            return rowCount == colCount;
        }

        /// <summary>The is not square.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool IsNotSquare(Complex[,] sourceArray)
        {
            return IsSquare(sourceArray) == false;
        }

        /// <summary>Is the matrix a 2 x 2 square matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>Returns true if the matrix is 2 rows by 2 columns; else false.</returns>
        private static bool IsSquare2X2(Complex[,] sourceArray)
        {
            var rowCount = sourceArray.GetUpperBound(Rows) + 1;
            var colCount = sourceArray.GetUpperBound(Columns) + 1;

            return rowCount == colCount && rowCount == 2;
        }

        /// <summary>The is not square 2 x 2.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool IsNotSquare2X2(Complex[,] sourceArray)
        {
            return IsSquare2X2(sourceArray) == false;
        }
    }
}
