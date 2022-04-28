using System;
using System.Text;

namespace GestorStock_BrandsGoodsSA.Models
{
    /// <summary>
    /// Classe 
    /// </summary>
    class UserStd : User, IUser
    {
        /// <summary>
        /// Enumerado dos items do Menu apresentado a um utilizador da classe UserStd (sem as funcionalidades de importação e exportação de stock)
        /// </summary>
        enum MenuStd { ManageClientRequests = 1, ManageArticles , ManageClients, Login, Exit }

        /// <summary>
        /// Apresenta o Menu com acesso a acções de utilizador Standard
        /// </summary>
        public void MainMenu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            //instâncias de outras classes para chamar métodos aí presentes.
            Menu m = new Menu();
            Error format = new Error();


            bool exit = false;
            while (exit == false)
            {
                Console.Clear();
                Console.WriteLine("-- MENU PRINCIPAL --\n");
                Console.WriteLine("1-Gerir Pedidos de Cliente\n2-Gerir Artigos\n3-Gerir Clientes\n4-Log Out\n5-Sair da Aplicação");
                Console.WriteLine("\nDigite um algarismo de 1 a 5 para escolher uma opção, por favor.");

                int option = 0;
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                    if (option < 1 || option > 5)
                    {
                        format.ErrorMenu();
                    }
                }
                catch (System.FormatException)
                {
                    format.ErrorMenu();
                }
                MenuStd choice = (MenuStd)option;

                switch (choice)
                {
                    case MenuStd.ManageClientRequests:
                        m.ManageClientRequests();
                        break;
                    case MenuStd.ManageArticles:
                        m.ManageArticles();
                        break;
                    case MenuStd.ManageClients:
                        m.ManageClients();
                        break;
                    case MenuStd.Login:
                        User u = new User();
                        u.Login();
                        break;
                    case MenuStd.Exit:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
