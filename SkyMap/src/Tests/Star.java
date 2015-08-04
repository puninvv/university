package Tests;

import Classes.myPoint;
import Classes.myPointListMatcher;

import java.util.LinkedList;
import java.util.List;

/**
 * Created by Виктор on 05.08.2015.
 */
public class Star {
    public static void main(String[] args) {
        List<myPoint> pointsFirst = new LinkedList<>();
        pointsFirst.add(new myPoint(0,0));
        pointsFirst.add(new myPoint(2,2));
        pointsFirst.add(new myPoint(2,4));
        pointsFirst.add(new myPoint(0,6));
        pointsFirst.add(new myPoint(-2,4));
        pointsFirst.add(new myPoint(-2,2));

        List<myPoint> pointsSecond = new LinkedList<>();
        pointsSecond.add(new myPoint(3,3));
        pointsSecond.add(new myPoint(5,3));
        pointsSecond.add(new myPoint(3,7));
        pointsSecond.add(new myPoint(5,7));

        myPointListMatcher matcher = new myPointListMatcher();

        System.out.println(matcher.Compare(pointsFirst, pointsSecond, 0.01, 0.1));
    }
}
