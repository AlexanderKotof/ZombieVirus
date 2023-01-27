public class EnemyComponent : CharacterComponent
{

    public float checkDistance = 10f;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        /*
        if (CurrentCommand == null)
        {
            foreach (var player in GamePlayFeature.Instance.team.characters)
            {
                if ((player.Position - Position).sqrMagnitude < checkDistance * checkDistance)
                {
                    ExecuteCommand(new AttackCommand(player));
                }
            }
        }
        */
    }
}