﻿using System;
using System.Collections.Generic;
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

	}
}
