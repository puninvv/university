clear all;
clc;
% 
% x' = Ax + f(t)
% A = 
% 1) [0 1; -1 -2]
% 2) [0 1; 1 2]
% 3) [0 1; -2 -2]
% 4) [0 1; 0 -1]
% f(t) = 
% a) (0 0)
% b) (0 t)

time_period = [0 10];
initial = [0, 0]';

Q_t = @(t,x) [x(2); -x(1)-2*x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 1); plot(t, Q(:,1)); grid on; hold on; 
title((eig([0 1; -1 -2])));

Q_t = @(t,x) [x(2); -x(1)-2*x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 2); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); x(1)+2*x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 3); plot(t, Q(:,1)); grid on; hold on; 
title((eig([0 1; 1 2])));

Q_t = @(t,x) [x(2); x(1)+2*x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 4); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -2*x(1)-2*x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 5); plot(t, Q(:,1)); grid on; hold on; 
title((eig([0 1; -2 -2])));

Q_t = @(t,x) [x(2); -2*x(1)-2*x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 6); plot(t, Q(:,1)); grid on; hold on; 


Q_t = @(t,x) [x(2); -x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 7); plot(t, Q(:,1)); grid on; hold on; 
title((eig([0 1; 0 -1])));

Q_t = @(t,x) [x(2); -x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 8); plot(t, Q(:,1)); grid on; hold on; 


time_period = [0 10];
initial = [0.5, 0]';

Q_t = @(t,x) [x(2); -x(1)-2*x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 1); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -x(1)-2*x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 2); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); x(1)+2*x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 3); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); x(1)+2*x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 4); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -2*x(1)-2*x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 5); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -2*x(1)-2*x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 6); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 7); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 8); plot(t, Q(:,1)); grid on; hold on; 


time_period = [0 10];
initial = [-0.5, 0]';

Q_t = @(t,x) [x(2); -x(1)-2*x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 1); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -x(1)-2*x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 2); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); x(1)+2*x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 3); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); x(1)+2*x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 4); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -2*x(1)-2*x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 5); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -2*x(1)-2*x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 6); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -x(2)];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 7); plot(t, Q(:,1)); grid on; hold on; 

Q_t = @(t,x) [x(2); -x(2) + t];
[t,Q] = ode45(Q_t, time_period, initial);
subplot(4, 2, 8); plot(t, Q(:,1)); grid on; hold on; 