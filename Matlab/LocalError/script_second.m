clear all;
clc;

time_start = 0;
time_end = 200;

step = 0.5;
time = time_start:step:time_end;

x = zeros(1,length(time));
y = zeros(1,length(time));
h = zeros(1,length(time));

x(1) = 2;
y(1) = 2;
h(1) = 0;

for k = 2 : length(time)
    x(k) = x(k-1) + step * (x(k-1)*sin(time(k-1)));
    c = x(k-1)/exp(-cos(time(k-1)));
    y(k) = c * exp(-cos(time(k)));
    h(k) = y(k) - x(k);
end;

subplot(1,2,1);
plot(time, x); hold on; grid on;
plot(time, y); hold on; grid on; legend('Численное','С заданными y(tk)=xk');
subplot(1,2,2);
plot(time, h); hold on; grid on;  legend('Ошибка');