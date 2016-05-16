time_period = [0 2];
initial = [0, 1]';

a = 10;
Q_t = @(t,x) [x(2); -x(1)-a*x(2)];
[t,Q] = ode15s(Q_t, time_period, initial);
plot(t,Q(:,2)); hold on; grid on;
N1 = roots([1 a 1]);

a = 100;
Q_t = @(t,x) [x(2); -x(1)-a*x(2)];
[t,Q] = ode15s(Q_t, time_period, initial);
plot(t,Q(:,2)); hold on; grid on;
N2 = roots([1 a 1]);

a = 1000;
Q_t = @(t,x) [x(2); -x(1)-a*x(2)];
[t,Q] = ode15s(Q_t, time_period, initial);
plot(t,Q(:,2)); hold on; grid on;
N3 = roots([1 a 1]);

a = 10000;
Q_t = @(t,x) [x(2); -x(1)-a*x(2)];
[t,Q] = ode15s(Q_t, time_period, initial);
plot(t,Q(:,2)); hold on; grid on;
N4 = roots([1 a 1]);

a = 100000;
Q_t = @(t,x) [x(2); -x(1)-a*x(2)];
[t,Q] = ode15s(Q_t, time_period, initial);
plot(t,Q(:,2)); hold on; grid on;
N5 = roots([1 a 1]);
legend(['lambda1=',num2str(N1(1)),'; lambda2=',num2str(N1(2))],['lambda1=',num2str(N2(1)),'; lambda2=',num2str(N2(2))],['lambda1=',num2str(N3(1)),'; lambda2=',num2str(N3(2))],['lambda1=',num2str(N4(1)),'; lambda2=',num2str(N4(2))],['lambda1=',num2str(N5(1)),'; lambda2=',num2str(N5(2))]);