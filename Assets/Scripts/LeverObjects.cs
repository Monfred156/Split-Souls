using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObjects : MonoBehaviour
{
    private GameObject _object;
    private Animator _animator;
    public GameObject Wall;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_object)
            return;
        if (_object.GetComponent<Rigidbody2D>().mass >= 1 && Input.GetKeyDown(KeyCode.F))
        {
            if (GetComponent<BoxCollider2D>().IsTouching(_object.GetComponent<BoxCollider2D>()))
            {
                _animator.SetTrigger("Trigger");
                _animator.SetBool("Activated", !_animator.GetBool("Activated"));
                Wall.SetActive(!_animator.GetBool("Activated"));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _object = other.gameObject;
        }
    }
}