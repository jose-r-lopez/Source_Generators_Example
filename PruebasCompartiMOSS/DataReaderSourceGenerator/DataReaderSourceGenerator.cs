using System;
// Debemos añadir este namespace
using Microsoft.CodeAnalysis; 

namespace SourceGenerator
{
    static class Context
    {
        public static bool ReadsDataFromWebService ()
        {
            return false;
        }
    }

    [Generator]
    public class DataReaderSourceGenerator : ISourceGenerator
    {
        bool dataSourceIsWebService = false;

        public void Execute(GeneratorExecutionContext context)
        {
            /* Aqui va la generación de código, generando el string con el códgo fuente de C# como queramos.
             * Para ello podemos usar cualquier dato que podamos calcular según sea necesario.*/

            /* Usamos un compilation object para obtener una referencia al método main, ya que el DataReader que
             * vamos a generar lo hemos creado en su mismo namespace */
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

            // Construir el código fuente según de donde se lean los datos
            if (dataSourceIsWebService)
            {
                string codigoWS = $@"
using System;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}
{{
    public partial class DataReader
    {{
        partial void _ReadData()
        {{
            this._Data = new string [] {{""Los datos se leerían de un servicio web""}};
        }}
    }}
}}
";
                // Añade el código fuente a la compilación
                context.AddSource("GeneratedWSReader", codigoWS);
            }
            else
            {
                string codigoDB = $@"
using System;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}
{{
    public partial class DataReader
    {{
        partial void _ReadData()
        {{
            this._Data = new string [] {{""Los datos se leerían de una BBDD""}};
        }}
    }}
}}
";
                // Añade el código fuente a la compilación
                context.AddSource("GeneratedBDReader", codigoDB);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            /* Aqui tendríamos que crear el código que nos permita calcular cualquier
             * información que nos haga falta para crear el SourceGenerator. En nuestro
             * caso simulamos aqui la consulta al entorno de ejecución en el
             * que el programa se va a compilar */
            dataSourceIsWebService = Context.ReadsDataFromWebService();
        }
    }
}
