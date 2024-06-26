using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private Rigidbody2D _rigidbody;

    private string _horizontalAxis = "Horizontal";
    private string _verticalAxis = "Vertical";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.gravityScale = 0f;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontalMovement = Input.GetAxis(_horizontalAxis);
        float verticalMovement = Input.GetAxis(_verticalAxis);

        Vector2 movement = new Vector2(horizontalMovement, verticalMovement).normalized;
        Vector2 newPosition = _rigidbody.position + movement * _speed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(newPosition);
    }
}