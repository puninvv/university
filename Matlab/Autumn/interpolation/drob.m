function [ y ] = drob( x )
%DROB Summary of this function goes here
%   Detailed explanation goes here
    %y = 1./(1 + 25*(x.*x));
    y = log(25 * sin(x) .* sin(x) + 36 * cos(x) .* cos(x) );
end

