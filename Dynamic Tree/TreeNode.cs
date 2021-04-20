using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicTreeStarter
{
	//Jayllen
	//IGME 106
	//Cutsom Trees
	/// <summary>
	/// Holds a single piece of data in a tree,
	/// along with up to two child nodes
	/// </summary>
	class TreeNode
	{
		// Data for a single node
		private int data;
		private TreeNode left;
		private TreeNode right;

		/// <summary>
		/// Gets or sets the data of this node
		/// </summary>
		public int Data { get { return data; } set { data = value; } }

		/// <summary>
		/// Gets or sets this node's left child
		/// </summary>
		public TreeNode Left { get { return left; } set { left = value; } }

		/// <summary>
		/// Gets or sets this node's right child
		/// </summary>
		public TreeNode Right { get { return right; } set { right = value; } }

		/// <summary>
		/// Creates a node with the specified data
		/// </summary>
		/// <param name="data">The data to store in this node</param>
		public TreeNode(int data)
		{
			this.data = data;
			this.left = null;
			this.right = null;
		}
	}
}
