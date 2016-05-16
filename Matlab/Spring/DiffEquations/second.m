clear all;
clc;

a = -1;
b = 0;
h = 0.001;
x = a : h : b;
y = zeros(1,length(x));
y(1) = 1;

for k = 1 : length(x)-1
    y(k+1)=y(k)+ h * y(k)*y(k);
end;

plot(x,y);