using Microsoft.CodeAnalysis;

namespace HashSourceGenerator
{
    [Generator]
    public class HashSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Generamos el nuevo código
            // Encontrar el método main
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

            // Nuevo código del source generator
            string source = $@"
using System;
using System.Text;
using System.Security.Cryptography;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}
{{
    public partial class HashComputeSG
    {{
        static partial void _GetHash(string inputString)
        {{
            //Ahora usamos el algoritmo SHA-512 en este SourceGenerator
            using (HashAlgorithm algorithm = SHA512.Create())
                 result_GetHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }}

        partial void _GetHashString(string inputString)
        {{
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString(""X2""));

            result_GetHashString = sb.ToString();
        }}
    }}
}}
";
            // Añadimos el código al proceso de compilación
            context.AddSource("GeneratedHashCompute", source);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
