using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _safeOffset = 0.2f;
    private Vector3 _groundMinBounds;
    private Vector3 _groundMaxBounds;
    


    private bool _isGrounded = true;

    private void Start()
    {
        GameObject groundObject = GameObject.FindGameObjectWithTag("Ground");
        Renderer groundRenderer = groundObject.GetComponent<Renderer>();

        _groundMinBounds = groundRenderer.bounds.min + new Vector3(_safeOffset, _safeOffset, _safeOffset);
        _groundMaxBounds = groundRenderer.bounds.max - new Vector3(_safeOffset, 0f, _safeOffset);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            _timer.isTimerRunning = false;
            _timer.GameWin();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit obstacle");
            _timer.SubtractTime(1f);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }


    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0f || horizontal > 0f)
        {
            Vector3 movementDirection = new Vector3(horizontal, 0f, vertical);
            movementDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);

            Vector3 newPosition = transform.position + transform.forward * _speed * Time.deltaTime;
            //transform.position = newPosition;
            transform.position = ClampPosition(newPosition);

            _animator.SetBool("Run", true);
        }
        else
        {
            _animator.SetBool("Run", false);
        }


        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _animator.SetTrigger("Jump");
        }

        if (!_isGrounded && transform.position.y < _groundMinBounds.y)
        {
            _timer.GameOver();
        }
    }
    private Vector3 ClampPosition(Vector3 position)
    {
        float clampedX = Mathf.Clamp(position.x, _groundMinBounds.x, _groundMaxBounds.x);
        float clampedY = Mathf.Clamp(position.y, _groundMinBounds.y, _groundMaxBounds.y);
        float clampedZ = Mathf.Clamp(position.z, _groundMinBounds.z, _groundMaxBounds.z);

        return new Vector3(clampedX, clampedY, clampedZ);
    }
}
