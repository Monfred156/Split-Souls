using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoScript : MonoBehaviour
{
    private bool _soul = false;
    private int recoltedSoul = 0;
    public PlayerHandler script;
    public GameObject _endZone;
    public GameObject _player;
    public GameObject _levier;
    public GameObject _door;
    private Vector3 _playerPos;

    // Start is called before the first frame update
    void Start()
    {
        _playerPos = _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_soul)
        {
            _endZone.SetActive(true);
        } else
            _endZone.SetActive(false);
        if (_endZone.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F) && _player.GetComponent<BoxCollider2D>().IsTouching(_endZone.GetComponent<BoxCollider2D>()))
            {
                if (recoltedSoul == 0)
                {
                    recoltedSoul = 1;
                    script.SetPlayerToState(1);
                }
                else if (recoltedSoul == 1)
                {
                    SceneManager.LoadScene("Level1");
                    Debug.Log("Level 1");
                }
                _player.transform.position = _playerPos;
                _door.SetActive(false);
                _levier.GetComponent<ActivableObjects>().resetLeverAnimation();
            }
        }
    }

    public void setSoul(bool soul)
    {
        _soul = soul;
    }
}
