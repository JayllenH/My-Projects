using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DynamicTreeStarter
{
	//Jayllen
	//IGME 106
	//Cutsom Trees
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		// The three trees
		Tree treeRed;
		Tree treeGreen;
		Tree treeBlue;
		Random rng = new Random();

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Create the three trees
			treeRed = new Tree(_spriteBatch, Color.Red);
			treeGreen = new Tree(_spriteBatch, Color.Green);
			treeBlue = new Tree(_spriteBatch, Color.DodgerBlue);

			// *** FILL EACH TREE WITH DATA HERE ***************************
			
			for(int i=0; i < 150; i++) { treeRed.Insert(rng.Next(0,150)); } //loop with random values to make creation
			for (int i = 0; i < 50; i++) { treeBlue.Insert(rng.Next(0,250) + i*8); } //makes it go more to right
			for (int i = 0; i < 100; i++) { treeGreen.Insert(50 + i*5); } // makes the circle
			// Come up with values (random or otherwise) to match
			// the general shape of the trees shown in the PE document
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// *** After you have the rest of the assignment working: ******
			//  What happens if you insert one new piece of data into each
			//  of the trees each frame?  Try it out!

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// Draw the trees
			treeRed.Draw(new Vector2(200, 400));
			treeGreen.Draw(new Vector2(400, 400));
			treeBlue.Draw(new Vector2(600, 400));

			base.Draw(gameTime);
		}
	}
}
