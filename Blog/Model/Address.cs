using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Blog.Model
{
	public class Address 
	{
 		public  int  id{ get; set; }
		public  int  mid{ get ;set;}
		public  string  name{ get ;set;}
		public  int  province{ get ;set;}
		public  int  city{ get ;set;}
		public  int  county{ get ;set;}
		public  string  address{ get ;set;}
		public  string  zipcode{ get ;set;}
		public  string  mobile{ get ;set;}
		public  string  phone{ get ;set;}
		public  int  isdefault{ get ;set;}
		public  int  contact{ get ;set;}
 
	}
}