using UnityEngine;

public class SnorlaxScript : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D myRigidBody;
    public float flapStrength = 5f;

    [Header("Game")]
    public LogicScript logic;
    public bool snorlaxIsAlive = true;

    float topLimit, bottomLimit;
    float halfHeight;

    void Awake()
    {
        if (myRigidBody == null) myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (logic == null)
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        var col = GetComponent<Collider2D>();
        if (col) halfHeight = col.bounds.extents.y;
        else
        {
            var sr = GetComponent<SpriteRenderer>();
            halfHeight = sr ? sr.bounds.extents.y : 0.25f; 
        }

        RecalculateBounds();
    }

    void RecalculateBounds()
    {
        var cam = Camera.main;
        float worldTop = cam.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
        float worldBottom = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y;

        topLimit = worldTop - halfHeight;
        bottomLimit = worldBottom + halfHeight;
    }

    void Update()
    {
        if (snorlaxIsAlive && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            myRigidBody.linearVelocity = Vector2.up * flapStrength;
        }

        RecalculateBounds();

        if (snorlaxIsAlive && (transform.position.y > topLimit || transform.position.y < bottomLimit))
        {
            snorlaxIsAlive = false;
            logic.gameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (snorlaxIsAlive && collision.collider.CompareTag("Pokeflute"))
        {
            snorlaxIsAlive = false;
            logic.gameOver();
        }
    }
}
