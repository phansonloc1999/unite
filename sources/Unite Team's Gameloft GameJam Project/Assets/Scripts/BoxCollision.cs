using System.Collections;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    [SerializeField] private float _collisionStayDuration;

    private void OnCollisionStay2D(Collision2D other)
    {
        IEnumerator MyCouroutine()
        {
            yield return new WaitForSeconds(5.0f);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
        StartCoroutine(MyCouroutine());
    }
}