clear all;
clc;

load('data.mat');

x = V;
y = b1f;

fresult1 = fit(x', y', 'poly1')
plot(fresult1, x, y, 'r');
hold on;

y = b2f;
fresult2 = fit(x', y', 'poly3')
plot(fresult2, x,y, 'b');
hold off;