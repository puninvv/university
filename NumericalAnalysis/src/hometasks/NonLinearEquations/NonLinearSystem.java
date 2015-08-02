package hometasks.NonLinearEquations;

import hometasks.Matrixes.Matrix;

public class NonLinearSystem {
    public static void HowToUse(){
        System.out.println("РЕШЕНИЕ СИСТЕМЫ НЕЛИНЕЙНЫХ УРАВНЕНИЙ");
        new NonLinearSystem().Solve(0.000000001);
    }
    
    public void Solve(double e){
        /*
            x_k+1 = x_k + dx
            dx = jacoby(x_k).count_slay(-F(x_k))
        `   if (|x_k+1 - x_k| < eps) return x_k+1;
        */
        System.out.println("Обычный метод ньютона");
        long t0 = System.nanoTime();
        
        double[] x = {0.5, 0.5, 1.5, -1.0, -0.5, 1.5, 0.5, -0.5, 1.5, -1.5};
        double error;
        int counter = 0;
        double[] x_copy = new double[x.length];
        
        do
        {
            System.arraycopy(x, 0, x_copy, 0, x.length);
            Matrix W = GetW(x);
            double[] F = GetF(x);
            double[] dx = W.CountSLAY_PLU(F);
            for (int i = 0; i < x.length; i++)
                x[i] += dx[i];
            error = Delta(x,x_copy);
            System.out.println((++counter)+"-итерация, находимся в точке: "+x[0]+" причем ошибка составляет: "+ error);
            //Matrix W = new Matrix()
        } while (error > e && counter < 2);
        long t1 = System.nanoTime();
        long t_nm = t1 - t0;
        System.out.println("На все про все ушло "+t_nm+" наносекунд");
        Check(x);

        System.out.println();
        
        
        System.out.println("Модифицированный метод Ньютона");
        t0 = System.nanoTime();
        double[] x_old = new double[x.length];
        System.arraycopy(x, 0, x_old, 0, x.length);
        counter = 0;
        Matrix W = GetW(x_old);
        x_copy = new double[x_old.length];
        do
        {
            System.arraycopy(x_old, 0, x_copy, 0, x.length);
            double[] F = GetF(x_old);
            double[] dx = W.CountSLAY_PLU(F);
            for (int i = 0; i < x_old.length; i++)
                x_old[i] += dx[i];
            error = Delta(x_old,x_copy);
            System.out.println((++counter)+"-итерация, причем ошибка составляет: "+ error);
            //Matrix W = new Matrix()
        } while (error > e && counter < 1000);
        t1 = System.nanoTime();
        long t_m = t1-t0;
        
        System.out.println("На все про все ушло "+t_m+" наносекунд");
        Check(x_old);
        
        System.out.println();
        System.out.println("Еще раз вектора:");
        for(int i = 0; i < 10; i++)
            System.out.println(i+"компонента: "+x[i]+" vs " + x_old[i]+" НЕВЯЗКА: "+(x_old[i]-x[i]));
        System.out.println("Модифицированный быстрее обычного на "+(t_nm-t_m)+" наносекунд!");
    }
    private void Check(double[] x){
        System.out.println("Вектор:");
        for (int i = 0; i < x.length; i++)
            System.out.print(x[i]+" ");
        System.out.println();
        double[] fi = new double[x.length];
        fi[0] = new F1().GetResult(x);
        fi[1] = new F2().GetResult(x);
        fi[2] = new F3().GetResult(x);
        fi[3] = new F4().GetResult(x);
        fi[4] = new F5().GetResult(x);
        fi[5] = new F6().GetResult(x);
        fi[6] = new F7().GetResult(x);
        fi[7] = new F8().GetResult(x);
        fi[8] = new F9().GetResult(x);
        fi[9] = new F10().GetResult(x);
        for (int i = 0; i < 10; i++){
            System.out.println("Значение функции:" +fi[i]+", а должны получить 0");
        }
    }
    
    private double Delta(double[] first, double[] second){
        double err = Math.abs(second[0] - first[0]);
        for (int i = 1; i < first.length; i++)
            if (Math.abs(second[i] - first[i]) > err)
                err = Math.abs(second[i] - first[i]);
        return err;
    }
    
    private Matrix GetW(double[] x){
        double[][] matrix = new double[x.length][x.length];
        matrix[0][0] = new J11().GetResult(x); matrix[0][1] = new J12().GetResult(x); matrix[0][2] = new J13().GetResult(x);
        matrix[0][3] = new J14().GetResult(x); matrix[0][4] = new J15().GetResult(x); matrix[0][5] = new J16().GetResult(x);
        matrix[0][6] = new J17().GetResult(x); matrix[0][7] = new J18().GetResult(x); matrix[0][8] = new J19().GetResult(x);
        matrix[0][9] = new J110().GetResult(x);
        
        matrix[1][0] = new J21().GetResult(x); matrix[1][1] = new J22().GetResult(x); matrix[1][2] = new J23().GetResult(x);
        matrix[1][3] = new J24().GetResult(x); matrix[1][4] = new J25().GetResult(x); matrix[1][5] = new J26().GetResult(x);
        matrix[1][6] = new J27().GetResult(x); matrix[1][7] = new J28().GetResult(x); matrix[1][8] = new J29().GetResult(x);
        matrix[1][9] = new J210().GetResult(x);
        
        matrix[2][0] = new J31().GetResult(x); matrix[2][1] = new J32().GetResult(x); matrix[2][2] = new J33().GetResult(x);
        matrix[2][3] = new J34().GetResult(x); matrix[2][4] = new J35().GetResult(x); matrix[2][5] = new J36().GetResult(x);
        matrix[2][6] = new J37().GetResult(x); matrix[2][7] = new J38().GetResult(x); matrix[2][8] = new J39().GetResult(x);
        matrix[2][9] = new J310().GetResult(x);

        matrix[3][0] = new J41().GetResult(x); matrix[3][1] = new J42().GetResult(x); matrix[3][2] = new J43().GetResult(x);
        matrix[3][3] = new J44().GetResult(x); matrix[3][4] = new J45().GetResult(x); matrix[3][5] = new J46().GetResult(x);
        matrix[3][6] = new J47().GetResult(x); matrix[3][7] = new J48().GetResult(x); matrix[3][8] = new J49().GetResult(x);
        matrix[3][9] = new J410().GetResult(x);
        
        matrix[4][0] = new J51().GetResult(x); matrix[4][1] = new J52().GetResult(x); matrix[4][2] = new J53().GetResult(x);
        matrix[4][3] = new J54().GetResult(x); matrix[4][4] = new J55().GetResult(x); matrix[4][5] = new J56().GetResult(x);
        matrix[4][6] = new J57().GetResult(x); matrix[4][7] = new J58().GetResult(x); matrix[4][8] = new J59().GetResult(x);
        matrix[4][9] = new J510().GetResult(x);
        
        matrix[5][0] = new J61().GetResult(x); matrix[5][1] = new J62().GetResult(x); matrix[5][2] = new J63().GetResult(x);
        matrix[5][3] = new J64().GetResult(x); matrix[5][4] = new J65().GetResult(x); matrix[5][5] = new J66().GetResult(x);
        matrix[5][6] = new J67().GetResult(x); matrix[5][7] = new J68().GetResult(x); matrix[5][8] = new J69().GetResult(x);
        matrix[5][9] = new J610().GetResult(x);
        
        matrix[6][0] = new J71().GetResult(x); matrix[6][1] = new J72().GetResult(x); matrix[6][2] = new J73().GetResult(x);
        matrix[6][3] = new J74().GetResult(x); matrix[6][4] = new J75().GetResult(x); matrix[6][5] = new J76().GetResult(x);
        matrix[6][6] = new J77().GetResult(x); matrix[6][7] = new J78().GetResult(x); matrix[6][8] = new J79().GetResult(x);
        matrix[6][9] = new J710().GetResult(x);
        
        matrix[7][0] = new J81().GetResult(x); matrix[7][1] = new J82().GetResult(x); matrix[7][2] = new J83().GetResult(x);
        matrix[7][3] = new J84().GetResult(x); matrix[7][4] = new J85().GetResult(x); matrix[7][5] = new J86().GetResult(x);
        matrix[7][6] = new J87().GetResult(x); matrix[7][7] = new J88().GetResult(x); matrix[7][8] = new J89().GetResult(x);
        matrix[7][9] = new J810().GetResult(x);
        
        matrix[8][0] = new J91().GetResult(x); matrix[8][1] = new J92().GetResult(x); matrix[8][2] = new J93().GetResult(x);
        matrix[8][3] = new J94().GetResult(x); matrix[8][4] = new J95().GetResult(x); matrix[8][5] = new J96().GetResult(x);
        matrix[8][6] = new J97().GetResult(x); matrix[8][7] = new J98().GetResult(x); matrix[8][8] = new J99().GetResult(x);
        matrix[8][9] = new J910().GetResult(x);

        matrix[9][0] = new J101().GetResult(x); matrix[9][1] = new J102().GetResult(x); matrix[9][2] = new J103().GetResult(x);
        matrix[9][3] = new J104().GetResult(x); matrix[9][4] = new J105().GetResult(x); matrix[9][5] = new J106().GetResult(x);
        matrix[9][6] = new J107().GetResult(x); matrix[9][7] = new J108().GetResult(x); matrix[9][8] = new J109().GetResult(x);
        matrix[9][9] = new J1010().GetResult(x);
        
        return new hometasks.Matrixes.Matrix(matrix);
    }
    private double[] GetF(double[] x){
        double[] result = new double[x.length];
        result[0] = -new F1().GetResult(x);
        result[1] = -new F2().GetResult(x);
        result[2] = -new F3().GetResult(x);
        result[3] = -new F4().GetResult(x);
        result[4] = -new F5().GetResult(x);
        result[5] = -new F6().GetResult(x);
        result[6] = -new F7().GetResult(x);
        result[7] = -new F8().GetResult(x);
        result[8] = -new F9().GetResult(x);
        result[9] = -new F10().GetResult(x);
        return result;
    }
    
    private class F1 implements Function{
        @Override
        public double GetResult(double[] x){
            return Math.cos(x[0]*x[1]) - Math.exp(-3*x[2])+ x[3]*x[4]*x[4] -
                    x[5] - Math.sinh(2*x[7]) * x[8] + 2*x[9] + 
                    2.0004339741653854440;
        }
    }
    private class F2 implements Function{
        @Override
        public double GetResult(double[] x){
            return Math.sin(x[0]*x[1]) + x[2]*x[6]*x[8] -
                    Math.exp(-x[9]+x[5]) + 3*x[4]*x[4] - x[5]*(x[7]+1) +
                    10.886272036407019994;
        }
    }
    private class F3 implements Function{
        @Override
        public double GetResult(double[] x){
            return x[0] - x[1] + x[2] - x[3] + x[4] - x[5] +
                   x[6] - x[7] + x[8] - x[9] -
                   3.1361904761904761904;
        }
    }
    private class F4 implements Function{
        @Override
        public double GetResult(double[] x) {
            return  2 * Math.cos(-x[8]+x[3]) + x[4]/(x[2] + x[0]) - Math.sin(x[1]*x[1]) +
                    Math.cos(x[6]*x[9]) * Math.cos(x[6]*x[9]) - x[7] -
                    0.1707472705022304757;
        }
    }
    private class F5 implements Function{
        @Override
        public double GetResult(double[] x) {
            return  Math.sin(x[4]) + 2 * x[7] * (x[2] + x[0]) -
                    Math.exp(-x[6] * (-x[9]+x[5])) + 2* Math.cos(x[1]) - 
                    1 / (x[3] - x[8]) - 0.3685896273101277862;
        }
    }
    private class F6 implements Function{
        @Override
        public double GetResult(double[] x) {
            return Math.exp(x[0] - x[3] - x[8]) + x[4]*x[4]/x[7]+
                   0.5 * Math.cos(3 * x[9] * x[1]) - x[5]*x[2] +
                   2.0491086016771875115;
        }
    }
    private class F7 implements Function{
        @Override
        public double GetResult(double[] x) {
            return x[1]*x[1]*x[1]*x[6] - Math.sin(x[9]/x[4] + x[7]) +
                    (x[0]-x[5]) * Math.cos(x[3]) + x[2] - 
                    0.7380430076202798014;
        }
    }
    private class F8 implements Function{
        @Override
        public double GetResult(double[] x) {
            return x[4] * (x[0] - 2 * x[5]) * (x[0] - 2 * x[5]) - 2 * Math.sin(-x[8]+x[2]) +
                    1.5*x[3] - Math.exp(x[1]*x[6]+x[9]) +
                    3.5668321989693809040;
        }
    }
    private class F9 implements Function{
        @Override
        public double GetResult(double[] x) {
            return 7 / x[5] + Math.exp(x[4]+x[3]) - 2* x[1]*x[7]*x[9]*x[6] +
                    3* x[8] - 3*x[0] - 
                    8.4394734508383257499;
        }
    }
    private class F10 implements Function{
        @Override
        public double GetResult(double[] x) {
            return x[9]*x[0]+x[8]*x[1]-x[7]*x[2] + 
                    Math.sin(x[3] + x[4] + x[5]) * x[6] -
                    0.78238095238095238096;
        }
    }
    
    private class J11 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -Math.sin(x[0]*x[1])*x[1];
        }
    
    }
    private class J12 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -Math.sin(x[0]*x[1])*x[0];
        }
    
    }
    private class J13 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 3 * Math.exp(-3*x[2]);
        }
    
    }
    private class J14 implements Function{

        @Override
        public double GetResult(double[] x) {
            return x[4]*x[4];
        }
    
    }
    private class J15 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 2 * x[3] * x[4];
        }
    
    }
    private class J16 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -1;
        }
    
    }
    private class J17 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 0;
        }
    
    }
    private class J18 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2 * Math.cosh(2*x[7])*x[8];
        }
    
    }
    private class J19 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -Math.sinh(2*x[7]);
        }
    
    }
    private class J110 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 2;
        }
    
    }
    
    private class J21 implements Function{

        @Override
        public double GetResult(double[] x) {
            return  Math.cos(x[0] * x[1]) * x[1];
        }
    
    }
    private class J22 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.cos(x[0]*x[1])*x[0];
        }
    
    }
    private class J23 implements Function{

        @Override
        public double GetResult(double[] x) {
            return x[8]*x[6];
        }
    
    }
    private class J24 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 0;
        }
    
    }
    private class J25 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 6 * x[4];
        }
    
    }
    private class J26 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -Math.exp(-x[9]+x[5])-x[7]-1;
        }
    
    }
    private class J27 implements Function{

        @Override
        public double GetResult(double[] x) {
            return x[2]*x[8];
        }
    
    }
    private class J28 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -x[5];
        }
    
    }
    private class J29 implements Function{

        @Override
        public double GetResult(double[] x) {
            return x[2]*x[6];
        }
    
    }
    private class J210 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.exp(-x[9]+x[5]);
        }
    
    }
    
    private class J31 implements Function{

        @Override
        public double GetResult(double[] x) {
            return  1;
        }
    
    }
    private class J32 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -1;
        }
    
    }
    private class J33 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 1;
        }
    
    }
    private class J34 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -1;
        }
    
    }
    private class J35 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 1;
        }
    
    }
    private class J36 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -1;
        }
    
    }
    private class J37 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 1;
        }
    
    }
    private class J38 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -1;
        }
    
    }
    private class J39 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 1;
        }
    
    }
    private class J310 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -1;
        }
    
    }
    
    private class J41 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -x[4]/((x[2]+x[0])*(x[2]+x[0]));
        }
    
    }
    private class J42 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2*Math.cos(x[1]*x[1])*x[1];
        }
    
    }
    private class J43 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -x[4]/((x[2]+x[0])*(x[2]+x[0]));
        }
    
    }
    private class J44 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2*Math.sin(-x[8]+x[3]);
        }
    
    }
    private class J45 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 1/(x[2]+x[0]);
        }
    
    }
    private class J46 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 0;
        }
    
    }
    private class J47 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2*Math.cos(x[6]*x[9])*Math.sin(x[6]*x[9])*x[9];
        }
    
    }
    private class J48 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -1;
        }
    
    }
    private class J49 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 2*Math.sin(-x[8]+x[3]);
        }
    
    }
    private class J410 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2*Math.cos(x[6]*x[9])*Math.sin(x[6]*x[9])*x[6];
        }
    
    }
    
    private class J51 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 2*x[7];
        }
    
    }
    private class J52 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2*Math.sin(x[1]);
        }
    
    }
    private class J53 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 2*x[7];
        }
    
    }
    private class J54 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 1/((-x[8]+x[3])*(-x[8]+x[3]));
        }
    
    }
    private class J55 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.cos(x[4]);
        }
    
    }
    private class J56 implements Function{

        @Override
        public double GetResult(double[] x) {
            return x[6]*Math.exp(-x[6] * (-x[9] + x[5]));
        }
    
    }
    private class J57 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -(x[9]-x[5])*Math.exp(-x[6]*(-x[9]+x[5]));
        }
    
    }
    private class J58 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 2*x[2]+2*x[0];
        }
    
    }
    private class J59 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -1/((-x[8]+x[3])*(-x[8]+x[3]));
        }
    
    }
    private class J510 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -x[6]*Math.exp(-x[6]*(-x[9]+x[5]));
        }
    
    }
        
    private class J61 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.exp(x[0] - x[3] - x[8]);
        }
    
    }
    private class J62 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -(3.0/2.0)*Math.sin(3*x[9]*x[1])*x[9];
        }
    
    }
    private class J63 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -x[5];
        }
    
    }
    private class J64 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -Math.exp(x[0] - x[3] - x[8]);
        }
    
    }
    private class J65 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 2*x[4]/x[7];
        }
    
    }
    private class J66 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -x[2];
        }
    
    }
    private class J67 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 0;
        }
    
    }
    private class J68 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -(x[4]/x[7])*(x[4]/x[7]);
        }
    
    }
    private class J69 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -Math.exp(x[0] - x[3] - x[8]);
        }
    
    }
    private class J610 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -(3.0/2.0)*Math.sin(3*x[9]*x[1])*x[1];
        }
    
    }
    
    private class J71 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.cos(x[3]);
        }
    
    }
    private class J72 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 3*x[1]*x[1]*x[6];
        }
    
    }
    private class J73 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 1;
        }
    
    }
    private class J74 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -(x[0]-x[5])*Math.sin(x[3]);
        }
    
    }
    private class J75 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.cos(x[9]/x[4]+x[7])*x[9]/(x[4]*x[4]);
        }
    
    }
    private class J76 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -Math.cos(x[3]);
        }
    
    }
    private class J77 implements Function{

        @Override
        public double GetResult(double[] x) {
            return x[1]*x[1]*x[1];
        }
    
    }
    private class J78 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -Math.cos(x[9]/x[4]+x[7]);
        }
    
    }
    private class J79 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 0;
        }
    
    }
    private class J710 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -Math.cos(x[9]/x[4]+x[7])/x[4];
        }
    
    } 

    private class J81 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 2*x[4]*(x[0]-2*x[5]);
        }
    
    }
    private class J82 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -x[6]*Math.exp(x[1]*x[6]+x[9]);
        }
    
    }
    private class J83 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2*Math.cos(-x[8]+x[2]);
        }
    
    }
    private class J84 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 1.5;
        }
    
    }
    private class J85 implements Function{

        @Override
        public double GetResult(double[] x) {
            return (x[0]-2*x[5])*(x[0]-2*x[5]);
        }
    
    }
    private class J86 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -4*x[4]*(x[0]-2*x[5]);
        }
    
    }
    private class J87 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -x[1]*Math.exp(x[1]*x[6]+x[9]);
        }
    
    }
    private class J88 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 0;
        }
    
    }
    private class J89 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 2*Math.cos(-x[8]+x[2]);
        }
    
    }
    private class J810 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -Math.exp(x[1]*x[6]+x[9]);
        }
    
    }     
    
    private class J91 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -3;
        }
    
    }
    private class J92 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2*x[7]*x[9]*x[6];
        }
    
    }
    private class J93 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 0;
        }
    
    }
    private class J94 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.exp(x[4]+x[3]);
        }
    
    }
    private class J95 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.exp(x[4]+x[3]);
        }
    
    }
    private class J96 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -7/(x[5]*x[5]);
        }
    
    }
    private class J97 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2*x[1]*x[7]*x[9];
        }
    
    }
    private class J98 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2*x[1]*x[9]*x[6];
        }
    
    }
    private class J99 implements Function{

        @Override
        public double GetResult(double[] x) {
            return 3;
        }
    
    }
    private class J910 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -2*x[1]*x[7]*x[6];
        }
    
    }     
    
    private class J101 implements Function{

        @Override
        public double GetResult(double[] x) {
            return x[9];
        }
    
    }
    private class J102 implements Function{

        @Override
        public double GetResult(double[] x) {
            return x[8];
        }
    
    }
    private class J103 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -x[7];
        }
    
    }
    private class J104 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.cos(x[3]+x[4]+x[5])*x[6];
        }
    
    }
    private class J105 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.cos(x[3]+x[4]+x[5])*x[6];
        }
    
    }
    private class J106 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.cos(x[3]+x[4]+x[5])*x[6];
        }
    
    }
    private class J107 implements Function{

        @Override
        public double GetResult(double[] x) {
            return Math.sin(x[3]+x[4]+x[5]);
        }
    
    }
    private class J108 implements Function{

        @Override
        public double GetResult(double[] x) {
            return -x[2];
        }
    
    }
    private class J109 implements Function{

        @Override
        public double GetResult(double[] x) {
            return x[1];
        }
    
    }
    private class J1010 implements Function{

        @Override
        public double GetResult(double[] x) {
            return x[0];
        }
    
    }     
}
