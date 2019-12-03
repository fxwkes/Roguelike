﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    abstract class Character : BaseCharacter
    {
        public enum Speed
        {
            Normal,
            High
        }
        public Speed CurrentSpeed { get; set; }
        public int HitPoints { get; set; }
        public int RangeOfVision { get; set; }
        public char Symbol { get; set; }
        public bool IsMoved { get; set; }
        protected abstract bool HandleCollisions(char mapSymbol, char entitySymbol);
        //sets CurrentGameAction, returns true if we can move, otherwise - false
        public override void Move() //sets IsMoved
        {
            Point tmp = new Point();
            switch (CurrentMoveAction)
            {
                case MoveAction.Up:
                    tmp.SetValue(Coords.X, Coords.Y - 1);
                    IsMoved = HandleCollisions(Program.GameEngine.GetMapSymbol(tmp),
                                               Program.GameEngine.GetEntitySymbol(tmp));
                    if (IsMoved) SetPrevPlusMove(MoveAction.Up);
                    break;
                case MoveAction.Down:
                    tmp.SetValue(Coords.X, Coords.Y + 1);
                    IsMoved = HandleCollisions(Program.GameEngine.GetMapSymbol(tmp),
                                               Program.GameEngine.GetEntitySymbol(tmp));
                    if (IsMoved) SetPrevPlusMove(MoveAction.Down);
                    break;
                case MoveAction.Left:
                    tmp.SetValue(Coords.X - 1, Coords.Y);
                    IsMoved = HandleCollisions(Program.GameEngine.GetMapSymbol(tmp),
                                               Program.GameEngine.GetEntitySymbol(tmp));
                    if (IsMoved) SetPrevPlusMove(MoveAction.Left);
                    break;
                case MoveAction.Right:
                    tmp.SetValue(Coords.X + 1, Coords.Y);
                    IsMoved = HandleCollisions(Program.GameEngine.GetMapSymbol(tmp),
                                               Program.GameEngine.GetEntitySymbol(tmp));
                    if (IsMoved) SetPrevPlusMove(MoveAction.Right);
                    break;
                default:
                    IsMoved = false;
                    break;
            }
            if(CurrentSpeed == Speed.High && IsMoved)
            {
                switch (CurrentMoveAction)
                {
                    case MoveAction.Up:
                        tmp.SetValue(Coords.X, Coords.Y - 1);
                        if (HandleCollisions(Program.GameEngine.GetMapSymbol(tmp),
                                             Program.GameEngine.GetEntitySymbol(tmp)))
                            MoveUp();
                        break;
                    case MoveAction.Down:
                        tmp.SetValue(Coords.X, Coords.Y + 1);
                        if (HandleCollisions(Program.GameEngine.GetMapSymbol(tmp),
                                             Program.GameEngine.GetEntitySymbol(tmp)))
                            MoveDown();
                        break;
                    case MoveAction.Left:
                        tmp.SetValue(Coords.X - 1, Coords.Y);
                        if (HandleCollisions(Program.GameEngine.GetMapSymbol(tmp),
                                             Program.GameEngine.GetEntitySymbol(tmp)))
                            MoveLeft();
                        break;
                    case MoveAction.Right:
                        tmp.SetValue(Coords.X + 1, Coords.Y);
                        if (HandleCollisions(Program.GameEngine.GetMapSymbol(tmp),
                                             Program.GameEngine.GetEntitySymbol(tmp)))
                            MoveRight();
                        break;
                }
            }
        }
    }
}
