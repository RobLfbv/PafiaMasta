using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastaBehaviour : MonoBehaviour
{
    private Vector3 posGoTo;
    private Vector2 vectorDir;
    private bool pointReach = false;

    [SerializeField] private float distanceGoTo;
    [SerializeField] private float speed;

    void Start()
    {
        RandomPos();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(posGoTo, transform.position) > 0.5f)
        {
            transform.Translate(vectorDir.normalized * speed);
        }
        else if (!pointReach)
        {
            pointReach = true;
            DoAfterDelay(1f, () =>
            {
                RandomPos();
                pointReach = false;
            });
        }
    }
    private IEnumerator DoAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        StartCoroutine(DoAfterDelay(0.5f));

    }
    private IEnumerator DoAfterDelay(float delaySeconds, System.Action thingToDo)
    {
        yield return new WaitForSeconds(delaySeconds);
        thingToDo();
    }

    void RandomPos()
    {
        vectorDir = new Vector2(Random.Range(0, distanceGoTo), Random.Range(0, distanceGoTo));
        posGoTo = new Vector3(transform.position.x + vectorDir.x, transform.position.y + vectorDir.y, transform.position.z);
    }


}
