using System;
using System.Text;

namespace GestorStock_BrandsGoodsSA.Models
{
    /// <summary>
    /// Classe com os métodos relativos aos menus principais da aplicação.
    /// </summary>
    class Menu
    {
        /// <summary>
        /// Enumerado com opções do menu de Gestão de Artigos -> ManageArticles().
        /// </summary>
        enum MenuManageArticles { ShowArticleList = 1, UpdateArticle, NewArticle }

        /// <summary>
        /// Enumerado com opções do menu de Gestão de Clientes -> ManageClients().
        /// </summary>
        enum MenuManageClients { ShowClientList = 1, UpdateClient, NewClient }

        /// <summary>
        /// Enumerado com opções do menu de Gestão de Pedidos de Cliente -> ManageClientRequests().
        /// </summary>
        enum MenuManageRequests { ShowRequestList = 1, NewClientRequest }


        /// <summary>
        /// Método contrutor da classe Menu.
        /// </summary>
        public Menu() { }


        /// <summary>
        /// Método que mostra o menu de funcionalidades de gestão dos pedidos de clientes chamando os métodos respetivos.
        /// </summary>
        public void ManageClientRequests()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Error e = new();
            bool exiT = false;

            while (exiT == false)
            {
                Console.Clear();
                Console.WriteLine("-- MENU GERIR PEDIDOS DE CLIENTE --\n");
                Console.WriteLine("1-Ver Pedidos\n2-Novo Pedido\nX-Menu Principal");
                Console.WriteLine("\nDigite um algarismo de 1 a 2\nOU\n'X' para escolher uma opção, por favor.");

                string exit = Console.ReadLine();
                int option = 0;

                if (exit == "x" || exit == "X")
                {
                    ChooseMainMenu();
                }
                else
                {
                    try
                    {
                        option = Convert.ToInt32(exit);
                        if (option < 1 || option > 2)
                        {
                            e.ErrorMenu();
                        }
                    }
                    catch (System.FormatException)
                    {
                        e.ErrorMenu();
                    }
                    MenuManageRequests choice = (MenuManageRequests)option;
                    Request r = new Request();
                    switch (choice)
                    {
                        case MenuManageRequests.ShowRequestList:
                            r.ShowRequests();
                            break;
                        case MenuManageRequests.NewClientRequest:
                            r.NewClientRequest();
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Método que mostra o menu de funcionalidades de gestão de artigos chamando os métodos respetivos.
        /// </summary>
        public void ManageArticles()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Error e = new();
            bool exiT = false;

            while (exiT == false)
            {
                Console.Clear();
                Console.WriteLine("-- MENU GERIR ARTIGOS --\n");
                Console.WriteLine("1-Lista de Artigos\n2-Actualizar Artigo\n3-Criar Novo Artigo\nX-Menu Principal");
                Console.WriteLine("\nDigite um algarismo de 1 a 3\nOU\n'X' para escolher uma opção, por favor.");

                string exit = Console.ReadLine();
                int option = 0;

                if (exit == "x" || exit == "X")
                {
                    ChooseMainMenu();
                }
                else
                {
                    try
                    {
                        option = Convert.ToInt32(exit);
                        if (option < 1 || option > 3)
                        {
                            e.ErrorMenu();
                        }
                    }
                    catch (System.FormatException)
                    {
                        e.ErrorMenu();
                    }
                    MenuManageArticles choice = (MenuManageArticles)option;
                    Article a = new Article();
                    switch (choice)
                    {
                        case MenuManageArticles.ShowArticleList:
                            a.ShowArticleList();
                            break;
                        case MenuManageArticles.UpdateArticle:
                            a.UpdateArticle();
                            break;
                        case MenuManageArticles.NewArticle:
                            a.NewArticle();
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Método que mostra o menu de funcionalidades de gestão dos clientes chamando os métodos respetivos.
        /// </summary>
        public void ManageClients()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Error e = new();
            bool exiT = false;

            while (exiT == false)
            {
                Console.Clear();
                Console.WriteLine("-- MENU GERIR CLIENTES --\n");
                Console.WriteLine("1-Lista de Clientes\n2-Actualizar Cliente\n3-Criar Novo Cliente\nX-Menu Principal");
                Console.WriteLine("\nDigite um algarismo de 1 a 3\nOU\n'X' para escolher uma opção, por favor.");

                string exit = Console.ReadLine();
                int option = 0;

                if (exit == "x" || exit == "X")
                {
                    ChooseMainMenu();
                }
                else
                {
                    try
                    {
                        option = Convert.ToInt32(exit);
                        if (option < 1 || option > 3)
                        {
                            e.ErrorMenu();
                        }
                    }
                    catch (System.FormatException)
                    {
                        e.ErrorMenu();
                    }
                    MenuManageClients choice = (MenuManageClients)option;
                    Client c = new Client();
                    switch (choice)
                    {
                        case MenuManageClients.ShowClientList:
                            c.ShowClientList();
                            break;
                        case MenuManageClients.UpdateClient:
                            c.UpdateClient();
                            break;
                        case MenuManageClients.NewClient:
                            c.NewClient();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Método que quando chamado, apresenta o menu de Administrador ou Utilizador Standard dependendo do utilizador que fez Log In.
        /// Utiliza o valor da propriedade AccessAdmin (true/false) para "escolher" o menu a mostrar.
        /// </summary>
        public void ChooseMainMenu()
        {
            User u = new User();
            if (u.AccessAdmin == true)
            {
                UserAdmin mainAdmin = new UserAdmin();
                mainAdmin.MainMenu();
            }
            else if (u.AccessAdmin == false)
            {
                UserStd mainStd = new UserStd();
                mainStd.MainMenu();
            }
        }

        /// <summary>
        /// Método utilizado para informar o utilizador que pode ao menu anterior premindo a tecla [Enter].
        /// </summary>
        public void BackENTER()
        {
            Console.WriteLine("\nPrima [ENTER] para voltar ao menu anterior.");
            Console.ReadLine();
        }

        /// <summary>
        /// Método que apresenta a hipótese do o utilizador voltar ao menu anterior.
        /// </summary>
        public void BackXENTER()
        {
            Console.WriteLine("Digite 'X' seguido de [ENTER] para voltar ao menu anterior: ");
        }

        /// <summary>
        /// Método que passa mensagens de conclusão de alguma tarefa ao utilizador.
        /// Recebe uma string como argumento, que é a mensagem correcta para o contexto especifico em que está a ser utilizado.
        /// </summary>
        /// <param name="message"></param>
        public void bdUpdatedBackENTER(string message)
        {
            Console.WriteLine("" +
                "\n---------------------------------------------------------------" +
                "\n" +
                $"          >>> {message.ToUpper()}  <<<          " +
                "\n" +
                "---------------------------------------------------------------" +
                "\n\n\n" +
                "Prima [ENTER] para voltar ao menu anterior.");
            Console.ReadLine();
        }

        /// <summary>
        /// Método que permite ao utilizador escolher entre sair da aplicação ou voltar ao Login
        /// </summary>
        public void ExitOrLogin()
        {
            Console.WriteLine("\n\nPara sair da aplicação, prima [ESC].\nPara voltar a tentar, prima [ENTER].");
            var pressedKey = Console.ReadKey();
            if (pressedKey.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            else if(pressedKey.Key == ConsoleKey.Enter)
            {
                User u = new User();
                u.Login();
            }
            Console.ReadLine();
        }
    }
}
