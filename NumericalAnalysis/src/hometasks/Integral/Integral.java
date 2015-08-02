/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hometasks.Integral;

import hometasks.Matrixes.Matrix;

/**
 *
 * @author Виктор
 */
public class Integral {
    
    private static final double resultIntegral = 6.23898;
    public static void HowToUse(){
        
        System.out.println("Контрольное значение: " + Integral.resultIntegral);
        
        double a = Integral.a;
        double b = Integral.b;
        Integral integral = new Integral();
	double J = integral.NewtonCotes(a, b);
        System.out.println("Значение интеграла по КФ Ньютона-Котеса  " + J);
	
	J=integral.Gauss(a,b,false);
        System.out.println("Значение интеграла по КФ Гаусса с использованием метода Ньютона " +J);

        J=integral.Gauss(a,b, true);
        System.out.println("Значение интеграла по КФ Гаусса с использованием метода Кардано " +J);
	
        //число шагов
        int k = 5;
	J = integral.CompositeNewtonCotes(a, b, k);
	System.out.println("Значение интеграла по CКФ Ньютона-Котеса " +J);

        J = integral.CompositeGauss(a, b, k, false);
	System.out.println("Значение интеграла по CКФ Гаусса с использованием метода Ньютона " +J);

        J = integral.CompositeGauss(a, b, k, true);
        System.out.println("Значение интеграла по CКФ Гаусса с использованием метода Кардано " +J);

                
        System.out.println("Значение интеграла по СКФ Ньютона-Котеса с оценкой по правилу Рунге " +integral.RoungeRule(a, b, k, 0));
        System.out.println("Значение интеграла по СКФ Гаусса (метод Ньютона) с оценкой по правилу Рунге " + integral.RoungeRule(a, b, k, 1));
        System.out.println("Значение интеграла по СКФ Гаусса (метод Кардано) с оценкой по правилу Рунге " + integral.RoungeRule(a, b, k, 2));
    }
    
    private final double e = 1e-12;
    private static final double a = 1.1;
    private static final double b = 2.3;
    private final int n = 3;
    private final double alpha = 4/5;
    private final double betta = 0;
    
    private double F(double x){
        return 3.5*Math.cos(0.7*x)*Math.exp(-(5/3)*x)+2.4*Math.sin(5.5*x)*Math.exp(-(3/4)*x)+5;
    }
    
    double functionForGaussMethod(double[] a, double x){
        return Math.pow(x, 3) + a[2]*x*x + a[1]*x+a[0];
    }
    double derForGaussMethod(double[] a, double x){
        return 3*x*x + 2*a[2]*x + a[1];
    }
    
    //моменты
    double m0(double z1, double z2){
        return ( Math.pow(z2 - a,1 - alpha) - Math.pow(z1- a, 1 - alpha) )/ (1 - alpha);
    }
    double m1(double z1, double z2){
        return (Math.pow(z2 - a,2 - alpha) - Math.pow(z1 - a, 2 - alpha)) / (2 - alpha) + a*m0(z1,z2);
    }
    double m2(double z1, double z2){
        return  (Math.pow(z2 - a,3 - alpha) - Math.pow(z1 - a, 3 - alpha)) / (3 - alpha) + 2*a*m1(z1,z2) - a*a*m0(z1,z2);
    }
    double m3(double z1, double z2){
        return (Math.pow(z2 - a,4 - alpha) - Math.pow(z1 - a, 4 - alpha)) / (4 - alpha) + 3*a*m2(z1,z2) - 3*a*a*m1(z1,z2) + Math.pow(a,3)*m0(z1,z2);
    }
    double m4(double z1, double z2){
        return (Math.pow(z2 - a,5 - alpha) - Math.pow(z1 - a, 5 - alpha)) / (5 - alpha) + 4*a*m3(z1,z2) - 6*a*a*m2(z1,z2) + 4*Math.pow(a,3)*m1(z1,z2) - Math.pow(a,4)*m0(z1,z2);
    }
    double m5(double z1, double z2){
        return (Math.pow(z2 - a,6 - alpha) - Math.pow(z1 - a, 6 - alpha)) / (6 - alpha) + 5*a*m4(z1,z2) - 10*a*a*m3(z1,z2)
            + 10*Math.pow(a,3)*m2(z1,z2) -5*Math.pow(a,4)*m1(z1,z2) + Math.pow(a,5)*m0(z1,z2);
    }

    double NewtonMethod(double[] a, double x0) {
        double currentX = - 1;
        double nextX = x0;

        while (Math.abs(currentX - nextX) > 1E-9) 
            {
                currentX = nextX;
                nextX = currentX - functionForGaussMethod(a, currentX) / derForGaussMethod(a, currentX);
            }
        return nextX;
    }

    double NewtonCotes(double a, double b){
            double[] t = {a, (b+a)/2, b};
            double[][] matrix_arr = new double[3][3];
            matrix_arr[0][0] = 1;
            matrix_arr[0][1] = 1;
            matrix_arr[0][2] = 1;
            matrix_arr[1][0] = t[0];
            matrix_arr[1][1] = t[1];
            matrix_arr[1][2] = t[2];
            matrix_arr[2][0] = t[0]*t[0];
            matrix_arr[2][1] = t[1]*t[1];
            matrix_arr[2][2] = t[2]*t[2];
            Matrix matrix = new Matrix(matrix_arr);

            double[] result_vec = new double[3];
            result_vec[0] = m0(a,b);
            result_vec[1] = m1(a,b);
            result_vec[2] = m2(a,b);

            double[] A = matrix.CountSLAY_PLU(result_vec);
            double J = A[0]*F(t[0]) + A[1]*F(t[1]) + A[2]*F(t[2]);
            return J;
    }

    //составные квадратурные формы Ньютона-Котеса
    double CompositeNewtonCotes(double a, double b, int k) {
        double sum = 0;
        double H = (b-a)/k;
        for (int j = 0; j < k ; j++)
            sum += NewtonCotes(a+j*H, a+(j+1)*H);
        
        return sum;
    }
    
    // false для Ньютона; true для Кардано
    double Gauss(double a, double b, boolean method) {
        double[][] matrix_arr = new double[3][3];
        matrix_arr[0][0] = m0(a,b);
        matrix_arr[0][1] = m1(a,b);
        matrix_arr[0][2] = m2(a,b);
        matrix_arr[1][0] = m1(a,b);
        matrix_arr[1][1] = m2(a,b);
        matrix_arr[1][2] = m3(a,b);
        matrix_arr[2][0] = m2(a,b);
        matrix_arr[2][1] = m3(a,b);
        matrix_arr[2][2] = m4(a,b);
        Matrix matrix = new Matrix(matrix_arr);

        double[] result_vec = new double[3];
        result_vec[0] = -m3(a,b);
        result_vec[1] = -m4(a,b);
        result_vec[2] = -m5(a,b);

        double[] A = matrix.CountSLAY_PLU(result_vec);

        double[] d = new double[3];
        d[0] = A[0];
        d[1] = A[1];
        d[2] = A[2];

        double Q = (d[2]*d[2]-3*d[1])/9;
        double R = (2*d[2]*d[2]*d[2] - 9*d[1]*d[2] + 27*d[0])/54;

        if (R*R >= Q*Q*Q)
        {
            System.out.println("Комплексные решения!");
            return 0;
        }

        double[] root = new double [3];

        if (method == false) //Newton
        {
            int i = 0;
            double t = a;
            while (t <= b && i <= 2)
            {
                if (functionForGaussMethod(d, t)*functionForGaussMethod(d, t+0.0001) <= 0)
                {
                    root[i] = t;
                    root[i] = NewtonMethod(d, root[i]);
                    i++;
                }
                t+=0.0001;
            }
        }
        else // Cardano
        {
            double t = d[2]*Math.cos(R/Math.sqrt(Q*Q*Q))/3;
            root[0] = -2*Math.sqrt(Q)*Math.cos(t)-d[2]/3;
            root[1] = -2*Math.sqrt(Q)*Math.cos(t+(2*Math.PI/3))-d[2]/3;
            root[2] = -2*Math.sqrt(Q)*Math.cos(t-(2*Math.PI/3))-d[2]/3;
        }

        double[][] m_arr = new double[3][3];
        m_arr[0][0] = 1;
        m_arr[0][1] = 1;
        m_arr[0][2] = 1;
        m_arr[1][0] = root[0];
        m_arr[1][1] = root[1];
        m_arr[1][2] = root[2];
        m_arr[2][0] = root[0] * root[0];
        m_arr[2][1] = root[1] * root[1];
        m_arr[2][2] = root[2] * root[2];
        Matrix m = new Matrix(m_arr);

        double[] r_vec = new double[3];
        r_vec[0] = m0(a,b);
        r_vec[1] = m1(a,b);
        r_vec[2] = m2(a,b);

        double[] C = m.CountSLAY_PLU(r_vec);

        double J = C[0]*F(root[0]) + C[1]*F(root[1]) + C[2]*F(root[2]);

        return J;
    }

    //СОставные квадратурные формы Гаусса
    // false для Ньютона; true для Кардано
    double CompositeGauss(double a, double b, double k, boolean method) {
        double sum = 0;
        double H = (b-a)/k;
        for (int j = 0; j < k ; j++)
            sum += Gauss(a+j*H, a+(j+1)*H, method);
        return sum;
    }

    double RoungeRule(double a, double b, int k, int method){
        int L = 2;
        double delta = 1;
        if (method == 0) // 0 for Newton-Cotes
        {
            int k1 = 2;
            int k2 = 2;
            while (delta >= e)
            {
                k1 = k;
                k2 = L*k;
                delta = Math.abs(CompositeNewtonCotes(a, b, k1)-CompositeNewtonCotes(a, b, k2))/(Math.pow((double)L,3.0)-1);
                k*=L;
            }
            System.out.println("Количество шагов: "+k);
            return CompositeNewtonCotes(a, b, k2);
        }
        else if (method == 1) // 1 for Gauss using Newton method
        {
            int k1 = 2;
            int k2 = 2;
            while (delta >= e)
            {
                k1 = k;
                k2 = L*k;
                delta = Math.abs(CompositeGauss(a, b, k1, false)-CompositeGauss(a, b, k2, false))/(Math.pow((double)L,6.0)-1);
                k*=L;
            }
            System.out.println("Количество шагов: "+k);
            return CompositeNewtonCotes(a, b, k2);
        }
        else if (method == 2) // 2 for Gauss using Cardano method
        {
            int k1 = 2;
            int k2 = 2;
            while (delta >= 0.5E-7)
            {
                k1 = k;
                k2 = L*k;
                delta = Math.abs(CompositeGauss(a, b, k1, true)-CompositeGauss(a, b, k2, true))/(Math.pow((double)L,6.0)-1);
                k*=L;
            }
            System.out.println("Количество шагов: "+k);
            return CompositeNewtonCotes(a, b, k2);
        }
        else
            System.out.println("Используйте другой метод");

        return -1;
    }
}
