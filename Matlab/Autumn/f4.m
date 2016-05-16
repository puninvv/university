function [ s ] = f4( x, n )
%F4 Summary of this function goes here
%   Detailed explanation goes here
    clc;
    k = (-n:1:n);
    denumerator = x - k;
    arr = ones(1,2*n+1);
    arr(2:2:end)=-1;
      
    n2_faq = factorial(2*n);
    n_plus_k_faq = factorial(n+k);
    n_minus_k_faq = factorial(n-k);
    
    C = n2_faq ./ (n_plus_k_faq .* n_minus_k_faq)
    
    result_without_pl_mi = 	/denumerator;
    
    result = result_without_pl_mi .* arr
    
    s = sum(result);
    
end

