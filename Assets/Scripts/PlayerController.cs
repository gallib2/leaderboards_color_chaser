using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //Sets also from jumpincreaseplatform cs + JumpDecreasePlatform cs
    [HideInInspector] public float JumpForce;
    [HideInInspector] public bool EnteredGreenPlatform, EnteredRedPlatform;

    [SerializeField] private TextMeshProUGUI _jumpForceUpdatedText;

    #region PrivateVariables
    private bool _canJump, _isJumping;
    private float  _moveSpeed, _speedPerSecond, _gravityMultiplier, _jumpTime;
    private float _jumpTimeCounter;
    private Rigidbody2D _rigid;
    private KeyCode _jumpKey;
    #endregion

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.gravityScale = _gravityMultiplier;

        InvokeRepeating("SpeedUpByTime", 2f, 3.0f);
    }

    private void SpeedUpByTime()
    {
        Debug.Log("Speeds up the player movement");
        _moveSpeed += _speedPerSecond;
    }

    //Called from GameManager
    public void Setup(float moveSpeed, float speedPerSecond, float jumpForce, float gravityMultiplier, float jumpTime, KeyCode jumpKey)
    {
        this._moveSpeed = moveSpeed;
        this.JumpForce = jumpForce;
        this._gravityMultiplier = gravityMultiplier;
        this._jumpTime = jumpTime;
        this._jumpKey = jumpKey;
        this._speedPerSecond = speedPerSecond;
    }

    //Physics should be handled in FixedUpdate().
    //There are perhaps a few exceptions, like making a one off call to ForceMode.Impulse type AddForce or making a one-off velocity change.
    //The effect otherwise will be inaccurate physics and missed collisions.
    void Update()
    {
        Move();
        CheckJump();
    }

    private void Move()
    {
        transform.position = new Vector3(transform.position.x + _moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    public void DisplayJumpForceAmount(float number, bool plus)
    {
        _jumpForceUpdatedText.gameObject.transform.parent.gameObject.SetActive(true);
        
        if(plus)
        _jumpForceUpdatedText.text = string.Format("Jump force +{0}", number);
        else
            _jumpForceUpdatedText.text = string.Format("Jump force -{0}", number);
    }

    private void CheckJump()
    {
        //Checks if we have pressed one on the jumping key + if we are grounded
        if (Input.GetKeyDown(_jumpKey) && _canJump)
        {
            _rigid.velocity = Vector2.up * JumpForce;
            _jumpTimeCounter = _jumpTime;
            _canJump = false;
            _isJumping = true;
        }

        //Checks for a long press + if we are in air
        if (Input.GetKey(_jumpKey) && _isJumping)
        {
            if (_jumpTimeCounter > 0)
            {
                _rigid.velocity = Vector2.up * JumpForce;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }

        //Checks if we have released the jumping key
        if (Input.GetKeyUp(_jumpKey))
        {
            _isJumping = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            _canJump = true;
        }
    }
}
