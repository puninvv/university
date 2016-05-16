function [ output ] = CountCoefficientsByLinearSystem( func, points )
%CountCoefficientsByLinearSystem return the coefficients of interpolation
%polynom, built by solving Wandermond Wx=f(points)
    [x y] = size(points);
    powers = repmat((y-1):-1:0,y,1);
    W = repmat(points',1,y).^powers;
    f = func(points)';
    output=(W\f)';
end

