/**
 * Created by Виктор on 04.08.2015.
 */
public class myVector{
    private myPoint start;
    private myPoint end;

    public myVector(myPoint start, myPoint end) {
        if (start.equals(null) || end.equals(null))
            throw new IllegalArgumentException("Не передавай пустые ссылки!");
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
        return "myVector{" + "start=" + start + ", end=" + end + "}";
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        myVector myVector = (myVector) o;

        if (!start.equals(myVector.start)) return false;
        return end.equals(myVector.end);

    }

    @Override
    public int hashCode() {
        int result = start.hashCode();
        result = 31 * result + end.hashCode();
        return result;
    }
}
