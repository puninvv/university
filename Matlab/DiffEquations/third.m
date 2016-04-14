clear all;
clc;

a = 0;
b = 1;
h = 0.001;
x = a : h : b;

y = zeros(1,length(x));
y(1) = 0;

y_1 = zeros(1,length(x));
y_1 (1) = 0.01;

y_2 = zeros(1,length(x));
y_2 (1) = 0.001;

y_3 = zeros(1,length(x));
y_3 (1) = 0.0001;

y_4 = zeros(1,length(x));
y_4 (1) = 0.00001;

y_5 = zeros(1,length(x));
y_5 (1) = 0.000001;

for k = 1 : length(x)-1
    y(k+1)=y(k)+ h * y(k)^(2/3);
    y_1(k+1)=y_1(k)+ h * y_1(k)^(2/3);
    y_2(k+1)=y_2(k)+ h * y_2(k)^(2/3);
    y_3(k+1)=y_3(k)+ h * y_3(k)^(2/3);
    y_4(k+1)=y_4(k)+ h * y_4(k)^(2/3);
    y_5(k+1)=y_5(k)+ h * y_5(k)^(2/3);
end;

hold on;
grid on;
plot(x,y_1,'Color','g');
plot(x,y_2,'Color','b');
plot(x,y_3,'Color','y');
plot(x,y_4,'Color','c');
plot(x,y_5,'Color','m');
plot(x,y,'Color','r');

legend('y(0)=0.01','y(0)=0.001','y(0)=0.0001','y(0)=0.00001','y(0)=0.000001','y(0)=0');
hold off;

