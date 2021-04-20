using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hw3
{
    //jayllen
    //child class
    class theBonus: GameObject
    {
        //fields
        private Boolean isActive;
        //constructor
        public theBonus(Texture2D texture, int x, int y, int height, int width)
            : base(texture, x, y, height, width)
        {
            isActive = true;
        }
        //checks to see if player collides with bonus
        public virtual bool CheckCollision(GameObject check)
        {
            bool intersect = false;

            if (isActive == true)
            {
                if (this.rect.Intersects(check.rect))
                {
                    intersect = true;
                }
            }
            else
                intersect = false;

            return intersect;
        }
        public override void Draw(SpriteBatch sb)
        {
            if (isActive == true)
                base.Draw(sb);
        }
    }
}

