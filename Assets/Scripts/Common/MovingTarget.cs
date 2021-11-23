using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovingTarget : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] bool twoSided = true;
    [SerializeField] int obstacleLayer = 8;

    Animator animator;
    BoxCollider2D col;
    Rigidbody2D rb;
    float blockThreshold;
    int mask;
    bool collided;
    bool paused;

    public float Speed {
        get => speed;
        set => speed = value;
    }

    public bool Paused {
        get => paused;
        set => paused = value;
    }

    private void Awake() {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        paused = false;
        mask = 1 << obstacleLayer;
    }

    private void Start() {
        blockThreshold = Mathf.Max(col.bounds.extents.x, col.bounds.extents.y) + 0.01f;
    }

    public void Move(Vector2 joystickOffset) {
        if (paused) return;
        Vector2 vel = new Vector2();
        vel.x += speed * joystickOffset.x;
        vel.y += speed * joystickOffset.y;

        Vector2 newPos = rb.position + vel * Time.deltaTime;
        if (!IsBlocked(newPos)) {
            rb.MovePosition(newPos);
            if (twoSided) {

            }
        }

    }

    bool IsBlocked(Vector2 newPos)
    {
        Vector2 a = new Vector2(newPos.x - col.bounds.extents.x,
                                newPos.y + col.bounds.extents.y);
        Vector2 b = new Vector2(newPos.x + col.bounds.extents.x,
                                newPos.y - col.bounds.extents.y);
        Collider2D[] colliders = Physics2D.OverlapAreaAll(a, b, mask);
        return colliders.Length > 0;
    }

}