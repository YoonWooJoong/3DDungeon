using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Rigidbody rigidbody;
    public float moveSpeed = 10f;
    private Vector3 startPosition;
    private Vector3 movePosition;
    public float distance = 1f;
    public bool isRight;
    public bool isForward;
    public bool isUp;
    public bool isLeft;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        startPosition = gameObject.transform.position;
    }
    void FixedUpdate()
    {
        if(isRight) Right();
        if (isLeft) Left();
        if (isForward) Forward();
        if(isUp) Up();
        

    }
    private void Right()
    {
        if (Mathf.Abs(Vector3.Distance(startPosition, this.gameObject.transform.position)) <= distance)
        {
            movePosition = Vector3.right * moveSpeed * Time.deltaTime;
            this.transform.position += movePosition;
        }
        else
        {
            moveSpeed *= -1;
            movePosition = Vector3.right * moveSpeed * Time.deltaTime;
            this.transform.position += movePosition;
        }
    }
    private void Left()
    {
        if (Mathf.Abs(Vector3.Distance(startPosition, this.gameObject.transform.position)) <= distance)
        {
            movePosition = Vector3.left * moveSpeed * Time.deltaTime;
            this.transform.position += movePosition;
        }
        else
        {
            moveSpeed *= -1;
            movePosition = Vector3.left * moveSpeed * Time.deltaTime;
            this.transform.position += movePosition;
        }
    }
    private void Forward()
    {
        if (Mathf.Abs(Vector3.Distance(startPosition, this.gameObject.transform.position)) <= distance)
        {
            movePosition = Vector3.forward * moveSpeed * Time.deltaTime;
            this.transform.position += movePosition;
        }
        else
        {
            moveSpeed *= -1;
            movePosition = Vector3.forward * moveSpeed * Time.deltaTime;
            this.transform.position += movePosition;
        }
    }

    private void Up()
    {
        movePosition = Vector3.up * moveSpeed * Time.deltaTime;
        this.transform.position += movePosition;
        if (this.gameObject.transform.position.y - startPosition.y >= distance)
        {
            moveSpeed *= -1;
        }
        else if(this.gameObject.transform.position.y - startPosition.y <= 0)
        {
            moveSpeed *= -1;
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
