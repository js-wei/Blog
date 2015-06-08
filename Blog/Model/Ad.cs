using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Blog.Model
{
	public class Ad 
	{
 		public  int  id{ get; set; }
		public  int  columnId{ get ;set;}
		public  string  title{ get ;set;}
		public  string  name{ get ;set;}
		public  string  savename{ get ;set;}
		public  int  size{ get ;set;}
		public  string  description{ get ;set;}
		public  string  url{ get ;set;}
		public  string  sort{ get ;set;}
		public  int  type{ get ;set;}
		public  int  status{ get ;set;}
		public  int  effective{ get ;set;}
		public  int  create_time{ get ;set;}
 
	}
}