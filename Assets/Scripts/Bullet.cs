using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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

        //if (collision.gameObject.CompareTag("AI"))
        //{
        //    print(collision.gameObject.name + " was hit by a bullet!");
        //    CreateBulletImpactEffect(collision);
        //    Destroy(gameObject);
        //}

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
