package hometasks.NonLinearEquations;

public class NonLinearEquation {
    public static void HowToUse(){
        System.out.println();
        System.out.println("РЕШЕНИЕ НЕЛИНЕЙНОГО УРАВНЕНИЯ");
        NonLinearEquation nle = new NonLinearEquation();
        nle.Solve(0.0000001);
    }
    
    public void Solve(double e){
        int counter = 0;
        double error;
        double[] x = {1};
        System.out.println("Начальное заначение 1");
        System.out.println("Метод Ньютона:");
        double dx = 0;
        Function f = new F();
        Function g = new G();
        long t0 = System.nanoTime();
        do
        {   
            dx = f.GetResult(x)/g.GetResult(x);
            error = Math.abs(dx);
            x[0] -= dx;
            counter++;
            System.out.println(counter+"-итерация, находимся в точке: "+x[0]+", причем ошибка:"+dx);
        } while (error > e);
        long t1 = System.nanoTime();
        System.out.println("Значение функции:"+f.GetResult(x)+". Потречено наносекунд:"+(t1-t0));
        
        System.out.println("Модифицированный метод Ньютона:");
        x[0] = 1;
        counter = 0;
        double g0 = g.GetResult(x);
        t0 = System.nanoTime();
        do
        {   
            dx = f.GetResult(x)/g0;
            error = Math.abs(dx);
            x[0] -= dx;
            counter++;
            System.out.println(counter+"-итерация, находимся в точке: "+x[0]+", причем ошибка:"+dx);
        } while (error > e);
        t1 = System.nanoTime();
        System.out.println("Значение функции:"+f.GetResult(x)+". Потречено наносекунд:"+(t1-t0));
        System.out.println();
    }
    
    private class F implements Function{
        @Override
        public double GetResult(double[] x) {
           return 1/(Math.tan(x[0])) - x[0];
        }
    }
    private class G implements Function{
        @Override
        public double GetResult(double[] x) {
            return -1/(Math.sin(x[0])*Math.sin(x[0])) - 1;
        }
    }
}
