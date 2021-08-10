using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Shoot _shoot;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _upSpedd;
    private Ball _ball;
    private void Awake()
    {
        _shoot.Fire += Appropriation;
    }
    private void Start()
    {
        GameController.Instance.Finishing += UpSpeed;
    }
    private void FixedUpdate()
    {
        if (_ball != null)
        {
            Vector3 direction = _ball.transform.position - transform.position;
            _rb.velocity = new Vector3(direction.x, 0f, 0f) * _speed * Time.deltaTime;
        }
    }

    private void Appropriation(Ball ball)
    {
        _ball = ball;
    }

    private void UpSpeed()
    {
        _speed += _upSpedd;
    }
}
