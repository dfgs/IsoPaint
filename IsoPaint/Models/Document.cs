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


		public void ExportToPOV(string FileName)
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
			Writer.WriteLine("#declare VoxelSize = 1;");
			Writer.WriteLine($"#declare SizeX = {SizeX};");
			Writer.WriteLine($"#declare SizeY = {SizeY};");
			Writer.WriteLine($"#declare SizeZ = {SizeZ};");
			Writer.WriteLine($"#declare UpScale=sqrt(2)/sqrt(3);");
			Writer.WriteLine();
			Writer.WriteLine("camera {");
			Writer.WriteLine("	orthographic");
			Writer.WriteLine("	location <0,0,-1>*10000");
			Writer.WriteLine("	right x*SizeX*sqrt(2)");
			Writer.WriteLine("	up y*SizeX*sqrt(2)");
			Writer.WriteLine("	rotate x*30");
			Writer.WriteLine("	rotate y*225");
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
			Writer.WriteLine();
			Writer.WriteLine("#declare voxel =");
			Writer.WriteLine("box {");
			Writer.WriteLine("	<-VoxelSize/2,-VoxelSize/2*UpScale,-VoxelSize/2>, <VoxelSize/2,VoxelSize/2*sqrt(2)/sqrt(3),VoxelSize/2>");
			Writer.WriteLine("}");
			foreach (Voxel voxel in Voxels)
			{
				Writer.WriteLine();
				Writer.WriteLine("object {");
				Writer.WriteLine("	voxel");
				Writer.WriteLine($"	texture {{ Color{voxel.ColorID}}}");
				Writer.WriteLine($"	translate <{voxel.X},{voxel.Z}*UpScale,{voxel.Y}>");
				Writer.WriteLine("}");
			}
		}



	}
}
