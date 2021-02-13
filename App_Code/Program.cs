using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Misc;
using System.Threading.Tasks;

using System.IO;

using RedirectIO;

//wrap each console.writeline by the following 
            /*
			 using ( new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
			 {
			 	
			 }
			*/

namespace Testing
{
    public class Program
    {
		
		
        static public string Main()
        {
			
				using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				Console.WriteLine("Entering in the Validation Process ");

				StreamReader inputStream = null;
				
				
				try
				{
					string path = System.Web.HttpContext.Current.Server.MapPath(@"~/temp.txt");
					
					
					inputStream = new StreamReader(path);
					AntlrInputStream input = new AntlrInputStream(inputStream);
					TestingLexer lexer = new TestingLexer(input);
					CommonTokenStream tokens = new CommonTokenStream(lexer);
					TestingParser parser = new TestingParser(tokens);
					TestingParser.GramContext sContext = parser.gram();
					//TestingParser.InitContext iContext = parser.init();
					TestVisitor visitor = new TestVisitor();
					
					visitor.VisitGram(sContext);
					//visitor.VisitInit(iContext);*/
					
										
					
                 
				}
				catch (Exception e)
				{
					using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
					{
						Console.WriteLine("Error--->>" + e);
						Console.WriteLine("Cannot open Redirect.txt for writing");
						Console.WriteLine(e.Message);
					}
					return "Failed! Message:\n" + e.Message;
				}
				finally
				{
					using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
					Console.WriteLine(" ");
					using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
					Console.WriteLine("Finished");
					using (new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )

					Console.WriteLine(" Thank You for using SQLVAL");

					inputStream.Close();
					
				}
            return "Success!";
            //Console.ReadKey();
        }
    }

}
