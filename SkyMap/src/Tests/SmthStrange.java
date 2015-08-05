package Tests;

import Classes.myPoint;
import Classes.myPointListMatcher;

import java.util.LinkedList;
import java.util.List;

/**
 * Created by Виктор on 05.08.2015.
 */
public class SmthStrange {
    public static void main(String[] args) {
        List<myPoint> pointsFirst = new LinkedList<>();
        pointsFirst.add(new myPoint(0,0));
        pointsFirst.add(new myPoint(-1,1));
        pointsFirst.add(new myPoint(1,1));
        pointsFirst.add(new myPoint(-1,-1));
        pointsFirst.add(new myPoint(1,-1));
        pointsFirst.add(new myPoint(2,2));
        pointsFirst.add(new myPoint(-2,-2));


        List<myPoint> pointsSecond = new LinkedList<>();
        pointsSecond.add(new myPoint(5,5));
        pointsSecond.add(new myPoint(6,6));
        pointsSecond.add(new myPoint(3,7));
        pointsSecond.add(new myPoint(7,3));


        System.out.println(new myPointListMatcher().CompareEquals(pointsFirst, pointsSecond, 0.01, 0.1));
    }
}
