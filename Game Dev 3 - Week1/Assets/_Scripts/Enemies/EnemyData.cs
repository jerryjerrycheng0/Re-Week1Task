using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]

public class EnemyData : ScriptableObject
{
    public Sprite shipSprite; //Appearance of the ship
    public float shipSpeed; //Speed of the ship
    public int shipHP; //Health of the ship
    public float fireRateEnemy; //Fire rate of the ship
    public int bulletDamage; //Damage of the ship's bullet
    public GameObject bulletPrefab; //Prefab of the bullet
    public enum MovementType //Activates a menu to select the way a ship moves
    {
        Straight,
        Circular,
        Sway
    }
    public MovementType movementType; //The way the ship moves
}
