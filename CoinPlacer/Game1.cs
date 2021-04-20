using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Exam1Practical
{
    //Jay'llen
    //IGME106
    //March 3rd
    //Exam
    public class Game1 : Game
    {
        //fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //Mouse Field 
        private MouseState previousMsState;
        private MouseState ms;
        private List<Coin> list;
        //Textures
        private Texture2D coinText2D;
        private SpriteFont arial;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            // create coin list
            list = new List<Coin>(10);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //font and coin image
            arial = Content.Load<SpriteFont>("arial");
            coinText2D = Content.Load<Texture2D>("coin");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //these get looped
            ProcessInput();
            previousMsState = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //begin
            _spriteBatch.Begin();
            //string draw
            _spriteBatch.DrawString(arial, "Click Anywhere To Place An Object", new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(arial, $"Object Count: {list.Count}", new Vector2(0, 20), Color.White);
            //Draw Coins
            foreach (Coin s in list)
            {
                s.Draw(_spriteBatch);
            }
            //end
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        //checks is left mouse button has been clicked
        protected bool SingleLeftButtonPress()
        {
            bool isPressed = false;
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed && !(previousMsState.LeftButton == ButtonState.Pressed))
            {
                isPressed = true;
            }
            else
            {
                isPressed = false;
            }
            previousMsState = Mouse.GetState();
            return isPressed;
        }
        //checks if right mouse button has been clicked
       protected bool SingleRightButtonPress()
        {
            bool isPressed = false;
            MouseState ms = Mouse.GetState();
            if (ms.RightButton == ButtonState.Pressed && !(previousMsState.RightButton == ButtonState.Pressed))
            {
                isPressed = true;
            }
            else
            {
                isPressed = false;
            }
            previousMsState = Mouse.GetState();
            return isPressed;

        }
        //adding and removing coins based off if mouse was clicked
       protected void ProcessInput()
        {
            ms = Mouse.GetState();
           
            if (ms.LeftButton == ButtonState.Pressed && SingleLeftButtonPress())
            {
                if (list.Count < 10)
                    list.Add(new Coin(coinText2D, ms.X, ms.Y, coinText2D.Width / 2));
                else
                {
                    list.RemoveAt(0);
                    list.Add(new Coin(coinText2D, ms.X, ms.Y, coinText2D.Width / 2));
                }
            }
            //removing the coins when clicked
            else 
                if (ms.RightButton == ButtonState.Pressed)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].IsActive)
                        {
                            list.Remove(list[i]);
                        }
                    }
                }
       }
    }
}
