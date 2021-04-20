using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace GravityAndCollisionPE
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private Texture2D playerTexture;
		private Texture2D obstacleTexture;

		private float playerSpeedX;
		private Vector2 playerVelocity;
		private Vector2 jumpVelocity;
		private Vector2 playerPosition;
		private Vector2 gravity;

		private List<Rectangle> obstacleRects;
		private KeyboardState prevKB;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}


		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// </summary>
		protected override void Initialize()
		{
			playerPosition = new Vector2(400, 100);
			playerVelocity = Vector2.Zero;
			jumpVelocity = new Vector2(0, -15.0f);
			gravity = new Vector2(0, 0.5f);

			playerSpeedX = 5.0f;

			obstacleRects = new List<Rectangle>();

			base.Initialize();
		}


		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			playerTexture = Content.Load<Texture2D>("mario2");
			obstacleTexture = Content.Load<Texture2D>("pixel");

			// Create a simple map out of crates
			int bottom = _graphics.PreferredBackBufferHeight;
			int right = _graphics.PreferredBackBufferWidth;
			int obstacleSize = 50;

			// Make a floor, walls and a platform
			obstacleRects.Add(new Rectangle(0, bottom - obstacleSize, right, obstacleSize));
			obstacleRects.Add(new Rectangle(0, 0, obstacleSize, bottom));
			obstacleRects.Add(new Rectangle(right - obstacleSize, 0, obstacleSize, bottom));
			obstacleRects.Add(new Rectangle(200, 250, 400, obstacleSize));
		}


		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, etc.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// Handle input, apply gravity and then deal with collisions
			ProcessInput();
			ApplyGravity();
			ResolveCollisions();

			// Save the old state at the end of the frame
			prevKB = Keyboard.GetState();
			base.Update(gameTime);
		}

		/// <summary>
		/// Handles movement for sidescrolling game with gravity
		/// </summary>
		private void ProcessInput()
		{
			// PRACTICE EXERCISE: Finish this method!
			KeyboardState kb = Keyboard.GetState();
			//left and right movement
			if (kb.IsKeyDown(Keys.Left) || kb.IsKeyDown(Keys.A))
				playerPosition.X -= playerSpeedX;
			if (kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.D))
				playerPosition.X += playerSpeedX;
			//jump
			if (SingleKeyPress(Keys.Space))
				playerVelocity.Y = jumpVelocity.Y;

		}

		/// <summary>
		/// Applies gravity to the player
		/// </summary>
		private void ApplyGravity()
		{
			// PRACTICE EXERCISE: Finish this method!

			//adding gravity !
			playerVelocity += gravity;
			playerPosition+=playerVelocity;
		}

		/// <summary>
		/// Handles player collisions with obstacles
		/// </summary>
		private void ResolveCollisions()
		{
			// Get the player's current rect
			Rectangle playerRect = GetPlayerRect();

			// Loop and check each obstacle
			foreach (Rectangle obstacle in obstacleRects)
			{
				// Are we overlapping?
				if (playerRect.Intersects(obstacle))
				{
					// PRACTICE EXERCISE: Finish this part of the method!
					// Some of it has been started for you.  You only need to
					// write code inside this section of the method.

					// Get the overlapping rectangle between the
					// player and the obstacle using Rectangle.Intersect()
					Rectangle overLap = Rectangle.Intersect(playerRect, obstacle);

					// Determine if that overlapping rectangle is
					// TALL (height >= width) or WIDE (width > height)

					// If overlap rectangle is TALL...
					//// Next question: Do we need to move Mario LEFT or RIGHT?
					//// To figure out which, compare Mario's X and obstacle's X
					//// Then move an amount equal to overlap rectangle's WIDTH (+ or -)
					if (overLap.Height >= overLap.Width)
                    {
						if (playerRect.X <= obstacle.X)
							playerRect.X -= overLap.Width;
						else
							if(playerRect.X > obstacle.X)
							  playerRect.X += overLap.Width;
                    }
					else
						if (overLap.Height < overLap.Width)
						{
							// If overlap rectangle is WIDE...
							//// Next question: Do we need to move Mario UP or DOWN?
							//// To figure out which, compare Mario's Y to obstacle's Y
							//// Then move an amount equal to overlap rectangle's HEIGHT (+ or -)
							//// ALSO set playerVelocity.Y to zero, since Mario hit a floor or ceiling
							if (playerRect.Y <= obstacle.Y)
									playerRect.Y -= overLap.Height;
								else
									if(playerRect.Y > obstacle.Y)
										playerRect.Y += overLap.Height;
							
								playerVelocity.Y = 0;
					    }
				}
			}
			

			// Save position from our temp rectangle
			playerPosition.X = playerRect.X;
			playerPosition.Y = playerRect.Y;
		}

		/// <summary>
		/// Determines if a key was initially pressed this frame
		/// </summary>
		/// <param name="key">The key to check</param>
		/// <returns>True if this is the first frame the key is pressed, false otherwise</returns>
		private bool SingleKeyPress(Keys key)
		{
			return Keyboard.GetState().IsKeyDown(key) && prevKB.IsKeyUp(key);
		}
		/// <summary>
		/// Gets a rectangle for the player based on the player's
		/// current position (a vector of float values)
		/// </summary>
		/// <returns>A rectangle representing the bounds of the player</returns>
		private Rectangle GetPlayerRect()
		{
			return new Rectangle((int)playerPosition.X,(int)playerPosition.Y,playerTexture.Width,playerTexture.Height);
		}
		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
		
			GraphicsDevice.Clear(Color.Black);
			_spriteBatch.Begin();

			// Draw the player using a rectangle make from their position
			
				_spriteBatch.Draw(playerTexture, GetPlayerRect(), Color.White);
			// Draw each obstactle
			foreach (Rectangle rect in obstacleRects)
			{
				_spriteBatch.Draw(obstacleTexture, rect, Color.SeaGreen);
			}
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
