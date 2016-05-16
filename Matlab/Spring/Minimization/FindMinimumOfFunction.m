function [ result ] = FindMinimumOfFunction( F, inputPoints, e, alpha, betta, gamma)
%Поиск минимума функции методом деформации многоугольника
%   double F([x1 x2 ... xn]) 

    [rows, columns] = size(inputPoints);

    if (nargin < 4)
        alpha = 1;
        betta = 0.5;
        gamma = 2.5;
    end;

    points = inputPoints';
    f = F(inputPoints)';
    [f, indexes] = sort(f(1, :));
    points = points(:, indexes);
    
    f_h = f(columns);
    x_h = points(:, columns);
    
    f_g = f(columns - 1);
    x_g = points(:, columns-1);
    
    f_l = f(1);
    x_l = points(:,1);
    
    x_c = (1/columns)*sum(inputPoints,2);
    
    x_r = (1+alpha)*x_c-alpha*x_h;
    f_r = F(x_r')';
    
    result = [f; points];
end

