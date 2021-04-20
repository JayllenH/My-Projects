using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hw3
{
    //jayllen
    //parent class
    public class GameObject
    {
        //fields
        protected Texture2D thePlayer;
        public Rectangle rect;
        //properties
        public int X { get { return rect.X; } set { rect.X = value; } }
        public int Y { get { return rect.Y; } set { rect.Y = value; } }
        public Texture2D getPlayer { get { return thePlayer; } set { thePlayer = value; } }
        //constructor
        public GameObject(Texture2D texture, int x, int y, int height, int width)
        {
            this.thePlayer = texture;
            rect = new Rectangle(x, y, height, width);
        }
        //draw method
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(thePlayer, rect ,Color.White);
        }
    }
}
