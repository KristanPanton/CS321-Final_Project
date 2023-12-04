using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    public Animator animator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("Hit " + collision.gameObject.name + "!");

            CreateBulletImpactEffect(collision);
            // Destroy the bullet
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            print("Hit a wall");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("AI"))
        {
            print(collision.gameObject.name + " was hit by a bullet!");
            animator = collision.gameObject.GetComponent<Animator>();
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            animator.SetTrigger("DEATH");

            Destroy(gameObject);
            StartCoroutine(DestroyAfterDelay(collision.gameObject, 30.0f));
        }

    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }

    void CreateBulletImpactEffect(Collision objWeHit)
    {
        ContactPoint contact = objWeHit.contacts[0];

        GameObject hole = Instantiate(
                GlobalReferences.Instance.bulletImpactEffectPrefab,
                contact.point,
                Quaternion.LookRotation(contact.normal)
            );

        hole.transform.SetParent(objWeHit.gameObject.transform);
    }


}
