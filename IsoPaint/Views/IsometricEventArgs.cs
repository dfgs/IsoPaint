using IsoPaint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IsoPaint.Views
{
	public class IsometricEventArgs:EventArgs
	{
		public int X
		{
			get;
			private set;
		}
		public int Y
		{
			get;
			private set;
		}
		public int Z
		{
			get;
			private set;
		}
		public Faces Face
		{
			get;
			private set;
		}
		public IsometricEventArgs(int X,int Y,int Z,Faces Face)
		{
			this.X = X;this.Y = Y;this.Z = Z;this.Face = Face;
		}
	}
	public delegate void IsometricEventHandler(DependencyObject sender, IsometricEventArgs e);
}
