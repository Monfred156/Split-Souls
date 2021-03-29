using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    public GameObject objectToMove;
    public float speed = 0.5f;

    public AudioSource audioSource;
    public AudioClip ghostTheme;
    public AudioClip humanTheme;
    
    private int _state = 0; // 0 = Ghost | 1 = Human
    private float _jumpForce = 5f;
    private bool _isGrounded = false;

    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public Transform groundCheckCollider;
    public LayerMask groundLayer;

    public bool Soul;
    
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        objectToMove = gameObject;

        SetPlayerToState(_state);
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Debug.Log(objectToMove ? objectToMove.name : "nil");
        if (!objectToMove)
            return;
        GameObject.FindGameObjectWithTag("CMCamera").GetComponent<CinemachineVirtualCamera>().Follow =
            objectToMove.transform;
        if (_state == 0)
            objectToMove.transform.Translate(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0);
        else if (_state == 1)
        {
            GroundChek();
            objectToMove.transform.Translate(horizontal * speed * Time.deltaTime, 0f, 0f);
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                objectToMove.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            }
        }
        handlePlayerAnimation(horizontal);
    }

    private void handlePlayerAnimation(float horizontal)
    {
        if (_isGrounded && Input.GetKeyDown("space"))
        {
            _animator.SetTrigger("takeOf");
            _animator.SetBool("isJumping", true);
        }
        if (_isGrounded)
            _animator.SetBool("isJumping", false);
        else
        {
            _animator.SetBool("isJumping", true);
        }
        if (horizontal != 0)
        {
            _animator.SetBool("Run", true);
            if (horizontal > 0)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = true;
        }
        else
            _animator.SetBool("Run", false);
    }

    public void SetPlayerToState(int state)
    {
        if (state == 0) // Ghost
        {
            _animator.SetInteger("state", 0);
            _state = 0;
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.mass = 0f;
            audioSource.Pause();
            audioSource.clip = ghostTheme;
            audioSource.Play();
        }
        else if (state == 1) // Human
        {
            _animator.SetBool("isJumping", false);
            _animator.SetInteger("state", 2);
            _state = 1;
            _rigidbody2D.gravityScale = 1f;
            _rigidbody2D.mass = 1f;
            audioSource.Pause();
            audioSource.clip = humanTheme;
            audioSource.Play();
        }
    }

    private void GroundChek()
    {
        _isGrounded = false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.1f, groundLayer);
        if (colliders.Length > 0)
        {
            _isGrounded = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KillZone") && _state == 1)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public int getState()
    {
        return _state;
    }

    public bool hasSoul()
    {
        return Soul;
    }

    public void setSoul(bool state)
    {
        Soul = state;
    }
    
}