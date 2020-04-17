using UnityEngine;

/// <summary>
/// Handles respawn of <c>GameObject</c>s with the tag <c>"Apple"</c>.
/// </summary>
public class AppleRespawn : MonoBehaviour {
    /// <summary>
    /// The apple <c>GameObject</c>.
    /// </summary>
    public GameObject apple_position;
    /// <summary>
    /// The <c>Transform</c> of the respawn point.
    /// </summary>
    public Transform respawn_point;
    private Rigidbody rigid_body;

    /// <summary>
    /// Sets the <c>rigid_body</c> variable at the start.
    /// </summary>
    void Start() {
        rigid_body = apple_position.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// The trigger event function
    /// </summary>
    /// <remarks>
    /// Checks if it was hit by a <c>GameObject</c> with the tag <c>"Apple"</c>, if yes
    /// then it teleports it to the repsawn point and then attempts to nullify the velocity.
    /// </remarks>
    /// <param name="collider"></param>
    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Apple")) {
            apple_position.transform.position = respawn_point.transform.position;
            rigid_body.velocity = new Vector3(0, 0, 0);
        }
    }
}
