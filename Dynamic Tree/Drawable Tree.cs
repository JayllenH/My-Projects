using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DynamicTreeStarter
{
	//Jayllen
	//IGME 106
	//Cutsom Trees
	class DrawableTree
	{
		// Constants for drawing
		private const float BranchAngle = 0.2f;
		private const float BranchLength = 25.0f;
		private const int BranchWidth = 2;

		// Fields for drawing
		private SpriteBatch sb;
		private Texture2D pixel;
		private Color treeColor;

		// The root of the tree
		protected TreeNode root;


		/// <summary>
		/// Sets up the drawable tree
		/// </summary>
		/// <param name="sb"></param>
		/// <param name="treeColor"></param>
		public DrawableTree(SpriteBatch sb, Color treeColor)
		{
			// Save params
			this.sb = sb;
			this.treeColor = treeColor;

			// No data yet
			this.root = null;

			// Dynamically create a 1x1 white pixel texture
			pixel = new Texture2D(sb.GraphicsDevice, 1, 1);
			pixel.SetData<Color>(new Color[] { Color.White });
		}

		/// <summary>
		/// Public facing Draw method - Draws the tree at
		/// the specified position
		/// </summary>
		/// <param name="position">Where to start drawing the tree</param>
		public void Draw(Vector2 position)
		{
			// Anything to draw?
			if (root == null)
				return;

			// Begin and end the spritebatch once and do
			// all the drawing between those calls
			sb.Begin();
			Draw(root, position, 0);
			sb.End();
		}


		/// <summary>
		/// Draws the lines from this node to its children (if they exist)
		/// </summary>
		/// <param name="node">The starting node</param>
		/// <param name="position">The position of this node on the screen</param>
		/// <param name="angle">The current angle of the line</param>
		private void Draw(TreeNode node, Vector2 position, float angle)
		{
			// Need to draw left?
			if (node.Left != null)
			{
				// Calculate the angle and position of the left node
				float leftAngle = angle - BranchAngle;
				Vector2 leftPos = position + Vector2.TransformNormal(Vector2.UnitY * -BranchLength, Matrix.CreateRotationZ(leftAngle));

				// Recursively draw
				DrawLine(position, leftPos);
				Draw(node.Left, leftPos, leftAngle);
			}

			// Need to draw right?
			if (node.Right != null)
			{
				// Calculate the angle and position of the right node
				float rightAngle = angle + BranchAngle;
				Vector2 rightPos = position + Vector2.TransformNormal(Vector2.UnitY * -BranchLength, Matrix.CreateRotationZ(rightAngle));

				// Recursively draw
				DrawLine(position, rightPos);
				Draw(node.Right, rightPos, rightAngle);
			}
		}


		/// <summary>
		/// Draws a line between two points by rotating a very
		/// thin rectangle, which is the same length as the distance
		/// between the two points
		/// </summary>
		/// <param name="p1">One end of the line</param>
		/// <param name="p2">The other end of the line</param>
		private void DrawLine(Vector2 p1, Vector2 p2)
		{
			// Get the "length" of the line
			int dist = (int)Vector2.Distance(p1, p2);

			// Draw the line as a rotated rectangle
			sb.Draw(
				pixel,
				new Rectangle((int)p1.X, (int)p1.Y, dist, BranchWidth),
				null,
				treeColor,
				(float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X),
				Vector2.Zero,
				SpriteEffects.None,
				0);
		}
	}
}