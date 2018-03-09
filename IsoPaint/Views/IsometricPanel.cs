using IsoPaint.Models;
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
		public event IsometricEventHandler Hover;
		public event IsometricEventHandler Click;

		public static readonly DependencyProperty XProperty = DependencyProperty.RegisterAttached("X", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange ));
		public static readonly DependencyProperty YProperty = DependencyProperty.RegisterAttached("Y", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange ));
		public static readonly DependencyProperty ZProperty = DependencyProperty.RegisterAttached("Z", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentArrange ));
		public static readonly DependencyProperty CenterXProperty = DependencyProperty.RegisterAttached("CenterX", typeof(float), typeof(IsometricPanel), new FrameworkPropertyMetadata(0.5f, FrameworkPropertyMetadataOptions.AffectsParentArrange));
		public static readonly DependencyProperty CenterYProperty = DependencyProperty.RegisterAttached("CenterY", typeof(float), typeof(IsometricPanel), new FrameworkPropertyMetadata(0.5f, FrameworkPropertyMetadataOptions.AffectsParentArrange));
		public static readonly DependencyProperty FaceProperty = DependencyProperty.RegisterAttached("Face", typeof(Faces), typeof(IsometricPanel));
		public static readonly DependencyProperty XSpanProperty = DependencyProperty.RegisterAttached("XSpan", typeof(int), typeof(IsometricPanel),new FrameworkPropertyMetadata(1,FrameworkPropertyMetadataOptions.AffectsParentMeasure));
		public static readonly DependencyProperty YSpanProperty = DependencyProperty.RegisterAttached("YSpan", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsParentMeasure));
		public static readonly DependencyProperty ZSpanProperty = DependencyProperty.RegisterAttached("ZSpan", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsParentMeasure));

		//public event ClickEventHandler Click;

		public static readonly DependencyProperty SizeXProperty = DependencyProperty.Register("SizeX", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));
		public int SizeX
		{
			get { return (int)GetValue(SizeXProperty); }
			set { SetValue(SizeXProperty, value); }
		}

		public static readonly DependencyProperty SizeYProperty = DependencyProperty.Register("SizeY", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));
		public int SizeY
		{
			get { return (int)GetValue(SizeYProperty); }
			set { SetValue(SizeYProperty, value); }
		}
		
		public static readonly DependencyProperty SizeZProperty = DependencyProperty.Register("SizeZ", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));
		public int SizeZ
		{
			get { return (int)GetValue(SizeZProperty); }
			set { SetValue(SizeZProperty, value); }
		}



		public static readonly DependencyProperty GridSizeProperty = DependencyProperty.Register("GridSize", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(32, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));
		public int GridSize
		{
			get { return (int)GetValue(GridSizeProperty); }
			set { SetValue(GridSizeProperty, value); }
		}



		public static readonly DependencyProperty CeilingProperty = DependencyProperty.Register("Ceiling", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));
		public int Ceiling
		{
			get { return (int)GetValue(CeilingProperty); }
			set { SetValue(CeilingProperty, value); }
		}
		public static readonly DependencyProperty FloorProperty = DependencyProperty.Register("Floor", typeof(int), typeof(IsometricPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));
		public int Floor
		{
			get { return (int)GetValue(FloorProperty); }
			set { SetValue(FloorProperty, value); }
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

		/*[Category("Appearance")]
		public static readonly DependencyProperty CeilingGridLineBrushProperty = DependencyProperty.Register("CeilingGridLineBrush", typeof(Brush), typeof(IsometricPanel), new FrameworkPropertyMetadata(Brushes.IndianRed, FrameworkPropertyMetadataOptions.AffectsRender));
		public Brush CeilingGridLineBrush
		{
			get { return (Brush)GetValue(CeilingGridLineBrushProperty); }
			set { SetValue(CeilingGridLineBrushProperty, value); }
		}*/


		[Category("Appearance")]
		public static readonly DependencyProperty DrawGridProperty = DependencyProperty.Register("DrawGrid", typeof(bool), typeof(IsometricPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
		public bool DrawGrid
		{
			get { return (bool)GetValue(DrawGridProperty); }
			set { SetValue(DrawGridProperty, value); }
		}






		#region X,Y,Z
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
		#endregion


		#region XSpan,YSpan,ZSpan
		public static int GetXSpan(UIElement Element)
		{
			return (int)Element.GetValue(XSpanProperty);
		}
		public static void SetXSpan(UIElement Element, int Value)
		{
			Element.SetValue(XSpanProperty, Value);
		}
		public static int GetYSpan(UIElement Element)
		{
			return (int)Element.GetValue(YSpanProperty);
		}
		public static void SetYSpan(UIElement Element, int Value)
		{
			Element.SetValue(YSpanProperty, Value);
		}
		public static int GetZSpan(UIElement Element)
		{
			return (int)Element.GetValue(ZSpanProperty);
		}
		public static void SetZSpan(UIElement Element, int Value)
		{
			Element.SetValue(ZSpanProperty, Value);
		}
		#endregion


		#region CenterX,CenterY,Face
		public static float GetCenterX(UIElement Element)
		{
			return (float)Element.GetValue(CenterXProperty);
		}
		public static void SetCenterX(UIElement Element, float Value)
		{
			Element.SetValue(CenterXProperty, Value);
		}
		public static float GetCenterY(UIElement Element)
		{
			return (float)Element.GetValue(CenterYProperty);
		}
		public static void SetCenterY(UIElement Element, float Value)
		{
			Element.SetValue(CenterYProperty, Value);
		}
		public static Faces GetFace(UIElement Element)
		{
			return (Faces)Element.GetValue(FaceProperty);
		}
		public static void SetFace(UIElement Element, Faces Value)
		{
			Element.SetValue(FaceProperty, Value);
		}
		#endregion


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
			X = (int)Math.Floor((2 * Position.Y - Position.X) / GridSize + Z - SizeZ + SizeX * 0.5f); //SizeX
			Y = (int)Math.Floor((2 * Position.Y + Position.X) / GridSize + Z - SizeZ - SizeX * 0.5f) ;
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
			float CenterX, CenterY;
			Point pt;

			X = GetX(Element);
			Y = GetY(Element);
			Z = GetZ(Element);
			CenterX = GetCenterX(Element);
			CenterY = GetCenterY(Element);
			pt = GetIsometricCoordinates(X, Y, Z);
			pt.Offset(-CenterX*Element.DesiredSize.Width,-CenterY * Element.DesiredSize.Height);
			return pt;
		}
		private void Pick(MouseEventArgs e, out int x,out int y,out int z,out Faces Face)
		{
			Point pt;

			pt = e.GetPosition(this);
			if (e.OriginalSource == this)
			{
				z = Math.Max(0, Floor); Face = Faces.Bottom;
				UnProject(pt, out x, out y, z);
				if (x < 0)
				{
					x = 0; Face = Faces.Back;
					UnProject(pt, x, out y, out z);
				}
				if (y < 0)
				{
					y = 0; Face = Faces.Left;
					UnProject(pt, out x, y, out z);
				}
			}
			else
			{
				x = IsometricPanel.GetX((UIElement)e.Source);
				y = IsometricPanel.GetY((UIElement)e.Source);
				z = IsometricPanel.GetZ((UIElement)e.Source);
				Face = IsometricPanel.GetFace((UIElement)e.OriginalSource);
			}
		}
		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			int x, y, z;
			Faces face;

			base.OnMouseMove(e);

			Pick(e, out x, out y, out z, out face);
			if (Click != null) Click(this, new IsometricEventArgs(x, y, z, face));
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			int x, y, z;
			Faces face;

			base.OnMouseMove(e);

			Pick(e, out x, out y, out z, out face);
			if (Hover != null) Hover(this, new IsometricEventArgs(x, y, z, face));
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			Rect finalRect;
			Size elementSize;
			int spanX, spanY, spanZ;

			foreach (UIElement element in Children)
			{
				spanX = GetXSpan(element); spanY = GetYSpan(element); spanZ = GetZSpan(element);
				elementSize = GetSize(spanX, spanY, spanZ);
				finalRect = new Rect(GetIsometricCoordinates(element), elementSize);
				element.Arrange(finalRect);
			}

			return DesiredSize;
		}
		protected override Size MeasureOverride(Size availableSize)
		{
			Point pt;
			Size elementSize;
			int spanX, spanY,spanZ;

			foreach (UIElement element in Children)
			{
				spanX = GetXSpan(element);spanY = GetYSpan(element);spanZ = GetZSpan(element);
				elementSize = GetSize(spanX, spanY, spanZ);
				element.Measure(elementSize);
			}

			pt = GetIsometricCoordinates(SizeX, SizeY, SizeZ);
			return GetSize(SizeX, SizeY, SizeZ);
		}
	

		

		protected override void OnRender(DrawingContext dc)
		{
			base.OnRender(dc);
			Pen primaryPen,secondaryPen;
			//Point A, B, C, D;
			int floor, ceiling;

			if (!DrawGrid) return;

			floor = Math.Max(0, Floor);
			ceiling = Math.Max(Floor, Ceiling);

			primaryPen = new Pen(PrimaryGridLineBrush, 1);
			secondaryPen = new Pen(SecondaryGridLineBrush, 1);
			//ceilingPen = new Pen(ceilingGridLineBrush, 1);

			dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, 0), GetIsometricCoordinates(SizeX, 0, 0));
			dc.DrawLine(primaryPen, GetIsometricCoordinates(SizeX, 0, 0), GetIsometricCoordinates(SizeX, SizeY, 0));
			dc.DrawLine(primaryPen, GetIsometricCoordinates(SizeX, SizeY, 0), GetIsometricCoordinates(0, SizeY, 0));
			dc.DrawLine(primaryPen, GetIsometricCoordinates(0, SizeY, 0), GetIsometricCoordinates(0, 0, 0));

			if (floor!=0)
			{
				dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, floor), GetIsometricCoordinates(SizeX, 0, floor));
				dc.DrawLine(primaryPen, GetIsometricCoordinates(SizeX, 0, floor), GetIsometricCoordinates(SizeX, SizeY, floor));
				dc.DrawLine(primaryPen, GetIsometricCoordinates(SizeX, SizeY, floor), GetIsometricCoordinates(0, SizeY, floor));
				dc.DrawLine(primaryPen, GetIsometricCoordinates(0, SizeY, floor), GetIsometricCoordinates(0, 0, floor));
			}

			for (int x = 1; x < SizeX; x++)
			{
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(x, 0, floor), GetIsometricCoordinates(x, SizeY, floor));
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(x, 0, floor), GetIsometricCoordinates(x, 0, ceiling));
				//dc.DrawLine(ceilingPen, GetIsometricCoordinates(x, 0, ceiling), GetIsometricCoordinates(x, 0, SizeZ));
			}
			for (int y = 1; y < SizeY; y++)
			{
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(0, y, floor), GetIsometricCoordinates(SizeX, y, floor));
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(0, y, floor), GetIsometricCoordinates(0, y, ceiling));
				//dc.DrawLine(ceilingPen, GetIsometricCoordinates(0, y,ceiling), GetIsometricCoordinates(0, y, SizeZ));
			}
			for (int z = floor+1; z < ceiling; z++)
			{
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(0, 0, z), GetIsometricCoordinates(SizeX, 0, z));
				dc.DrawLine(secondaryPen, GetIsometricCoordinates(0, 0, z), GetIsometricCoordinates(0, SizeY, z));
			}//*/
			/*for (int z = ceiling; z < SizeZ; z++)
			{
				dc.DrawLine(ceilingPen, GetIsometricCoordinates(0, 0, z), GetIsometricCoordinates(SizeX, 0, z));
				dc.DrawLine(ceilingPen, GetIsometricCoordinates(0, 0, z), GetIsometricCoordinates(0, SizeY, z));
			}//*/


			dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, floor), GetIsometricCoordinates(0, 0, SizeZ));
			dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, SizeZ), GetIsometricCoordinates(SizeX, 0, SizeZ));
			dc.DrawLine(primaryPen, GetIsometricCoordinates(SizeX, 0, SizeZ), GetIsometricCoordinates(SizeX, 0, 0));

			dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, SizeZ), GetIsometricCoordinates(0, SizeY, SizeZ));
			dc.DrawLine(primaryPen,  GetIsometricCoordinates(0, SizeY, SizeZ), GetIsometricCoordinates(0, SizeY, 0));

			if ((ceiling > 0) && (ceiling < SizeZ))
			{
				dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, ceiling), GetIsometricCoordinates(SizeX, 0, ceiling));
				dc.DrawLine(primaryPen, GetIsometricCoordinates(0, 0, ceiling), GetIsometricCoordinates(0, SizeY, ceiling));
			}

		}


	}
}
