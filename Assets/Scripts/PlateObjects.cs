using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateObjects : MonoBehaviour
{
    private GameObject _object;
    private Animator _animator;
    public GameObject Item;
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
            if (GetComponent<BoxCollider2D>().IsTouching(_object.GetComponent<CircleCollider2D>()))
            {
                _animator.SetBool("pressed", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Item.CompareTag("SpawnableObject"))
                Instantiate(Item, position, Quaternion.identity);
            _object = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_animator.GetBool("Pressed"))
            _animator.SetBool("pressed", false);
    }
}