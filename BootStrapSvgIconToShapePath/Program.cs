using System.Xml;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BootStrapSvgIconToShapePath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Input file full path for [bootstrap-icons.svg] : ");
            //string? path = Console.ReadLine();

            string filename = @"C:\Users\hskim\Downloads\bootstrap-icons-1.11.3\bootstrap-icons-1.11.3\bootstrap-icons.svg";

            if (filename != null)
            {
                using (StreamWriter writer = new StreamWriter(@".\BootStrapIcons.xaml"))
                {
                    XElement svg = XElement.Load(filename);

                    svg.Elements().ToList().ForEach(e1 => {

                        if (e1.Name.LocalName == "symbol")
                        {
                            string id = e1.Attribute("id")!.Value;

                            //Console.WriteLine(string.Format("{0}", id));
                            writer.WriteLine(string.Format("<Grid x:Key=\"{0}\" Width=\"16\" Height=\"16\" />", id));

                            e1.Elements().ToList().ForEach(e2 =>
                            {
                                if (e2.Name.LocalName == "path")
                                {

                                    string data = e2.Attribute("d")!.Value;
                                    //Console.WriteLine(string.Format("{0}", data));
                                    writer.WriteLine(string.Format("<Path Data=\"{0}\" />", data));

                                }
                                else
                                {
                                    Console.WriteLine(e2.Name.LocalName);
                                }
                            });

                            writer.WriteLine(string.Format("</Grid>"));
                        }
                    });


                }
               
            }
            //Console.WriteLine("Hello, World!");
        }
    }
}
