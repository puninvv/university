function [ res ] = Copter(K)
z_final=10;
m=1;

fly=@(t,z)[z(2); -(K(1)*(z(1)-z_final)+K(2)*z(2))/m];
[t, Z]=ode45(fly, 0:0.01:z_final, [0 0]);
T=K(1).*(Z(1)-z_final) + Z(2).*Z(2)+9.8*m;
tmp=(Z(1)-z_final)*(Z(1)-z_final)+Z(2)+T.*T;
a= tmp(2:length(tmp));
b= tmp(1:length(tmp)-1);
c=(a+b)*0.005;
res=sum(c);
end