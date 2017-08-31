# Matrices 2

## Complex Matrices
  
_This is a project including a Matrix extension class for Complex Matrices (2D arrays of System.Numerics.Complex types). It also includes gherkin feature file tests (NUnit test runner) with significant code coverage. The program is just a demo. Extract the extension classes into your project to use the matrix tools._   

_This project also includes a System.Numerics.Complex extension class for extending the math tools of complex numbers._    

_Once you have declared a Matrix, Z,_ where Z =    
| 3 + i,  1 + i2 |     
| 5 - i3, 2 - i  |    
  
_like this..._    
            var complexMatrix1 = new Complex[2, 2];  
            complexMatrix1[0, 0] = new Complex(3, 1);  
            complexMatrix1[0, 1] = new Complex(1, 2);  
            complexMatrix1[1, 0] = new Complex(5, -3);  
            complexMatrix1[1, 1] = new Complex(2, -1);  
  
_Then you can perform sophisticated matrix operations._  

- Add to another matrix   
- Subtract from a matrix   
- Multiply by a constant   
- Multiply by a matrix   
- Raise to powers   
- Calculate roots   
- Calculate a complex determinant     
- Calculate a Reciprocal matrix   
- Calculate an Inverse matrix   
- Calculate a Conjugate matrix   
- Calculate an adjunct matrix      
- Calculate a complaex trace value   
- Calculate the Eigen vectors of a 2x2 matrix  
- Calculate the Eigen values of a 2x2 matrix  
- Recognise an involution matrix    
- Recognise a nilpotent matrix   
- Recognise a triangular matrix

_//// Example:   
//// The inverse matrix, v, of Z is calculated like this_   
var v = Z.Inverse()   

(C) Steven Digby, 31 August 2017
