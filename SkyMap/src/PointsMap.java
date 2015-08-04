import java.util.*;

/**
 * Created by Виктор on 04.08.2015.
 */
public class PointsMap {
    private List<myPoint> pointList;
    private Map<myPoint,myPoint> coordinatesMap;

    public PointsMap(List<myPoint> pointList) {
        this.pointList = pointList;
    }

    public List<myPoint> CountCoordinatesInBasis(myBasis basis){
        List<myPoint> result = new LinkedList<myPoint>();

        coordinatesMap = new HashMap<myPoint, myPoint>();

        for (myPoint point : pointList) {
            myPoint coordinates = basis.GetCoordinatesOfVector(new myVector(basis.getStart(), point));
            result.add(coordinates);
            coordinatesMap.put(point, coordinates);
        }

        return result;
    }
}
