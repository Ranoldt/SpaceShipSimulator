using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// The goal is to be able to use this script for every weapon type;
/// unique behaviors will apply through scripable objects
/// However, they all use this same logic to know when to fire vs. when to not
/// 
/// This would make character customization easier, as all we need to do is swap out
/// the mining tool asset field in the ship scriptable object for changes to be reflected everywhere else.
/// It also makes the mining tool assets very expandable.
/// The player ship prefab won't have to carry all the different firing logic behaviors as well; all it needs to do is
/// tell the asset to shoot when it needs to.
/// 
/// For example, beams and bullets have drastically different behaviors.
/// - Beams are raycast lines, visible with line renderers.
/// >They do constant tick damage per interval
/// 
/// - Bullets are projectiles, which can either be a capsule object or a trail renderer
/// >they shoot 1 bullet per firing point every interval
/// 
/// The different shooting behaviors are dealt with in the BeamType and BulletType scriptable objects,
/// which utilize the IShoot interface so that this script can activate the shooting methods.
/// </summary>
public class WeaponsBehaviorTEST : MonoBehaviour
{
    private MineObjects tool;

    private IShoot shooter;
    private bool held = false;

    [SerializeField]
    private Transform[] shooters;

    [SerializeField]
    private Transform OriginMiddle;

    private AmmoFiringLogic al;
    private void Start()
    {
        //tool = GetComponent<SpaceShip>().shipdata.;

        al = GetComponent<AmmoFiringLogic>();
    }
    void Update()
{
    if (held&& al.canFire)
    {
            //tool._Shoot(shooters, OriginMiddle);
    }
    else if (!held)
    {
            //tool._OnShootRelease();
    }
}
//switch the status of held based on whether the button is being pressed or released. OnAttack is called every time the button is pressed and every time it is released, the if statements are what determine which of those two is currently happening.
public void OnShoot(InputAction.CallbackContext ctx) {
    if (ctx.performed)
        held = true;
    if (ctx.canceled)
        held = false;
} 
}
