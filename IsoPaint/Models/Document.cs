using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsoPaint.Models
{
	public class Document
	{
		public int SizeX
		{
			get;
			set;
		}
		public int SizeY
		{
			get;
			set;
		}
		public int SizeZ
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
			Voxels.Add(new Voxel() { X = 0 });
			Voxels.Add(new Voxel() { X = 1 });
		}

	}
}
