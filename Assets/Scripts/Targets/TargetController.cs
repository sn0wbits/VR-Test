using System.Collections;
using UnityEngine;

/// <summary>
/// The controller class for the Targets, it allows us to control and/or monitor the targets at a single point.
/// </summary>
/// <remarks>
/// By using a controller we can manipulate multiple objects at a time, which we can then interact with or monitor.
/// The controller is able to set public variables and/or use public functions located in the objects it control.
/// </remarks>
public class TargetController : MonoBehaviour {
    /// <summary>
    /// Debug <c>bool</c>, default: <c>false</c>.
    /// </summary>
    public bool debug = false; // Change this to enable debug messages
    private GameObject target = null;
    private GameObject prev_target = null;
    private bool target_active = false;

    /// <summary>
    /// A simple clamp function that clamps the value given within the limits of the maximum and minimum integers also given.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns>An <c>int</c> that has either been clamped or not</returns>
    private int clamp(int value, int min, int max) {
        int numb = value;
        if (value > max) {
            numb = max;
        }
        else if (value < min) {
            numb = min;
        }
        return numb;
    }

    /// <summary>
    /// Get all objects with tag "Targets" and put into an array
    /// </summary>
    /// <returns>The selected target in the <c>GameObject</c> type</returns>
    private GameObject getTargets() {
        GameObject[] target_array = null;
        GameObject selected_target;
        try {
            if (target_array == null) {
                target_array = GameObject.FindGameObjectsWithTag("Targets");
            }
            selected_target = target_array[clamp(Random.Range(0, target_array.Length), target_array.GetLowerBound(0), target_array.GetUpperBound(0))];
            if (prev_target == null) {
                prev_target = selected_target;
            }
            else if (prev_target == selected_target) {
                do {
                    selected_target = target_array[clamp(Random.Range(0, target_array.Length), target_array.GetLowerBound(0), target_array.GetUpperBound(0))];
                } while (prev_target == selected_target);
            }
            target_array = null;
            prev_target = selected_target;
            return selected_target;
        }
        catch {
            return null;
        }
    }
    /// <summary>
    /// Detect if object hits active or inactive target
    /// </summary>
    /// <param name="selected"></param>
    /// <returns>A <c>bool</c> based on the target being active or not</returns>
    private bool isTargetActive(GameObject selected) {
        TargetBehaviour t_box = selected.GetComponent<TargetBehaviour>();
        if (t_box.is_active) {
            return true;
        }
        else {
            return false;
        }
    }

    /// <summary>
    /// Select the target and makes it active
    /// </summary>
    /// <param name="selected"></param>
    /// <returns>A <c>true</c> when it has set the target to active</returns>
    private bool setTargetActive(GameObject selected) {
        TargetBehaviour t_box = selected.GetComponent<TargetBehaviour>();
        t_box.is_active = true;
        return true;
    }

    /// <summary>
    /// Disable the active target
    /// </summary>
    /// <param name="selected"></param>
    private void disableActive(GameObject selected) {
        TargetBehaviour t_box = selected.GetComponent<TargetBehaviour>();
        t_box.is_active = false;
    }

    /// <summary>
    /// Check what target the apple hit and compare to active target
    /// </summary>
    /// <remarks>
    /// 2 = Apple has hit the active
    /// 1 = Apple has hit an inactive
    /// 0 = Apple has not hit
    /// </remarks>
    /// <param name="selected"></param>
    /// <returns>And <c>int</c> based on what hit the apple, if any</returns>
    private int isActiveHit(GameObject selected) {
        AppleTrigger apple = GameObject.FindGameObjectWithTag("Apple").GetComponent<AppleTrigger>();
        if (apple.has_hit) {
            if (apple.hasHit() == selected) {
                return 2;
            }
            else {
                return 1;
            }
        }
        else {
            return 0;
        }
    }

    /// <summary>
    /// The update function is executed at each frame
    /// </summary>
    /// <remarks>
    /// Checks if a target is set to be active, if not it sets a target to active.
    /// If a target is active it will pass and check if the currently selected target does not
    /// equal <c>null</c>, if it does not it will check what type of target it is (active or inactive).
    /// If the hit target was the active one it will then execute the <c>disableActive()</c> function that
    /// disables the current active target and resets the process.
    /// </remarks>
    private void Update() {
        if (!target_active) {
            target = getTargets();
            if (!isTargetActive(target)) {
                target_active = setTargetActive(target);
            }
        }
        if (target != null) {
            int hit = isActiveHit(target);
            if (hit == 2) {
                disableActive(target);
                target_active = false;
                if (debug) Debug.Log("Good hit!");
            }
            else if (hit == 1) {
                if (debug) Debug.Log("Bad hit!");
            }
        }
    }
}