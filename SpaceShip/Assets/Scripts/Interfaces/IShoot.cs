using UnityEngine;
/// <summary>
/// used for anything that has the ability to shoot
/// </summary>
public interface IShoot
{
    public float interval { get;}

    public void Shoot(Transform[] origin, Transform aimDirection);

    public void OnShootRelease();
}
