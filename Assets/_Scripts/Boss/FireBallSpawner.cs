using System;
using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public Transform player;

    public float fireballSpeed = 5f;
    public float spreadAngle = 30f; // Angle between each fireball



    public void Awake()
    {
        MessageBuffer<SetPlayer>.Subscribe(CharacterSet);
    }

    private void CharacterSet(SetPlayer obj)
    {
        player = obj.player.transform;
    }

    public void Shoot()
    {
        if (player == null)
            return;

        // Calculate direction towards the player
        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;

        // Calculate angles based on the direction to the player
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // Calculate angles for the three fireballs
        float angle1 = angleToPlayer + spreadAngle;
        float angle2 = angleToPlayer - spreadAngle;
        float angle3 = angleToPlayer;

        // Spawn and shoot the fireballs
        SpawnAndShootFireball(angle1);
        SpawnAndShootFireball(angle2);
        SpawnAndShootFireball(angle3);
    }

    private void SpawnAndShootFireball(float angle)
    {
        // Calculate the direction vector from the fire point based on the angle
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;

        // Instantiate the fireball
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        // Get the Rigidbody2D component of the fireball
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

        // Apply force to the fireball to shoot it
        rb.velocity = direction * fireballSpeed;
    }
}
