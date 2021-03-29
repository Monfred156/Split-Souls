using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivableObjects : MonoBehaviour
{
    public GameObject uICanvas;
    public TutoScript script;
    public GameObject _InteractedObject;
    private GameObject _object;
    private Animator _animator;
    private bool _isHover = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (uICanvas.activeSelf && _isHover)
        {
            Vector3 transPos = transform.position;
            transPos.y += 0.25f;
            if (Camera.main != null)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(transPos);
                uICanvas.transform.position = pos;
            }
        }
        if (!_object)
            return;
        if (_object.GetComponent<Rigidbody2D>().mass >= 1 && Input.GetKeyDown(KeyCode.F))
        {
            if (GetComponent<BoxCollider2D>().IsTouching(_object.GetComponent<BoxCollider2D>()))
            {
                if (_animator)
                {
                    _animator.SetTrigger("Trigger");
                    _animator.SetBool("Activated", !_animator.GetBool("Activated"));
                }
                if (_InteractedObject && _InteractedObject.tag == "OpenedDoor")
                {
                    _InteractedObject.SetActive(!_InteractedObject.activeSelf);
                    script.setSoul(true);
                }
            }
        }
    }

    public void resetLeverAnimation()
    {
        if (_animator)
            _animator.SetBool("Activated", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _object = other.gameObject;
            _isHover = true;
            uICanvas.GetComponent<Text>().text = "Use 'F' to interact with this object";
            uICanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        uICanvas.SetActive(false);
        _isHover = false;
    }
}
