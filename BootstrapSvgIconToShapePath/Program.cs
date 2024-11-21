using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BootstrapSvgIconToShapePath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input file's full-path for [bootstrap-icons.svg, ver1.11.3] : ");
            string? filename = Console.ReadLine();

            if (filename != null)
            {
                using (StreamWriter writer = new StreamWriter(@".\BootstrapSvgIcons.xaml"))
                {
                    writer.WriteLine("<ResourceDictionary xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">");

                    XElement svg = XElement.Load(filename);

                    svg.Elements().ToList().ForEach(e1 => { 

                        if (e1.Name.LocalName == "symbol")
                        {
                            string id = e1.Attribute("id")!.Value;

                            writer.WriteLine("<Style x:Key=\"{0}\" TargetType=\"{{x:Type UserControl}}\">", id);
                            writer.WriteLine("    <Setter Property=\"Width\" Value=\"16\" />");
                            writer.WriteLine("    <Setter Property=\"Height\" Value=\"16\" />");
                            writer.WriteLine("    <Setter Property=\"Control.Template\">");
                            writer.WriteLine("        <Setter.Value>");
                            writer.WriteLine("            <ControlTemplate>");
                            writer.WriteLine("                <Canvas Background=\"{TemplateBinding Background}\">");

                            e1.Elements().ToList().ForEach(e2 =>
                            {
                                if (e2.Name.LocalName == "path")
                                {
                                    string data = e2.Attribute("d")!.Value;
                                    writer.WriteLine("                    <Path Fill=\"{{TemplateBinding Foreground}}\" Data=\"{0}\" />", data);
                                }
                                else
                                {
                                    switch (e2.Name.LocalName)
                                    {
                                        case "rect":
                                            {
                                                if (id == "align-bottom")
                                                {
                                                    string width = e2.Attribute("width")!.Value;
                                                    string height = e2.Attribute("height")!.Value;
                                                    string x = e2.Attribute("x")!.Value;
                                                    string y = e2.Attribute("y")!.Value;
                                                    string rx = e2.Attribute("rx")!.Value;

                                                    writer.WriteLine("                    <Path Fill=\"{TemplateBinding Foreground}\">");
                                                    writer.WriteLine("                        <Path.Data>");
                                                    writer.WriteLine("                            <RectangleGeometry Rect=\"{0},{1},{2},{3}\" RadiusX=\"{4}\" />", x, y, width, height, rx);
                                                    writer.WriteLine("                        </Path.Data>");
                                                    writer.WriteLine("                    </Path>");
                                                }
                                                else if (id == "align-top")
                                                {
                                                    //id = "align-top" >< rect width = "4" height = "12" rx = "1" transform = "matrix(1 0 0 -1 6 15)" />
                                                    string width = e2.Attribute("width")!.Value;
                                                    string height = e2.Attribute("height")!.Value;
                                                    string x = "0";
                                                    string y = "0";
                                                    string rx = e2.Attribute("rx")!.Value;

                                                    var matches = Regex.Matches(e2.Attribute("transform")!.Value, @"\((.+)\)");

                                                    string matrix = string.Empty;
                                                    foreach (Match match in matches)
                                                        matrix = match.Groups[1].Value;

                                                    writer.WriteLine("                    <Path Fill=\"{TemplateBinding Foreground}\">");
                                                    writer.WriteLine("                        <Path.Data>");
                                                    writer.WriteLine("                            <RectangleGeometry Rect=\"{0},{1},{2},{3}\" RadiusX=\"{4}\">", x, y, width, height, rx);
                                                    writer.WriteLine("                                <RectangleGeometry.Transform>");
                                                    writer.WriteLine("                                    <MatrixTransform Matrix=\"{0}\" />", matrix);
                                                    writer.WriteLine("                                </RectangleGeometry.Transform>");
                                                    writer.WriteLine("                            </RectangleGeometry>");
                                                    writer.WriteLine("                        </Path.Data>");
                                                    writer.WriteLine("                    </Path>");
                                                }
                                            }
                                            break;

                                        case "circle":
                                            {
                                                string cx = e2.Attribute("cx")!.Value;
                                                string cy = e2.Attribute("cy")!.Value;
                                                string r = e2.Attribute("r")!.Value;

                                                writer.WriteLine("                    <Path Fill=\"{TemplateBinding Foreground}\">");
                                                writer.WriteLine("                        <Path.Data>");
                                                writer.WriteLine("                            <EllipseGeometry RadiusX=\"{0}\" RadiusY=\"{0}\" Center=\"{1},{2}\" />", r, cx, cy);
                                                writer.WriteLine("                        </Path.Data>");
                                                writer.WriteLine("                    </Path>");
                                            }
                                            break;
                                    }
                                }
                            });

                            writer.WriteLine("                </Canvas>");
                            writer.WriteLine("            </ControlTemplate>");
                            writer.WriteLine("        </Setter.Value>");
                            writer.WriteLine("    </Setter>");
                            writer.WriteLine("</Style>");
                        }
                    });

                    writer.WriteLine("</ResourceDictionary>");
                }

                using (StreamWriter writer = new StreamWriter(@".\BootstrapSvgIconControls.xaml"))
                {
                    XElement svg = XElement.Load(filename);

                    svg.Elements().ToList().ForEach(e1 => {

                        if (e1.Name.LocalName == "symbol")
                        {
                            string id = e1.Attribute("id")!.Value;
                            writer.WriteLine("<StackPanel HorizontalAlignment=\"Left\" Margin=\"4\">");
                            writer.WriteLine("<controls:BootstrapSvgIconControl Id=\"{0}\" />", id);
                            writer.WriteLine("<TextBlock Text=\"{0}\" />", id);
                            writer.WriteLine("</StackPanel>");
                        }
                    });
                }

            }
        }
    }
}
