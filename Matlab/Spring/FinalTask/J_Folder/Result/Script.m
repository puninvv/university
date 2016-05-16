clear all;
clc;

DiffPlot([0 0]);

%k = fminsearch('J_10_10_1', [0 0]);
%DiffPlot(k);
k = fminsearch('J_100_100_1', [0 0]);
DiffPlot(k);
%k = fminsearch('J_1000_1000_1', [0 0]);
%DiffPlot(k);
%k = fminsearch('J_10000_10000_1', [0 0]);
%DiffPlot(k);
k = fminsearch('J_50_50_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_25_25_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_30_30_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_35_35_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_40_40_1', [0 0]);
DiffPlot(k);

%legend('u == 0','10 10 1', '100 100 1','1000 1000 1','10000 10000 1','50 50 1');
legend('u == 0','100 100 1', '50 50 1','25 25 1','30 30 1','35 35 1','40 40 1');