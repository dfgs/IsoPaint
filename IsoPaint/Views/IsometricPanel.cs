using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace IsoPaint.Views
{

	public class IsometricPanel : Panel
	{
		public static readonly DependencyProperty XProperty = DependencyProperty.RegisterAttached("X", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange ));
		public static readonly DependencyProperty YProperty = DependencyProperty.RegisterAttached("Y", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange ));
		public static readonly DependencyProperty ZProperty = DependencyProperty.RegisterAttached("Z", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange ));
		public static readonly DependencyProperty CenterXProperty = DependencyProperty.RegisterAttached("CenterX", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange));
		public static readonly DependencyProperty CenterYProperty = DependencyProperty.RegisterAttached("CenterY", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange));

		//public event ClickEventHandler Click;

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


		[Category("Appearance")]
		public static readonly DependencyProperty PrimaryGridLineBrushProperty = DependencyProperty.Register("PrimaryGridLineBrush", typeof(Brush), typeof(IsometricPanel), new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.AffectsRender));
		public Brush PrimaryGridLineBrush
		{
			get { return (Brush)GetValue(PrimaryGridLineBrushProperty); }
			set { SetValue(PrimaryGridLineBrushProperty, value); }
		}

		[Category("Appearance")]
		public static readonly DependencyProperty SecondaryGridLineBrushProperty = DependencyProperty.Register("SecondaryGridLineBrush", typeof(Brush), typeof(IsometricPanel), new FrameworkPropertyMetadata(Brushes.DarkGray, FrameworkPropertyMetadataOptions.AffectsRender));
		public Brush SecondaryGridLineBrush
		{
			get { return (Brush)GetValue(SecondaryGridLineBrushProperty); }
			set { SetValue(SecondaryGridLineBrushProperty, value); }
		}


		[Category("Appearance")]
		public static readonly DependencyProperty DrawGridProperty = DependencyProperty.Register("DrawGrid", typeof(bool), typeof(IsometricPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
		public bool DrawGrid
		{
			get { return (bool)GetValue(DrawGridProperty); }
			set { SetValue(DrawGridProperty, value); }
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
		public static int GetCenterX(UIElement Element)
		{
			return (int)Element.GetValue(CenterXProperty);
		}
		public static void SetCenterX(UIElement Element, int Value)
		{
			Element.SetValue(CenterXProperty, Value);
		}
		public static int GetCenterY(UIElement Element)
		{
			return (int)Element.GetValue(CenterYProperty);
		}
		public static void SetCenterY(UIElement Element, int Value)
		{
			Element.SetValue(CenterYProperty, Value);
		}

		

		public IsometricPanel()
		{
			ClipToBounds = true;
		}

		public Size GetSize(int SizeX,int SizeY,int SizeZ)
		{
			return new Size((SizeX+SizeY) * GridSize*0.5, (SizeY+SizeX )* GridSize * 0.25f + (SizeZ)*GridSize*0.5f );
		}

		
		public void UnProject(Point Position, out int X, out int Y, int Z)
		{
			Y = (int)Math.Floor((2 * Position.Y + Position.X) / GridSize + Z - SizeZ - SizeX * 0.5f) ;
			X = (int)Math.Floor((2 * Position.Y - Position.X) / GridSize + Z - SizeZ + SizeX * 0.5f);
		}
		public void UnProject(Point Position, int X, out int Y, out int Z)
		{
			Y = (int)Math.Floor(2 * Position.X / GridSize + X - SizeX);
			Z = (int)Math.Floor((Position.X - 2 * Position.Y) / GridSize + X + SizeZ - SizeX * 0.5f);
		}
		public void UnProject(Point Position, out int X, int Y, out int Z)
		{
			X = (int)Math.Floor(-2 * Position.X / GridSize + Y + SizeX);
			Z = (int)Math.Floor( (-2 * Position.Y - Position.X) / GridSize + Y + SizeZ + SizeX * 0.5f );
		}


		public Point GetIsometricCoordinates(int X, int Y, int Z)
		{
			double x, y;

			x = (Y + SizeX-X) * GridSize * 0.5f;
			y = (Y +X) * GridSize * 0.25f - (Z-SizeZ) * GridSize * 0.5f    ;
			
			return new Point(x, y);
		}
		public Point GetIsometricCoordinates(UIElement Element)
		{
			int X, Y, Z;
			int CenterX, CenterY;
			Point pt;

			X = GetX(Element);
			Y = GetY(Element);
			Z = GetZ(Element);
			CenterX = GetCenterX(Element);
			CenterY = GetCenterY(Element);
			pt = GetIsometricCoordinates(X, Y, Z);
			pt.Offset(-CenterX,-CenterY);
			return pt;
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

		

		protected override void OnRender(DrawingContext dc)
		{
			base.OnRender(dc);
			Pen primaryPen,secondaryPen;
			//Point A, B, C, D;

			if (!DrawGrid) return;

			primaryPen = new Pen(PrimaryGridLineBrush, 1);
			secondaryPen = new Pen(SecondaryGridLineBrush, 1);

			dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, 0), GetIsometricCoordinates(SizeX, 0, 0));
			dc.DrawLine(primaryPen, GetIsometricCoordinates(SizeX, 0, 0), GetIsometricCoordinates(SizeX, SizeY, 0));
			dc.DrawLine(primaryPen, GetIsometricCoordinates(SizeX, SizeY, 0), GetIsometricCoordinates(0, SizeY, 0));
			dc.DrawLine(primaryPen, GetIsometricCoordinates(0, SizeY, 0), GetIsometricCoordinates(0, 0, 0));

			for (int x = 1; x < SizeX; x++)
			{
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(x, 0, 0), GetIsometricCoordinates(x, SizeY, 0));
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(x, 0, 0), GetIsometricCoordinates(x, 0, SizeZ));
			}
			for (int y = 1; y < SizeY; y++)
			{
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(0, y, 0), GetIsometricCoordinates(SizeX, y, 0));
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(0, y, 0), GetIsometricCoordinates(0, y, SizeZ));
			}
			for (int z = 1; z < SizeZ; z++)
			{
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(0, 0, z), GetIsometricCoordinates(SizeX, 0, z));
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(0, 0, z), GetIsometricCoordinates(0, SizeY, z));
			}//*/


			dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, 0), GetIsometricCoordinates(0, 0, SizeZ));
			dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, SizeZ), GetIsometricCoordinates(SizeX, 0, SizeZ));
			dc.DrawLine(primaryPen, GetIsometricCoordinates(SizeX, 0, SizeZ), GetIsometricCoordinates(SizeX, 0, 0));

			dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, SizeZ), GetIsometricCoordinates(0, SizeY, SizeZ));
			dc.DrawLine(primaryPen,  GetIsometricCoordinates(0, SizeY, SizeZ), GetIsometricCoordinates(0, SizeY, 0));

			/*if ((HoverX >= 0) && (HoverY >= 0) && (HoverZ >= 0) && (HoverX < SizeX) && (HoverY < SizeY) && (HoverZ < SizeZ))
			{
				switch(hoveredFace)
				{
					case HoverFaces.Bottom:
						A = GetIsometricCoordinates(HoverX, HoverY, HoverZ);
						B = GetIsometricCoordinates(HoverX + 1, HoverY, HoverZ);
						C = GetIsometricCoordinates(HoverX + 1, HoverY + 1, HoverZ);
						D = GetIsometricCoordinates(HoverX, HoverY + 1, HoverZ);
						break;
					case HoverFaces.Top:
						A = GetIsometricCoordinates(HoverX, HoverY, HoverZ+1);
						B = GetIsometricCoordinates(HoverX + 1, HoverY, HoverZ + 1);
						C = GetIsometricCoordinates(HoverX + 1, HoverY + 1, HoverZ + 1);
						D = GetIsometricCoordinates(HoverX, HoverY + 1, HoverZ + 1);
						break;
					case HoverFaces.Left:
						A = GetIsometricCoordinates(HoverX, HoverY, HoverZ);
						B = GetIsometricCoordinates(HoverX + 1, HoverY, HoverZ);
						C = GetIsometricCoordinates(HoverX + 1, HoverY, HoverZ + 1);
						D = GetIsometricCoordinates(HoverX, HoverY, HoverZ + 1);
						break;
					case HoverFaces.Right:
						A = GetIsometricCoordinates(HoverX, HoverY+1, HoverZ);
						B = GetIsometricCoordinates(HoverX + 1, HoverY+1, HoverZ);
						C = GetIsometricCoordinates(HoverX + 1, HoverY+1, HoverZ + 1);
						D = GetIsometricCoordinates(HoverX, HoverY+1, HoverZ + 1);
						break;
					default:
						A = GetIsometricCoordinates(HoverX, HoverY, HoverZ);
						B = GetIsometricCoordinates(HoverX , HoverY + 1, HoverZ);
						C = GetIsometricCoordinates(HoverX , HoverY + 1, HoverZ + 1);
						D = GetIsometricCoordinates(HoverX, HoverY , HoverZ + 1);
						break;
				}

				dc.DrawLine(hoverPen, A, B);
				dc.DrawLine(hoverPen, B, C);
				dc.DrawLine(hoverPen, C, D);
				dc.DrawLine(hoverPen, D, A);
			}*/

		}


	}
}
