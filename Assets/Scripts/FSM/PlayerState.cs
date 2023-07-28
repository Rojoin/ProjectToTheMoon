using UnityEngine;

public class PlayerState : State
{
    protected PlayerMovement playerMovement;
    protected PlayerHealth playerHealthSystem;
    protected PlayerShooting playerShooting ;
    protected BoxCollider boxCollider ;

    public PlayerState(PlayerMovement playerMovement,PlayerHealth playerHealthSystem, PlayerShooting playerShooting, string name, StateMachine stateMachine, BoxCollider boxCollider) : base(name, stateMachine)
    {
        this.playerMovement = playerMovement;
        this.playerHealthSystem = playerHealthSystem;
        this.playerShooting = playerShooting;
        this.boxCollider = boxCollider;
    }
}