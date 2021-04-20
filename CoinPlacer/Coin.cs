using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Exam1Practical
{
    //Jayllen
    //Coin Class
    class Coin
    {
        //Fields
        private Rectangle coinPosition;
        private Texture2D coinImage;
        private int radius;
        private bool active;
        //Properties
        public int GetX{ get { return coinPosition.X + radius; } set { coinPosition.X = value - radius; } }
        public int GetY{ get { return coinPosition.Y + radius; } set { coinPosition.Y = value - radius; } }
        public int GetRadius{ get { return radius; } }
        public bool IsActive { get { return active; } }
        //Constructor
        public Coin(Texture2D texture, int x, int y, int radius)
        {
            this.coinImage = texture;
            this.radius = radius;
            coinPosition = new Rectangle(x - radius, y - radius, radius, radius);
        }
        //Draw Method
        public void Draw(SpriteBatch sb)
        {
            MouseState ms = Mouse.GetState();
            //checks the position of coin and mouse
            if (coinPosition.Contains(ms.Position))
            {
                sb.Draw(coinImage, coinPosition, Color.Red);
                active = true;
            }
            else
            {
                sb.Draw(coinImage, coinPosition, Color.White);
                active = false;
            }
        }
    }
}
