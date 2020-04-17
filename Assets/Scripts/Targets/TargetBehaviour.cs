using UnityEngine;

/// <summary>
/// The target behaviour script controls the behaviour of the target it is attached to.
/// </summary>
public class TargetBehaviour : MonoBehaviour {
    private bool is_triggered = false;
    /// <summary>
    /// The <c>bool</c> to show if the target is active or not.
    /// </summary>
    public bool is_active = false;


    /// <summary>
    /// The trigger event function
    /// </summary>
    /// <remarks>
    /// If the trigger is activated by an object with the tag <c>"Apple"</c> it will
    /// set <c>is_triggered</c> to <c>true</c>
    /// </remarks>
    /// <param name="projectile"></param>
    private void OnTriggerEnter(Collider projectile) {
        if (projectile.CompareTag("Apple")) {
            is_triggered = true;
        }
    }

    /// <summary>
    /// Sets the colour of the target dependent on if it is active or not
    /// </summary>
    /// <param name="active"></param>
    private void setColour(bool active) {
        if (active) {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
    }

    /// <summary>
    /// Checks if it has been hit or not
    /// </summary>
    /// <remarks>
    /// If it has been hit, it will set <c>is_triggered</c> to <c>false</c>
    /// and <c>return</c> <c>true</c>. If not it will <c>return</c> <c>false</c>.
    /// </remarks>
    /// <returns><c>true</c> or <c>false</c> dependent on if it has been hit or not</returns>
    public bool isHit() {
        if (is_triggered) {
            Debug.Log("HIT " + gameObject.name + "!");
            is_triggered = false;
            return true;
        }
        else {
            return false;
        }
    }

    /// <summary>
    /// The update function is executed at each frame
    /// </summary>
    /// <remarks>
    /// It will, based on the <c>bool</c> <c>is_active</c>, set the colour of the target.
    /// </remarks>
    private void Update() {
        setColour(is_active);
    }
}
