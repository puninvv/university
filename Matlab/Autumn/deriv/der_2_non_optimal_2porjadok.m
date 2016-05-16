function [ result, h ] = der_2_non_optimal_2porjadok( f, x, e, maxIterations)
%DER_1_NON_OPTIMAL Summary of this function goes here
%   Detailed explanation goes here
    
    n = 1;
    h = 1;
    result = (f(x-h)-2*f(x)+f(x+h))/(h*h);
    while (n < maxIterations)   
        n = n + 1;
        
        h = h / n;
        df = (f(x-h)-2*f(x)+f(x+h))/(h*h);
        if (abs(df-result)<e)
            result = df;
            break;
        end;
        
        result = df;
    end;

end

