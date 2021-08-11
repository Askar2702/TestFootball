using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private Rigidbody _rb;
    public bool IsFreedom { get; private set; }
    private float _timer = 3f;
    public Rigidbody Rb => _rb;
    public void FireStart()
    {
        IsFreedom = true;
    }
    private void FixedUpdate()
    {
        if (!IsFreedom) 
        { 
            transform.position = _point.position;
            _timer = 3;
        }
       
    }

    private void Update()
    {
        if (IsFreedom)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else if (_timer <= 0)
            {
                _timer = 3;
                IsFreedom = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            collision.gameObject.SetActive(false);
            GameController.Instance.Checkfinish();
            transform.position = _point.position;
            transform.rotation = _point.rotation;
            IsFreedom = false;

        }
        else if (collision.transform.GetComponent<EnemyController>())
        {
            _rb.velocity = Vector3.zero;
            transform.rotation = _point.rotation;
            IsFreedom = false;
        }
    }

    
}
