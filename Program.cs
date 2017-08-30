// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Azven.com">
//   (C) Steven Digby
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Matrix2
{
    using System.Numerics;

    /// <summary>The program.</summary>
    public class Program
    {
        /// <summary>The main.</summary>
        /// <param name="args">The args.</param>
        public static void Main(string[] args)
        {
            var complexMatrix1 = new Complex[2, 2];
            complexMatrix1[0, 0] = new Complex(1, 5);
            complexMatrix1[0, 1] = new Complex(2, 3);
            complexMatrix1[1, 0] = new Complex(-1, 3);
            complexMatrix1[1, 1] = new Complex(3, 1);
        }
    }
}
