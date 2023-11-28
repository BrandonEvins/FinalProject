using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

     private Transform target;
    private AudioSource shootingAudioSource;
    private AudioSource hitAudioSource;

    public AudioClip shootSound; // Assign the shoot sound effect in the Unity Editor
    public AudioClip hitSound;   // Assign the hit sound effect in the Unity Editor

    private void Awake()
    {
        // Create empty GameObjects to hold the AudioSources
        GameObject shootingAudioSourceObject = new GameObject("BulletShootingAudioSource");
        GameObject hitAudioSourceObject = new GameObject("BulletHitAudioSource");

        shootingAudioSource = shootingAudioSourceObject.AddComponent<AudioSource>();
        hitAudioSource = hitAudioSourceObject.AddComponent<AudioSource>();

        shootingAudioSource.clip = shootSound;
        hitAudioSource.clip = hitSound;

        // Ensure that the audio sources persist between scenes
        DontDestroyOnLoad(shootingAudioSourceObject);
        DontDestroyOnLoad(hitAudioSourceObject);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        PlayShootSound();
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(bulletDamage);
        }

        PlayHitSound();
        ReturnToPool(); // Instead of destroying the bullet, return it to the object pool
    }

    // Object pool management
    private void ReturnToPool()
    {
        // Detach the AudioSources from the bullet before disabling it
        shootingAudioSource.transform.SetParent(null);
        hitAudioSource.transform.SetParent(null);

        gameObject.SetActive(false); // Disable the GameObject instead of destroying it
    }

    private void PlayShootSound()
    {
        // Play the shooting sound using the detached audio source
        shootingAudioSource.Play();
    }

    private void PlayHitSound()
    {
        // Play the hit sound using the detached audio source
        hitAudioSource.Play();
    }
}
