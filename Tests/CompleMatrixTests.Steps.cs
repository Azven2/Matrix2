// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompleMatrixTests.Steps.cs" company="">
//   
// </copyright>
// <summary>
//   The comple matrix tests steps.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Matrix2.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    using NUnit.Framework;

    using TechTalk.SpecFlow;
   
    /// <summary>The comple matrix tests steps.</summary>
    [Binding]
    public class CompleMatrixTestsSteps
    {
        /// <summary>The a complex number to test.</summary>
        private Complex aComplexNumberToTest;

        /// <summary>The a string number to test.</summary>
        private string aStringNumberToTest;

        /// <summary>The example matrix.</summary>
        private Complex[,] exampleMatrix1;

        /// <summary>The example matrix 2.</summary>
        private Complex[,] exampleMatrix2;

        /// <summary>The actual matrix.</summary>
        private Complex[,] actualMatrix;

        /// <summary>The actual boolean result.</summary>
        private bool? actualBooleanResult;

        /// <summary>The actual string result.</summary>
        private string exampleString;

        /// <summary>The actual double result.</summary>
        private double actualDoubleResult;

        /// <summary>The actual value.</summary>
        private Complex actualComplexResult;

        /// <summary>The expected vector 1.</summary>
        private Complex[,] expectedVector1;

        /// <summary>The example vector 2.</summary>
        private Complex[,] expectedVector2;

        /// <summary>The actual vector 1.</summary>
        private Complex[,] actualVector1;

        /// <summary>The actual vector 2.</summary>
        private Complex[,] actualVector2;

        /// <summary>The actual eigen value 1.</summary>
        private Complex actualEigenValue1;

        /// <summary>The actual eigen value 2.</summary>
        private Complex actualEigenValue2;

        /// <summary>The actual root matrices.</summary>
        private List<Complex[,]> actualRootMatrices = new List<Complex[,]>();

        /// <summary>The triangular type.</summary>
        private ComplexMatrixExtensions.TriangularType triangularType;

        /// <summary>The given a string.</summary>
        /// <param name="inputNumber">The input number.</param>
        [Given(@"a string (.*)")]
        public void GivenAString(string inputNumber)
        {
            this.aStringNumberToTest = inputNumber;
        }

        /// <summary>The given the following matrix.</summary>
        /// <param name="exampleMatrixTable">The example matrix table.</param>
        [Given(@"The following matrix")]
        public void GivenTheFollowingMatrix(Table exampleMatrixTable)
        {
            try
            {
                this.exampleMatrix1 = ConvertTableToMatrix(exampleMatrixTable);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }            
        }

        /// <summary>The given another matrix.</summary>
        /// <param name="exampleMatrixTable">The example matrix table.</param>
        [Given(@"another matrix")]
        public void GivenAnotherMatrix(Table exampleMatrixTable)
        {
            try
            {
                this.exampleMatrix2 = ConvertTableToMatrix(exampleMatrixTable);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>The given the string.</summary>
        /// <param name="stringMatrix">The string matrix.</param>
        [Given(@"The string ""(.*)""")]
        public void GivenTheString(string stringMatrix)
        {
            this.exampleString = stringMatrix;
        }

        /// <summary>The given the index is.</summary>
        /// <param name="index">The index.</param>
        [Given(@"the index is (.*)")]
        public void GivenTheIndexIs(double index)
        {
            this.actualDoubleResult = index;
        }

        // ////////////////////////////////////////////////////

        /// <summary>The when i compare the matrices for equality.</summary>
        [When(@"I compare the matrices for equality")]
        public void WhenICompareTheMatricesForEquality()
        {
            this.actualBooleanResult = this.exampleMatrix1.IsEqual(this.exampleMatrix2);
        }

        /// <summary>The when I parse the string.</summary>
        [When(@"I parse the string")]
        public void WhenIParseTheString()
        {
            this.actualBooleanResult = this.aStringNumberToTest.TryParse(out this.aComplexNumberToTest);
        }

        /// <summary>The when i compare the matrix to a zero matrix.</summary>
        [When(@"I compare the matrix to a zero matrix")]
        public void WhenICompareTheMatrixToAZeroMatrix()
        {
            this.actualBooleanResult = this.exampleMatrix1.IsEqual(this.exampleMatrix1.ZeroMatrix());
        }

        /// <summary>The when i compare the matrix to an identity matrix.</summary>
        [When(@"I compare the matrix to an identity matrix")]
        public void WhenICompareTheMatrixToAnIdentityMatrix()
        {
            this.actualBooleanResult = this.exampleMatrix1.IsEqual(this.exampleMatrix1.IdentityMatrix());
        }

        /// <summary>The when i look_ up element row column.</summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        [When(@"I look-up element row (.*), column (.*)")]
        public void WhenILookUpElementRowColumn(int row, int column)
        {
            this.actualComplexResult = this.exampleMatrix1.GetElement(row - 1, column - 1);
        }

        /// <summary>The when i transpose the matrix.</summary>
        [When(@"I transpose the matrix")]
        public void WhenITransposeTheMatrix()
        {
            this.actualMatrix = this.exampleMatrix1.Transpose();
        }

        /// <summary>The when i add the two matrices.</summary>
        [When(@"I add the two matrices")]
        public void WhenIAddTheTwoMatrices()
        {
            this.actualMatrix = this.exampleMatrix1.AddMatrix(this.exampleMatrix2);
        }

        /// <summary>The when i subtract the second matrix from the first.</summary>
        [When(@"I subtract the second matrix from the first")]
        public void WhenISubtractTheSecondMatrixFromTheFirst()
        {
            this.actualMatrix = this.exampleMatrix1.SubtractMatrix(this.exampleMatrix2);
        }

        /// <summary>The when i multiply the first matrix by the constant.</summary>
        /// <param name="constant">The constant.</param>
        [When(@"I multiply the first matrix by the constant (.*)")]
        public void WhenIMultiplyTheFirstMatrixByTheConstant(double constant)
        {
            this.actualMatrix = this.exampleMatrix1.MultiplyMatrixByConstant(constant);
        }

        /// <summary>The when i multiply the first matrix by the second.</summary>
        [When(@"I multiply the first matrix by the second")]
        public void WhenIMultiplyTheFirstMatrixByTheSecond()
        {
            this.actualMatrix = this.exampleMatrix1.MultiplyMatrix(this.exampleMatrix2);
        }

        /// <summary>The when i extract the submatrix of row column.</summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        [When(@"I extract the submatrix of row (.*) column (.*)")]
        public void WhenIExtractTheSubmatrixOfRowColumn(int row, int column)
        {
            this.actualMatrix = this.exampleMatrix1.SubMatrix(row - 1, column - 1);
        }

        /// <summary>The when i negate the matrix.</summary>
        [When(@"I negate the matrix")]
        public void WhenINegateTheMatrix()
        {
            this.actualMatrix = this.exampleMatrix1.Negate();
        }

        /// <summary>The when i extract the minor matrix of column.</summary>
        /// <param name="column">The column.</param>
        [When(@"I extract the minor matrix of column (.*)")]
        public void WhenIExtractTheMinorMatrixOfColumn(int column)
        {
            this.actualMatrix = this.exampleMatrix1.MinorMatrix(column - 1);
        }

        /// <summary>The when i calculate the determinant.</summary>
        [When(@"I calculate the determinant")]
        public void WhenICalculateTheDeterminant()
        {
            this.actualComplexResult = this.exampleMatrix1.Determinant();
        }

        /// <summary>The when i calculate the inverse matrix.</summary>
        /// <param name="operatorType">The operator Type.</param>
        [When(@"I calculate the (.*) matrix")]
        public void WhenICalculateTheMatrix(string operatorType)
        {
            switch (operatorType.ToLower())
            {
                case "inverse":
                    this.actualMatrix = this.exampleMatrix1.InverseMatrix();
                    break;
                case "adjunct":
                    this.actualMatrix = this.exampleMatrix1.Adjunct(); 
                    break;
                case "cofactor":
                    this.actualMatrix = this.exampleMatrix1.CoFactorMatrix(); 
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }

        /// <summary>The when i divide the first matrix by the second.</summary>
        [When(@"I divide the first matrix by the second")]
        public void WhenIDivideTheFirstMatrixByTheSecond()
        {
            this.actualMatrix = this.exampleMatrix1.DivideMatrix(this.exampleMatrix2)[0];
        }

        /// <summary>The when i convert the string to a matrix.</summary>
        [When(@"I convert the string to a matrix")]
        public void WhenIConvertTheStringToAMatrix()
        {
            this.actualMatrix = this.exampleString.ConvertStringToMatrix();
        }

        /// <summary>The when i get the trace of the matrix.</summary>
        [When(@"I get the trace of the matrix")]
        public void WhenIGetTheTraceOfTheMatrix()
        {
            this.actualComplexResult = this.exampleMatrix1.Trace();
        }

        /// <summary>The when i raise the matrix to the power of.</summary>
        /// <param name="index">The index.</param>
        [When(@"I raise the matrix to the power of (.*)")]
        public void WhenIRaiseTheMatrixToThePowerOf(double index)
        {
            this.actualMatrix = this.exampleMatrix1.Pow(index);
        }

        /// <summary>The when i get the eigen value.</summary>
        [When(@"I get the Eigen value")]
        public void WhenIGetTheEigenValue()
        {
            var eValues = this.exampleMatrix1.EigenValues2X2();

            this.actualEigenValue1 = eValues[0];

            this.actualEigenValue2 = eValues[1];
        }

        /// <summary>The when i get the eigen vectors.</summary>
        [When(@"I get the Eigen vectors")]
        public void WhenIGetTheEigenVectors()
        {
            var eVectors = this.exampleMatrix1.EigenVectors2X2();

            this.actualVector1 = eVectors[0];

            this.actualVector2 = eVectors[1];
        }

        /// <summary>The when check to see if the matrix is.</summary>
        /// <param name="matrixType">The matrix type.</param>
        [When(@"check to see if the matrix is (.*)")]
        public void WhenCheckToSeeIfTheMatrixIs(string matrixType)
        {
            switch (matrixType.ToLower())
            {
                case "diagonal":
                    this.actualBooleanResult = this.exampleMatrix1.IsDiagonal();
                    break;
                case "scalar":
                    this.actualBooleanResult = this.exampleMatrix1.IsScalarMatrix();
                    break;
                case "involution":
                    this.actualBooleanResult = this.exampleMatrix1.IsInvolutionMatrix(); 
                    break;
                case "nilpotent":
                    this.actualBooleanResult = this.exampleMatrix1.IsNilPotentForSpecifiedIndex((int)this.actualDoubleResult);
                    break;
                case "nilpotent for all":
                    this.actualBooleanResult = this.exampleMatrix1.IsNilPotent();
                    break;
                case "triangular":
                    this.triangularType = this.exampleMatrix1.IsTriangularMatrix();
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }

        /// <summary>The when i get the lowest term matrix.</summary>
        [When(@"I get the lowest term matrix")]
        public void WhenIGetTheLowestTermMatrix()
        {
            this.actualMatrix = this.exampleMatrix1.ReduceMatrixToLowestTerms(out this.actualDoubleResult);
        }

        /// <summary>The when i calculate the matrix square roots.</summary>
        [When(@"I calculate the matrix square roots")]
        public void WhenICalculateTheMatrixSquareRoots()
        {
            this.actualRootMatrices.AddRange(this.exampleMatrix1.SquareRoots());
        }

        // ////////////////////////////////////////////////////

        /// <summary>The then the result will be.</summary>
        /// <param name="expectedBooleanResult">The expected boolean result.</param>
        [Then(@"the result will be (.*)")]
        public void ThenTheResultWillBe(bool expectedBooleanResult)
        {
            Assert.AreEqual(this.actualBooleanResult, expectedBooleanResult);
        }

        /// <summary>The then the complex number will be i.</summary>
        /// <param name="real">The real.</param>
        /// <param name="imaginary">The imaginary.</param>
        [Then(@"the Complex number will be (.*) \+ i(.*)")]
        public void ThenTheComplexNumberWillBeI(double real, double imaginary)
        {
            // This function looks similar to ThenTheResultIsTheComplexNumber but note that 
            // this one is used for reading columns from a Scenario Outline table.
            if (this.actualBooleanResult == true)
            {
                Assert.AreEqual(real, this.aComplexNumberToTest.Real);

                Assert.AreEqual(imaginary, this.aComplexNumberToTest.Imaginary);
            }
        }

        /// <summary>The then the result is the complex number i.</summary>
        /// <param name="complexNumber">The complex Number.</param>
        [Then(@"the result is the complex number (.*)")]
        public void ThenTheResultIsTheComplexNumber(string complexNumber)
        {
            Complex expectedComplexResult;

            if (!complexNumber.TryParse(out expectedComplexResult))
            {
                Assert.Fail("Failed to convert '" + complexNumber + "' to a complex number.");
            }

            Assert.AreEqual(this.actualComplexResult, expectedComplexResult);
        }

        /// <summary>The then the result should be.</summary>
        /// <param name="expectedTable">The expected table.</param>
        [Then(@"the result should be")]
        public void ThenTheResultShouldBe(Table expectedTable)
        {
            var expectedMatrix = ConvertTableToMatrix(expectedTable);

            Assert.IsTrue(this.actualMatrix.IsEqual(expectedMatrix));
        }

        /// <summary>The then the result is.</summary>
        /// <param name="expectedDoubleResult">The expected double result.</param>
        [Then(@"the result is (.*)")]
        public void ThenTheResultIs(double expectedDoubleResult)
        {
            Assert.AreEqual(expectedDoubleResult, this.actualDoubleResult);
        }

        /// <summary>The then the resulting matrix will be an identity matrix.</summary>
        [Then(@"the resulting matrix will be an identity matrix")]
        public void ThenTheResultingMatrixWillBeAnIdentityMatrix()
        {
            var expectedMatrix = this.actualMatrix.IdentityMatrix();

            Assert.IsTrue(this.actualMatrix.IsEqual(expectedMatrix));
        }

        /// <summary>The then the resulting matrix will not be an identity matrix.</summary>
        [Then(@"the resulting matrix will not be an identity matrix")]
        public void ThenTheResultingMatrixWillNotBeAnIdentityMatrix()
        {
            var expectedMatrix = this.actualMatrix.IdentityMatrix();

            Assert.IsFalse(this.actualMatrix.IsEqual(expectedMatrix));
        }

        /// <summary>The then the matrix type will be.</summary>
        /// <param name="matrixType">The matrix type.</param>
        [Then(@"the matrix type will be (.*)")]
        public void ThenTheMatrixTypeWillBe(string matrixType)
        {
            switch (matrixType.ToLower())
            {
                case "not triangular":
                    Assert.IsTrue(this.triangularType == ComplexMatrixExtensions.TriangularType.NotTrangular);
                    break;
                case "null":
                    Assert.IsTrue(this.triangularType == ComplexMatrixExtensions.TriangularType.NullMatrix);
                    break;
                case "diagonal":
                    Assert.IsTrue(this.triangularType == ComplexMatrixExtensions.TriangularType.DiagonalMatrix);
                    break;
                case "identity":
                    Assert.IsTrue(this.triangularType == ComplexMatrixExtensions.TriangularType.IdentityMatrix);
                    break;
                case "upper":
                    Assert.IsTrue(this.triangularType == ComplexMatrixExtensions.TriangularType.UpperTriangular);
                    break;
                case "lower":
                    Assert.IsTrue(this.triangularType == ComplexMatrixExtensions.TriangularType.LowerTriangular);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }

        /// <summary>The then the complex eigen values are and.</summary>
        /// <param name="eigenValue1">The eigen value 1.</param>
        /// <param name="eigenValue2">The eigen value 2.</param>
        [Then(@"the complex Eigen values are (.*) and (.*)")]
        public void ThenTheComplexEigenValuesAreAnd(string eigenValue1, string eigenValue2)
        {
            Complex expectedEigenValue1;
            Complex expectedEigenValue2;
            
            if (!eigenValue1.TryParse(out expectedEigenValue1))
            {
                Assert.Fail(@"Invalid complex number (" + eigenValue1 + @").");
            }

            if (!eigenValue2.TryParse(out expectedEigenValue2))
            {
                Assert.Fail(@"Invalid complex number (" + eigenValue2 + @").");
            }

            var b11 = expectedEigenValue1.EqualTo(this.actualEigenValue1);
            var b12 = expectedEigenValue1.EqualTo(this.actualEigenValue2);

            var b21 = expectedEigenValue2.EqualTo(this.actualEigenValue1);
            var b22 = expectedEigenValue2.EqualTo(this.actualEigenValue2);

            Assert.IsTrue(b11 || b12);

            Assert.IsTrue(b21 || b22);
        }

        /// <summary>The then the complex eigen vector is.</summary>
        /// <param name="tableVector">The table vector.</param>
        [Then(@"the complex Eigen vector is")]
        public void ThenTheComplexEigenVectorIs(Table tableVector)
        {
            this.expectedVector1 = ConvertTableToMatrix(tableVector);

            // See "ThenTheOtherVectorIs" for the assert
        }

        /// <summary>The then the other complex vector is.</summary>
        /// <param name="tableVector">The table vector.</param>
        [Then(@"the other complex vector is")]
        public void ThenTheOtherComplexVectorIs(Table tableVector)
        {
            this.expectedVector2 = ConvertTableToMatrix(tableVector);

            var b11 = this.expectedVector1.IsEqual(this.actualVector1);
            var b12 = this.expectedVector1.IsEqual(this.actualVector2);

            var b21 = this.expectedVector2.IsEqual(this.actualVector1);
            var b22 = this.expectedVector2.IsEqual(this.actualVector2);

            Assert.IsTrue(b11 || b12);

            Assert.IsTrue(b21 || b22);
        }

        /// <summary>The then one root should be.</summary>
        /// <param name="rootTable">The root table.</param>
        [Then(@"one root should be")]
        public void ThenOneRootShouldBe(Table rootTable)
        {
            var expectedRoot = ConvertTableToMatrix(rootTable);

            var found = this.actualRootMatrices.Any(m => m.IsEqual(expectedRoot));

            Assert.IsTrue(found);
        }

        // ////////////////////////////////////////////////////

        /// <summary>Convert the SpecFlow table in to a complex matrix.</summary>
        /// <param name="inTable">The SpecFlow table.</param>
        /// <returns>The the 2D complex matrix.</returns>
        /// <exception cref="Exception">Invalid complex number.</exception>
        private static Complex[,] ConvertTableToMatrix(Table inTable)
        {
            var returnMatrix = new Complex[inTable.RowCount, inTable.Header.Count];

            for (var r = 0; r < inTable.RowCount; r++)
            {
                for (var c = 0; c < inTable.Header.Count; c++)
                {
                    Complex complex;

                    if (!inTable.Rows[r][c].TryParse(out complex))
                    {
                        throw new Exception(@"Invalid complex number (" + inTable.Rows[r][c] + @").");
                    }

                    returnMatrix[r, c] = complex;
                }
            }

            return returnMatrix;
        }
    }
}
