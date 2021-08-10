using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public event Action<Ball> Fire;

    [SerializeField] private Transform _point;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _arrow;
    [SerializeField] private float _forse;
    [SerializeField] private Ball _ball;
    private Ball currentBall;
    private float _impulse = 1000f;
    private Quaternion _rotateY;
    private Vector3 _arrowScale;
   
    

    void Update()
    {
        if (UIManager.Instance.GameState == GameMode.Play)
        {
            if (Input.GetMouseButton(0))
            {
                TargetGuidance();
            }
            else if (Input.GetMouseButtonUp(0))
                ShootBall();
        }
    }

    private void TargetGuidance()
    {
        _rotateY = Quaternion.Euler(0, -Input.GetAxis("Mouse X") * _rotateSpeed, 0);
        transform.rotation = _rotateY * transform.rotation;
        _arrowScale = new Vector3(_arrow.localScale.x, _arrow.localScale.y + -Input.GetAxis("Mouse Y") * 0.5f
            , _arrow.localScale.z);
        _forse = _arrowScale.y;
        if (_arrowScale.y > 1f) _arrowScale.y = 1;
        else if (_arrowScale.y < 0.3f) _arrowScale.y = 0.3f;
        _arrow.localScale = _arrowScale;
    }

    private void ShootBall()
    {
        _arrow.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        if (currentBall != null) return;
        _forse *= _impulse;
        currentBall  = Instantiate(_ball, _point.position, _point.rotation);
        currentBall.GetComponent<Rigidbody>().AddForce(_point.forward * _forse, ForceMode.Force);
        Fire?.Invoke(currentBall);
    }
}
