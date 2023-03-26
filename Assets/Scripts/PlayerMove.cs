using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private Timer _timer;

    private bool _isGrounded = true;

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
            transform.position = newPosition;

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
    }
}
