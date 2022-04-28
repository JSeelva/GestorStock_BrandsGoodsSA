using System;
using System.Linq;
using System.Text;

namespace GestorStock_BrandsGoodsSA.Models
{
    /// <summary>
    /// Classe Mae de UserStd e UserAdmin. Contém os métodos e variáveis relativos à identificação do tipo de utilizador.
    /// É a mesma classe da tabela Clientes na Base de Dados.
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Variável que regista se a combinação de Utilizador e Password está correcta
        /// </summary>
        private bool correctPassWord;

        /// <summary>
        /// Variável que é alterada apenas na classe User conforme o utilizador que faça login. 
        /// Deve ser acessível apenas para escolha do método MainMenu() das classes filhas UserAdmin ou UserStd
        /// </summary>
        static private bool _accessAdmin;
        public bool AccessAdmin
        {
            get { return _accessAdmin; }
        }

        private string typedUser;
        private string typedPassword;

        public User()
        {
        }

        /// <summary>
        /// Método que recebe o Username e Password e distingue através da Base de Dados, que tipo de acesso o user tem.
        /// </summary>
        public void Login()
        {
            Console.OutputEncoding = Encoding.UTF8;

            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            while (correctPassWord == false)
            {
                Console.Clear();
                Console.WriteLine("\n== BENVINDO AO GESTOR DE STOCK - Brands & Goods S.A. ==\n\n\nDigite os seus dados de acesso, por favor:");
                Console.Write("\nusername: ");
                typedUser = Console.ReadLine();
                Console.Write("password: ");

                //Máscara da password
                StringBuilder passwordString = new StringBuilder();
                bool typePassword = true;
                char enterKey = '\r';
                char backSpaceKey = '\b';
                while (typePassword == true)
                {
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                    char insertedCharacter = consoleKeyInfo.KeyChar;

                    if (insertedCharacter == enterKey)
                    {
                        typePassword = false;
                    }
                    else if (insertedCharacter == backSpaceKey && passwordString.Length > 0)
                    {
                        Console.Write("\b \b");
                        passwordString.Remove(passwordString.Length - 1, 1).ToString();
                    }
                    else if (insertedCharacter == backSpaceKey && passwordString.Length < 1)
                    {
                        Console.Write("");
                    }
                    else
                    {
                        Console.Write('*');
                        passwordString.Append(insertedCharacter.ToString());
                    }
                }
                typedPassword = passwordString.ToString();

                var check = (from a in bd.Users
                             where a.UserName == typedUser && a.PassWord == typedPassword
                             select a).FirstOrDefault();

                if (check != null)
                {
                    correctPassWord = true;
                    var queryResult = (from b in bd.Users
                                       where b.UserName == typedUser
                                       select b).FirstOrDefault();
                    _accessAdmin = queryResult.Admin;

                    if (_accessAdmin == true)
                    {
                        UserAdmin mainAdmin = new UserAdmin();
                        mainAdmin.MainMenu();
                    }
                    else if (_accessAdmin == false)
                    {
                        UserStd mainStd = new UserStd();
                        mainStd.MainMenu();
                    }
                }
                else
                {
                    correctPassWord = false;
                    Console.WriteLine("\n\nPassword ou Username Errado\nPrima [ENTER] para tentar novamente.");
                    Console.ReadLine();
                }
            }

            Menu m = new Menu();
            m.ExitOrLogin();
        }
    }
}
