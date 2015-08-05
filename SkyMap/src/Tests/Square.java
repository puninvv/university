package Tests;

import Classes.myPoint;
import Classes.myPointListMatcher;

import java.util.LinkedList;
import java.util.List;

/**
 * Created by Виктор on 04.08.2015.
 */
public class Square {
    public static void main(String[] args) {
        List<myPoint> pointsFirst = new LinkedList<>();
        pointsFirst.add(new myPoint(0,0));
        pointsFirst.add(new myPoint(1,0));
        pointsFirst.add(new myPoint(0,1));
        pointsFirst.add(new myPoint(1,1));
        pointsFirst.add(new myPoint(5,5));

        List<myPoint> pointsSecond = new LinkedList<>();
        pointsSecond.add(new myPoint(0,0));
        pointsSecond.add(new myPoint(2,2));
        pointsSecond.add(new myPoint(0,4));
        pointsSecond.add(new myPoint(-2,2));

        myPointListMatcher matcher = new myPointListMatcher();

        System.out.println(matcher.CompareImagination(pointsFirst, pointsSecond, 0.01, 0.1));
    }
}
