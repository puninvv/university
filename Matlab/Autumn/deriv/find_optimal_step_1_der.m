function [ h, M2 ] = find_optimal_step_1_der( f, a, b, n, e, maxIterations, mantissa)
%FIND_OPTIMAL_STEP_1_DER Summary of this function goes here
%   Detailed explanation goes here
    x_i = linspace(a,b,n);
    der_2 = zeros(n);
    for k=1:n 
        der_2(k)=der_2_non_optimal_2porjadok(f,x_i(k),e,maxIterations);
    end;
    M2 = max(max(der_2));
    h = 2 * sqrt((2^(-mantissa))/M2);
end

