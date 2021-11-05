using System;
using System.Security.Cryptography;
using System.Text;

namespace PruebasCompartiMOSS2
{
    /**
     * Clase original
     */
    public class HashCompute
    {
        private static byte[] GetHash(string inputString)
        {
            //Usamos el algoritmo SHA-256
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }

    /**
     * Clase adaptada a partie de la original para poder modificarse con un SourceGenerator
     */
    public partial class HashComputeSG
    {
        private static byte[] result_GetHash;
        //Primer metodo a ser adaptado por un SourceGenerator
        static partial void _GetHash(string inputString);

        private static byte[] GetHash(string inputString)
        {
            _GetHash(inputString);
            return result_GetHash;
        }

        private string result_GetHashString;
        //Segundo metodo a ser adaptado por un SourceGenerator
        partial void _GetHashString(string inputString);

        public string GetHashString(string inputString)
        {
            _GetHashString(inputString);

            return result_GetHashString;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Llamamos a la clase original y al SourceGenerator
            var hash = new HashCompute();
            Console.WriteLine(hash.GetHashString("CompartiMOSS"));
            var hash2 = new HashComputeSG();
            Console.WriteLine(hash2.GetHashString("CompartiMOSS"));
        }
    }
}
