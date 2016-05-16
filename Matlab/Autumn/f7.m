function [ result ] = f7( A, B )
%F7 Summary of this function goes here
%   Detailed explanation goes here
    clc;
    disp('Исходные данные:');
    disp(A);
    [Ax Ay] = size(A);
    disp(B);
    [Bx By] = size(B);
    new_A = repmat(A(:),1,Bx*By);
    new_B = repmat(B(:)',Ax*Ay,1);
    result = max(max(sin(new_A+new_B)));
end

