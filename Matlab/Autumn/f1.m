function [ result ] = f1( x, a )

    clc;
    result = eye(length(a),length(a));
    
    x_new(1:length(a)-1) = x;
    x_new = diag(x_new,1);
    
    result(length(a), :) = a;
    result = result - x_new;
end

