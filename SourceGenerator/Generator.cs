using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceGenerator
{
    [Generator]
    public class MySourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // определяем генерируемый код
            var code = @"
            namespace Metanit
            {
                public static class Welcome 
                {
                    public const string Name = ""Eugene"";
                    public static void Print() => Console.WriteLine($""Hello {Name}!"");
                }
            }";
            context.AddSource("metanit.welcome.generated.cs", code);
        }
        public void Initialize(GeneratorInitializationContext context)
        {
            // инициализация не нужна
        }
    }
}
