using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmmoFiringLogic : MonoBehaviour
{
    private MineAmmoConfig ammoConfig;
    private MineObjects minetool;

    [SerializeField]
    private FloatVariable currentAmmoAmount;
    [SerializeField]
    private FloatVariable maxAmmoAmount;
    public bool canFire { get; private set; }

    private float lastShootTime;

    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
        ammoConfig = GetComponent<SpaceShip>().shipdata.miningTool.mineAmmoConfig;
        currentAmmoAmount.SetValue(ammoConfig.defaultCapacity);
        maxAmmoAmount.SetValue(ammoConfig.defaultCapacity);
        minetool = GetComponent<SpaceShip>().shipdata.miningTool;
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
        if (currentAmmoAmount.FloatValue > ammoConfig.defaultCapacity)
        {
            currentAmmoAmount.SetValue(ammoConfig.defaultCapacity);
        }
    }

    public void decrementAmmo()
    {
        lastShootTime = Time.time;
        currentAmmoAmount.DecrementValue(ammoConfig.ammoUsedPerShot);
        if (currentAmmoAmount.FloatValue <= 0)
        {
            canFire = false;
            if(minetool is BeamType)
            {
                var tool = minetool as BeamType;
                tool.OnShootRelease();
            }
        }
    }

    public void regenAmmo()
    {
        currentAmmoAmount.IncrementValue(ammoConfig.regenRate);

        if (!canFire)
        {
            if (currentAmmoAmount.FloatValue >= maxAmmoAmount.FloatValue)
            {
                canFire = true;
            }
        }
    }
}
