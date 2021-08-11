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
    private float _impulse = 350;
    private Vector3 _arrowScale;
    private bool isPressed;




    void Update()
    {
        if (UIManager.Instance.GameState != GameMode.Play) return;
        Guidance();
    }

    private void TargetGuidance()
    {
        if (!isPressed) return;
        _arrowScale = new Vector3(_arrow.localScale.x, _arrow.localScale.y +
            Vector3.Distance(transform.position, _point.position) * 0.5f, _arrow.localScale.z);
        _forse = _arrowScale.y;
        if (_arrowScale.y > 1f) _arrowScale.y = 1;
        else if (_arrowScale.y < 0.3f) _arrowScale.y = 0.3f;
        _arrow.localScale = _arrowScale;
        _point.LookAt(transform);
    }

    private void ShootBall()
    {
        if (!isPressed || _ball.IsFreedom) return;
        _forse *= _impulse;
        _arrow.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        _ball.Rb.AddForce(_point.forward * _forse, ForceMode.Force);
        _ball.FireStart();
        Fire?.Invoke(_ball);
        isPressed = false;
    }

   

    void Guidance()
    {
        Ray ray;
#if UNITY_EDITOR
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            TargetGuidance();
        }
        else if (Input.GetMouseButtonUp(0))
            ShootBall();

#endif
#if PLATFORM_ANDROID
        // ray =  Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                TargetGuidance();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                ShootBall();
            }
        }
#endif
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Ground")))
        {
            if (hit.point.z >= 0) return;
            transform.position = hit.point;
            isPressed = true;
        }
        else if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("UI")))
            isPressed = false;
    }
}
