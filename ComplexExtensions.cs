// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComplexExtensions.cs" company="Azven">
//   (C) Steven Digby
// </copyright>
// <summary>
//   Defines the ComplexExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Matrix2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Numerics;

    /// <summary>The complex extensions.</summary>
    public static class ComplexExtensions
    {
        /// <summary>The string formats.</summary>
        public enum StringFormats
        {
            /// <summary>The cartesian format ( X + iY ).</summary>
            Cartesian = 0,

            /// <summary>The polar format ( RxCos(Q) + iRxSin(Q) ).</summary>
            Polar = 1
        }

        /// <summary>The conjugate.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <returns>The complex conjugate value.</returns>
        public static Complex Conjugate(this Complex complexNumber)
        {
            return new Complex(complexNumber.Real, -complexNumber.Imaginary);
        }

        /// <summary>Raise a complex number to the power of a numerical index.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="index">The index.</param>
        /// <returns>The complex return value.</returns>
        public static Complex Pow(this Complex complexNumber, double index)
        {
            var complexIndex = new Complex(index, 0);
            return Complex.Pow(complexNumber, complexIndex);
        }

        /// <summary>Raise a complex number to the power of a complex index.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="complexIndex">The complex index.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex Pow(this Complex complexNumber, Complex complexIndex)
        {
            return Complex.Pow(complexNumber, complexIndex);
        }

        /// <summary>The xth root of a complex number: 2 = square root, 3 = cube root, etc.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="x">The root: 2 = square root.</param>
        /// <returns>The enumerable list of complex roots.</returns>
        /// <exception cref="OverflowException">OverflowException: Cannot find the 0 root.</exception>        
        public static IEnumerable<Complex> Root(this Complex complexNumber, int x)
        {
            if (x == 0)
            {
                throw new OverflowException("Cannot find the 0th root.");
            }

            var root = 1 / (double)x;
            var phaseAdjust = 2 * Math.PI / x;

            var magnitude = Math.Pow(complexNumber.Magnitude, root);
            var phase = complexNumber.Phase * root;

            for (var i = 0; i < x; i++)
            {
                var theta = phase + (i * phaseAdjust);

                var real = magnitude * Math.Cos(theta);
                var imaginary = magnitude * Math.Sin(theta);

                yield return new Complex(real, imaginary);
            }
        }

        /// <summary>The square.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <returns>The complex square value of the complex numeber.</returns>
        public static Complex Square(this Complex complexNumber)
        {
            return complexNumber.MultiplyBy(complexNumber);
        }

        /// <summary>The first square root of a complex numner.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <returns>The first square complex root value of the complex number.</returns>
        public static Complex SquareRoot(this Complex complexNumber)
        {
            return Complex.Pow(complexNumber, new Complex(0.5, 0));
        }

        /// <summary>The multiply by.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="anotherComplexNumber">The another complex number.</param>
        /// <returns>The complex product of two complex numbers.</returns>
        public static Complex MultiplyBy(this Complex complexNumber, Complex anotherComplexNumber)
        {
            // (a + ib)
            var a = complexNumber.Real;
            var b = complexNumber.Imaginary;

            // (complexIndex + id)
            var c = anotherComplexNumber.Real;
            var d = anotherComplexNumber.Imaginary;

            // (a + ib) index (complexIndex + id)  =  (ac - bd) + i(ad + bc)
            var real = (a * c) - (b * d);
            var imaginary = (a * d) + (b * c);

            return new Complex(real, imaginary);
        }

        /// <summary>The multiply by.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="constant">The constant.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex MultiplyBy(this Complex complexNumber, double constant)
        {
            return new Complex(constant * complexNumber.Real, constant * complexNumber.Imaginary);
        }

        /// <summary>The divide by.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="anotherComplexNumber">Another complex number.</param>
        /// <returns>The complex return value.</returns>
        /// <exception cref="DivideByZeroException">Cannot divide by (0 + i0).</exception>
        public static Complex DivideBy(this Complex complexNumber, Complex anotherComplexNumber)
        {
            // (a + ib)
            var a = complexNumber.Real;
            var b = complexNumber.Imaginary;

            // (complexIndex + id)
            var c = anotherComplexNumber.Real;
            var d = anotherComplexNumber.Imaginary;

            if (Math.Abs((c * c) + (d * d)) < 1E-10)
            {
                throw new DivideByZeroException("Cannot divide by (0 + i0).");
            }
            
            // (a + ib) / (complexIndex + id)  =  [ (a + ib) index (complexIndex - id) ] / [ (complexIndex + id) index (complexIndex - id) ]
            //                      =  [ (ac + bd) / (complexIndex² + d²) ] + i [ (bc - ad) / (complexIndex² + d²) ]
            var real = ((a * c) + (b * d)) / ((c * c) + (d * d));
            var imaginary = ((b * c) + (a * d)) / ((c * c) + (d * d));

            return new Complex(real, imaginary);
        }

        /// <summary>The divide by.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="constant">The constant.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        /// <exception cref="DivideByZeroException">Cannot divide by zero.</exception>
        public static Complex DivideBy(this Complex complexNumber, double constant, double tolerance = 1E-10)
        {
            if (Math.Abs(constant) < tolerance)
            {
                throw new DivideByZeroException("Cannot divide a complex number by a real number when that number is zero.");
            }

            return new Complex(complexNumber.Real / constant, complexNumber.Imaginary / constant);
        }

        /// <summary>The inverse (synonym of Reciprocal).</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <returns>The complex value of one over a complex number.</returns>
        public static Complex Inverse(this Complex complexNumber)
        {
            return Complex.Reciprocal(complexNumber);
        }

        /// <summary>The reciprocal (synonym of Inverse).</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex Reciprocal(this Complex complexNumber)
        {
            return Complex.Reciprocal(complexNumber);
        }

        /// <summary>The add complex.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="anotherComplexNumber">The another complex number.</param>
        /// <returns>The complex sum of two complex numbers.</returns>
        public static Complex AddComplex(this Complex complexNumber, Complex anotherComplexNumber)
        {
            // (a + ib)
            var a = complexNumber.Real;
            var b = complexNumber.Imaginary;

            // (complexIndex + id)
            var c = anotherComplexNumber.Real;
            var d = anotherComplexNumber.Imaginary;

            // (a + ib) + (complexIndex + id)  =  (a + complexIndex) + i(b + d)
            var real = a + c;
            var imaginary = b + d;

            return new Complex(real, imaginary);
        }

        /// <summary>The subtract complex.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="anotherComplexNumber">The another complex number.</param>
        /// <returns>The complex return value of one complex number subtracted from another.</returns>
        public static Complex SubtractComplex(this Complex complexNumber, Complex anotherComplexNumber)
        {
            // (a + ib)
            var a = complexNumber.Real;
            var b = complexNumber.Imaginary;

            // (complexIndex + id)
            var c = anotherComplexNumber.Real;
            var d = anotherComplexNumber.Imaginary;

            // (a + ib) - (complexIndex + id)  =  (a - complexIndex) + i(b - d)
            var real = a - c;
            var imaginary = b - d;

            return new Complex(real, imaginary);
        }

        /// <summary>The negative of a comple number. Minus is a synonym of Negate.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <returns>For the complex number (A + iB) this returns (-A - iB).</returns>
        public static Complex Minus(this Complex complexNumber)
        {
            return new Complex(-complexNumber.Real, -complexNumber.Imaginary);
        }

        /// <summary>The negative of a comple number. Negate is a synonym of Minus.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <returns>For the complex number (A + iB) this returns (-A - iB).</returns>
        public static Complex Negate(this Complex complexNumber)
        {
            return complexNumber.Minus();
        }

        /// <summary>The equal to function.</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="anotherComplexNumber">The another complex number.</param>
        /// <param name="tolerance">The optional tolerance value (defaults to 1E-10).</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool EqualTo(this Complex complexNumber, Complex anotherComplexNumber, double tolerance = 1E-10)
        {
            return (Math.Abs(complexNumber.Real - anotherComplexNumber.Real) < tolerance) 
                && (Math.Abs(complexNumber.Imaginary - anotherComplexNumber.Imaginary) < tolerance);
        }

        /// <summary>The complex number is zero (0 + i0).</summary>
        /// <param name="complexNumber">The complex number.</param>
        /// <param name="tolerance">The numerical tolerance for equality.</param>
        /// <returns>The true if the complex number is (0 + i0); otherwise false.</returns>
        public static bool IsZero(this Complex complexNumber, double tolerance = 1E-10)
        {
            return Math.Abs(complexNumber.Real) < tolerance && Math.Abs(complexNumber.Imaginary) < tolerance;
        }

        public static string ImaginaryValue(string imaginaryPart)
        {
            var defaultValue = "0";

            if (imaginaryPart == string.Empty)
            {
                return defaultValue;
            }
            if (!double.TryParse(imaginaryPart, out double imaginaryDouble))
            {
                return defaultValue;
            }

            return Math.Abs(imaginaryDouble) < 1E-10 ? string.Empty : Math.Abs(imaginaryDouble).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>The try parse.</summary>
        /// <param name="stringToTest">The string to test.</param>
        /// <param name="complexNumber">The complex number.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool TryParse(this string stringToTest, out Complex complexNumber)
        {
            var parts = new string[5];

            // defaults (+1 + i0)
            parts[0] = "+";
            parts[1] = "0";
            parts[2] = "+";
            parts[3] = "i";
            parts[4] = "0";          

            complexNumber = new Complex(0, 0);

            var workingStringToTest = new string(stringToTest.Trim().ToLower().ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());

            // Separate the real part from the imaginary part
            var numberPartsArray = workingStringToTest.Split('i');

            var realPart = numberPartsArray[0].Trim(); // The real part           

            if (numberPartsArray.Count() > 2)
            {
                // Too many i's
                return false;
            }

            // Section 1: Imaginary Number
            if (numberPartsArray.Count() > 1)
            {
                var imaginaryPart = numberPartsArray[1].Trim();

                if ((numberPartsArray[0].Trim() == string.Empty || numberPartsArray[0].Trim() == "+") && numberPartsArray[1].Trim() == string.Empty)
                {
                    // Special case for "i" and "+i"
                    complexNumber = new Complex(0, 1);
                    return true;
                }

                if (numberPartsArray[0].Trim() == "-" && numberPartsArray[1].Trim() == string.Empty)
                {
                    // Special case for "-i"
                    complexNumber = new Complex(0, -1);
                    return true;
                }

                var iSign = realPart != string.Empty && realPart.Substring(realPart.Length - 1, 1) == "-" ? "-" : "+";                

                if (imaginaryPart == string.Empty)
                {
                    parts[2] = iSign; 
                    parts[3] = "i";
                    parts[4] = "1"; 
                }
                else
                {
                    double imaginaryDouble;

                    if (!double.TryParse(imaginaryPart, out imaginaryDouble))
                    {
                        return false;
                    }

                    if (Math.Abs(imaginaryDouble) < 1E-10)
                    {
                        parts[2] = string.Empty;
                        parts[3] = string.Empty;
                        parts[4] = string.Empty;
                    }
                    else
                    {
                        var tempSign = @"-++".Substring(Math.Sign(imaginaryDouble) + 1, 1);

                        parts[2] = tempSign == iSign ? "+" : "-";
                        parts[4] = Math.Abs(imaginaryDouble).ToString(CultureInfo.InvariantCulture);                        
                    }
                }
            }

            

            // Section 2: Real Number
            if (realPart != string.Empty)
            {
                if (realPart.Substring(realPart.Length - 1, 1) == "+" || realPart.Substring(realPart.Length - 1, 1) == "-")
                {
                    realPart = realPart.Substring(0, realPart.Length - 1);
                }
            }

            parts[0] = string.Empty;
            parts[1] = string.Empty;

            if (realPart != string.Empty)
            {
                double realDouble;
                if (!double.TryParse(realPart, out realDouble))
                {
                    return false;
                }

                parts[0] = @"-++".Substring(Math.Sign(realDouble) + 1, 1);
                parts[1] = Math.Abs(realDouble).ToString(CultureInfo.InvariantCulture);
            }

            double r;
            if (!double.TryParse(parts[0] + parts[1], out r))
            {
                parts[0] = string.Empty;
                parts[1] = string.Empty;
                r = 0;
            }

            double i;
            if (!double.TryParse(parts[2] + parts[4], out i))
            {
                parts[2] = string.Empty;
                parts[4] = string.Empty;
                i = 0;
            }

            if (parts[1] == string.Empty && parts[4] == string.Empty)
            {
                return false;
            }

            complexNumber = new Complex(r, i);
            return true;
        }

        /// <summary>The to string.</summary>
        /// <param name="complexNumber">The complex Number.</param>
        /// <param name="usePolarCoordinateFormat">Use Argand (Cartesian) format (default); or Polar coordinates</param>
        /// <param name="phaseAngleInRadians">Phase Angle will be in Radians; or else degrees (default).</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ToString(this Complex complexNumber, bool usePolarCoordinateFormat = false, bool phaseAngleInRadians = false)
        {
            // The format can be "polar" or other; ie is boolean.  
            string rtn;

            // X + iY
            // RCos(Q) + iRSin(Q)
            // Re^iQ
            if (usePolarCoordinateFormat)
            {
                if (Math.Abs(complexNumber.Magnitude) < 1E-10)
                {
                    // If the magnitude is zero, it doesn't matter what the phase is.
                    return "0"; // 0 + i0
                }

                var phaseAngle = Math.Abs(complexNumber.Phase * (phaseAngleInRadians ? 1 : (180 / Math.PI))).ToString("##.###");

                // If the magnitude is 1 we don't have to display it
                rtn = Math.Abs(complexNumber.Magnitude - 1) < 1E-10
                          ? string.Empty
                          : complexNumber.Magnitude.ToString("##.###");
                rtn += "Cos(" + phaseAngle + ") ";

                rtn += (Math.Sign(complexNumber.Phase) * Math.Sign(complexNumber.Magnitude)) >= 0 ? "+ i" : "- i";

                // Again, if the magnitude is 1 we don't have to display it
                rtn += Math.Abs(complexNumber.Magnitude - 1) < 1E-10
                          ? string.Empty
                          : Math.Abs(complexNumber.Magnitude).ToString("##.###");
                rtn += "Sin(" + phaseAngle + ")";
            }
            else
            {
                rtn = complexNumber.Real.ToString("##.###");
                if (!(Math.Abs(complexNumber.Imaginary) > 1E-10))
                {
                    // If there's no imaginary part we can return a 'real' number
                    return rtn;
                }

                rtn += Math.Sign(complexNumber.Imaginary) >= 0 ? " + i" : " - i";

                // If the Imaginary part is 1 we don't have to display it
                if (Math.Abs(Math.Abs(complexNumber.Imaginary) - 1) > 1E-10)
                {
                    rtn += Math.Abs(complexNumber.Imaginary).ToString("##.###");
                }
            }

            return rtn;
        }
    }  
}
