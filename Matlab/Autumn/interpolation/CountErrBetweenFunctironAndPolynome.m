function [ err ] = CountErrBetweenFunctironAndPolynome( f, p, a, b)
%CountErrBetweenFunctironAndPolynome return e^2
    x = linspace(a,b,100);
    p_val = polyval(p,x);
    f_val = f(x);
    err = sum((p_val - f_val).^2);
end

