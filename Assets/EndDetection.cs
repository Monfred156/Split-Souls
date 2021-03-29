using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDetection : MonoBehaviour
{
    private GameObject _object;
    public GameObject chest;
    public GameObject opened_chest;
    
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
                bool hasSoul = _object.GetComponent<PlayerHandler>().hasSoul();
                if (hasSoul)
                {
                    int value = _object.GetComponent<PlayerHandler>().getState();
                    if (value == 0)
                    {
                        value = 1;
                    }
                    else
                        SceneManager.LoadScene("Credits");

                    _object.GetComponent<PlayerHandler>().SetPlayerToState(value);
                    _object.GetComponent<PlayerHandler>().setSoul(false);
                    chest.SetActive(true);
                    opened_chest.SetActive(false);
                    _object.transform.position = new Vector3(-10.75f,-1.59f, -1.63f);
                }
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
