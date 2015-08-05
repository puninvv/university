package Classes;

/**
 * Created by ������ on 04.08.2015.
 */
public class myVector {
    private myPoint start;
    private myPoint end;

    public myVector(myPoint start, myPoint end) {
        this.start = start;
        this.end = end;
    }

    public myPoint getStart() {
        return start;
    }

    public myPoint getEnd() {
        return end;
    }

    @Override
    public String toString() {
        return "myVector {" + "start=" + start + ", end=" + end + "}";
    }

    public double getDX(){
        return end.getX() - start.getX();
    }

    public double getDY(){
        return end.getY() - start.getY();
    }

    public double DotProduct(myVector vector) {
        return getDX() * vector.getDY() - getDY() * vector.getDX();
    }

    public double ScalarProduct(myVector vector) {return getDX() * vector.getDX() + getDX() * vector.getDY();}

    public double getLength(){
        return end.getDistance(start);
    }

    public boolean equalsLength(myVector vector){
        return this.getLength() == vector.getLength();
    }


}
