function [ result ] = f5( v )
%F5 Summary of this function goes here
%   Detailed explanation goes here
    clc;
    disp('Исходный массив');
    disp(v);
    disp('');
    positions = find(v==min(v))
    result = zeros(1,length(v))
    
    tmp_arr = 1:length(positions)
    result(positions)=tmp_arr;
end

