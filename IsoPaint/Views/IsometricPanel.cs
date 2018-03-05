using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IsoPaint.Views
{
	public class IsometricPanel : Panel
	{
		public static readonly DependencyProperty XProperty = DependencyProperty.RegisterAttached("X", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange ));
		public static readonly DependencyProperty YProperty = DependencyProperty.RegisterAttached("Y", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange ));
		public static readonly DependencyProperty ZProperty = DependencyProperty.RegisterAttached("Z", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange ));



		public static readonly DependencyProperty SizeXProperty = DependencyProperty.Register("SizeX", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
		public int SizeX
		{
			get { return (int)GetValue(SizeXProperty); }
			set { SetValue(SizeXProperty, value); }
		}

		public static readonly DependencyProperty SizeYProperty = DependencyProperty.Register("SizeY", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
		public int SizeY
		{
			get { return (int)GetValue(SizeYProperty); }
			set { SetValue(SizeYProperty, value); }
		}
		
		public static readonly DependencyProperty SizeZProperty = DependencyProperty.Register("SizeZ", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
		public int SizeZ
		{
			get { return (int)GetValue(SizeZProperty); }
			set { SetValue(SizeZProperty, value); }
		}



		public static readonly DependencyProperty GridSizeProperty = DependencyProperty.Register("GridSize", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(32, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
		public int GridSize
		{
			get { return (int)GetValue(GridSizeProperty); }
			set { SetValue(GridSizeProperty, value); }
		}

		public static int GetX(UIElement Element)
		{
			return (int)Element.GetValue(XProperty);
		}
		public static void SetX(UIElement Element,int Value)
		{
			Element.SetValue(XProperty, Value);
		}
		public static int GetY(UIElement Element)
		{
			return (int)Element.GetValue(YProperty);
		}
		public static void SetY(UIElement Element, int Value)
		{
			Element.SetValue(YProperty, Value);
		}
		public static int GetZ(UIElement Element)
		{
			return (int)Element.GetValue(ZProperty);
		}
		public static void SetZ(UIElement Element, int Value)
		{
			Element.SetValue(ZProperty, Value);
		}

		public Size GetSize(int SizeX,int SizeY,int SizeZ)
		{

			return new Size((SizeX+SizeY) * GridSize*0.5, (SizeY+SizeX )* GridSize * 0.25f + (SizeZ)*GridSize*0.5f );
		}

		public Point GetIsometricCoordinates(int X, int Y, int Z)
		{
			double x, y;

			x = (Y + X) * GridSize * 0.5f;
			y = (Y - X) * GridSize * 0.25f - Z * GridSize * 0.5f    + ((SizeX-1) * GridSize * 0.25f) +(SizeZ-1) * GridSize * 0.5f;
			
			return new Point(x, y);
		}
		public Point GetIsometricCoordinates(UIElement Element)
		{
			int X, Y, Z;

			X = GetX(Element);
			Y = GetY(Element);
			Z = GetZ(Element);
			return GetIsometricCoordinates(X, Y, Z);
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			Point pt;
			Size elementSize;

			elementSize = new Size(GridSize, GridSize);
			foreach (UIElement element in Children)
			{
				element.Measure(elementSize);
			}

			pt = GetIsometricCoordinates(SizeX, SizeY, SizeZ);
			return GetSize(SizeX, SizeY, SizeZ);
		}
		protected override Size ArrangeOverride(Size finalSize)
		{
			Rect finalRect;
			Size elementSize;

			elementSize = new Size(GridSize, GridSize);
			foreach(UIElement element in Children)
			{
				finalRect = new Rect(GetIsometricCoordinates(element), elementSize);
				element.Arrange(finalRect);
			}

			return DesiredSize;
		}





		/*protected override void OnRender(DrawingContext dc)
		{
			base.OnRender(dc);
			Point pt;
			Pen pen;

			pen = new Pen(Brushes.Blue, 1);
			dc.DrawLine(pen, GetIsometricCoordinates(0, 0, 0), GetIsometricCoordinates(7, 0, 0));
			dc.DrawLine(pen, GetIsometricCoordinates(7, 0, 0), GetIsometricCoordinates(7, 7, 0));
			dc.DrawLine(pen, GetIsometricCoordinates(7, 7, 0), GetIsometricCoordinates(0, 7, 0));
			dc.DrawLine(pen, GetIsometricCoordinates(0, 7, 0), GetIsometricCoordinates(0, 0, 0));
			

			for (int z = 0; z < 1; z++)
			{
				for (int y = 0; y < 8; y++)
				{
					for (int x = 0; x < 8; x++)
					{
						pt = GetIsometricCoordinates(x, y, z);
						dc.DrawEllipse(Brushes.Red, null, pt, 3, 3);
					}
				}

			}
		}*/


	}
}
