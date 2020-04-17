using UnityEngine;

/// <summary>
/// The apple trigger collects information of what the apple hits.
/// </summary>
public class AppleTrigger : MonoBehaviour {
    private GameObject hit_target = null;
    /// <summary>
    /// If the apple has hit a target or not
    /// </summary>
    public bool has_hit = false;


    /// <summary>
    /// The trigger event function.
    /// </summary>
    /// <remarks>
    /// The trigger function where it checks if it hit a target and, if it did, the information about the target.
    /// </remarks>
    /// <param name="collider"></param>
    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Targets")) {
            hit_target = GameObject.Find(collider.name);
            has_hit = true;
        }
    }

    /// <summary>
    /// Checks if it has hit a target and <c>return</c>s target
    /// </summary>
    /// <returns>The target as type <c>GameObject</c>.</returns>
    public GameObject hasHit() {
        if (has_hit) {
            has_hit = false;
            return hit_target;
        }
        else {
            return null;
        }
    }
}
