using UnityEngine;
using UnityEngine.Events;

public class SwordSwing : MonoBehaviour
{
    private GameObject player;
    private Transform playerPosition;
    [SerializeField] public float attackDelay;
    [SerializeField] private float rangeMultiplier;
    private Transform swordPosition;
    private Vector3 lastSwordPosition;
    private ContactFilter2D enemyFilter;
    private Collider2D[] swordColliders = new Collider2D[5];
    private Animator swordAnimator;
    public ControlsManager controlsManager;

    public UnityEvent swordSwingEvent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        swordAnimator = GetComponent<Animator>();
        playerPosition = player.GetComponent<Transform>();
        swordPosition = GetComponent<Transform>();
        LayerMask collisionMask = Physics2D.GetLayerCollisionMask(gameObject.layer);
        enemyFilter.SetLayerMask(collisionMask);

        swordSwingEvent = controlsManager.swordSwingEvent;
        swordSwingEvent.AddListener(Swing);
    }

    public void Swing()
    {

        // Animate SwordDelay
        swordAnimator.SetTrigger("Slash");

        // Calcualte collisions
        for (int i = 0; i < swordColliders.Length; i++)
        {
            //Bugged for some reason
            //swordColliders[i].GetComponent<Enemy>().health -= 1;
            //Debug.Log(swordColliders[i].GetComponent<SpriteRenderer>().color);
        }

        swordPosition.position = playerPosition.position;
        Vector3 tempSword = ControlsManager.moveInput;
        swordPosition.rotation = new Quaternion(0, 0, 0.5f, 0);
        tempSword = Vector3.Normalize(tempSword);
        tempSword *= rangeMultiplier;
        if (tempSword != Vector3.zero)
        {
            swordPosition.position += tempSword;
            lastSwordPosition = tempSword;
        }
        else
        {
            swordPosition.position += lastSwordPosition;
        }
    }
}
