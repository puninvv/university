package Tests;

/**
 * Created by Виктор on 04.08.2015.
 */
public class myBasisTest {
    public static void main(String[] args) {
        myPoint start_1 = new myPoint(1,1);
        myVector right = new myVector(start_1, new myPoint(2,1));
        myVector left = new myVector(start_1, new myPoint(1,2));
        myBasis basis = new myBasis(start_1, right, left);

        myVector test_vector = new myVector(new myPoint(4,2), new myPoint(2,4));
        System.out.println(basis.GetCoordinatesOfVector(test_vector));
    }
}
