clear all;
clc;

time_period = [0 100];
initial = [0, 2]';

Q_t1 = @(t,x) [x(2); -sin(x(1))];
[t,Q] = ode45(Q_t1, time_period, initial);
subplot(3, 1, 1); plot(t, Q(:,1)); grid on; hold on; 
h = legend('$\ddot{\Theta}+sin(\Theta)=0$');
set(h,'Interpreter','latex');

alpha = 0.25;
Q_t2 = @(t,x) [x(2); -sin(x(1))-alpha*x(2)];
[t,Q] = ode45(Q_t2, time_period, initial);
subplot(3, 1, 2); plot(t, Q(:,1)); grid on; hold on; 
h = legend('$\ddot{\Theta}+\alpha\dot{\Theta}+sin(\Theta)=0$');
set(h,'Interpreter','latex');

alpha = 0.25;
Q_t3 = @(t,x) [x(2); -x(1)-alpha*x(2)];
[t,Q] = ode45(Q_t3, time_period, initial);
subplot(3, 1, 3); plot(t, Q(:,1)); grid on; hold on;
h = legend('$\ddot{\Theta}+\alpha\dot{\Theta}+\Theta=0$');
set(h,'Interpreter','latex');