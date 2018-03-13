using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IsoPaint.Models
{
	public class Document
	{
		[XmlAttribute]
		public int SizeX
		{
			get;
			set;
		}
		[XmlAttribute]
		public int SizeY
		{
			get;
			set;
		}
		[XmlAttribute]
		public int SizeZ
		{
			get;
			set;
		}

		public Palette Palette
		{
			get;
			set;
		}

		public List<Voxel> Voxels
		{
			get;
			set;
		}
		

		public Document()
		{
			Voxels = new List<Voxel>();
			Palette = new Palette();
			//Voxels.Add(new Voxel() { X = 0 });
			//Voxels.Add(new Voxel() { X = 1 });
		}

		public void SaveToFile(string FileName)
		{
			XmlSerializer serializer;
			using (FileStream stream = new FileStream(FileName, FileMode.Create))
			{
				serializer = new XmlSerializer(typeof(Document));
				serializer.Serialize(stream, this);
			}
		}

		public static Document LoadFromFile(string FileName)
		{
			XmlSerializer serializer;

			using (FileStream stream = new FileStream(FileName, FileMode.Open))
			{
				serializer = new XmlSerializer(typeof(Document));
				return (Document)serializer.Deserialize(stream);
			}
		}


		public void ExportToPOC(string FileName)
		{
			StreamWriter writer;
			
			using (FileStream stream = new FileStream(FileName, FileMode.Create))
			{
				writer = new StreamWriter(stream);
				ExportCameraToPOV(writer);
				ExportLightToPOV(writer);
				ExportPaletteToPOV(writer);
				ExportVoxelsToPOV(writer);
				writer.Flush();
			}
		}
		private void ExportCameraToPOV(StreamWriter Writer)
		{
			Writer.WriteLine("#include \"colors.inc\"");

			Writer.WriteLine();
			Writer.WriteLine("camera {");
			Writer.WriteLine("	orthographic angle 50");
			Writer.WriteLine("	location <1,1,1> *5");
			Writer.WriteLine("	look_at <0,0,0>");
			Writer.WriteLine("	right x* image_width/ image_height");
			Writer.WriteLine("}");
		}
		private void ExportLightToPOV(StreamWriter Writer)
		{
			Writer.WriteLine();
			Writer.WriteLine("light_source {");
			Writer.WriteLine("	<1,0.5,0>");
			Writer.WriteLine("	color rgb <1,1,1>");
			Writer.WriteLine("	parallel");
			Writer.WriteLine("	point_at <0,0,0>");
			Writer.WriteLine("}");
		}


		private void ExportPaletteToPOV(StreamWriter Writer)
		{
			foreach (Color color in Palette.Colors)
			{
				Writer.WriteLine();
				Writer.WriteLine($"#declare Color{color.ID}=");
				Writer.WriteLine("texture {");
				Writer.WriteLine($"	pigment {{ rgbf{color.ToPOVColor()} }}");
				Writer.WriteLine("	finish { phong 1}");
				Writer.WriteLine("}");
			}
		}

		private void ExportVoxelsToPOV(StreamWriter Writer)
		{
			foreach (Voxel voxel in Voxels)
			{
				Writer.WriteLine();
				Writer.WriteLine("box {");
				Writer.WriteLine("	<-0.5,-0.5,-0.5>, <0.5,0.5,0.5>");
				Writer.WriteLine($"	texture {{ Color{voxel.ColorID}}}");
				Writer.WriteLine($"	translate <{voxel.X},{voxel.Z},{voxel.Y}>");
				Writer.WriteLine("}");
			}
		}



	}
}
