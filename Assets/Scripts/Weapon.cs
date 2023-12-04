using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    // Shooting
    public bool isShooting, readyToShoot;

    private bool allowReset = true;
    public float shootingDelay = 2f;

    // Burst
    public int bulletsPerBurst = 3;

    public int burstBulletsLeft;

    // Spread
    public float spreadFactor;

    // Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30.0f;
    public float bulletLifeTime = 3.0f;

    // Ammo
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    // Muzzle Flash
    public GameObject muzzleEffect;

    public Animator animator;


    public enum ShootingMode
    { Auto, Burst, Single };

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        bulletsLeft = magazineSize;
        burstBulletsLeft = bulletsPerBurst;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //ShootWeapon();

        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single
            || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && isShooting && bulletsLeft > 0)
        {
            burstBulletsLeft = bulletsPerBurst;
            ShootWeapon();
        }

        if (isShooting && bulletsLeft <= 0)
        {
            SoundManager.Instance.shootingEmptyRevolverSound.Play(); 
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading)
        {
            Reload();
        }

        if (AmmoManager.Instance.ammoDisplay != null)
        {
            AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft / bulletsPerBurst}/{magazineSize / bulletsPerBurst}";
        }

        // If you want to automatically reload when magazine is empty
        //if ( readyToShoot && !isShooting && !isReloading && bulletsLeft <= 0) {
            //Reload();
        //}
    }

    private void ShootWeapon()
    {
        bulletsLeft--;


        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        SoundManager.Instance.shootingRevolverSound.Play();

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate a bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Rotate the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        // Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);

        // Destroy the bullet after a certain amount of time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));

        // Check if we are done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Burst mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1) // We already shot one bullet before this check
        {
            burstBulletsLeft--;
            Invoke("ShootWeapon", shootingDelay);
        }

    }

    private void Reload()
    {

        isReloading = true;
        animator.SetTrigger("RELOAD");
        Invoke("ReloadCompleted", reloadTime);

    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        // Calculate the direction from the camera to the mouse position
        //Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        //Vector3 direction = playerCamera.ScreenToWorldPoint(Input.mousePosition) - rayOrigin;

        //// Calculate the spread
        //float x = Random.Range(-spreadFactor, spreadFactor);
        //float y = Random.Range(-spreadFactor, spreadFactor);

        //// Apply the spread to the direction
        //direction.x += x;
        //direction.y += y;

        //return direction;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit))
        {
            // Hitting something
            targetPoint = hit.point;
        }
        else
        {
            // Shooting at the air
            targetPoint = ray.GetPoint(1000);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadFactor, spreadFactor);
        float y = UnityEngine.Random.Range(-spreadFactor, spreadFactor);

        // Return the shooting direction and spread
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}