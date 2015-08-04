package Classes;

import FromUniversity.Matrix;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;

/**
 * Created by Виктор on 04.08.2015.
 */
public class myBasis {
    private myPoint start;
    private myVector e1;
    private myVector e2;

    public myBasis(myPoint start, myPoint e1, myPoint e2) throws IllegalArgumentException {
        this.start = start;
        this.e1 = new myVector(start, e1);
        this.e2 = new myVector(start, e2);
        if (this.e1.DotProduct(this.e2) == 0)
            throw new IllegalArgumentException();
    }

    public myBasis(List<myPoint> points, myPoint base){
        double minDistance1 = -1;
        double minDistance2 = -1;
        int minI1 = -1;
        int minI2 = -1;

        for (int i = 0; i < points.size(); i++) {
            if (points.get(i).equals(base)) continue;

            double distance = points.get(i).getDistance(base);
            if (minI1 == -1 || distance < minDistance1){
                minI2 = minI1;
                minDistance2 = minDistance1;

                minDistance1 = distance;
                minI1 = i;
            }else
            if (minI2 == -1 || distance <= minDistance2){
                minI2 = i;
                minDistance2 = distance;
            }
        }

        myVector v1 = new myVector(base, points.get(minI1));
        myVector v2 = new myVector(base, points.get(minI2));
        if (v1.DotProduct(v2) > 0)
        {
            start = base;
            e1 = v1;
            e2 = v2;
        } else
        if (v1.DotProduct(v2) < 0)
        {
            start = base;
            e1 = v2;
            e2 = v1;
        } else
            throw new IllegalArgumentException("Вектора коллинеарны, выбери другие");
    }

    public myPoint getStart() {
        return start;
    }

    public myPoint getCoordinates(myPoint point) {
        double[][] A = new double[2][2];
        A[0][0] = e1.getDX();
        A[0][1] = e2.getDX();
        A[1][0] = e1.getDY();
        A[1][1] = e2.getDY();

        double[] b = new double[2];
        b[0] = point.getX() - start.getX();
        b[1] = point.getY() - start.getY();

        double[] x = new Matrix(A).CountSLAY_PLU(b);

        return new myPoint(x[0],x[1]);
    }

    public myPoint getPoint(myPoint coordinates){
        double dx = e1.getDX() * coordinates.getX() + start.getX() + e2.getDX() * coordinates.getY();
        double dy = e1.getDY() * coordinates.getX() + start.getY() + e2.getDY() * coordinates.getY();
        myPoint point = new myPoint(dx, dy);
        return point;
    }

    public List<myPoint> updateCoordinates(List<myPoint> points){
        List<myPoint> result = new LinkedList<>();
        for (myPoint point : points) {
            result.add(getCoordinates(point));
        }
        return result;
    }

    public List<myPoint> updatePoints(List<myPoint> coordinates){
        List<myPoint> result = new LinkedList<>();
        for (myPoint point : coordinates) {
            result.add(getPoint(point));
        }
        return result;
    }
}
