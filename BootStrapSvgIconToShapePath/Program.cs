using System.Xml;
using System.Xml.Linq;

namespace BootStrapSvgIconToShapePath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input path for bootstrap svg files : ");
            string? path = Console.ReadLine();

            if (path != null)
            {
                string rdfile = @".\BootStrapIcons.xmal";
                string[] files = Directory.GetFiles(path, "*.svg");

                
                foreach (string file in files)
                {
                    Console.WriteLine(file);

                    XElement svg = XElement.Load(file);

                    svg.Elements().ToList().ForEach(e => { 
                        
                        if (e.Name == "path")
                        {
                            string data = e.Attribute("d")!.Value;


                        }
                    });

                }
                
            }
            //Console.WriteLine("Hello, World!");
        }
    }
}
