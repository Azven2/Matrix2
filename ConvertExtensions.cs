// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConvertExtensions.cs" company="Azven.com">
//   (C) Steven Digby
// </copyright>
// <summary>
//   The convert extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Matrix2
{
    using System;
    using System.Numerics;

    /// <summary>The convert extensions.</summary>
    public static class ConvertExtensions
    {
        /// <summary>The rows.</summary>
        private const int Rows = 0;

        /// <summary>The columns.</summary>
        private const int Columns = 1;

        /// <summary>The convert matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The real part of the complex matrix but as a matrix of doubles.</returns>
        public static double[,] RealConvertToDoubleMatrix(this Complex[,] sourceArray)
        {
            var resultArray = new double[sourceArray.GetUpperBound(Rows) + 1, sourceArray.GetUpperBound(Columns) + 1];

            for (var r = 0; r <= sourceArray.GetUpperBound(Rows); r++)
            {
                for (var c = 0; c <= sourceArray.GetUpperBound(Columns); c++)
                {
                    resultArray[r, c] = sourceArray[r, c].Real;
                }
            }

            return resultArray;
        }

        /// <summary>The imaginary convert to double matrix.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>The imaginary part of the complex matrix but as a matrix of doubles.</returns>
        public static double[,] ImaginaryConvertToDoubleMatrix(this Complex[,] sourceArray)
        {
            var resultArray = new double[sourceArray.GetUpperBound(Rows) + 1, sourceArray.GetUpperBound(Columns) + 1];

            for (var r = 0; r <= sourceArray.GetUpperBound(Rows); r++)
            {
                for (var c = 0; c <= sourceArray.GetUpperBound(Columns); c++)
                {
                    resultArray[r, c] = sourceArray[r, c].Imaginary;
                }
            }

            return resultArray;
        }

        /// <summary>The convert double matrix to real.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>A complex matrix with the real imaginary part set to 0.</returns>
        public static Complex[,] ConvertDoubleMatrixToReal(this double[,] sourceArray)
        {
            var resultArray = new Complex[sourceArray.GetUpperBound(Rows) + 1, sourceArray.GetUpperBound(Columns) + 1];

            for (var r = 0; r <= sourceArray.GetUpperBound(Rows); r++)
            {
                for (var c = 0; c <= sourceArray.GetUpperBound(Columns); c++)
                {
                    resultArray[r, c] = new Complex(sourceArray[r, c], 0);
                }
            }

            return resultArray;
        }

        /// <summary>The convert double matrix to imaginary.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <returns>A complex matrix with the real real part set to 0.</returns>
        public static Complex[,] ConvertDoubleMatrixToImaginary(this double[,] sourceArray)
        {
            var resultArray = new Complex[sourceArray.GetUpperBound(Rows) + 1, sourceArray.GetUpperBound(Columns) + 1];

            for (var r = 0; r <= sourceArray.GetUpperBound(Rows); r++)
            {
                for (var c = 0; c <= sourceArray.GetUpperBound(Columns); c++)
                {
                    resultArray[r, c] = new Complex(0, sourceArray[r, c]);
                }
            }

            return resultArray;
        }

        /// <summary>The merge two double arrays.</summary>
        /// <param name="partRealArray">The part real array.</param>
        /// <param name="partImaginaryArray">The part imaginary array.</param>
        /// <returns>A complex matrix with the real real part from one matrix and the imaginary part from a second matrix.</returns>
        /// <exception cref="Exception">Same dimensions exception</exception>
        public static Complex[,] MergeTwoDoubleArrays(this double[,] partRealArray, double[,] partImaginaryArray)
        {
            if ((partRealArray.GetUpperBound(Rows) != partImaginaryArray.GetUpperBound(Rows))
                || (partRealArray.GetUpperBound(Columns) != partImaginaryArray.GetUpperBound(Columns)))
            {
                throw new Exception(
                    "MergeTwoDoubleArrays can only work when the arrays have the same dimensions.");
            }

            var resultArray = new Complex[partRealArray.GetUpperBound(Rows) + 1, partRealArray.GetUpperBound(Columns) + 1];

            for (var r = 0; r <= partRealArray.GetUpperBound(Rows); r++)
            {
                for (var c = 0; c <= partRealArray.GetUpperBound(Columns); c++)
                {
                    resultArray[r, c] = new Complex(partRealArray[r, c], partImaginaryArray[r, c]);
                }
            }

            return resultArray;
        }
    }
}
