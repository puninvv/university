clear all;
clc;

DiffPlot([0 0]);

k1 = fminsearch('J', [-1 -1]);
DiffPlot(k1);
k2 = fminsearch('J', [-1 0]);
DiffPlot(k2);
k3 = fminsearch('J', [-1 1]);
DiffPlot(k3);


k4 = fminsearch('J', [0 -1]);
DiffPlot(k4);
k5 = fminsearch('J', [0 0]);
DiffPlot(k5);
k6 = fminsearch('J', [0 1]);
DiffPlot(k6);


k7 = fminsearch('J', [1 -1]);
DiffPlot(k7);
k8 = fminsearch('J', [1 0]);
DiffPlot(k8);
k9 = fminsearch('J', [1 1]);
DiffPlot(k9);