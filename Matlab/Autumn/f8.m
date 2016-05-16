function [ result ] = f8( A )
%F8 Summary of this function goes here
%   Detailed explanation goes here
     clc;
     result = max(A(isprime(A)));
end

