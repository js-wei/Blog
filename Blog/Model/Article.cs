using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Blog.Model
{
	public class Article 
	{
 		public  int  id{ get; set; }
		public  int  columnId{ get ;set;}
		public  int  author{ get ;set;}
		public  string  title{ get ;set;}
		public  string  keyword{ get ;set;}
		public  string  description{ get ;set;}
		public  string  banner{ get ;set;}
		public  string  image{ get ;set;}
		public  string  ico{ get ;set;}
		public  string  file{ get ;set;}
		public  string  content{ get ;set;}
		public  int  news{ get ;set;}
		public  int  hot{ get ;set;}
		public  int  head{ get ;set;}
		public  int  com{ get ;set;}
		public  int  hits{ get ;set;}
		public  int  sort{ get ;set;}
		public  int  status{ get ;set;}
		public  int  createTime{ get ;set;}
		public  int  updateTime{ get ;set;}
 
	}
}