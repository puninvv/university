clear all;
clc;

DiffPlot([0 0]);

k = fminsearch('J_1_1_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_10_10_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_10_100_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_100_10_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_100_100_1', [0 0]);
DiffPlot(k);

legend('u == 0','1 1 1', '10 10 1','10 100 1', '100 10 1', '100 100 1');

