clear all;
clc;

a = 0;
b = pi/2;
h = 0.001;
x = a : h : b;
y = zeros(1,length(x));

for k = 1 : length(x)-1
    y(k+1)=y(k)+ h * sqrt(1-y(k) * y(k));
end;

plot(x,y);
grid on;