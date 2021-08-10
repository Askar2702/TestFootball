using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private Rigidbody _rb;
    private bool isFreedom;
    public Rigidbody Rb => _rb;
    public void FireStart()
    {
        isFreedom = true;
        StartCoroutine(RestartPos());
    }
    private void Update()
    {
        if (!isFreedom) transform.position = _point.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            collision.gameObject.SetActive(false);
            GameController.Instance.Checkfinish();
            transform.position = _point.position;
            transform.rotation = _point.rotation;
            _rb.velocity = Vector3.zero;
            isFreedom = false;

        }
        else if (collision.transform.GetComponent<EnemyController>())
        {
            _rb.velocity = Vector3.zero;
            transform.rotation = _point.rotation;
            isFreedom = false;
        }
    }

    IEnumerator RestartPos()
    {
        yield return new WaitForSeconds(3f);
        isFreedom = false;
    }
}
