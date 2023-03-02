using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOrigin : MonoBehaviour
{
    private MineObjects mineTool;
    private LineRenderer beamFab;
    private Transform[] origins;
    private MineToolFiring toolFiring;

    private void Start()
    {
        toolFiring = GetComponentInParent<MineToolFiring>();
    }

    //runs whenever a new beam object is enabled.
    //expected behavior: it instantiates the beam prefabs as children of each origin object
    //It also stores references to each beam inside a list created in the beam scriptable object
    public void beamInitialization()
    {
        MineObjects tool = GetComponentInParent<SpaceShip>().inv.equippedMineTool;
        beamFab = tool.fab.GetComponent<LineRenderer>();

        //clear beams
        toolFiring.beams.Clear();

        origins = this.gameObject.GetComponentsInChildren<Transform>();

        if (toolFiring.beams.Count != 0)
        {
            toolFiring.beams.Clear(); //when initializing, make sure the list is empty before populating
        }
        //Instantiate beam instances
        foreach (Transform origin in origins)
        {
            //if there are already laser prefabs instantiated, then destroy them before
            //initializing. Edge cases yahoo
            var leftoverBeam = origin.gameObject.GetComponent<LineRenderer>();
            if (leftoverBeam != null)
            {
                Destroy(leftoverBeam.gameObject);
            }

            var beam = Instantiate(beamFab, 
                origin);

            toolFiring.beams.Add(beam);
        }

    }
}
