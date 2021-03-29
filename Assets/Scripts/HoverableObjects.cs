using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverableObjects : MonoBehaviour
{
    private GameObject _object;
    public TutoScript script;

    public GameObject _InteractedObject;
    private Animator _animator;
    public Vector3 position;
    
    void Start()
    {
        GameObject child01 = transform.parent.gameObject.transform.GetChild(1).gameObject;
        _animator = child01.GetComponent<Animator>();
    }

    void Update()
    {
        if (!_object)
            return;
        if (_object.GetComponent<Rigidbody2D>().mass >= 1)
        {
            if (_object.tag == "Object" && GetComponent<BoxCollider2D>().IsTouching(_object.GetComponentInChildren<BoxCollider2D>()))
            {
                _animator.SetBool("pressed", true);

                _InteractedObject.SetActive(!_InteractedObject.activeSelf);
                script.setSoul(true);
                return;
            }
            else if (GetComponent<BoxCollider2D>().IsTouching(_object.GetComponent<CircleCollider2D>()))
            {
                _animator.SetBool("pressed", true);
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _object = other.gameObject;
        }
        if (other.CompareTag("Object"))
        {
            _object = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_animator.GetBool("Pressed"))
            _animator.SetBool("pressed", false);
    }
}
