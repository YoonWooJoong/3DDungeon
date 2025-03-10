using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastObject : MonoBehaviour
{
    public LayerMask playerLayer;

    public bool isRight;
    public bool isLeft;
    public bool isForward;
    public bool isBack;
    public int count;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            if (RightRay(distance))
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    StartCoroutine(RightDelay(i));
                }
            }
        }
        if (isLeft)
        {
            if (LeftRay(distance))
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    StartCoroutine(LeftDelay(i));
                }
            }
        }
        if (isForward)
        {
            if (ForwardRay(distance))
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    StartCoroutine(ForwardDelay(i));
                }
            }
        }
        if (isBack)
        {
            if (BackRay(distance))
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    StartCoroutine(BackDelay(i));
                }
            }
        }
    }

    IEnumerator RightDelay(int i)
    {
        transform.GetChild(i).gameObject.SetActive(false);
        isRight = false;
        yield return new WaitForSeconds(2f);
        transform.GetChild(i).gameObject.SetActive(true);
        isRight = true;
    }
    IEnumerator LeftDelay(int i)
    {
        transform.GetChild(i).gameObject.SetActive(false);
        isLeft = false;
        yield return new WaitForSeconds(2f);
        transform.GetChild(i).gameObject.SetActive(true);
        isLeft = true;
    }
    IEnumerator ForwardDelay(int i)
    {
        transform.GetChild(i).gameObject.SetActive(false);
        isForward = false;
        yield return new WaitForSeconds(2f);
        transform.GetChild(i).gameObject.SetActive(true);
        isForward = true;
    }
    IEnumerator BackDelay(int i)
    {
        transform.GetChild(i).gameObject.SetActive(false);
        isBack = false;
        yield return new WaitForSeconds(2f);
        transform.GetChild(i).gameObject.SetActive(true);
        isBack = true;
    }

    public bool RightRay(float distance)
    {
        Debug.DrawRay(transform.position, Vector3.right * distance, Color.red);
        if (Physics.Raycast(transform.position, Vector3.right, distance, playerLayer))
        {
            return true;
        }
        return false;
    }
    public bool LeftRay(float distance)
    {
        Debug.DrawRay(transform.position, Vector3.left * distance, Color.red);
        if (Physics.Raycast(transform.position, Vector3.left, distance, playerLayer))
        {
            return true;
        }
        return false;
    }
    public bool ForwardRay(float distance)
    {
        Debug.DrawRay(transform.position, Vector3.forward * distance, Color.red);
        if (Physics.Raycast(transform.position, Vector3.forward, distance, playerLayer))
        {
            return true;
        }
        return false;
    }
    public bool BackRay(float distance)
    {
        Debug.DrawRay(transform.position, Vector3.back * distance, Color.red);
        if (Physics.Raycast(transform.position, Vector3.back, distance, playerLayer))
        {
            return true;
        }
        return false;
    }
}
