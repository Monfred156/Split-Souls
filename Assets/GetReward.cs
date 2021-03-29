using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetReward : MonoBehaviour
{
    private GameObject _object;
    private Animator _animator;
    public GameObject ChestOpenObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_object)
            return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (GetComponent<BoxCollider2D>().IsTouching(_object.GetComponent<BoxCollider2D>()))
            {
                _object.GetComponent<PlayerHandler>().setSoul(true);
                gameObject.SetActive(false);
                ChestOpenObject.SetActive(true);
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