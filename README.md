#Matrices 2

##Complex Matrices
  
This is a project including a Matrix extension class for non-Complex Matrices (2D arrays of doubles). It also includes gherkin feature file tests (NUnit test runner) with significant code coverage. The program is just a demo. Extract the extension classes into your project to use the matrix tools.
  
Once you have declared a Matrix, Z, where Z =    
| 3 + i,  1 + i2 |     
| 5 - i3, 2 - i  |     
  
like this...  
            var complexMatrix1 = new Complex[2, 2];  
            complexMatrix1[0, 0] = new Complex(3, 1);  
            complexMatrix1[0, 1] = new Complex(1, 2);  
            complexMatrix1[1, 0] = new Complex(5, -3);  
            complexMatrix1[1, 1] = new Complex(2, -1);  
  
Then you can perform sophisticated matrix operations.   

Add to another matrix   
Subtract from a matrix   
Multiply by a constant   
Multiply by a matrix   
Raise to powers   
Calculate roots   
Calculate a complex determinant     
Calculate a Reciprocal matrix   
Calculate an Inverse matrix   
Calculate a Conjugate matrix   
Calculate an adjunct matrix      
Calculate a complaex trace value   
Calculate the Eigen vectors of a 2x2 matrix  
Calculate the Eigen values of a 2x2 matrix  
Recognise an involution matrix    
Recognise a nilpotent matrix   
Recognise a triangular matrix   

//// Example:   
//// The inverse matrix, v, of Z is calculated like this   
var v = Z.Inverse()   

(C) Steven Digby, 30 August 2017
