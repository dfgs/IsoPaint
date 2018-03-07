using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IsoPaint.Models
{
    public class Color
    {
		[XmlAttribute]
		public byte A
		{
			get;
			set;
		}
		[XmlAttribute]
		public byte R
		{
			get;
			set;
		}
		[XmlAttribute]
		public byte G
		{
			get;
			set;
		}
		[XmlAttribute]
		public byte B
		{
			get;
			set;
		}

		public Color()
		{
			A = 255;
		}
		public Color(byte A, byte R, byte G, byte B)
		{
			this.A = A;this.R = R;this.G = G;this.B = B;
		}

		
    }
}
