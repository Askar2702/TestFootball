using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }
    private void Update()
    {
        if (transform.position.y < 0) Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            collision.gameObject.SetActive(false);
            GameController.Instance.Checkfinish();
            Destroy(gameObject);
        }
        else if (collision.transform.GetComponent<EnemyController>()) Destroy(gameObject);
    }
}
