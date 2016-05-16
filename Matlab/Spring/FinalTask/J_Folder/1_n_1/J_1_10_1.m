function [ result ] = J_1_10_1(k)
    a = 0;
    b = 50;
    h = 0.1;
    
    c = 2;
    m = 1;
    l = 1;
    g = 9.8;
    
    initial = [pi/3, 0];
    time = a:h:b;
    
    Q_t = @(t,x) [x(2); k(1)*x(1)+k(2)*x(2)-(c/(m*l*l))*x(2)-(g/l)*sin(x(1))];
    [t,Q] = ode45(Q_t, time, initial);
   
    result = sum((Q(:,1).*Q(:,1) + 10*Q(:,2).*Q(:,2) + (k(1)*Q(:,1)+k(2)*Q(:,2)).*(k(1)*Q(:,1)+k(2)*Q(:,2)))*h);
end

