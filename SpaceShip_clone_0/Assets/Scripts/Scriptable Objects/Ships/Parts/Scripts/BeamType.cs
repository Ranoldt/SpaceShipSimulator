using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Beam", menuName = "Agents/Parts/Mine/Beam")]

public class BeamType : MineObjects, IShoot
{
    public Event initializationEvent;
    public Event shootEvent;

    [SerializeField]
    private GameObject laserOrigin;
    public float interval { get { return _interval; } private set { _interval = value; } }
    [SerializeField]
    private float _interval; //for beam types, this will refer to the time in seconds between damage calculations
    //TODO: Implement this

    public LineRenderer beam { get { return _beam; } private set { _beam = value; } }
    [SerializeField]
    private LineRenderer _beam;

    public List<LineRenderer> beams = new List<LineRenderer>();
    //references to the beam objects created by the initialization event

    private void Awake()
    {
        mineType = mineToolType.beam;
    }

    public void Shoot(Transform[] origin, Transform aimDirection)
    {
        RaycastHit Hitinfo;


        if (TargetInfo.IsTargetInRange(aimDirection.position, aimDirection.forward, out Hitinfo, laserRange, shootingMask))
        {
            IShootable target = Hitinfo.transform.GetComponent<IShootable>();
            if (target != null)
            {
                target.damage(miningPower + (1.5f * minepowerlevel.FloatValue));//total power of laser
            }
            Instantiate(laserHitParticles, Hitinfo.point, Quaternion.LookRotation(Hitinfo.normal));

            foreach (var beam in beams)
            {
                Vector3 localHitPosition = beam.transform.InverseTransformPoint(Hitinfo.point);
                beam.gameObject.SetActive(true);
                beam.SetPosition(1, localHitPosition);
            }
        }
        else
        {
            foreach (var beam in beams)
            {
                beam.gameObject.SetActive(true);
                beam.SetPosition(1, new Vector3(0, 0, laserRange));
            }
        }
        shootEvent.Raise();

    }

    public void OnShootRelease()
    {
        foreach(LineRenderer beam in beams)
        {
            beam.gameObject.SetActive(false);
        }
    }


}
