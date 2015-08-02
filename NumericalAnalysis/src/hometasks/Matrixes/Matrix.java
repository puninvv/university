/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hometasks.Matrixes;

import java.util.Random;

/**@author Виктор*/
public class Matrix {
    private double[][] matrix;
    private int size;
    
    private int[] P;
    private double[][] LU;
    
    private double[][] Q;
    private double[][] R;
           
    public Matrix(int n, double from, double to){
        Random rnd = new Random();
        
        matrix = new double[n][n];
        size = n;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                matrix[i][j] = from + rnd.nextDouble() * (to - from);
        
        P = null;
        LU = null;
        
        Q = null;
        R = null;
    }
    public Matrix(double[][] m){
        P = null;
        LU = null;
        Q = null;
        R = null;
        size = m.length;
        matrix = new double[size][size];
        for (int i = 0; i < size; i++)
            System.arraycopy(m[i], 0, matrix[i], 0, size);    
    }
    public Matrix(Matrix tmp){
        size = tmp.size;
        matrix = new double[size][size];
        for (int i = 0; i < size; i++)
            System.arraycopy(tmp.matrix[i],0,matrix[i],0,size);
        
        
        if (tmp.P != null){
            P = new int[size];
            System.arraycopy(tmp.P,0,P,0,size);
        }
        else
            P = null;
        
        if (tmp.LU != null){
            LU = new double[size][size];
            for (int i = 0; i < size; i++)
                System.arraycopy(tmp.LU[i],0,LU[i],0,size);
        }
        else
            LU = null;
        
        if (tmp.Q != null){
            Q = new double[size][size];
            for (int i = 0; i < size; i++)
                System.arraycopy(tmp.Q[i],0,Q[i],0,size);
        }
        else
           Q = null;
        
        if (tmp.R != null){
            R = new double[size][size];
            for (int i = 0; i < size; i++)
                System.arraycopy(tmp.R[i],0,R[i],0,size);
        }
        else
           R = null;
    }
    public Matrix(int n,  double from, double to, boolean with_diag){
        //если with_diag - получим матрицу с диагональным преобладанием
        //если !with_diag - получим матрицу положительно определенную
        
        Random rnd = new Random();
        
        matrix = new double[n][n];
        size = n;
        if (with_diag){
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (i == j)
                        matrix[i][j] += 5;
                    else
                    {
                        matrix[i][j] = from + (to-from)*rnd.nextDouble();
                        matrix[i][i] += Math.abs(matrix[i][j]);
                    }
        }
        else
        {
            double[][] matrix_non_trans = new double[size][size];
            double[][] matrix_trans = new double[size][size];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    matrix_non_trans[i][j] = from + rnd.nextDouble() * (to - from);
                    matrix_trans[j][i] = matrix_non_trans[i][j];
                }       
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++)
                        matrix[i][j] += matrix_non_trans[i][k] * matrix_trans[k][j];
        }
        P = null;
        LU = null;
        
        Q = null;
        R = null;   
    }
    
    public double GetIJ(int i, int j){ return matrix[i][j];}
    public double GetNorm1OfMatrix(){
        double norm = 0;
        double sum;
        for (int i = 0; i < size; i++){
            sum = 0;
            for (int j = 0; j < size; j++)
                sum += Math.abs(matrix[j][i]);
            if (sum > norm) norm = sum;
        }
        return norm;
    }
    public double GetNormInfOfMatrix(){
        double norm = 0;
        double sum;
        for (int i = 0; i < size; i++){
            sum = 0;
            for (int j = 0; j < size; j++)
                sum += Math.abs(matrix[i][j]);
            if (sum > norm) norm = sum;
        }
        return norm;
    }
    public double GetNormOfMatrix(){
        double norm = 0;
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                if (Math.abs(matrix[i][j]) > norm) norm = Math.abs(matrix[i][j]);
        return norm;
    }
    public double GetNormEuclid(){
        double norm = 0;
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                norm += matrix[i][j] * matrix[i][j];
        return Math.sqrt(norm);    
    }
    public double GetCond(){
        try{
            return GetNorm1OfMatrix()*CountBackMatrix().GetNorm1OfMatrix();
        }
        catch(Exception ex){
            return 0;
        }
    }
    public void Print(){
        for (int i = 0; i < size; i++){
            for (int j = 0; j < size; j++)
                System.out.print(String.format( "%.3f", matrix[i][j] )+" ");
            System.out.println();
        }
    }
    public void CheckVector(double[] x, double[] b){
        if (x == null) 
            System.out.println("Система не совместна!");
        else
        {
            for (int i = 0; i < size; i++)
                System.out.print(x[i]+" ");
            System.out.println();
            System.out.println("Проверка:");
            double sum;
            for (int i = 0; i < size; i++)
            {
                sum = 0;
                for (int j = 0; j < size; j++)
                {
                    System.out.print("("+String.format( "%.2f", matrix[i][j] )+"*"+String.format( "%.2f", x[j] )+")");
                    if (j < size -1)
                        System.out.print("+");
                    else
                        System.out.print("=");
                    sum += matrix[i][j]*x[j];
                }    
                System.out.println(sum+", а должно быть: "+b[i]+" ,причем невязка: "+(b[i]-sum));
            }
            System.out.println();    
        }
    }
    
    public Matrix multiply(Matrix tmp){
        Matrix result = new Matrix(tmp.size,0,0);
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                for (int k = 0; k < size; k++)
                    result.matrix[i][j] += matrix[i][k] * tmp.matrix[k][j];
        return result;
    }
   
    /*Всё, что связано с plu*/
    private boolean CountPLU(boolean flag){
        //если plu уже есть - ничего не делаем   
        if (P != null)
            return true;
        
        P = new int[size];
        for (int i = 0; i < size; i++) P[i] = i;
        
        LU = new double[size][size];
        for (int i = 0; i < size; i++)
            System.arraycopy(matrix[i],0,LU[i],0,matrix.length);
        
        int err = 0;
        for (int i = 0; i < size - 1; i++){
            //поиск максимального
            if (err + i == size) break;
            double max_val = Math.abs(LU[i][i+err]);
            int n_mv = i;
            for (int j = i+1; j < size; j++)
                if (Math.abs(LU[j][i+err]) > max_val){
                    max_val = Math.abs(LU[j][i+err]);
                    n_mv = j;
                }
            
            if (max_val == 0 && flag == false){
                P = null;
                LU = null;
                return false;
            }
            else            
            if (max_val == 0)
            {
                err++;
                i--;
                continue;
            }
            
            //перестановка
            if (n_mv != i){
                double[] tmp_d = LU[i];
                LU[i] = LU[n_mv];
                LU[n_mv] = tmp_d;
                
                int tmp_i = P[i];
                P[i] = P[n_mv];
                P[n_mv] = tmp_i;
            }
           
            if (max_val != 0)
                for (int j = i+1; j < size; j++)
                {
                    double koef = LU[j][i+err]/LU[i][i+err];
                    for (int k = i+err; k < size; k++)
                        LU[j][k] -= LU[i][k]*koef;
                    LU[j][i] = koef;
                }    
        }
        return true;
    }   
    public double CountDet(){
        CountPLU(true);
        double det = 1;
        int[] P_tmp = new int[size];
        System.arraycopy(P, 0, P_tmp, 0, size);
        
        int k = 0;
        for (int i = 0; i < size; i++){
            if (P[i] != i){
                for (int j = i+1; j < size; j++)
                    if (P_tmp[j] == i){
                        int tmp = P_tmp[j];
                        P_tmp[j] = P_tmp[i];
                        P_tmp[i] = tmp;
                        break;
                    }
                k++;
            }
        }
        for (int i = 0; i < size; i++)
            det *= LU[i][i];
        if (k%2.0 == 1) det *= -1;
        return det;
    }   
    public int CountRank(){
        CountPLU(true);
        for (int i = 0; i < size; i++){
            boolean string_is_null = true;
            for (int j = i; j < size; j++)
            {
                if (LU[i][j] != 0) 
                {
                    string_is_null = false;
                    break;
                }
            }
            if (string_is_null) return i;
        }
        return size;
    }
    public double[] CountSLAY_PLU(double[] b){
        double[] b_tmp = new double[size];
        int rank = CountRank();
        //если матрица не вырождена
        if (rank == size){
            //переворачиваем столбец b
            for (int i = 0; i < size; i++)
                b_tmp[i] = b[P[i]];

            //Ly = b
            double[] y = new double[size];
            double sum;
            for (int i = 0; i < size; i++)
            {
                sum = 0;
                for (int j = 0; j < i; j++)
                    sum += y[j] * LU[i][j];
                y[i] = b_tmp[i] - sum;
            }    

            //Ux = y
            double[] x = new double[size];
            for (int i = size - 1; i >= 0; i--)
            {
                sum = 0;
                for (int j = size - 1; j > i; j--)
                    sum += x[j] * LU[i][j];
                x[i] = (y[i] - sum) / LU[i][i];
            }

            return x;
        }
        
        //матрица вырождена
        System.arraycopy(b, 0, b_tmp, 0, size);
        double[][] tmp_LU = new double[size][size];
        for (int i = 0; i < size; i++)
            System.arraycopy(matrix[i],0,tmp_LU[i],0,size);
        //теперь у нас есть матрица lu и b
        
        
        int err = 0;
        for (int i = 0; i < size - 1; i++){
            //поиск максимального
            if (err + i == size) break;
            double max_val = Math.abs(tmp_LU[i][i+err]);
            int n_mv = i;
            for (int j = i; j < size; j++)
                if (Math.abs(tmp_LU[j][i+err]) > max_val){
                    max_val = Math.abs(tmp_LU[j][i+err]);
                    n_mv = j;
                }
            
            if (max_val == 0)
            {
                err++;
                i--;
                continue;
            }
            
            //перестановка
            if (n_mv != i){
                double[] tmp_d = tmp_LU[i];
                tmp_LU[i] = tmp_LU[n_mv];
                tmp_LU[n_mv] = tmp_d;
                
                double tmp_i = b_tmp[i];
                b_tmp[i] = b_tmp[n_mv];
                b_tmp[n_mv] = tmp_i;
            }
           
            if (max_val != 0)
                for (int j = i+1; j < size; j++)
                {
                    double koef = tmp_LU[j][i+err]/tmp_LU[i][i+err];
                    for (int k = i+err; k < size; k++)
                        tmp_LU[j][k] -= tmp_LU[i][k]*koef;
                    b_tmp[j] -= b_tmp[i] * koef;
                }    
        }
                 
        //теперь проверяем, нет ли нулевых строк, с ненулевым значением
        for (int i = size-1; i >= rank; i--)
            if (b_tmp[i] != 0) return null; 

        
        double[] x = new double[size];
        for (int i = 0; i < size; i++)
            x[i] = 0;
        
        for (int i = rank - 1; i >= 0; i--)
        {
            int j = 0;
            while ( j < size - 1 && tmp_LU[i][j] == 0) j++;
            
            double sum = 0;
            for (int k = size-1; k > j; k--)
                sum += x[k] * tmp_LU[i][k];
            
            x[j] = (b_tmp[i]-sum)/tmp_LU[i][j];
        }
        return x;
    }
    public Matrix CountBackMatrix(){
        if (CountDet() == 0)
            return null;
        
        double[][] result = new double[size][size];
        
        double[] b_i = new double[size];
        for (int i = 0; i < size; i++)
            b_i[i] = 0;
        
        double[] vector;
        for (int i = 0; i < size; i++){
            b_i[i] = 1;
            if (i != 0) b_i[i-1] = 0;
            vector = CountSLAY_PLU(b_i);
            for (int j = 0; j < size; j++)
                result[j][i] = vector[j];
        }
        
        return new Matrix(result);
    }
    
    public Matrix GetP(){
        CountPLU(true);
        Matrix result = new Matrix(size,0,0);
        for (int i = 0; i < size; i++)
                result.matrix[i][P[i]] = 1;
        return result;
    }
    public Matrix GetL(){
        CountPLU(true);
        Matrix result = new Matrix(size, 0, 0);
	for (int i = 1; i < size; i++)
            for (int j = 0; j < i; j++)
		result.matrix[i][j] = LU[i][j];
	for (int i = 0; i < size; i++)
            result.matrix[i][i] = 1;
	return result;
    }
    public Matrix GetU(){
        CountPLU(true);
        Matrix result = new Matrix(size, 0, 0);
	for (int i = 0; i < size; i++)
            for (int j = i; j < size; j++)
		result.matrix[i][j] = LU[i][j];
	return result;
    }
    
    /*Всё, что связано с qr*/    
    private void CountQR(){
        Q = new double[size][size];
        R = new double[size][size];
        
        for (int i = 0; i < size; i++)
            System.arraycopy(matrix[i],0, Q[i], 0, size);
        
        for (int k = 0; k < size; k++){
            for (int i = 0; i < k; i++){
                double s = 0;
                for (int j = 0; j < size; j++)
                    s += Q[j][i]*Q[j][k];
                R[i][k] = s;
            }

            for (int i = 0; i < k; i++)
                for (int j = 0; j < size; j++)
                    Q[j][k] -= Q[j][i] * R[i][k];
                    
            double s = 0;
            for (int j = 0; j < size; j++) 
                s += Q[j][k]*Q[j][k];
            R[k][k] = Math.sqrt(s);
            
            for (int j = 0; j < size; j++) 
                Q[j][k] /= R[k][k];
        }
    }
    public Matrix GetQ(){
        if (Q == null)
            CountQR();
        return new Matrix(Q);
    }
    public Matrix GetR(){
        if (Q == null)
            CountQR();
        return new Matrix(R);
    }
    public double[] CountSLAY_QR(double[] b){
        if (Q == null) CountQR();
        double[] b_tmp = new double[size];
        for (int i = 0; i < size; i++)
            for (int j = i + 1; j < size; j++){
                double tmp = Q[i][j];
                Q[i][j] = Q[j][i];
                Q[j][i] = tmp;
            }
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                b_tmp[i] += Q[i][j] * b[j];
        double[] x = new double[size];
        for (int i = size - 1; i >= 0; i--)
        {
            double sum = 0;
            for (int j = size - 1; j > i; j--)
                sum += x[j] * R[i][j];
            x[i] = (b_tmp[i] - sum) / R[i][i];
        }
        
        for (int i = 0; i < size; i++)
           for (int j = i + 1; j < size; j++){
               double tmp = Q[i][j];
               Q[i][j] = Q[j][i];
               Q[j][i] = tmp;
           }
        return x;
    }
    
    /*Всё, что связано с итерационным методом*/
    public double[] CountSLAY_JACOBI(double[] b, double Err){
        double[] b_tmp = new double[size];
        System.arraycopy(b,0,b_tmp,0,size);
        double[] x_i = new double[size];
        double[] x_i1 = new double[size];
        double[][] alpha = new double[size][size];
        for (int i = 0; i < size; i++){
            for (int j = 0; j < i; j++)
                alpha[i][j] = -matrix[i][j]/matrix[i][i];
            alpha[i][i] = 0;
            for (int j = i + 1; j < size; j++)
                alpha[i][j] = -matrix[i][j]/matrix[i][i];        
            b_tmp[i] /= matrix[i][i];
        }
        Matrix alpha_tmp = new Matrix(alpha);
        double epsilon = (1 - alpha_tmp.GetNormInfOfMatrix())/(alpha_tmp.GetNormInfOfMatrix()) * Err;
        //if (epsilon <= 0)
         //  return null;
        double error = 0;
        double error2 = 0;
        int iter = 0;
        do
        {
            
            error2 = error;
            for (int i = 0; i < size; i++){
                x_i1[i] = 0;
                for (int j = 0; j <size; j++)
                    x_i1[i] += alpha[i][j] * x_i[j]; 
                x_i1[i] += b_tmp[i];
            }
            
            double sum = 0;
            for (int i = 0; i < size; i++)
                sum += (x_i[i]-x_i1[i])*(x_i[i]-x_i1[i]);
            error = Math.sqrt(sum);
            
            System.arraycopy(x_i1,0,x_i,0,size);
            iter++;
            if (iter > 1 && error2 < error)
            {
                System.out.println("Разность начала увеличиваться на "+iter+" итерации");
                return null;
            }
            
            if (iter >= 1000) 
            {
                System.out.println("За 1000 итераций метод не сошелся");
                return null;
            }
        }  while (error >= epsilon);
        System.out.println("Ответ получена за "+iter+" итераций");
        return x_i;
    }
    public double[] CountSLAY_ZEIDEL(double[] b, double Err){
        double[] x_i = new double[size];
        double[] x_i1 = new double[size];
        boolean cont;
        int iter = 0;
        do
        {
            iter ++;
            System.arraycopy(x_i1, 0, x_i, 0, size);

            for (int i = 0; i < size; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                    sum += (matrix[i][j] * x_i1[j]);
                for (int j = i + 1; j < size; j++)
                    sum += (matrix[i][j] * x_i[j]);
                x_i1[i] = (b[i] - sum) / matrix[i][i];
            }
            
            double norm = 0;
            for (int i = 0; i < size; i++) 
                norm += (x_i[i] - x_i1[i])*(x_i[i] - x_i1[i]);

            cont = Math.sqrt(norm) < Err;
            if (iter > 2000) return null;
        }
        while (!cont);
        System.out.println("Ответ получена за "+iter+" итераций");
        return x_i1;
    }
    
    /*Как этим пользоваться*/
    public static void HowToUse(){ 
        double from = -5;
        double to = 5;
        int size = 6;


        /*Обычная матрица, PLU разложение*/
        Matrix a = new Matrix(size,from,to);
        System.out.println("Исходная матрица:");
        a.Print();
        System.out.println("Определитель = " + a.CountDet());
        System.out.println("Ранг = "+a.CountRank());
        System.out.println("Число обусловленности = "+a.GetCond());

        Matrix P = a.GetP();
        System.out.println("Матрица P:");
        P.Print();

        Matrix L = a.GetL();
        System.out.println("Матрица L:");
        L.Print();

        Matrix U = a.GetU();
        System.out.println("Матрица U:");
        U.Print();

        Matrix Pa = a.GetP().multiply(a);
        System.out.println("Матрица PA:");
        Pa.Print();

        Matrix LU = a.GetL().multiply(a.GetU());
        System.out.println("Матрица LU:");
        LU.Print();

        System.out.println("Столбец b:");
        double[] b = new double[size];
        Random rnd = new Random();
        for (int i = 0; i < size; i++)
        {
            b[i] = rnd.nextDouble()*(to - from) + to;
            System.out.print(b[i]+" ");
        }
        System.out.println();
        double[] b_copy = new double[size];
        System.arraycopy(b, 0, b_copy, 0, size);

        System.out.println("Вектор x:");
        double[] x = a.CountSLAY_PLU(b);
        a.CheckVector(x, b);

        Matrix a1 = a.CountBackMatrix();
        System.out.println("Обратная матрица:");
        a1.Print();
        System.out.println("A * A^-1:");
        a.multiply(a1).Print();
        System.out.println("A^-1 * A:");
        a1.multiply(a).Print();        
        System.out.println();





        /*Матрица с нулевым определителем PLU*/
        double[][] m = {{1,2,2,1,0,0},{2,4,4,2,0,0},{1,1,1,1,0,0},{-1,-1,-1,-1,0,0},{0,0,1,0,0,100},{0,0,-1,0,0,-100}};
        Matrix r0 = new Matrix(m);
        System.out.println("Матрица с нулевым определителем:");
        r0.Print();
        System.out.println("Её нижне-треугольная матрица:");
        r0.GetL().Print();
        System.out.println("Её верхне-треугольная матрица:");
        r0.GetU().Print();
        System.out.println("Её матрица перестановок:");
        r0.GetP().Print();

        System.out.println("Матрица PA:");
        r0.GetP().multiply(r0).Print();

        System.out.println("Матрица LU:");
        r0.GetL().multiply(r0.GetU()).Print();

        System.out.println("Определитель = "+r0.CountDet());
        System.out.println("Ранг = "+r0.CountRank());

        b[0] = 1;
        b[1] = 2;
        b[2] = 1;
        b[3] = -1;
        b[4] = 0;
        b[5] = 0;

        System.out.println("Столбец b:");
        for (int i = 0; i < size; i++)
            System.out.print(b[i]+" ");
        System.out.println();

        System.out.println("Вектор x:");

        double[] x_r0 = r0.CountSLAY_PLU(b);
        r0.CheckVector(x_r0, b);

        System.out.println("Обратная матрица");
        Matrix r0_1 = r0.CountBackMatrix();
        if (r0_1 == null) System.out.println("Не существует обратной матрицы");
        System.out.println("Число обусловленности = "+r0.GetCond());




        /*Просто матрица, QR разложение Грамма - Шмидта*/
        System.out.println();
        System.out.println("Матрица A");
        a.Print();
        System.out.println("Матрица Q");
        a.GetQ().Print();
        System.out.println("Матрица R");
        a.GetR().Print();
        System.out.println("Матрица Q*R");
        a.GetQ().multiply(a.GetR()).Print();
        b = b_copy;

        for (int i = 0; i < size; i++)
            System.out.print(b[i]+" ");
        System.out.println();

        System.out.println("Вектор x:");
        x = a.CountSLAY_QR(b);
        a.CheckVector(x, b);

        /*Итерационный метод Якоби*/
        System.out.println("Метод Якоби:");
        Matrix Jacobi = new Matrix(size,-10,10,true);
        Jacobi.Print();
        b = new double[size];
        System.out.println("Вектор b:");
        for (int i = 0; i < size; i++){
            b[i] = i;
            System.out.print(b[i]+" ");
        }
        System.out.println();
        x = Jacobi.CountSLAY_JACOBI(b, 0.000001);
        Jacobi.CheckVector(x, b);

        /*Итерационный метод Зейделя*/
        System.out.println("Метод Зейделя:");
        Jacobi.Print();
        b = new double[size];
        System.out.println("Вектор b:");
        for (int i = 0; i < size; i++){
            b[i] = i;
            System.out.print(b[i]+" ");
        }
        System.out.println();
        x = Jacobi.CountSLAY_ZEIDEL(b, 0.00000001);
        Jacobi.CheckVector(x, b);
    }
}