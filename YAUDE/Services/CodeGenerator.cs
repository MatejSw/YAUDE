using System;
using System.Collections.Generic;
using System.Text;
using YAUDE.Model;

namespace YAUDE.Services
{
    public static class CodeGenerator
    {
        public static void GenerateSingleFile(List<DiagramClass> elements, string filepath, string projectName)
        {
            using (StreamWriter sw = new(filepath))
            {
                sw.WriteLine($"namespace {projectName}");
                sw.WriteLine("{");

                foreach (DiagramClass @class in elements)
                {
                    sw.WriteLine($"\tpublic class {@class.Name}");
                    sw.WriteLine("\t{");
                    foreach (YAUDE.Model.Attribute attribute in @class.Attributes)
                    {
                        sw.WriteLine($"\t\t{attribute.Visibility.ToString().ToLower()} {attribute.Type} {attribute.Name};");
                        sw.WriteLine();
                    }
                    foreach (YAUDE.Model.Method method in @class.Methods)
                    {
                        sw.WriteLine($"\t\t{method.Visibility.ToString().ToLower()} {method.ReturnType} {method.Name}({method.Parameters})");
                        sw.WriteLine("""
                                    {

                                    }

                            """);
                    }
                    sw.WriteLine("\t}");
                    sw.WriteLine();
                }
                sw.WriteLine("}");
            }
        }
    }
}
