package Classes;

import java.util.LinkedList;
import java.util.List;

/**
 * Created by Виктор on 04.08.2015.
 */
public class myPointListMatcher {
    private List<myPoint> equalsPoints = null;
    private List<myPoint> nearPoints = null;

    private myBasis basis = null;

    private List<myPoint> equalsPointsTMP = null;
    private List<myPoint> nearPointsTMP = null;

    public List<myPoint> CompareImagination(List<myPoint> pointsFrom, List<myPoint> pointsTo, double equalDistance, double nearDistance){
        equalsPoints = null;
        nearPoints = null;

        List<myPoint> result = new LinkedList<>();

        //TODO уменьшить сложность
        List<myBasis> basisesFrom = new LinkedList<>();

        for (myPoint start : pointsFrom) {
            for (myPoint left : pointsFrom) {
                for (myPoint right : pointsFrom) {
                    try{
                        basisesFrom.add(new myBasis(start, right, left));
                    } catch (Exception e){}
                }
            }
        }

        List<myBasis> basisesTo = new LinkedList<>();
        for (myPoint start : pointsTo) {
            for (myPoint left : pointsTo) {
                for (myPoint right : pointsTo) {
                    try{
                        basisesTo.add(new myBasis(start, right, left));
                    } catch (Exception e){}
                }
            }
        }

        for (myBasis basisFrom : basisesFrom) {

            List<myPoint> coordinatesFrom = basisFrom.updateCoordinates(pointsFrom);

            for (myBasis  basisTo: basisesTo) {
                List<myPoint> coordinatesTo = basisTo.updateCoordinates(pointsTo);

                MatchLists(coordinatesFrom, coordinatesTo, equalDistance, nearDistance);

                if (equalsPoints == null || (equalsPointsTMP.size() + nearPointsTMP.size() > equalsPoints.size() + nearPoints.size())){
                    equalsPoints = equalsPointsTMP;
                    nearPoints = nearPointsTMP;
                    basis = basisFrom;
                }
            }
        }

        result.addAll(basis.updatePoints(equalsPoints));
        result.addAll(basis.updatePoints(nearPoints));

        return result;
    }

    public List<myPoint> CompareEquals(List<myPoint> pointsFrom, List<myPoint> pointsTo, double equalDistance, double nearDistance){
        equalsPoints = null;
        nearPoints = null;

        List<myPoint> result = new LinkedList<>();

        //TODO попробовать уменьший количество оперций, сейчас n^3, можно немного поменьше
        List<myBasis> basisesFrom = new LinkedList<>();

        for (myPoint start : pointsFrom) {
            for (myPoint left : pointsFrom) {
                for (myPoint right : pointsFrom) {
                    try{
                        basisesFrom.add(new myBasis(start, right, left));
                    } catch (Exception e){}
                }
            }
        }

        List<myBasis> basisesTo = new LinkedList<>();
        for (myPoint start : pointsTo) {
            for (myPoint left : pointsTo) {
                for (myPoint right : pointsTo) {
                    try{
                        basisesTo.add(new myBasis(start, right, left));
                    } catch (Exception e){}
                }
            }
        }

        for (myBasis basisFrom : basisesFrom) {

            List<myPoint> coordinatesFrom = basisFrom.updateCoordinates(pointsFrom);

            for (myBasis  basisTo: basisesTo) {
                if (!basisFrom.equals(basisTo))
                    continue;

                List<myPoint> coordinatesTo = basisTo.updateCoordinates(pointsTo);

                MatchLists(coordinatesFrom, coordinatesTo, equalDistance, nearDistance);

                if (equalsPoints == null || (equalsPointsTMP.size() + nearPointsTMP.size() > equalsPoints.size() + nearPoints.size())){
                    equalsPoints = equalsPointsTMP;
                    nearPoints = nearPointsTMP;
                    basis = basisFrom;
                }
            }
        }

        if (basis != null) {
            result.addAll(basis.updatePoints(equalsPoints));
            result.addAll(basis.updatePoints(nearPoints));
        }

        return result;
    }

    private void MatchLists(List<myPoint> pointsFrom, List<myPoint> pointsTo, double equalDistance, double nearDistance){
        equalsPointsTMP = new LinkedList<>();
        nearPointsTMP = new LinkedList<>();

        for (myPoint f : pointsFrom) {
            boolean stop = false;
            for (myPoint t : pointsTo) {
                if(f.getDistance(t) < equalDistance){
                    equalsPointsTMP.add(f);
                    stop = true;
                    break;
                }
            }
            if (stop) continue;

            for (myPoint t : pointsTo) {
                if(f.getDistance(t) < nearDistance){
                    nearPointsTMP.add(f);
                    stop = true;
                    break;
                }
            }
        }
    }
}
