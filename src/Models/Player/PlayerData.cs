namespace Pokebot.Models.Player
{
    //gPlayerFacingPosition
    //gPlayerAvatar
    public class PlayerData
    {
        public int Id { get; }
        public string Name { get; }
        public Position Position { get; }
        public Position PreviousPosition { get; }
        public PlayerRunningState RunningState { get; }
        public TileTransitionState TransitionState { get; }
        public bool Gender { get; }
        public PlayerFacingDirection FacingDirection { get; }
        public int MapGroup { get; } = 0;
        public int MapNumber { get; } = 0;

        public PlayerData(Position position, Position prevPosition, PlayerRunningState runningState, TileTransitionState transitionState, bool gender, PlayerFacingDirection facingDirection, int mapGroup = 0, int mapNumber = 0, int id = 0, string name = null)
        {
            Position = position;
            PreviousPosition = prevPosition;
            RunningState = runningState;
            TransitionState = transitionState;
            Gender = gender;
            FacingDirection = facingDirection;
            MapGroup = mapGroup;
            MapNumber = mapNumber;
            Id = id;
            Name = name;
        }
    }
}
