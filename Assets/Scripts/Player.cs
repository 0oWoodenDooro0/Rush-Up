using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool IsGameOver;
    public float moveSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public LayerMask layerMask;
    public GameObject startPlatform;
    private GameObject _scorePanel;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _dirX;
    private float _dirY;
    private bool _isGrounded;
    private bool _doubleJump;

    private enum MovementState
    {
        Idle,
        Run,
        Jump,
        Fall
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _scorePanel = GameObject.FindWithTag("Finish");
        _scorePanel.SetActive(false);
        Instantiate(startPlatform, new Vector3(0, -1.5f, 0), new Quaternion(0, 0, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bronze Coin"))
        {
            Destroy(other.gameObject);
            ScoreBoard.Score += 1;
        }
        else if (other.gameObject.CompareTag("Silver Coin"))
        {
            Destroy(other.gameObject);
            ScoreBoard.Score += 3;
        }
        else if (other.gameObject.CompareTag("Gold Coin"))
        {
            Destroy(other.gameObject);
            ScoreBoard.Score += 5;
        }
        else if (other.gameObject.CompareTag("Spike"))
        {
            IsGameOver = true;
            _scorePanel.SetActive(true);
            _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, new Vector2(.5f, .5f), 0f, Vector2.down, .25f, layerMask);
    }

    private void Update()
    {
        if (!IsGameOver)
        {
            MovementUpdate();
            AnimationUpdate();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            Instantiate(startPlatform, new Vector3(0, -1.5f, 0), new Quaternion(0, 0, 0, 0));
            ScoreBoard.Score = 0;
            _scorePanel.SetActive(false);
            IsGameOver = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void MovementUpdate()
    {
        _isGrounded = IsGrounded();
        var climb = Physics2D.BoxCast(transform.position, new Vector2(.5f, .5f), 0f, new Vector2(-1, 0), .2501f, layerMask) ||
                    Physics2D.BoxCast(transform.position, new Vector2(.5f, .5f), 0f, new Vector2(1, 0), .2501f, layerMask);
        _dirX = Input.GetAxisRaw("Horizontal");
        _dirY = _rigidbody2D.velocity.y;
        if (!_isGrounded && climb)
        {
            _dirX = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded)
            {
                _doubleJump = true;
                _dirY = jumpSpeed;
            }
            else if (_doubleJump)
            {
                _doubleJump = false;
                _dirY = doubleJumpSpeed;
            }
        }

        _rigidbody2D.velocity = new Vector2(_dirX * moveSpeed, _dirY);
    }

    private void AnimationUpdate()
    {
        _dirX = _rigidbody2D.velocity.x;
        var state = MovementState.Idle;
        if (_dirX > 0f)
        {
            state = MovementState.Run;
            _spriteRenderer.flipX = false;
        }
        else if (_dirX < 0f)
        {
            state = MovementState.Run;
            _spriteRenderer.flipX = true;
        }

        if (!_isGrounded && _dirY > 0f)
        {
            state = MovementState.Jump;
        }
        else if (!_isGrounded && _dirY <= 0f)
        {
            state = MovementState.Fall;
        }

        _animator.SetInteger("state", (int)state);
    }
}