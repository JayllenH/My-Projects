using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DynamicTreeStarter
{
	//Jayllen
	//IGME 106
	//Cutsom Trees
	/// <summary>
	/// Represents a tree-centric data structure
	/// that can have data dynamically inserted, 
	/// and can be drawn as a literal "tree" on the screen
	/// </summary>
	class Tree : DrawableTree
	{
		// Already has an inherited root node field called "root"

		/// <summary>
		/// Creates a tree that can be drawn
		/// </summary>
		/// <param name="sb">The sprite batch used to draw</param>
		/// <param name="treeColor">The color of this tree</param>
		public Tree(SpriteBatch sb, Color treeColor)
			: base(sb, treeColor)
		{ }

		/// <summary>
		/// Public facing Insert method
		/// </summary>
		/// <param name="data">The data to insert</param>
		public void Insert(int data)
		{
			// *** Fill in this method ****************************************
			if (root != null) //check is it has aa value
				Insert(data, root);
			else
				root = new TreeNode(data); //null so we create it

  
			// Remember that this is the PUBLIC version, which
			// starts the insert process.  This method is the only
			// one that is concerned with the root.

			// For your reference, you should use the TreeNode field 
			// called "root" (which is defined in the parent class)
		}

		/// <summary>
		/// Private recursive insert method
		/// </summary>
		/// <param name="data">The data to insert</param>
		/// <param name="node">The node to attempt to insert into</param>
		private void Insert(int data, TreeNode node)
		{
			// *** Fill in this method ****************************************
			if (data < node.Data) //compares the data and picks a side
			{
				if (node.Left != null)
					Insert(data, node.Left); //adds it
				else
					node.Left = new TreeNode(data); //if its null it gets value
			}
			else
				if (data > node.Data)
				{
					if (node.Right != null)
						Insert(data, node.Right); //adds ts
					else
						node.Right = new TreeNode(data); //if its null gives value
				}
			
			// Remember that this is the PRIVATE version which operates on the
			// node provided.  This method doesn't care if the node is the root.
		}

	}
}

