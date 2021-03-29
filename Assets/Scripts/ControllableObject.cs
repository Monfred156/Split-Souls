using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllableObject : MonoBehaviour
{
    public GameObject uICanvas;

    private GameObject _player;
    private bool _isControlled = false;
    private bool _isHover = false;
    
    private int _playerState = 0;

    private void Update()
    {
        if (uICanvas.activeSelf && _isHover) {
            Vector3 transPos = transform.position;
            transPos.y += 0.25f;
            if (Camera.main != null) {
                Vector3 pos = Camera.main.WorldToScreenPoint(transPos);
                uICanvas.transform.position = pos;
            }
        }
        if (!_player)
            return;
        if (Input.GetKeyUp("f") && _playerState == 0) {
            _isControlled = !_isControlled;
            if (_isControlled && GetComponent<BoxCollider2D>().IsTouching(_player.GetComponent<BoxCollider2D>())) {
                Physics2D.IgnoreCollision(_player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>(), true);
                _player.GetComponent<Rigidbody2D>().isKinematic = true;
                _player.GetComponent<PlayerHandler>().objectToMove = gameObject;
                GetComponent<BoxCollider2D>().isTrigger = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().freezeRotation = true;
                uICanvas.SetActive(false);
            } else {
                Physics2D.IgnoreCollision(_player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>(), false);
                _player.GetComponent<Rigidbody2D>().isKinematic = false;
                _player.GetComponent<PlayerHandler>().objectToMove = _player.gameObject;
                GetComponent<BoxCollider2D>().isTrigger = true;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                GetComponent<Rigidbody2D>().freezeRotation = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        _player = other.gameObject;
        _playerState = _player.GetComponent<PlayerHandler>().getState();
        if (_playerState == 0)
        {
            _isHover = true;
            uICanvas.GetComponent<Text>().text = "Use 'F' to take possession of this object";
            uICanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        uICanvas.SetActive(false);
        _isHover = false;
        //_player = null;
    }
}
