using System;
using System.Text;



namespace GestorStock_BrandsGoodsSA.Models
{
    class Program
    {
        static void Main(string[] args)
        {



            Console.OutputEncoding = Encoding.UTF8;

            Menu m = new Menu();
            User u = new User();

            try
            {
                u.Login();
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                Console.WriteLine("\n\n>>> ACESSO À BASE DE DADOS INTERROMPIDO <<<.\nEntre em contacto com o suporte técnico, por favor.");
                m.ExitOrLogin();
            }
        }
    }
}
