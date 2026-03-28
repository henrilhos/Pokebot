using BizHawk.Client.Common;
using Pokebot.Models.Memory;
using Pokebot.Models.Player;
using Pokebot.Utils;
using System.Collections.Generic;

namespace Pokebot.Models.ActionRunners
{
    public abstract class CommonActionRunner : IActionRunner
    {
        public ApiContainer APIContainer { get; }
        public IGameMemory GameVersion { get; }
        protected Dictionary<PlayerFacingDirection, string> NextDirections { get; private set; }

        public CommonActionRunner(ApiContainer apiContainer, IGameMemory gameVersion)
        {
            APIContainer = apiContainer;
            GameVersion = gameVersion;
            NextDirections = new Dictionary<PlayerFacingDirection, string>()
            {
                { PlayerFacingDirection.Up, "Right" },
                { PlayerFacingDirection.Right, "Down" },
                { PlayerFacingDirection.Down, "Left" },
                { PlayerFacingDirection.Left, "Up" },
            };
        }

        public abstract bool ExecuteStarter(int indexStarter);

        public virtual bool Spin()
        {
            var player = GameVersion.GetPlayer();
            if (NextDirections.ContainsKey(player.FacingDirection))
            {
                APIContainer.Joypad.Set(NextDirections[player.FacingDirection], true);

                return true;
            }

            return false;
        }

        public virtual bool Escape()
        {
            var state = GameVersion.GetGameState();
            if (state == GameState.Battle)
            {
                GameVersion.TrySetEscape();

                var action = (BattleActionSelectionCursor)GameVersion.GetActionSelectionCursor();

                switch (action)
                {
                    case BattleActionSelectionCursor.Moves:
                        PressB();
                        PressRight();
                        return false;
                    case BattleActionSelectionCursor.Bag:
                        PressDown();
                        return false;
                    case BattleActionSelectionCursor.Team:
                        PressUp();
                        return false;
                    case BattleActionSelectionCursor.Escape:
                        PressA();
                        return false;
                }
            }

            return false;
        }

        public virtual bool UseRegisteredItem()
        {
            PressSelect();

            return true;
        }

        public virtual bool Headbutt()
        {
            PressA();

            return true;
        }

        public void PressA() => APIContainer.Joypad.SetWhenInactive("A");
        public void PressB() => APIContainer.Joypad.SetWhenInactive("B");
        public void PressX() => APIContainer.Joypad.SetWhenInactive("X");
        public void PressY() => APIContainer.Joypad.SetWhenInactive("Y");
        public void PressLeft() => APIContainer.Joypad.SetWhenInactive("Left");
        public void PressRight() => APIContainer.Joypad.SetWhenInactive("Right");
        public void PressDown() => APIContainer.Joypad.SetWhenInactive("Down");
        public void PressUp() => APIContainer.Joypad.SetWhenInactive("Up");
        public void PressStart() => APIContainer.Joypad.SetWhenInactive("Start");
        public void PressSelect() => APIContainer.Joypad.SetWhenInactive("Select");
    }
}
