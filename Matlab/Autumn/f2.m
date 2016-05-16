function [ matrix_det, characteristical_polynome, lambdas ] = f2( a )
    clc;
    matrix = f1(0,a)
    matrix_det = det(matrix);
    characteristical_polynome = poly(matrix);
    
    [R, lambdas] = eig(matrix);
end

