clear all;
clc;

DiffPlot([0 0]);

k = fminsearch('J_1_100_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_1_100_10', [0 0]);
DiffPlot(k);
k = fminsearch('J_1_100_100', [0 0]);
DiffPlot(k);
k = fminsearch('J_1_100_1000', [0 0]);
DiffPlot(k);


legend('u == 0','1 100 1', '1 100 10','1 100 100','1 100 1000');