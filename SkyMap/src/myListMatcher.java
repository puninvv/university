import java.util.LinkedList;
import java.util.List;

/**
 * Created by Виктор on 04.08.2015.
 */
public class myListMatcher {
    public MatcherResult Match(List<myPoint> first, List<myPoint> second, double rangeIdentity, double rangeNear){
        MatcherResult result = new MatcherResult();

        for (myPoint f : first) {
            for (myPoint s : second) {
                double distance = f.CountDistaneTo(s);
                if (distance < rangeIdentity) {
                    result.AddIdentity(f);
                    break;
                } else
                if (distance < rangeNear) {
                    result.AddNear(f);
                    break;
                }
            }
        }

        return result;
    }

    public class MatcherResult{
        private List<myPoint> identity;
        private List<myPoint> near;
        private List<myPoint> farAway;
        public MatcherResult(){
            identity = new LinkedList<>();
            near = new LinkedList<>();
            farAway = new LinkedList<>();
        }

        public void AddIdentity(myPoint e){ identity.add(e); }
        public void AddNear(myPoint e) { near.add(e); }
        public void setFarAway(myPoint e) { farAway. add(e); }
    }
}
