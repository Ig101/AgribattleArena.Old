using AgribattleArenaBackendServer.Engine.ActorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public delegate void SceneTileAction(Scene scene, Tile tile, float mod, float time);
    public delegate void SceneObjectAction(Scene scene, GameObject obj, float mod, float time);
    public delegate void SceneObjectTargetAction(Scene scene, GameObject obj, Tile targetTile, float mod, float time);
    public delegate void SkillObjectTargetAction(Scene scene, Actor act, Tile targetTime, Skill skill);
    public delegate void BuffAction(BuffManager manager, float mod);

    public struct Point
    {
        int x;
        int y;

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public Point (int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Misc
    {
        public const int tileSize = 128;

        public const int maxActionPoints = 8;

        public const int healthPerConst = 5;
        public const float willpowerMod = 0.1f;
        public const float strengthMod = 0.1f;
        public const float speedMod = 0.1f;

        public static float RangeBetween (float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }
    }
}
