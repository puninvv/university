clear all;
clc;

mantissa = 53;
e = 0.0000001;
a = 0;
b = 2 * pi;
max_iter = 1000;

[df, h] = der_1_non_optimal_1porjadok(@sin,pi/2,e,max_iter);
[h_opt, M2] = find_optimal_step_1_der(@sin,a,b,1000,e,max_iter,mantissa);

g_non_optimal = find_min_guarantie_e(M2, mantissa, h);
g_optimal = find_min_guarantie_e(M2, mantissa, h_opt);

%вторая производная
M3 = 1;

h_opt2 = find_optimal_step_2_der( M3, mantissa );
[ df2, h2 ] = der_2_non_optimal_2porjadok( @sin, pi/2, e, max_iter);

g_non_optimal2 = find_min_guarantie_e(M3, mantissa, h2);
g_optimal2 = find_min_guarantie_e(M3, mantissa, h_opt2);

