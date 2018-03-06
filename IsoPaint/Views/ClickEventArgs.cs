using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IsoPaint.Views
{
	public class ClickEventArgs:EventArgs
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

		public ClickEventArgs(int X,int Y,int Z)
		{
			this.X = X;this.Y = Y;this.Z = Z;
		}
	}
	public delegate void ClickEventHandler(DependencyObject sender, ClickEventArgs e);
}
