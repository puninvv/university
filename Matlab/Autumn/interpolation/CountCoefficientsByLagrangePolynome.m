function [ p ] = CountCoefficientsByLagrangePolynome( func, points, print )
%CountCoefficientsByLagrangePolynome 
    p = 0;
    
    for k=1:length(points)
        
        w = poly([points(1:(k-1)), points((k+1):length(points))]');
        s = prod(points(k)-[points(1:(k-1)), points((k+1):length(points))]);   
        
        p = p +  func(points(k)) * w / s;
    
    end;
    
end

