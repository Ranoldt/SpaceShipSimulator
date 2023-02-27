using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOrigin : MonoBehaviour
{
    private MineObjects mineTool;
    private LineRenderer beamFab;
    private Transform[] origins;
    // Start is called before the first frame update
    void Start()
    {
        //this is to make sure every system changes accordingly when SpaceShip's shipdata changes.
        mineTool = this.gameObject.GetComponentInParent<SpaceShip>().shipdata.miningTool;

        if (mineTool.GetType() == typeof(BeamType))
        {
            var tool = mineTool as BeamType;
            tool.initializationEvent.Raise();
        }
    }

    //runs whenever a new beam object is enabled.
    //expected behavior: it instantiates the beam prefabs as children of each origin object
    //It also stores references to each beam inside a list created in the beam scriptable object
    public void beamInitialization()
    {
        var tool = mineTool as BeamType;
        beamFab = tool.beam;

        //clear beams
        tool.beams.Clear();

        origins = this.gameObject.GetComponentsInChildren<Transform>();

        //Instantiate beam instances
        foreach (Transform origin in origins)
        {
            //if there are already laser prefabs instantiated, then destroy them before
            //initializing. Edge cases yahoo
            var leftoverBeam = origin.gameObject.GetComponent<LineRenderer>();
            if(leftoverBeam != null)
            {
                Destroy(leftoverBeam.gameObject);
            }

            var beam = Instantiate(beamFab, 
                origin);

            tool.beams.Add(beam);
        }

    }
}
