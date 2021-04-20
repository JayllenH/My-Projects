using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hw3
{
    //jayllen
    //child class
    class Player : GameObject
    {
        //fields
        private int lvlScore =0;
        private int totalScore =0;
        //construtpr
        public Player(int scoreLvl, int scoreTotal, Texture2D texture, int x, int y, int height, int width)
            : base(texture, x, y, height, width)
        {
            lvlScore = scoreLvl;
            totalScore += scoreTotal;
        }
        //properties
        public int getLevelScore { get { return lvlScore; }  set { lvlScore = value; } }
        public int getTotalScore { get { return totalScore; } set { totalScore = value; } }
    }
}
