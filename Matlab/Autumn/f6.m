function [ matrix ] = f6( n )
%F6 Summary of this function goes here
%   Detailed explanation goes here
    clc;
    arr = (1:n)';
    disp('1 ������');
    matrix = repmat(arr,1,n);
    disp(matrix);
    disp('');
    
    disp('2 ������');
    matrix = arr * ones(1,n);
    disp(matrix);
    disp('');
end

