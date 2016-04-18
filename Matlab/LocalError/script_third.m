clear all;
clc;

time_start = 0;
time_end = 2;

step = 0.1;
time = time_start:step:time_end;

x = zeros(1,length(time));
y = zeros(1,length(time));
h = zeros(1,length(time));

x(1) = 0.5;
y(1) = 0.5;
h(1) = 0;

for k = 2 : length(time)
    x(k) = x(k-1) - step * 5 * (x(k-1)-(time(k-1))^2);
    c = (x(k-1) - time(k-1)^2 + 0.4 * time(k-1) - 0.08)/ exp(-5*time(k-1));
    y(k) = time(k)^2 - 0.4 * time(k) + 0.08 + c * exp(-5*time(k));
    h(k) = y(k) - x(k);
end;

subplot(1,2,1);
plot(time, x); hold on; grid on;
plot(time, y); hold on; grid on; legend('Численное','С заданными y(tk)=xk');
subplot(1,2,2);
plot(time, h); hold on; grid on;  legend('Ошибка'); 