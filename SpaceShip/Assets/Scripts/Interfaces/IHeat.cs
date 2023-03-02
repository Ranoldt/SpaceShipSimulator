/// <summary>
/// Used for anything that operates on a heat/ cool system.
/// for example, mining beams that need to cool down when it's too hot.
/// </summary>
public interface IAmmoModifier
{
    public void OnShoot();
}
