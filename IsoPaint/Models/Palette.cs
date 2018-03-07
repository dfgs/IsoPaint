using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsoPaint.Models
{
    public class Palette
    {
		
		public List<Color> Colors
		{
			get;
			set;
		}

		public Palette()
		{
			Colors = new List<Color>();
			Colors.Add(new Color());
		}

    }
}
