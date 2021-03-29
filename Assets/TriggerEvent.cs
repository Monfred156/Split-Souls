using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameObject Item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private String SpawnItem()
    {
        Instantiate(Item, new Vector3(0, -3), Quaternion.identity);
        return "";
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("oui");
        SpawnItem();
        Invoke(SpawnItem(), 1);
    }

    private void OnTriggerExit(Collider other)
    {
        throw new NotImplementedException();
    }
}
