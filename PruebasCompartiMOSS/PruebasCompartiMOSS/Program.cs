using System;

namespace PruebasCompartiMOSS
{
    /**
     * Esto sería una clase parcial cuyo contenido será completado mediante el SourceGenerator. 
     * Sirve como interfaz de acceso a los datos, pero cómo acceder a los datos en sí lo 
     * definiremos mediante un Source Generator enseguida.
     */
    public partial class DataReader
    {
        string[] _Data;
        /**
         * Este método es el que se completará vía SourceGenerator. Al tratarse de un método parcial, 
         * debe devolver void y no llevar modificador de acceso.
         */
        partial void _ReadData(); 

        /**
         * La definición de métodos parciales tiene limitaciones obvias, pero podemos solucionarlas 
         * fácilmente con un método público que se encargue de mostrar el interfaz exacto que el 
         * cliente espera y preparar la llamada al mismo (parametros y valores devueltos).
         * Un método parcial puede llevar parámetros, pero está limitado en el retorno. Es sencillo
         * saltarse esa limitación creandolo de manera que escriba lo que calcule en un atributo 
         * privado, (_Data en este caso).
         */
        public string[] Data
        {
            get
            {
                _ReadData();
                return _Data;
            }
        }
    }
    class MainProgram
    {
        static void Main(string[] args)
        {
            //Simulamos leer información
            var reader = new DataReader();
            Console.WriteLine(reader.Data[0]);
        }
    }
}
