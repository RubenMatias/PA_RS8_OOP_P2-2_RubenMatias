using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace D00_Utility
{
    public static class Utility
    {
        // Encoding da consola, i.e, preparar a consola para receber carateres especiais
        public static void SetUnicodeConsole() 
        {
            //Console.WriteLine("á Á à À ã Ã â Â ç Ç º ª");

            Console.OutputEncoding = Encoding.UTF8;  

            //Console.WriteLine("á Á à À ã Ã â Â ç Ç º ª");
        }

        public static void WriteTitle(string title) 
        {
             
            Console.WriteLine(new string('-', 50));

            Console.WriteLine(title.ToUpper());

            Console.WriteLine(new string('-', 50));


        }

        public static void BlockSeparator(string separator) 
        {
            Console.Write(separator);
        }

        public static void TerminateConsole() 
        {

            Console.Write("\n\nPrime qualquer tecla para saires."); 
            Console.ReadKey();
            Console.Clear();

        }


        // Criar o método ValidateNumber0() em D00_Utility:
        //Recebe 1 valor double
        //Devolve true se o número recebido for 0
        //Testem para ver se funciona

        public static bool ValidateNumber0(double number)
        {
            #region v1: Funcional, mas pouco otimizado

            /*
            if (number == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            */

            #endregion

            #region v2: Usando operador ternário


            // return number == 0 ? true : false;

            #endregion

            #region v3:Mais Otimizado

            return number == 0;

            #endregion

        }

        internal static void TestInternal() 
        {

            Console.WriteLine("Qq coisa!");

        }

        public static bool ValidateNumberDouble(string number)
        {
                                                                                                                                         

            return double.TryParse(number, out double result);

        }

    }
}
