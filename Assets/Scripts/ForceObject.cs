using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObject : MonoBehaviour
{
    public float time;
    public Vector3 forceMove;
    public bool isCollision;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerData>() != null)
            {
                isCollision = true;
                StartCoroutine(ForceDelay(collision));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerData>() != null)
            {
                isCollision = false;
                StopCoroutine(ForceDelay(collision));
            }
        }
    }

    IEnumerator ForceDelay(Collision collision)
    {
        yield return new WaitForSeconds(time);
        if (isCollision)
        {
            PlayerData player = collision.gameObject.GetComponent<PlayerData>();
            player.rigidbody.AddForce(forceMove, ForceMode.Impulse);
            player.ForceMove = forceMove;
            StartCoroutine(ForceMove(player.ForceMove));
        }
    }

    IEnumerator ForceMove(Vector3 force)
    {
        while (!CharactorManager.Instance.Player._playerController.IsGrounded() || force.z == 0)
        {
            if (force.z > 0)
            {
                force.z = Mathf.Max(force.z - 1 * Time.deltaTime, 0);
            }
            else if (force.z < 0)
            {
                force.z = Mathf.Min(force.z + 1 * Time.deltaTime, 0);
            }
            if (force.x > 0)
            {
                force.x = Mathf.Max(force.x - 1 * Time.deltaTime, 0);
            }
            else if (force.z < 0)
            {
                force.x = Mathf.Min(force.x + 1 * Time.deltaTime, 0);
            }
            yield return null;
            force.y = 0;
            CharactorManager.Instance.Player._playerData.ForceMove = force;
        }
        CharactorManager.Instance.Player._playerData.ForceMove = Vector3.zero;
    }
}
