clear all;
clc;

error = 0.001;

a = 0;
b = 10*pi;

x = a:0.001:(b);
y = sin(x);

    points = a : 1 : b;
    p = CountCoefficientsByLagrangePolynome(@sin,points,0);
    f = polyval(p,x);
    
    plot(x,y,'Color','r');
    title(1);
    grid on;
    hold on;
    
    plot(x,f, 'Color', 'b');
    grid on;
    hold off;
    pause(0.1);
       
    err = CountErrBetweenFunctironAndPolynome(@sin,p,a,b);
    p_end = p;
    k_end = 1;

for k=2:57
    points = linspace(a,b,k);
    p = CountCoefficientsByLagrangePolynome(@sin,points,1);
    f = polyval(p,x);
    
    plot(x,y,'Color','r');
    grid on;
    hold on;
    
    plot(x,f, 'Color', 'b');
    grid on;
    title(k);
    hold off;
    pause(0.1);
    
    err_new = CountErrBetweenFunctironAndPolynome(@sin,p,a,b);
    
    if (err_new < error)
        p_end = p;
        k_end = k;
        err = err_new;
        break;
    end;
    
    if (err_new < err)
        err = err_new;
        p_end = p;
        k_end = k;
    end;
   
end;

    
    plot(x,y,'Color','r');
    hold on;
    
    f = polyval(p_end,x);
    plot(x,f, 'Color', 'b');
    grid on;
    title(k_end);
    hold off;
    