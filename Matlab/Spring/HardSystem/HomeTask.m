clear all
clc;

time_period = [0 4];
initial = [2, 0]';

e = 0.1;
y_t = @(t,y) [y(2); ((1-y(1)*y(1))*y(2)-y(1))/e];
[t,y] = ode15s(y_t, time_period, initial);
plot(t,y(:,1)); hold on; grid on;

e = 0.01;
y_t = @(t,y) [y(2); ((1-y(1)*y(1))*y(2)-y(1))/e];
[t,y] = ode15s(y_t, time_period, initial);
plot(t,y(:,1)); hold on; grid on;

e = 0.001;
y_t = @(t,y) [y(2); ((1-y(1)*y(1))*y(2)-y(1))/e];
[t,y] = ode15s(y_t, time_period, initial);
plot(t,y(:,1)); hold on; grid on;

e = 0.0001;
y_t = @(t,y) [y(2); ((1-y(1)*y(1))*y(2)-y(1))/e];
[t,y] = ode15s(y_t, time_period, initial);
plot(t,y(:,1)); hold on; grid on;

legend('0.1','0.01','0.001','0.0001');