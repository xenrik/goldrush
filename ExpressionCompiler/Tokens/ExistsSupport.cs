/**
 * Interface that tokens can implement if they support checking for 'exists'
 */
public interface ExistsSupport {
    /**
     * Return true if the item this token represents "exists"
     */
    bool Exists(UnityELEvaluator context);
}