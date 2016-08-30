public interface IPlayerState
{
    IPlayerState HandleInput(Player p);
    void Enter(Player p);
    void Update(Player p);
}
