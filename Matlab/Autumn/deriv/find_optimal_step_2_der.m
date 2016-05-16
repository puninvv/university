function [ h ] = find_optimal_step_2_der( M3, mantissa )
%FIND_OPTIMAL_STEP_2_DER Summary of this function goes here
%   Detailed explanation goes here

    h = 2 * sqrt((2^(-mantissa))/M3);
end

