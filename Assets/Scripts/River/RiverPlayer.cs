
namespace River
{
    public class RiverPlayer : Character.Player {

        public void Catch() {
            movingTarget.SetState(Character.MovingState.PICK);
        }

        public void Release() {
            movingTarget.SetState(Character.MovingState.DEFAULT);
        }

    }
}