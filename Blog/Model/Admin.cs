using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Blog.Model
{
	public class Admin 
	{
 		public  int  id{ get; set; }
		public  string  username{ get ;set;}
		public  string  password{ get ;set;}
		public  string  tel{ get ;set;}
		public  string  email{ get ;set;}
		public  int  province{ get ;set;}
		public  int  city{ get ;set;}
		public  int  county{ get ;set;}
		public  string  address{ get ;set;}
		public  string  login_time{ get ;set;}
		public  string  login_ip{ get ;set;}
		public  int  remember{ get ;set;}
		public  int  status{ get ;set;}
 
	}
}