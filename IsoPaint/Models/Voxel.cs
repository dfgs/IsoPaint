using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IsoPaint.Models
{
	public class Voxel
	{
		[XmlAttribute]
		public int X
		{
			get;
			set;
		}
		[XmlAttribute]
		public int Y
		{
			get;
			set;
		}
		[XmlAttribute]
		public int Z
		{
			get;
			set;
		}
		[XmlAttribute]
		public int ColorID
		{
			get;
			set;
		}

	}
}
