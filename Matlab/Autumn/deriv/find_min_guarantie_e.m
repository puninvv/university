function [ e ] = find_min_guarantie_e( M2, mantissa, h )
%FIN_MIN_GUARANTIE_E Summary of this function goes here
%   Detailed explanation goes here
    e = 2 * (2^(-mantissa))/h+h*M2/2;
end

