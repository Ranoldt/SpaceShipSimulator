using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmmoFiringLogic : MonoBehaviour
{
    [SerializeField]
    private Event BeamInturruption;

    private MineAmmoConfig ammoConfig;
    private MineObjects minetool;

    private PlayerManager manager;
    public bool canFire { get; private set; }

    private float lastShootTime;

    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
        minetool = GetComponent<SpaceShip>().inv.equippedMineTool;
        ammoConfig = minetool.mineAmmoConfig;

        manager = GetComponent<SpaceShip>().playerData;

        manager.ammoCapacity = ammoConfig.defaultCapacity;
        manager.ammoLeft = manager.ammoCapacity;
    }

    private void Update()
    {
        if (Time.time >= lastShootTime + ammoConfig.regenInterval)
        {
            if (ammoConfig.regenRate != 0)
            {
                regenAmmo();
            }
        }
        if (manager.ammoLeft > ammoConfig.defaultCapacity)
        {
            manager.ammoLeft = ammoConfig.defaultCapacity;
        }
    }

    public void decrementAmmo()
    {
        lastShootTime = Time.time;
        manager.ammoLeft -= ammoConfig.ammoUsedPerShot;
        if (manager.ammoLeft <= 0)
        {
            canFire = false;
            if(minetool.mineType == mineToolType.beam)
            {
                BeamInturruption.Raise();
            }
        }
    }

    public void regenAmmo()
    {
        manager.ammoLeft += ammoConfig.regenRate;

        if (!canFire)
        {
            if (manager.ammoLeft >= manager.ammoCapacity)
            {
                canFire = true;
            }
        }
    }
}
