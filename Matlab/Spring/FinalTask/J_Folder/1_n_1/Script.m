clear all;
clc;

DiffPlot([0 0]);

k = fminsearch('J_1_1_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_1_10_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_1_100_1', [0 0]);
DiffPlot(k);
k = fminsearch('J_1_1000_1', [0 0]);
DiffPlot(k);


legend('u == 0','1 1 1', '1 10 1','1 100 1','1 1000 1');