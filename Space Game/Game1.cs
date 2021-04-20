using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Hw3
{
    //igme 106
    //first mono game
    //Jayllen

    //enums
    enum GameState
    {
        MainMenu,
        Game,
        GameOver
    }
    //constructor
    public class Game1 : Game
    {
        //all my fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont verdana14;
        private SpriteFont bigText;
        private Player playerClass;
        //random
        private Random rng = new Random();
        //images
        private Texture2D playerImage;
        private Texture2D collectibleImage;
        private Texture2D backImage;
        private Texture2D bonusImage;
        private Rectangle background;
        //list
        private List<Collectible> items;
        private List<theBonus> bonusItem;
        //game state
        private GameState currentState;
        private KeyboardState kb;
        private KeyboardState previousKbState;
        private int currentLevel;
        private double timer;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            // setting everything to their values
            timer = 0;
            currentLevel = 0;
            background.X = 0;
            background.Y = 0;
            items = new List<Collectible>(5);
            bonusItem = new List<theBonus>(1);
            background.Width = _graphics.PreferredBackBufferWidth;
            background.Height = _graphics.PreferredBackBufferHeight;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //loads up images
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //background
            backImage = Content.Load<Texture2D>("space");
            //font
            verdana14 = Content.Load<SpriteFont>("Verdana14");
            bigText = Content.Load<SpriteFont>("bigtext");

            //loads player to screen
            playerImage = Content.Load<Texture2D>("blackhole1");
            //collects
            collectibleImage = Content.Load<Texture2D>("star");
            bonusImage = Content.Load<Texture2D>("earth");
            
            playerClass = new Player(0, 0, playerImage, (_graphics.PreferredBackBufferWidth / 2),
                (_graphics.PreferredBackBufferHeight / 2), 80, 80);
            // TODO: use this.Content to load your game content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           kb = Keyboard.GetState();
            //switch statements
            switch (currentState)
            {
                case GameState.MainMenu:
                    if (SingleKeyPress(Keys.Enter))
                    {
                        currentState = GameState.Game;
                        ResetGame();
                    }
                    break;
                case GameState.Game:
                    timer -= gameTime.ElapsedGameTime.TotalSeconds;
                    MovePlayer();
                    ScreenWrap(playerClass);
                    //spawns collects
                    for(int i =0; i < items.Count; i++)
                    {
                        if(items[i].CheckCollision(playerClass))
                        {
                            playerClass.getLevelScore += 1;
                            playerClass.getTotalScore += 1;
                            items.Remove(items[i]);
                        }
                    }
                    if (items.Count == 0)
                        NextLvl();
                    //every 5 levels spawns bonus
                    if (currentLevel % 5 == 0)
                    {
                        for (int i = 0; i < bonusItem.Count; i++)
                        {
                            if (bonusItem[i].CheckCollision(playerClass))
                            {
                                playerClass.getLevelScore += 5;
                                playerClass.getTotalScore += 5;
                                bonusItem.Remove(bonusItem[i]);
                            }
                        }
                    }
                   //ends game at 0 or less
                    if (timer <= 0)
                        currentState = GameState.GameOver;
                    break;
                case GameState.GameOver:
                    if (kb.IsKeyDown(Keys.Enter))
                        currentState = GameState.MainMenu;
                    break;
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //draws everything
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            //background image
           _spriteBatch.Draw(backImage,background, Color.White);
            //switch statement
            switch (currentState)
            {
                case GameState.MainMenu:
                    // Draw the main menu 
                    _spriteBatch.DrawString(bigText, "Black Hole T A K E  O V E R", 
                        new Vector2(100, 100), Color.White);
                    _spriteBatch.DrawString(verdana14, "This is the MAIN MENU state!\n\n" +
                        "Collect as many stars as you can!\n\nBONUS - Collect planets for extra Points!!\n\n" +
                        "Press 'ENTER' to Play.", new Vector2(200, 200), Color.White);
                    break;
                case GameState.Game:
                    //draw string
                    _spriteBatch.DrawString(verdana14, "Level Score: " + playerClass.getLevelScore
                        , new Vector2(0,0), Color.White);
                    _spriteBatch.DrawString(verdana14, "Total Score: " + playerClass.getTotalScore
                       , new Vector2(0, 20), Color.White);
                    _spriteBatch.DrawString(verdana14, "Level: " + currentLevel
                       , new Vector2(0, 40), Color.White);
                   _spriteBatch.DrawString(verdana14, string.Format("{0:0.00}",timer)
                      , new Vector2(0, 60), Color.White);
                  //draws player to sceen
                    playerClass.Draw(_spriteBatch);
                    //regular items
                    for (int i = 0; i < items.Count; i++)
                    {
                        items[i].Draw(_spriteBatch);
                        if (items[i].CheckCollision(playerClass))
                        {

                            playerClass.getLevelScore += 1;
                            playerClass.getTotalScore += 1;
                            items.Remove(items[i]);
                        }
                    }
                    //bonus item
                    if (currentLevel % 5 == 0)
                    {
                        for (int i = 0; i < bonusItem.Count; i++)
                        {
                            bonusItem[i].Draw(_spriteBatch);
                            if (bonusItem[i].CheckCollision(playerClass))
                            {
                                bonusItem.Remove(bonusItem[i]);
                            }
                        }
                    }
                    break;
                case GameState.GameOver:
                    _spriteBatch.DrawString(verdana14, "G A M E  O V E R ",
                      new Vector2(200, 200), Color.White);
                    _spriteBatch.DrawString(verdana14, "Level Reached:  " + currentLevel 
                        + "\nTotal Score: " +playerClass.getTotalScore + "\nPress 'ENTER'" +
                        " to return to main menu", new Vector2(200, 220), Color.White);
                    break;
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        protected void NextLvl()
        {
            //gets height and width and updates current level and time
            int width = _graphics.PreferredBackBufferWidth;
            int height = _graphics.PreferredBackBufferHeight;
            currentLevel += 1;
            timer = 10;
          // playerClass = new Player(0, playerClass.getTotalScore, playerImage, width/2,height/2, 80, 80);
            items.Clear();
            if(currentLevel == 1 )
            {
                items.Capacity = 5;
            }
            else
                items.Capacity = (items.Capacity + 3);
            //spawns items
            for (int i = 0; i < items.Capacity; i++)
            {
                items.Add(new Collectible(collectibleImage,
                    rng.Next(0, _graphics.PreferredBackBufferWidth-100),
                    rng.Next(0, _graphics.PreferredBackBufferHeight-100), 50, 50));
            }
            playerClass.getLevelScore = 0;
            //bonus item
            if(currentLevel % 5 == 0)
            {
                bonusItem.Add(new theBonus(bonusImage, rng.Next(0, _graphics.PreferredBackBufferWidth - 100),
                    rng.Next(0, _graphics.PreferredBackBufferHeight - 100), 50, 50));
                
            }
        }
        protected void ResetGame()
        {
            //turns all vaules back to zero
            currentLevel = 0;
            playerClass.getTotalScore = 0;
            playerClass.getLevelScore = 0;
            NextLvl();
        }
        protected void ScreenWrap(GameObject objToWrap)
        {
            //checks X and y values with borders
            if (objToWrap.X > _graphics.PreferredBackBufferWidth)
            {
                objToWrap.X = 0;
            }
            else
                if (objToWrap.X < -70)
                {
                 objToWrap.X = _graphics.PreferredBackBufferWidth;
                }
                  else
                    if (objToWrap.Y > _graphics.PreferredBackBufferHeight)
                    {
                         objToWrap.Y = 0;
                    }
                       else
                         if (objToWrap.Y < -70)
                         {
                            objToWrap.Y = _graphics.PreferredBackBufferHeight;
                         }
        }
        protected bool SingleKeyPress(Keys key)
        {
            //checks to see if enter key was pressed to start the game
            bool pressed;
            var currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(key) && !previousKbState.IsKeyDown(key))
            {
                pressed = true;
            }
            else
                pressed = false;
            previousKbState = Keyboard.GetState();
            return pressed;

        }
        public void MovePlayer()
        {
            //checks what keys are pushed to determine where to move player
            KeyboardState move = Keyboard.GetState();
            if (move.IsKeyDown(Keys.Up) || move.IsKeyDown(Keys.W))
                playerClass.Y -= 4;

            if (move.IsKeyDown(Keys.Down) || move.IsKeyDown(Keys.S))
                playerClass.Y += 4;

            if (move.IsKeyDown(Keys.Left) || move.IsKeyDown(Keys.A))
                playerClass.X -= 4;

            if (move.IsKeyDown(Keys.Right) || move.IsKeyDown(Keys.D))
                playerClass.X += 4;
        }
    }
}
