
using UnityEngine;

public class EndingPlayer : Character.Player {

    protected new void Start() {
        base.Start();
        movingTarget.Paused = true;
        var animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = movingTarget.AnimatorControllers[Character.MovingState.RIGHT];
        Debug.Log("EndingPlayer");
    }

    public void Reincarnate()
    {
        UserStateManager.Instance.CurCharacter = UserStateManager.Instance.NewCharacter;
        UserStateManager.Instance.IsNewGame = true;
        GameManager.Instance.LoadSceneAndClose("04-End-NewCharacter");
    }
}