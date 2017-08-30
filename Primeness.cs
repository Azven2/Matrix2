// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Primeness.cs" company="">
//   
// </copyright>
// <summary>
//   The "primeness" of a number.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Matrix1
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Numerics;

    /// <summary>The "primeness" of a number.</summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class Primeness
    {
        /// <summary>The is prime.</summary>
        /// <param name="input">The input.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsPrime(int input)
        {            
            if (input == 2 || input == 3)
            {
                return true;
            }

            if (input % 2 == 0)
            {
                return false;
            }

            var listOfPrimesSoFar = new List<int> { 3 };
            var sqrtInput = (int)Math.Sqrt(input);

            for (var p = 5; p < sqrtInput; p += 2)
            {
                if (listOfPrimesSoFar.Aggregate(true, (current, prime) => current && p % prime != 0))
                {
                    listOfPrimesSoFar.Add(p);
                }
            }

            return listOfPrimesSoFar.Aggregate(true, (current, prime) => current && (input % prime != 0));
        }

        /// <summary>The the first n primes.</summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see>
        ///         <cref>List</cref>
        ///     </see>
        /// .</returns>
        public IEnumerable<BigInteger> TheFirstNPrimes(long n)
        {
            if (n <= 0)
            {
                return new List<BigInteger>();
            }

            var listOfPrimesSoFar = new List<BigInteger> { 2 };
            if (n == 1)
            {
                return listOfPrimesSoFar;
            }

            listOfPrimesSoFar.Add(3);         

            BigInteger p = 5;
            while (listOfPrimesSoFar.Count < n)
            {
                if (listOfPrimesSoFar.Aggregate(true, (current, prime) => current && p % prime != 0))
                {                  
                    listOfPrimesSoFar.Add(p);
                }

                p += 2;
            }

            return listOfPrimesSoFar;
        }
    }
}
