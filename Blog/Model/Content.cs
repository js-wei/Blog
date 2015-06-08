using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Blog.Model
{
	public class Content 
	{
 		public  int  id{ get; set; }
		public  string  name{ get ;set;}
		public  string  path{ get ;set;}
		public  string  content{ get ;set;}
		public  int  timespan{ get ;set;}
		public  int  expire{ get ;set;}
 
	}
}