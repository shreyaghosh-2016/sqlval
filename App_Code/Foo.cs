using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using RedirectIO;

namespace Testing
{
    class Foo
    {
		

        public Dictionary<string, Dictionary<string, List<string>>> dictionary =
    new Dictionary<string, Dictionary<string, List<string>>>();
        public Dictionary<string, List<string>> inner = new Dictionary<string, List<string>>();

        public List<string> list;
        public Dictionary<string, List<string>> this[string key]
        {
            
            get
            {
				
                
				if (!dictionary.TryGetValue(key, out inner))
                {
                    inner = new Dictionary<string, List<string>>();

			//		using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				//	Console.WriteLine("in dict" + inner+"---"+key);
					

                    dictionary[key] = inner;
                }
                else
                {
					using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
					Console.WriteLine("ERROR : COLUMN " + key + "already exists");
                }
                return inner;
            }
        }

        public List<string> Mydata(string key)
        {
			
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine("in dict2"+key);

            if (!inner.TryGetValue(key, out list))
            {
                list = new List<string>();
                inner.Add(key, list);
                /*foreach(var w in inner.Keys)
                        Console.WriteLine(w);
                */
                //inner[key] = list;
            }
            return list;

        }

        //print 
       
       




    }
}
