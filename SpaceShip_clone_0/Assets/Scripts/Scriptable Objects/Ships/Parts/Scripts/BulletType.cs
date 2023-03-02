using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Bullet", menuName = "Agents/Parts/Mine/Bullet")]
public class BulletType : MineObjects, IShoot
{
    /// <summary>
    /// every bullet type object will share the same logic based on how it will fire
    /// so the generic type will implement the IShoot interface
    /// the Shoot method will be called when appropriate in the player prefab's shooting script
    /// </summary>
    public GameObject bullet;
    public Event shootEvent;

    public float interval { get { return _interval; } private set { _interval = value; } }
    [SerializeField]
    private float _interval;

    private float lastShootTime;
    private void Awake()
    {
        mineType = mineToolType.bullet;
    }
    private void OnEnable()
    {
        lastShootTime = 0;
    }

    public void Shoot(Transform[] origin, Transform aimDirection)
        //when to fire, whether firing increases a heat bar, decreases ammo count, or both,
        //will be determined in higher levels

    {
        if (Time.time >= lastShootTime + interval)
        {
            foreach (var firePoint in origin)
            {
                Instantiate(bullet, firePoint);
            }
            lastShootTime = Time.time;
            shootEvent.Raise();
        }

    }

    private void OnDisable()
    {
        lastShootTime = 0;
    }

    public void OnShootRelease()
    {
    }
}
