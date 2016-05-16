function [ v ] = getChebushevRoots(a, b, n)
%GETCHEBUSHEVROOTS Summary of this function goes here
%   Detailed explanation goes here
    v = cos((2 * [0:(n-1)] + 1)*pi/(2*n));
    v = a + ((v+1)./2) *(b-a);
end

