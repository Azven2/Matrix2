Feature: CompleMatrixTests
	In order to avoid mistakes in a complicated subject
	I want to test the functionality of the Complex Matrix tools

@myComplexMatrixTests
Scenario Outline: TryParse test
	Given a string <ComplexNumber>
	When I parse the string
	Then the result will be <Result> 
	And the Complex number will be <R> + i<I>
	Examples: 
	| ComplexNumber | Result | R  | I  |
	| na            | false  | 0  | 0  |
	| 2 + i3        | true   | 2  | 3  |
	| 2 + 3i        | false  | 0  | 0  |
	| 2             | true   | 2  | 0  |
	| 2 + i0        | true   | 2  | 0  |
	| 2 + i         | true   | 2  | 1  |
	| 2 + i1        | true   | 2  | 1  |
	| -2 + i3       | true   | -2 | 3  |
	| 2 - i3        | true   | 2  | -3 |
	| 2 - i-3       | true   | 2  | 3  |
	| i2 + 3        | false  | 0  | 0  |
	| i             | true   | 0  | 1  |
	| i3            | true   | 0  | 3  |
	| -i3           | true   | 0  | -3 |
	| - i           | true   | 0  | -1 |

@myComplexMatrixTests
Scenario: Test the IsEqual method
	Given The following matrix
	| C1    | C2     |
	| 1 + i | 3 + i2 |
	| 5     | 4 - i1 |
	And another matrix
	| C1     | C2     |
	| 1 + i  | 3 + i2 |
	| 5 + i0 | 4 - i  |
	When I compare the matrices for equality
	Then the result will be True

@myComplexMatrixTests
Scenario: Test the IsEqual method for inequality
	Given The following matrix
	| C1    | C2     |
	| 1 + i | 3 + i2 |
	| 5     | 4 - i1 |
	And another matrix
	| C1     | C2     |
	| 1 + i  | 3 + i2 |
	| 5 + i0 | 3 - i  |
	When I compare the matrices for equality
	Then the result will be False

@myComplexMatrixTests
Scenario: Test that a zero matrix can be recognised
	Given The following matrix
	| C1     | C2 |
	| 0 + i0 | 0  |
	| 0      | 0  |
	When I compare the matrix to a zero matrix
	Then the result will be True

@myComplexMatrixTests
Scenario: Test that a matrix can be recognised as not a NULL matrix (1)
	Given The following matrix
	| C1 | C2 |
	| i  | 0  |
	| 0  | 0  |
	When I compare the matrix to a zero matrix
	Then the result will be False

@myComplexMatrixTests
Scenario: Test that a matrix can be recognised as not a NULL matrix (2)
	Given The following matrix
	| C1 | C2 |
	| 1  | 0  |
	| 0  | 0  |
	When I compare the matrix to a zero matrix
	Then the result will be False

@myComplexMatrixTests
Scenario: Test that an identity matrix can be recognised
	Given The following matrix
	| C1     | C2 |
	| 1 + i0 | 0  |
	| 0 + i0 | 1  |
	When I compare the matrix to an identity matrix
	Then the result will be True

@myComplexMatrixTests
Scenario: Test that a matrix can be recognised as not an identity matrix
	Given The following matrix
	| C1 | C2 |
	| 1  | 0  |
	| 0  | i  |
	When I compare the matrix to an identity matrix
	Then the result will be False

@myComplexMatrixTests
Scenario: Extract a complex element from the complex matrix
	Given The following matrix
	| C1    | C2      |
	| 7 + i | 6 + i5  |
	| 5 - i | -4 + i3 |
	| 3     | -2 - i3 |
	When I look-up element row 2, column 2
	Then the result is the complex number -4 + i3

@myComplexMatrixTests
Scenario: Transpose a complex matrix
	Given The following matrix
	| C1    | C2      |
	| 7 + i | 6 + i5  |
	| 5 - i | -4 + i3 |
	| 3     | -2 - i3 |
	When I transpose the matrix
	Then the result should be 
	| C1     | C2      | C3     |
	| 7 + i  | 5 - i   | 3      |
	| 6 + i5 | -4 + i3 | -2 -i3 |

@myComplexMatrixTests
Scenario: Can I add two complex matrices together
	Given The following matrix
	| C1    | C2      |
	| 7 + i | 6 + i5  |
	| 5 - i | -4 + i3 |
	And another matrix
	| C1 | C2     |
	| -6 | -3 -i3 |
	| 0  | 4 - i2 |
	When I add the two matrices
	Then the result should be 
	| C1    | C2     |
	| 1 + i | 3 + i2 |
	| 5 - i | i      |
	
@myComplexMatrixTests
Scenario: Can I subtract one complex matrices from another
	Given The following matrix
	| C1    | C2      |
	| 7 + i | 6 + i5  |
	| 5 - i | -4 + i3 |
	And another matrix
	| C1     | C2      |
	| 7 + i2 | 3 + i3  |
	| 0      | -4 + i2 |
	When I subtract the second matrix from the first
	Then the result should be 
	| C1    | C2     |
	| -i    | 3 + i2 |
	| 5 - i | i      |  

@myComplexMatrixTests
Scenario: Can I multiply one matrix by a constant
	Given The following matrix
	| C1    | C2      |
	| 7 + i | 6 + i5  |
	| 5 - i | -4 + i3 |
	When I multiply the first matrix by the constant 3
	Then the result should be 
	| C1      | C2       |
	| 21 + i3 | 18 + i15 |
	| 15 - i3 | -12 + i9 |

@myComplexMatrixTests
Scenario: Can I multiply one matrix with another
	Given The following matrix
	| C1    | C2      |
	| 7 + i | 6 + i5  |
	| 5 - i | -4 + i3 |
	And another matrix
	| C1    | C2     |
	| -i    | 3 + i2 |
	| 5 - i | i      |
	When I multiply the first matrix by the second
	Then the result should be 
	| C1        | C2       |
	| 36 + i12  | 14 + i23 |
	| -18 + i14 | 14 + i3  |

@myComplexMatrixTests
Scenario: Can I divide one matrix with another
	Given The following matrix
	| C1        | C2       |
	| 36 + i12  | 14 + i23 |
	| -18 + i14 | 14 + i3  |
	And another matrix
	| C1    | C2      |
	| 7 + i | 6 + i5  |
	| 5 - i | -4 + i3 |
	When I divide the first matrix by the second
	Then the result should be 
	| C1    | C2     |
	| -i    | 3 + i2 |
	| 5 - i | i      |

@myComplexMatrixTests
Scenario: Extract a sub-matrix
	Given The following matrix
	| C1     | C2     | C3     |
	| 1 + i  | 2 -i3  | 3 -i7  |
	| i      | 4 - i3 | -5 + i |
	| 1 - i2 | 0      | -i     |
	When I extract the submatrix of row 2 column 3
	Then the result should be 
	| C1     | C2    |
	| 1 + i  | 2 -i3 |
	| 1 - i2 | 0     | 

@myComplexMatrixTests
Scenario: Get the negative matrix
	Given The following matrix
	| C1    | C2      |
	| 7 + i | 6 + i5  |
	| 5 - i | -4 + i3 |
	When I negate the matrix
	Then the result should be 
	| C1     | C2      |
	| -7 - i | -6 - i5 |
	| -5 + i | 4 - i3  |

@myComplexMatrixTests
Scenario: Extract a minor matrix
	Given The following matrix
	| C1     | C2     | C3     |
	| 1 + i  | 2 -i3  | 3 -i7  |
	| i      | 4 - i3 | -5 + i |
	| 1 - i2 | 0      | -i     |
	When I extract the minor matrix of column 3
	Then the result should be 
	| C1     | C2     |
	| i      | 4 - i3 |
	| 1 - i2 | 0      |

@myComplexMatrixTests
Scenario: Calculate the cofactor 
	Given The following matrix
	| C1 | C2 | C3 |
	| 1  | 2  | 3  |
	| 0  | 4  | 5  |
	| 1  | 0  | 6  |
	When I calculate the cofactor matrix
	Then the result should be 
	| C1  | C2 | C3 |
	| 24  | 5  | -4 |
	| -12 | 3  | 2  |
	| -2  | -5 | 4  |

@myComplexMatrixTests
Scenario: Calculate the determinant (2x2)
	Given The following matrix
	| C1  | C2     |
	| i   | 1 + i2 |
	| -i3 | -i     |
	When I calculate the determinant
	Then the result is the complex number -5 + i3

@myComplexMatrixTests
Scenario: Calculate the determinant (4x4)
	Given The following matrix
	| C1 | C2 | C3 | C4 |
	| 3  | 2  | 0  | 1  |
	| 4  | 0  | 1  | 2  |
	| 3  | 0  | 2  | 1  |
	| 9  | 2  | 3  | 1  |
	When I calculate the determinant
	Then the result is the complex number 24 + i0

@myComplexMatrixTests
Scenario: Calculate the adjunct (2x2)
	Given The following matrix
	| C1 | C2 |
	| 7  | 6  |
	| 5  | 4  |
	When I calculate the adjunct matrix
	Then the result should be 
	| C1 | C2 |
	| 4  | -6 |
	| -5 | 7  |

@myComplexMatrixTests
Scenario: Calculate the adjunct (3x3)
	Given The following matrix
	| C1 | C2 | C3 |
	| -3 | 2  | -5 |
	| -1 | 0  | -2 |
	| 3  | -4 | 1  |
	When I calculate the adjunct matrix
	Then the result should be 
	| C1 | C2 | C3 |
	| -8 | 18 | -4 |
	| -5 | 12 | -1 |
	| 4  | -6 | 2  |

@myComplexMatrixTests
Scenario: Calculate the inverse matrix
	Given The following matrix
	| C1    | C2    |
	| 1 + i | i     |
	| 1 + i | 2 - i |
	When I calculate the inverse matrix
	Then the result should be 
	| C1            | C2           |
	| 0.5 - i0.25   | -i0.25       |
	| -0.25 - i0.25 | 0.25 + i0.25 |

@myComplexMatrixTests
Scenario: Calculate the inverse matrix (reverse check)
	Given The following matrix
	| C1    | C2    |
	| 1 + i | i     |
	| 1 + i | 2 - i |
	And another matrix
	| C1            | C2           |
	| 0.5 - i0.25   | -i0.25       |
	| -0.25 - i0.25 | 0.25 + i0.25 |
	When I multiply the first matrix by the second
	Then the resulting matrix will be an identity matrix

@myComplexMatrixTests
Scenario: Calculate the inverse matrix (reverse check, fail)
	Given The following matrix
	| C1    | C2    |
	| 1 + i | i     |
	| 1 + i | 2 - i |
	And another matrix
	| C1           | C2           |
	| 0.5 - i0.25  | -i0.25       |
	| -0.5 - i0.25 | 0.25 + i0.25 |
	When I multiply the first matrix by the second
	Then the resulting matrix will not be an identity matrix

@myComplexMatrixTests
Scenario: Can I create a matrix from a string 1
	Given The string "[ 1 + i, 4 - i3, -2 ; i, -7 + i3, 2 - i5 ]"
	When I convert the string to a matrix
	Then the result should be 
	| C1    | C2      | C3     |
	| 1 + i | 4 - i3  | -2     |
	| i     | -7 + i3 | 2 - i5 |

@myComplexMatrixTests
Scenario: Can I create a matrix from a string 2
	Given The string "[ 1 + i, 4 - i3; -2, i; -7 + i3, 2 - i5 ]"
	When I convert the string to a matrix
	Then the result should be 
	| C1      | C2     |
	| 1 + i   | 4 - i3 |
	| -2      | i      |
	| -7 + i3 | 2 - i5 |

@myComplexMatrixTests
Scenario: Get the trace of a given matrix
	Given The following matrix
	| C1    | C2     |
	| 1 + i | i      |
	| 1 + i | -2 + i |
	When I get the trace of the matrix
	Then the result is the complex number -1 + i2


@myComplexMatrixTests
Scenario: Can recognise a diagonal matrix (pass)
	Given The following matrix
	| C1     | C2      | C3  |
	| 3 + i  | 0       | 0   |
	| 0      | -2 - i3 | 0   |
	| 0 + i0 | 0       | -i2 |
	When check to see if the matrix is diagonal
	Then the result will be True

@myComplexMatrixTests
Scenario: Can recognise a diagonal matrix (fail)
	Given The following matrix
	| C1    | C2      | C3  |
	| 3 + i | 0       | 0   |
	| 0     | -2 - i3 | 0   |
	| 0 + i | 0       | -i2 |	
	When check to see if the matrix is diagonal
	Then the result will be False

@myComplexMatrixTests
Scenario: Can recognise a scalar matrix (pass)
	Given The following matrix
	| C1     | C2     | C3     |
	| 4 + i4 | 0      | 0      |
	| 0      | 4 + i4 | 0      |
	| 0      | 0      | 4 + i4 |
	When check to see if the matrix is scalar
	Then the result will be True

@myComplexMatrixTests
Scenario: Can recognise a scalar matrix (fail1)
	Given The following matrix
	| C1     | C2     | C3     |
	| 4 + i4 | 0      | 0      |
	| 0      | 4 + i4 | 0      |
	| 0      | 0      | 4 + i6 |
	When check to see if the matrix is scalar
	Then the result will be False

@myComplexMatrixTests
Scenario: Can recognise a scalar matrix (fail2)
	Given The following matrix
	| C1 | C2 | C3 |
	| i3 | 0  | 0  |
	| 0  | i3 | 0  |
	| i  | 0  | i3 |
	When check to see if the matrix is scalar
	Then the result will be False

@myComplexMatrixTests
Scenario: Can recognise a involution matrix (pass)
	Given The following matrix
	| C1 | C2      |
	| 4  | -1      |
	| 15 | -4 + i0 |
	When check to see if the matrix is involution
	Then the result will be True

@myComplexMatrixTests
Scenario: Can recognise a nilpotent matrix for an index (pass)
	Given The following matrix
	| C1 | C2 | C3 |
	| 5  | -3 | 2  |
	| 15 | -9 | 6  |
	| 10 | -6 | 4  |
	And the index is 2
	When check to see if the matrix is nilpotent
	Then the result will be True

@myComplexMatrixTests
Scenario: Can recognise a nilpotent matrix for all indecise (pass)
	Given The following matrix
	| C1 | C2    | C3     | C4      |
	| 0  | 2 + i | 1 - i7 | -1 + i6 |
	| 0  | 0     | i      | 2 + i3  |
	| 0  | 0     | 0      | 3       |
	| 0  | 0     | 0      | 0       |
	When check to see if the matrix is nilpotent for all
	Then the result will be True

@myComplexMatrixTests
Scenario: Can recognise a triangular matrix and return its type (fail)
	Given The following matrix
	| C1 | C2    | C3     | C4      |
	| 0  | 2 + i | 1 - i7 | -1 + i6 |
	| 0  | 0     | i      | 2 + i3  |
	| 0  | 0     | 0      | 3       |
	| i  | 0     | 0      | 0       |
	When check to see if the matrix is triangular
	Then the matrix type will be Not Triangular

@myComplexMatrixTests
Scenario: Can recognise a triangular matrix and return its type (upper)
	Given The following matrix
	| C1 | C2    | C3     | C4      |
	| 0  | 2 + i | 1 - i7 | -1 + i6 |
	| 0  | 0     | i      | 2 + i3  |
	| 0  | 0     | 0      | 3       |
	| 0  | 0     | 0      | 0       |
	When check to see if the matrix is triangular
	Then the matrix type will be Upper

@myComplexMatrixTests
Scenario: Can recognise a triangular matrix and return its type (lower)
	Given The following matrix
	| C1      | C2      | C3    | C4 |
	| 0       | 0       | 0     | 0  |
	| -2 + i3 | 0       | 0     | 0  |
	| -1 + i5 | 5 - i6  | 0     | 0  |
	| i2      | -1 + i3 | 3 -i7 | 0  |
	When check to see if the matrix is triangular
	Then the matrix type will be Lower

@myComplexMatrixTests
Scenario: Can raise an identity matrix to a power
	Given The following matrix
	| C1     | C2     | C3 |
	| 1      | 0      | 0  |
	| 0      | 1 + i0 | 0  |
	| 0 + i0 | 0      | 1  |
	When I raise the matrix to the power of 3
	Then the result should be 
	| C1 | C2 | C3     |
	| 1  | 0  | 0      |
	| 0  | 1  | 0      |
	| 0  | 0  | 1 + i0 |

@myComplexMatrixTests
Scenario: Can raise a diagonal matrix to a power
	Given The following matrix
	| C1 | C2  | C3 |
	| i  | 0   | 0  |
	| 0  | -i2 | 0  |
	| 0  | 0   | i3 |
	When I raise the matrix to the power of 3
	Then the result should be 
	| C1 | C2 | C3   |
	| -i | 0  | 0    |
	| 0  | i8 | 0    |
	| 0  | 0  | -i27 |

@myComplexMatrixTests
Scenario: Can raise a matrix to the power of -1
	Given The following matrix
		| C1    | C2    |
		| 1 + i | i     |
		| 1 + i | 2 - i |
	When I raise the matrix to the power of -1
	Then the result should be 
		| C1            | C2           |
		| 0.5 - i0.25   | -i0.25       |
		| -0.25 - i0.25 | 0.25 + i0.25 |

@myComplexMatrixTests
Scenario: Can raise a matrix to a power
	Given The following matrix
	| C1 | C2 |
	| 1  | 2  |
	| -1 | 1  |
	When I raise the matrix to the power of 3
	Then the result should be 
	| C1 | C2 |
	| -5 | 2  |
	| -1 | -5 |

@myComplexMatrixTests	
	Scenario: Get the lowest terms of a given matrix
	Given The following matrix
	| C1        | C2        |
	| -12 + i18 | 24 + i30  |
	| 6 + i0    | -18 - i48 |
	When I get the lowest term matrix
	Then the result should be 
	| C1      | C2      |
	| -2 + i3 | 4 +i5   |
	| 1       | -3 - i8 |
	And the result is 6

@myComplexMatrixTests	
	Scenario: Get the lowest terms of a given matrix 2
	Given The following matrix
	| C1          | C2           |
	| -128 + i512 | 128 + i1024  |
	| 256 + i0    | -512 - i2048 |
	When I get the lowest term matrix
	Then the result should be 
	| C1      | C2       |
	| -1 + i4 | 1 + i8   |
	| 2       | -4 - i16 |
	And the result is 128

@myComplexMatrixTests
Scenario: Get the lowest terms of a given matrix (no change)
	Given The following matrix
	| C1      | C2      |
	| -2 + i3 | 4 +i5   |
	| 1       | -3 - i8 |
	When I get the lowest term matrix
	Then the result should be 
	| C1      | C2      |
	| -2 + i3 | 4 +i5   |
	| 1       | -3 - i8 |
	And the result is 1

	@myComplexMatrixTests
Scenario: Get Eigen value of a 2x2 matrix
	Given The following matrix
	| C1 | C2 |
	| 2  | 4  |
	| 3  | 13 |
	When I get the Eigen value
	Then the complex Eigen values are 1 and 14

@myComplexMatrixTests
Scenario: Get Eigen vectors of a 2x2 matrix
	Given The following matrix
	| C1 | C2 |
	| 3  | 5  |
	| -2 | -4 |
	When I get the Eigen vectors
	Then the complex Eigen vector is
	| V  |
	| 5  |
	| -2 |
	And the other complex vector is
	| V  |
	| 1  |
	| -1 |

Scenario: Can I find the square root of a 2x2 matrix
	Given The following matrix
	| C1 | C2 |
	| 33 | 24 |
	| 48 | 57 |
	When I calculate the matrix square roots 
	Then one root should be 
	| C1 | C2 |
	| 5  | 2  |
	| 4  | 7  |
	And one root should be 
	| C1 | C2 |
	| 1  | 4  |
	| 8  | 5  |
	And one root should be 
	| C1 | C2 |
	| -5 | -2 |
	| -4 | -7 |
	And one root should be 
	| C1 | C2 |
	| -1 | -4 |
	| -8 | -5 |