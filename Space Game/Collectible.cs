using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hw3
{
    //jayllen
    //child class
    class Collectible : GameObject
    {
        //field
        private Boolean isActive;
        //constructor
        public Collectible( Texture2D texture, int x, int y, int height, int width)
            : base(texture, x, y, height, width)
        {
            isActive = true;
        }
        //checls collision
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
        //overrides draw method
        public override void Draw(SpriteBatch sb)
        {
            if(isActive == true)
                base.Draw(sb);
        }
    }
}
