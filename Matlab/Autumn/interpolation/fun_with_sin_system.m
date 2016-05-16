clear all;
clc;

error = 0.001;

a = 0;
b = pi/2;

x = a:0.001:(b);
y = sin(x);

    points = linspace(a,b,1);
    pointsv2 = getChebushevRoots(a,b,1);
    
    p = CountCoefficientsByLinearSystem(@sin,points);
    p2 = CountCoefficientsByLinearSystem(@sin,pointsv2);
    
    f = polyval(p,x);
    f2 = polyval(p2,x);
    
    plot(x,y,'Color','r');
    title(1);
    grid on;
	hold on;
    plot(x,f, 'Color', 'b');
    hold on;
    plot(x,f2, 'Color', 'g');
    
    pause(1);
    hold off;
       
    err = CountErrBetweenFunctironAndPolynome(@sin,p,a,b);
    err2 = CountErrBetweenFunctironAndPolynome(@sin,p,a,b);
    p_end = p;
    k_end = 1;
    
    color = 'g';

for k=3:40
    points = linspace(a,b,k);
    pointsv2 = getChebushevRoots(a,b,k-1);
    
    p = CountCoefficientsByLinearSystem(@sin,points);
    p2 = CountCoefficientsByLinearSystem(@sin,pointsv2);
    
    f = polyval(p,x);
    f2 = polyval(p2,x);
    
    plot(x,y,'Color','r');
    
    grid on;
	hold on;
    
    hold on;
    
    plot(x,f2, 'Color', 'g');
    plot(x,f, 'Color', 'b');   
    err_new = CountErrBetweenFunctironAndPolynome(@sin,p,a,b);
    err_new2 = CountErrBetweenFunctironAndPolynome(@sin,p2,a,b);
    
    title(strcat(num2str(k),'равнораспр.:',num2str(err_new),'; чебышев:',num2str(err_new2)));
    
    
    pause(1);
    hold off;
    
%     if (err_new2 < error)
%         p_end = p2;
%         k_end = k-1;
%         err = err_new2;
%         color='g';
%         break;
%     end;
%     
%     if (err_new < error)
%         p_end = p;
%         k_end = k;
%         err = err_new;
%         color='b';
%         break;
%     end;
%     
%     if (err_new < err)
%         err = err_new;
%         p_end = p;
%         k_end = k;
%         color='b';
%     end;
%    
%     if (err_new2 < err)
%         err = err_new2;
%         p_end = p2;
%         k_end = k-1;
%         color='g';
%     end;
end;

    
    plot(x,y,'Color','r');
    hold on;
    
    f = polyval(p_end,x);
    plot(x,f, 'Color', color);
    grid on;
    title(k_end);
    hold off;
