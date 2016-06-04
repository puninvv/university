clear all;
hold off;
clc;

f = @(t,y)[(t+y)/(t*y+1)];
[Y,T] = OdeSolve(f, [0 10], 0, 0.5, 0.001);
plot(T,Y);
grid on;