using Antlr4.Runtime;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Misc;
using System.Threading.Tasks;

using RedirectIO;

namespace Testing
{
    class TestVisitor : TestingBaseVisitor<string>
    {
        ///

            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
        
        ///
        Foo d = new Foo();
        string table;
		string no_ins_table;
        List<string> table_list = new List<string>();
        List<string> col_list = new List<string>();
        List<string> values = new List<string>();
        int table_flag = 1;
        // List<string>  table_list=null;
        int print = 0;
        public void printDictionary(Foo d)
        {
            foreach (var key1 in d.dictionary.Keys)
            {
				//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) ) 
                //Console.WriteLine("Table name : {0}", key1);
                var value1 = d.dictionary[key1];
                foreach (KeyValuePair<string, List<string>> w in value1)
                {
					//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                    //Console.WriteLine("Column Name {0}", w.Key);
                    foreach (var st in w.Value)
                    {
						//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                        //Console.WriteLine("Constraints " + st + " ");
                    }
                }
            }

            //  printDictionary(d);
        }
        //printDictionary(d);

        public override string VisitGram(TestingParser.GramContext context)
        {
        using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
					Console.WriteLine(" ");
		    Console.WriteLine("Hi");
            //sConsole.WriteLine(context.GetToken(TestingParser.SC, 0));
            return base.VisitGram(context);
        }
        public override string VisitStart(TestingParser.StartContext context)
        {

            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine(ex1);
            if (!d.dictionary.TryGetValue(ex1, out d.inner))
            {
                d.inner = new Dictionary<string, List<string>>();
                d.dictionary.Add(ex1, d.inner);
				//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                //Console.WriteLine("Table created Successfully ");
            }
            else
            {
				using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                Console.WriteLine("ERROR : TABLE " + ex1 + " already exists");
				using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                Console.WriteLine(" Table NOT created");
            }

            return base.VisitStart(context);
        }
        public override string VisitIdnc1(TestingParser.Idnc1Context context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE1, 0) + " NO CONSTRAINT");
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex, out d.list))
            {
                d.list = d.Mydata(ex);
            }
            string ex2 = context.GetToken(TestingParser.TYPE1, 0).ToString();
            d.list.Add(ex2);
            d.list.Add("no"); //no constraints :)
            // Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            return base.VisitIdnc1(context);
        }
        //printDictionary(d);

        public override string VisitIdnc2(TestingParser.Idnc2Context context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " NO CONSTRAINT");
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex, out d.list))
            {
                d.list = d.Mydata(ex);
            }
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")";
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex2);
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine(ex2);
            d.list.Add("no"); //no constraints :)
            return base.VisitIdnc2(context);
        }
        public override string VisitIdnc2c(TestingParser.Idnc2cContext context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " NO CONSTRAINT");
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex, out d.list))
            {
                d.list = d.Mydata(ex);
            }
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")";
            d.list.Add(ex2);
            //Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine(ex2);
            d.list.Add("no"); //no constraints :)
            return base.VisitIdnc2c(context);
        }

        public override string VisitRidnc1(TestingParser.Ridnc1Context context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE1, 0) + " NO CONSTRAINT");
            //string ex = context.GetToken(TestingParser.TYPE1, 0).ToString() + context.GetToken(TestingParser.ID, 0).ToString();
            string ex = context.GetToken(TestingParser.ID, 0).ToString();

            if (!d.inner.TryGetValue(ex, out d.list))
            {
                d.list = d.Mydata(ex);
            }
            string ex2 = context.GetToken(TestingParser.TYPE1, 0).ToString();
            d.list.Add(ex2);
            d.list.Add("no"); //no constraints :)
            //  printDictionary(d);
            return base.VisitRidnc1(context);

        }
        public override string VisitRidnc2(TestingParser.Ridnc2Context context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " NO CONSTRAINT");
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex, out d.list))
            {
              // using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
			   // Console.WriteLine("checking" + ex);

                d.list = d.Mydata(ex);
            }
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")";  //SOME PROBLEM REPORTED WHILE GIVING CREATE TABLE EE (NAME CHAR);
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine("checking2" + ex2);
            d.list.Add(ex2);
            d.list.Add("no"); //no constraints :)
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            return base.VisitRidnc2(context);
        }
        public override string VisitRidnc2c(TestingParser.Ridnc2cContext context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " NO CONSTRAINT");
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex, out d.list))
            {
                //using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				//Console.WriteLine("checking" + ex);

                d.list = d.Mydata(ex);
            }
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")";  //SOME PROBLEM REPORTED WHILE GIVING CREATE TABLE EE (NAME CHAR);
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine("checking2" + ex2);
            // Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex2);
            d.list.Add("no"); //no constraints :)

            return base.VisitRidnc2c(context);
        }

        public override string VisitIdc11(TestingParser.Idc11Context context)
        {
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE1, 0) + " CONSTRAINT:" + context.GetToken(TestingParser.CONSTRAINT1, 0));
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            // string ex2 = context.GetToken(TestingParser.TYPE1, 0).ToString() + " " + context.GetToken(TestingParser.CONSTRAINT1, 0).ToString();
            string ex2 = context.GetToken(TestingParser.TYPE1, 0).ToString(); //+ " " + context.GetToken(TestingParser.CONSTRAINT1, 0).ToString();
            string ex21 = context.GetToken(TestingParser.CONSTRAINT1, 0).ToString();
            
            d.list.Add(ex2);
            d.list.Add(ex21);
            //printDictionary(d);
            return base.VisitIdc11(context);
        }
        public override string VisitIdc12(TestingParser.Idc12Context context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " CONSTRAINT:" + context.GetToken(TestingParser.CONSTRAINT1, 0));
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            //string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + " " + context.GetToken(TestingParser.CONSTRAINT1, 0).ToString();
            //d.list.Add(ex2);
            //printDictionary(d);
            //modify
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")";
            string ex21 = context.GetToken(TestingParser.CONSTRAINT1, 0).ToString();
            d.list.Add(ex2);
            d.list.Add(ex21);
            //  printDictionary(d);
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            return base.VisitIdc12(context);
        }
        public override string VisitIdc12c(TestingParser.Idc12cContext context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " CONSTRAINT:" + context.GetToken(TestingParser.CONSTRAINT1, 0));
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }

            //modify
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")";
            string ex21 = context.GetToken(TestingParser.CONSTRAINT1, 0).ToString();
            d.list.Add(ex2);
            // Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex21);
            //  printDictionary(d);

            return base.VisitIdc12c(context);
        }

        public override string VisitIdc21(TestingParser.Idc21Context context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE1, 0) + " CONSTRAINT:" + context.GetToken(TestingParser.CONSTRAINT2, 0));
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }

            //string ex2 = context.GetToken(TestingParser.TYPE1, 0).ToString() + " " + context.GetToken(TestingParser.CONSTRAINT2, 0).ToString() + " " + context.GetToken(TestingParser.NUM, 0).ToString();
            string ex2 = context.GetToken(TestingParser.TYPE1, 0).ToString();
            string ex21 = context.GetToken(TestingParser.CONSTRAINT2, 0).ToString() + " " + context.GetToken(TestingParser.ID, 1).ToString() + "(" + context.GetToken(TestingParser.ID, 2).ToString() + ")";

            //checking circular reference !! 
            int Flag = 0;
            int Flag_col = 0;
            string table_c = context.GetToken(TestingParser.ID, 1).ToString();
            string col_c = context.GetToken(TestingParser.ID, 2).ToString();
            //whether table_c exists or not !!
            foreach (var key1 in d.dictionary.Keys)
            {
                if (table_c.Equals(key1))
                {
                    Flag = 1;
					//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                    //Console.WriteLine("found the table to be referred");


                    d.list.Add(ex2);
                    d.list.Add(ex21);
                    //table_c , col_c
                    var value1 = d.dictionary[table_c];
                    Flag_col = 0;
                    foreach (KeyValuePair<string, List<string>> w in value1)
                    {

                        if (w.Key.Equals(col_c))
                        {
                            Flag_col = 1;
							//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                            //Console.WriteLine("here i'm messed up" + w.Value[0] + "and ->" + w.Value[1] + "checked " + col_c + w.Key);
                            if ((w.Value[1].Equals("FOREIGN KEY REFERENCES") || w.Value[1].Equals("REFERENCES")))
                            {
                               //using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
							   //Console.WriteLine("It's ok");
                            }

                        }
                    }


                    break;

                }
            }

            if (Flag == 0)
            {
                using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				Console.WriteLine("ERROR : Referred table  " + table_c + " does not exists ");
                d.list.Add(ex2);
                //  d.list.Add(ex21); foreign key not inserted 

            }



            // printDictionary(d);
            return base.VisitIdc21(context);
        }


        public override string VisitIdc22(TestingParser.Idc22Context context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " CONSTRAINT:" + context.GetToken(TestingParser.CONSTRAINT2, 0));
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")"; // + context.GetToken(TestingParser.ID, 1).ToString();
            string ex21 = context.GetToken(TestingParser.CONSTRAINT2, 0).ToString() + " " + context.GetToken(TestingParser.ID, 1).ToString();
            d.list.Add(ex2);
            d.list.Add(ex21);
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            //printDictionary(d);
            return base.VisitIdc22(context);
        }
        public override string VisitIdc22c(TestingParser.Idc22cContext context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " CONSTRAINT:" + context.GetToken(TestingParser.CONSTRAINT2, 0));
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")"; // + context.GetToken(TestingParser.ID, 1).ToString();
            string ex21 = context.GetToken(TestingParser.CONSTRAINT2, 0).ToString() + " " + context.GetToken(TestingParser.ID, 1).ToString();
            d.list.Add(ex2);
            //Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex21);
            //printDictionary(d);
            return base.VisitIdc22c(context);
        }
        public override string VisitIdcht1(TestingParser.Idcht1Context context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE: " + context.GetToken(TestingParser.TYPE1, 0) + " CHECK CONSTARINT " + context.GetToken(TestingParser.ID,1)+ context.GetToken(TestingParser.REL,0) +context.GetToken(TestingParser.NUM,0) );
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE1, 0).ToString(); //+ " " + context.GetToken(TestingParser.CH, 0).ToString() + '(' + context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.NUM, 0).ToString() + ')';
            string ex21 = context.GetToken(TestingParser.CH, 0).ToString() + '(' + context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.NUM, 0).ToString() + ')';
            d.list.Add(ex2);
            d.list.Add(ex21);
            //printDictionary(d);
            return base.VisitIdcht1(context);
        }
        public override string VisitDropt(TestingParser.DroptContext context)
        {
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            int Flag = 0;

            foreach (var key1 in d.dictionary.Keys)
            {
                if (ex.Equals(key1))
                {
                    Flag = 1;
			//		using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
             //       Console.WriteLine("found the table to be dropped!!" + "before");
               //     printDictionary(d);
                    //d.Remove(ex);
                    //Dictionary.Remove(ex);
                    d.dictionary.Remove(ex);
				//	using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                 //   Console.WriteLine("found the table to be dropped!!" + "after");
                  //  printDictionary(d);




                    break;

                }
            }
            if (Flag == 0)
            {
               using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
			    Console.WriteLine("Error: Table to be dropped " + ex + " does not exist");
            }

            return base.VisitDropt(context);
        }
        public override string VisitIdch(TestingParser.IdchContext context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE: " + context.GetToken(TestingParser.TYPE1, 0) + " CHECK CONSTARINT " + context.GetToken(TestingParser.ID,1)+ context.GetToken(TestingParser.REL,0) +context.GetToken(TestingParser.NUM,0) );
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")"; // +context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.ID, 2).ToString() + ')';
            string ex21 = context.GetToken(TestingParser.CH, 0).ToString() + '(' + context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.ID, 2).ToString() + ')';
           using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
		    Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex2);
            d.list.Add(ex21);
            //printDictionary(d);
            return base.VisitIdch(context);
        }
        public override string VisitIdchc(TestingParser.IdchcContext context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE: " + context.GetToken(TestingParser.TYPE1, 0) + " CHECK CONSTARINT " + context.GetToken(TestingParser.ID,1)+ context.GetToken(TestingParser.REL,0) +context.GetToken(TestingParser.NUM,0) );
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")"; // +context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.ID, 2).ToString() + ')';
            string ex21 = context.GetToken(TestingParser.CH, 0).ToString() + '(' + context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.ID, 2).ToString() + ')';
            //Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex2);
            d.list.Add(ex21);
            //printDictionary(d);
            return base.VisitIdchc(context);
        }
        public override string VisitIddf1(TestingParser.Iddf1Context context)
        {
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE1, 0) + " DEFAULT CONSTRAINT: " + context.GetToken(TestingParser.NUM, 0));
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE1, 0).ToString(); // +" " + context.GetToken(TestingParser.DF, 0).ToString() + context.GetToken(TestingParser.NUM, 0).ToString();
            string ex21 = context.GetToken(TestingParser.DF, 0).ToString() + context.GetToken(TestingParser.NUM, 0).ToString();

            d.list.Add(ex2);
            d.list.Add(ex21);
            //printDictionary(d);
            return base.VisitIddf1(context);
        }
        public override string VisitIddf2(TestingParser.Iddf2Context context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " DEFAULT CONSTRAINT:" + context.GetToken(TestingParser.ID, 1));
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")"; // context.GetToken(TestingParser.ID, 1).ToString();
            string ex21 = context.GetToken(TestingParser.DF, 0).ToString() + context.GetToken(TestingParser.ID, 1).ToString();
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex2);
            d.list.Add(ex21);
            //printDictionary(d);
            return base.VisitIddf2(context);
        }

        public override string VisitIddf2c(TestingParser.Iddf2cContext context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " DEFAULT CONSTRAINT:" + context.GetToken(TestingParser.ID, 1));
            string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")"; // context.GetToken(TestingParser.ID, 1).ToString();
            // Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            string ex21 = context.GetToken(TestingParser.DF, 0).ToString() + context.GetToken(TestingParser.ID, 1).ToString();

            d.list.Add(ex2);
            d.list.Add(ex21);
            //printDictionary(d);
            return base.VisitIddf2c(context);
        }

        public override string VisitInstart(TestingParser.InstartContext context)
        {
            int Flag = 0;
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine("here " + ex);
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine("-----------" + "final-dictionary Created Based on your create Query " + "----------------");
            printDictionary(d);
            foreach (var key1 in d.dictionary.Keys)
            {
                if (ex.Equals(key1))
                {
                    Flag = 1;
                    break;
                }
            }
            if (Flag == 0)  //checking if table exists or not
            {
				no_ins_table=ex;
				table = "none";
            }
            else
            {
                //using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				//Console.WriteLine("Table exists!! ");
                table = ex;
            }
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine("---------------------------");
            return base.VisitInstart(context);

        }
        public override string VisitInid1(TestingParser.Inid1Context context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            col_list.Add(ex1);
            return base.VisitInid1(context);
        }
        public override string VisitInid21(TestingParser.Inid21Context context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            values.Add(ex1);
            return base.VisitInid21(context);
        }
        public override string VisitInum1(TestingParser.Inum1Context context)
        {
            string ex1 = context.GetToken(TestingParser.NUM, 0).ToString();
            values.Add(ex1);
            return base.VisitInum1(context);
        }
        public override string VisitInid2(TestingParser.Inid2Context context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            string colname;
            string val;
            int ctn = 0;
            if (table.Equals("none"))
            {
                using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				Console.WriteLine("ERROR: Error while inserting values because table " + no_ins_table+ " does not exist.");
            }
            else
            {
                values.Add(ex1);
                if (col_list.Count != values.Count)
                {
                    using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
					Console.WriteLine("ERROR: Error while inserting the values in " + table +",number of columns and number of values are not matching.");
                }
                else
                {
                    ctn = values.Count;
                    for (int i = 0; i < ctn; i++)
                    {
                        colname = col_list[i];
                        val = values[i];
                        var value1 = d.dictionary[table];
                        foreach (KeyValuePair<string, List<string>> w in value1)
                        {
                            if (colname.Equals(w.Key))
                            {
								if(w.Value[0].StartsWith("char")||w.Value[0].StartsWith("CHAR") && Regex.IsMatch(val, @"^[a-zA-Z]+$"))
								{
									string str=w.Value[0].Substring(5,w.Value[0].Length-6);
									try {
											int decimalVal = int.Parse(str);
											if(val.Length > decimalVal)
											using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
											Console.WriteLine("ERROR: Error while inserting " + val + " datatype size does not match");
										}
									catch (ArgumentNullException) {
									} 
								}	
								if(w.Value[0].StartsWith("varchar")||w.Value[0].StartsWith("VARCHAR") && Regex.IsMatch(val, @"^[a-zA-Z]+$"))
								{
									string str=w.Value[0].Substring(8,w.Value[0].Length-9);
									try {
											int decimalVal = int.Parse(str);
											if(val.Length > decimalVal)
											using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
											Console.WriteLine("ERROR: Error while inserting " + val + " datatype size does not match");
										}
									catch (ArgumentNullException) {
									} 
								}	
                            
                                if (w.Value[0].Equals("int") && !Regex.IsMatch(val, @"^\d+$"))
                                {
                                    using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
									Console.WriteLine("ERROR: Error while inserting " + val + " datatype does not match.");

                                }
                                if ((w.Value[0].StartsWith("char") || w.Value[0].StartsWith("varchar")) && !Regex.IsMatch(val, @"^[a-zA-Z]+$"))
                                {
                                    using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
									Console.WriteLine("ERROR: Error while inserting " + val + " datatype does not match.");
                                }
                                else
                                {
                                    if (w.Value[1].StartsWith("CHECK") || w.Value[1].StartsWith("check"))
                                    {
                                        string cons = w.Value[1].Substring(5);
                                        if (cons.Length > 2)
                                        {
                                            if (cons.StartsWith("(") && cons.EndsWith(")"))
                                            {
                                                cons = cons.Substring(1, cons.Length - 2);
                                            }
                                        }
										//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                                        //Console.WriteLine(cons);

                                    }
                                }
                            }
                        }
                    }
                    col_list.RemoveRange(0, col_list.Count - 1);
                    values.RemoveRange(0, values.Count - 1);
                }

            }
            return base.VisitInid2(context);
        }
        public override string VisitInum(TestingParser.InumContext context)
        {
            string ex1 = context.GetToken(TestingParser.NUM, 0).ToString();
            string colname;
            string val;
            int ctn = 0;
            if (table.Equals("none"))
            {
                using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				Console.WriteLine("ERROR: Error while inserting values because table " +no_ins_table+" does not exist.");
            }
            else
            {
                values.Add(ex1);
                if (col_list.Count == values.Count)
                    ctn = values.Count;
                else
                {
					using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                    Console.WriteLine("ERROR: Error while inserting the values in " + table +",number of columns and number of values are not matching.");
                    //using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				    //Console.WriteLine(values.Count);
                    //using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				    //Console.WriteLine(col_list.Count);

                }
                for (int i = 0; i < ctn; i++)
                {
                    colname = col_list[i];
                    val = values[i];
                    var value1 = d.dictionary[table];
                    foreach (KeyValuePair<string, List<string>> w in value1)
                    {
                        if (colname.Equals(w.Key))
                        {
							if(w.Value[0].StartsWith("char")||w.Value[0].StartsWith("CHAR") && Regex.IsMatch(val, @"^[a-zA-Z]+$"))
							{
								string str=w.Value[0].Substring(5,w.Value[0].Length-6);
								try {
										int decimalVal = int.Parse(str);
										if(val.Length > decimalVal)
										using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
										Console.WriteLine("ERROR: Error while inserting " + val + " datatype size does not match");
									}
								catch (ArgumentNullException) {
								} 
							}	
							if(w.Value[0].StartsWith("varchar")||w.Value[0].StartsWith("VARCHAR") && Regex.IsMatch(val, @"^[a-zA-Z]+$"))
							{
								string str=w.Value[0].Substring(8,w.Value[0].Length-9);
								try {
										int decimalVal = int.Parse(str);
										if(val.Length > decimalVal)
										using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
										Console.WriteLine("ERROR: Error while inserting " + val + " datatype size does not match");
									}
								catch (ArgumentNullException) {
								} 
							}	
                            if ((w.Value[0].Equals("int")||w.Value[0].Equals("INT")) && !Regex.IsMatch(val, @"^\d+$"))
							{
								using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                                Console.WriteLine("ERROR: Error while inserting " + val + " datatype does not match");
							}
                            if ((w.Value[0].StartsWith("char") || w.Value[0].StartsWith("varchar") || w.Value[0].StartsWith("CHAR") ||w.Value[0].StartsWith("VARCHAR")) && !Regex.IsMatch(val, @"^[a-zA-Z]+$"))
							{
								using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                                Console.WriteLine("ERROR: Error while inserting " + val + " datatype does not match");
							}
                            else
                            {
                                if (w.Value[1].StartsWith("CHECK"))
                                {
                                    string cons = w.Value[1].Substring(5);
                                    if (cons.Length > 2)
                                    {
                                        if (cons.StartsWith("(") && cons.EndsWith(")"))
                                        {
                                            cons = cons.Substring(1, cons.Length - 2);
                                        }
                                    }
									//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                                    //Console.WriteLine(cons);

                                }
                            }
                        }
                    }
                }
                col_list.RemoveRange(0, col_list.Count - 1);
                values.RemoveRange(0, values.Count - 1);
            }
            return base.VisitInum(context);
        }

        public override string VisitSelwarn(TestingParser.SelwarnContext context)
        {
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : SELECT * FROM " + ex + " is not advisable to use in your script ");
            int Flag = 0;

            foreach (var key1 in d.dictionary.Keys)
            {
                if (ex.Equals(key1))
                {
                    Flag = 1;
                    break;

                }
            }
            if (Flag == 0)
            {
               using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
			    Console.WriteLine("ERROR: Table " + ex + " does not exist");
            }

            return base.VisitSelwarn(context);
        }
        public override string VisitTid(TestingParser.TidContext context) //checking table exists or not!!
        {
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            // Console.WriteLine("in tid");
            int Flag = 0;
            int flag_col = 0;
            //  table_flag = 1;
            foreach (var key1 in d.dictionary.Keys)
            {
                if (ex.Equals(key1))
                {
                    Flag = 1;  //table_found!!
                    table_list.Add(ex);
                    break;
                }
            } //ends dict search for table name
            if (Flag == 0)
            {
                using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				Console.WriteLine("ERROR: " + "NO table " + ex + " exists ");
                table_flag = 0;
            }
            return base.VisitTid(context);
        }

        //checking of all table and columns !! 
        public override string VisitTid1(TestingParser.Tid1Context context)
        {
            //Console.WriteLine("inside tid1");
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            int Flag = 0;
            foreach (var key1 in d.dictionary.Keys)
            {
                if (ex.Equals(key1))
                {
                    Flag = 1;  //table_found!!
                    table_list.Add(ex);
                    break;
                }
            } //ends dict search for table name
            if (Flag == 0)
            {
                using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				Console.WriteLine("ERROR: " + "NO table " + ex + " exists ");
                table_flag = 0;
            }

            // modify
            int flag_col = 0;

            foreach (string key2 in col_list)
            {
                flag_col = 0;
                //Console.Write("key2--->" + key2);
                foreach (string key1 in table_list)
                {
                    var value1 = d.dictionary[key1];
                    foreach (KeyValuePair<string, List<string>> w in value1)
                    {
                        //            Console.WriteLine("WTF--->");
                        //            Console.WriteLine(w.Key);
                        if (key2.Equals(w.Key))
                        {
                          //  using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
						//	Console.WriteLine("yeahhh got it :)" + "--->" + key2);
                            flag_col = 1;
                            break;
                        }
                    }
                }
                if (flag_col == 0)
                {
                    using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
					Console.WriteLine("ERROR : no such " + key2 + " column exists in "+ ex);

                }
            }
            //HERE TWO LISTS MUST BE TRUNCATED -- CHECK HERE 
            table_list.RemoveRange(0, table_list.Count - 0);
            //table_list = null;

            //col_list = null;
            col_list.RemoveRange(0, col_list.Count - 0);

            return base.VisitTid1(context);
        }



        public override string VisitSid(TestingParser.SidContext context)
        {
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            // Console.WriteLine("in sid");
            // int Flag = 0;
            col_list.Add(ex);

            return base.VisitSid(context);
        }

        //have to modify
        public override string VisitSid1(TestingParser.Sid1Context context)
        {
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            // Console.WriteLine("in sid");
            // int Flag = 0;
            col_list.Add(ex);

            return base.VisitSid1(context);
        }
        /*******************************************MY ADDITION***********************************88********************************/
      /* public override void BaseErrorListener(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            Console.WriteLine("Error in parser at line " + ":" + e.OffendingToken.Column + e.OffendingToken.Line + e.Message);
                

        }
     //   {
            /*
            string ENode = node.ToString();
            Console.WriteLine("Syntax Error is near " + ENode);*/
         //   public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    //{
      //           Console.WriteLine("Error in parser at line " + ":" + e.OffendingToken.Column + e.OffendingToken.Line + e.Message);
                
                // base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);
    
            //return base.VisitErrorNode(node);
        //}

        public override string VisitErrorNode(Antlr4.Runtime.Tree.IErrorNode node)
        {
            string ENode = node.ToString();

            if (print == 0)
            {
                using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				Console.WriteLine("ERROR: Syntax Error is : ");
				using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
                Console.Write(" " + ENode);
            
                print = 1;
            }
            else
            {
                using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
				Console.Write(" " + ENode);
            }
            return base.VisitErrorNode(node);
        }
        public override string VisitIdnc3(TestingParser.Idnc3Context context)
        {
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex, out d.list))
            {
                d.list = d.Mydata(ex);
            }
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString();
            d.list.Add(ex2);
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine(ex2);
            d.list.Add("no"); //no constraints :)
            return base.VisitIdnc3(context);
        }
        public override string VisitIdnc3a(TestingParser.Idnc3aContext context)
        {
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex, out d.list))
            {
                d.list = d.Mydata(ex);
            }
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")";
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex2);
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine(ex2);
            d.list.Add("no"); //no constraints :)
            return base.VisitIdnc3a(context);
        }
        public override string VisitIdnc3c(TestingParser.Idnc3cContext context)
        {
            string ex = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex, out d.list))
            {
                d.list = d.Mydata(ex);
            }
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")";
            d.list.Add(ex2);
			//using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            //Console.WriteLine(ex2);
            d.list.Add("no"); //no constraints :)
            return base.VisitIdnc3c(context);
        }
        public override string VisitIdc13(TestingParser.Idc13Context context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString();
            string ex21 = context.GetToken(TestingParser.CONSTRAINT1, 0).ToString();
            d.list.Add(ex2);
            d.list.Add(ex21);
            return base.VisitIdc13(context);
        }
        public override string VisitIdc13a(TestingParser.Idc13aContext context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            //string ex2 = context.GetToken(TestingParser.TYPE2, 0).ToString() + " " + context.GetToken(TestingParser.CONSTRAINT1, 0).ToString();
            //d.list.Add(ex2);
            //printDictionary(d);
            //modify
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")";
            string ex21 = context.GetToken(TestingParser.CONSTRAINT1, 0).ToString();
            d.list.Add(ex2);
            d.list.Add(ex21);
            //  printDictionary(d);
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");

            return base.VisitIdc13a(context);
        }
        public override string VisitIdc13c(TestingParser.Idc13cContext context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }

            //modify
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")";
            string ex21 = context.GetToken(TestingParser.CONSTRAINT1, 0).ToString();
            d.list.Add(ex2);
            d.list.Add(ex21);

            return base.VisitIdc13c(context);
        }
        public override string VisitIdc23(TestingParser.Idc23Context context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }

            //modify
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString();
            string ex21 = context.GetToken(TestingParser.CONSTRAINT2, 0).ToString();
            d.list.Add(ex2);
            d.list.Add(ex21);

            return base.VisitIdc23(context);
        }
        public override string VisitIdc23a(TestingParser.Idc23aContext context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")"; // + context.GetToken(TestingParser.ID, 1).ToString();
            string ex21 = context.GetToken(TestingParser.CONSTRAINT2, 0).ToString() + " " + context.GetToken(TestingParser.ID, 1).ToString();
            d.list.Add(ex2);
            d.list.Add(ex21);
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");

            return base.VisitIdc23a(context);
        }
        public override string VisitIdc23c(TestingParser.Idc23cContext context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")"; // + context.GetToken(TestingParser.ID, 1).ToString();
            string ex21 = context.GetToken(TestingParser.CONSTRAINT2, 0).ToString() + " " + context.GetToken(TestingParser.ID, 1).ToString();
            d.list.Add(ex2);

            d.list.Add(ex21);
            return base.VisitIdc23c(context);
        }
        public override string VisitIdcht3(TestingParser.Idcht3Context context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString(); // +context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.ID, 2).ToString() + ')';
            string ex21 = context.GetToken(TestingParser.CH, 0).ToString() + '(' + context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.ID, 2).ToString() + ')';
            d.list.Add(ex2);
            d.list.Add(ex21);
            //printDictionary(d);
            return base.VisitIdcht3(context);
        }
        public override string VisitIdch3a(TestingParser.Idch3aContext context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")"; // +context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.ID, 2).ToString() + ')';
            string ex21 = context.GetToken(TestingParser.CH, 0).ToString() + '(' + context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.ID, 2).ToString() + ')';
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex2);
            d.list.Add(ex21);

            return base.VisitIdch3a(context);
        }
        public override string VisitIdch3c(TestingParser.Idch3cContext context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")"; // +context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.ID, 2).ToString() + ')';
            string ex21 = context.GetToken(TestingParser.CH, 0).ToString() + '(' + context.GetToken(TestingParser.ID, 1).ToString() + context.GetToken(TestingParser.REL, 0).ToString() + context.GetToken(TestingParser.ID, 2).ToString() + ')';
            //Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex2);
            d.list.Add(ex21);
            //printDictionary(d);

            return base.VisitIdch3c(context);
        }
        public override string VisitIddf3(TestingParser.Iddf3Context context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " DEFAULT CONSTRAINT:" + context.GetToken(TestingParser.ID, 1));
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString(); // context.GetToken(TestingParser.ID, 1).ToString();
            string ex21 = context.GetToken(TestingParser.DF, 0).ToString() + context.GetToken(TestingParser.ID, 1).ToString();

            d.list.Add(ex2);
            d.list.Add(ex21);

            return base.VisitIddf3(context);
        }
        public override string VisitIddf3a(TestingParser.Iddf3aContext context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " DEFAULT CONSTRAINT:" + context.GetToken(TestingParser.ID, 1));
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString() + "(" + context.GetToken(TestingParser.A, 0).ToString() + ")"; // context.GetToken(TestingParser.ID, 1).ToString();
            string ex21 = context.GetToken(TestingParser.DF, 0).ToString() + context.GetToken(TestingParser.ID, 1).ToString();
			using (OutToFile redir = new OutToFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Redirect.txt")) )
            Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            d.list.Add(ex2);
            d.list.Add(ex21);

            return base.VisitIddf3a(context);
        }
        public override string VisitIddf3c(TestingParser.Iddf3cContext context)
        {
            string ex1 = context.GetToken(TestingParser.ID, 0).ToString();
            if (!d.inner.TryGetValue(ex1, out d.list))
            {
                d.list = d.Mydata(ex1);
            }
            //Console.WriteLine(context.GetToken(TestingParser.ID, 0) + " TYPE:" + context.GetToken(TestingParser.TYPE2, 0) + " DEFAULT CONSTRAINT:" + context.GetToken(TestingParser.ID, 1));
            string ex2 = context.GetToken(TestingParser.TYPE3, 0).ToString() + "(" + context.GetToken(TestingParser.NUM, 0).ToString() + ")"; // context.GetToken(TestingParser.ID, 1).ToString();
            // Console.WriteLine("WARNING : " + ex2 + " is not advisable to use  in your script ");
            string ex21 = context.GetToken(TestingParser.DF, 0).ToString() + context.GetToken(TestingParser.ID, 1).ToString();

            d.list.Add(ex2);
            d.list.Add(ex21);

            return base.VisitIddf3c(context);
        }
    }
}
