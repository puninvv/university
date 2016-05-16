function [ matrix, lambdas, det ] = f3( n )
    clc;
    lambdas = n:-1:1;
    one(1:n)=1;
    matrix = tril(lambdas' * one);
    det = factorial(n);
    
    disp('Текущая матрица:');
    disp(matrix);
    disp('');
    
    disp('Её собственные числа:');
    disp(lambdas);
    disp('');
    
    disp('Её определитель:');
    disp(det);
    disp('');
    
    disp('Изменённая');
    new_matrix = matrix;
    new_matrix(n-1,n)=2;
    disp(new_matrix);

end

