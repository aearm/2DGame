public interface IPlayerRespawnListener
{
    //this function on each object implement the interface 
    void OnPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, Player player);
}