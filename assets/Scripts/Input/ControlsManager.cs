using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ControlsManagerScriptableObject", order = 1)]
public class ControlsManager : ScriptableObject
{
    public static Vector2 moveInput = Vector2.zero;
    private float timeSinceLastAttack;
    private float attackDelay;

    public UnityEvent swordSwingEvent;

    public void Start()
    {
        timeSinceLastAttack = Time.time;
        GameObject sword = GameObject.FindGameObjectWithTag("Sword");
        attackDelay = sword.GetComponent<SwordSwing>().attackDelay;
    }


    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && Time.time >= timeSinceLastAttack + attackDelay)
        {
            // Send sword swing event
            swordSwingEvent.Invoke();
            // Reset cooldown
            timeSinceLastAttack = Time.time;
        }
    }
}
