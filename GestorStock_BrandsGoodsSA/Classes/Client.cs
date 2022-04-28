using System;
using System.Linq;
using System.Text;


namespace GestorStock_BrandsGoodsSA.Models
{
    /// <summary>
    /// Classe com todos os métodos relativos à gestão atualização de Clientes na base de dados através da aplicação.
    /// É a mesma classe da tabela Client na Base de Dados.
    /// </summary>
    partial class Client
    {
        /// <summary>
        /// Variavel de tipo cliente que guarda as informações do cliente temporário a editar ou criar.
        /// </summary>
        static Client tempClient;

        /// <summary>
        /// Variável que guarda a informação (true/false) se a lista de clientes está vazia ou não.
        /// </summary>
        private bool listEmpty;

        Error e = new Error();
        Menu m = new Menu();

        /// <summary>
        /// Método que escreve as informações de um CLIENTE específico na consola.
        /// </summary>
        /// <param name="tempClient"></param>
        public void ShowClientInfo(Client tempClient)
        {
            Console.OutputEncoding = Encoding.UTF8;
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            Console.WriteLine("_____________");
            Console.WriteLine($"{tempClient.ClientCode} >> {tempClient.ClientName}");
        }

        /// <summary>
        /// Método com a Query à base de dados para consulta da lista de clientes.
        /// </summary>
        public void ClientList()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            var clientList = (from tempClient in bd.Clients
                             select tempClient).ToList();

            if (clientList.Count < 1)
            {
                listEmpty = true;
                Console.WriteLine("A lista de clientes está vazia.");
            }
            else
            {
                foreach (Client tempClient in clientList)
                {
                    ShowClientInfo(tempClient);
                }
            }
        }

        /// <summary>
        /// Método para mostrar a lista de Clientes.
        /// </summary>
        public void ShowClientList()
        {
            Console.Clear();
            Console.WriteLine("-- LISTA DE CLIENTES --\n");
            ClientList();
            m.BackENTER();
        }

        /// <summary>
        /// Método para o utilizador actualizar os dados do CLIENTE: nome.
        /// </summary>
        public void UpdateClient()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            Console.Clear();
            Console.WriteLine("-- Actualizar Cliente --\n");
            ClientList();
            if (listEmpty == true)
            {
                m.BackENTER();
            }
            else
            {
                Console.WriteLine("\nDigite o código do cliente que pretende actualizar.");
                Console.WriteLine("OU");
                m.BackXENTER();
                string exit = Console.ReadLine();
                string selectedCODE;

                if (exit == "x" || exit == "X")
                {
                    m.ManageClients();
                }

                else
                {
                    try
                    {
                        selectedCODE = exit;
                        tempClient = (from b in bd.Clients
                                      where b.ClientCode == selectedCODE
                                      select b).FirstOrDefault();

                        if (tempClient is Client)
                        {
                            Console.Clear();
                            Console.WriteLine($"\n-- Actualizar dados do cliente: {tempClient.ClientName.ToUpper()} --");
                            Console.WriteLine("\n\nDados atuais do cliente:\n");
                            ShowClientInfo(tempClient);
                            UpdateClientInfo();
                            bd.SaveChanges();
                            m.bdUpdatedBackENTER("cliente atualizado com sucesso!");
                        }

                        else
                        {
                            Error e = new Error();
                            e.NoOption();
                            UpdateClient();
                        }
                    }
                    catch (System.FormatException)
                    {
                        e.FormatNumbers();
                        UpdateClient();
                    }
                }
            }
            
        }

        /// <summary>
        /// Método para criar um novo Cliente.
        /// </summary>
        public void NewClient()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            Console.Clear();
            Console.WriteLine("-- CRIAR NOVO CLIENTE --\n");
            tempClient = new Client();

            Console.WriteLine("--------------------------------");
            Console.WriteLine("--> Lista de Clientes Existentes");
            Console.WriteLine("--------------------------------\n");

            ClientList();
            Console.WriteLine("\nDigite um código até 4 caracteres (máx.) para o seu novo cliente");
            Console.Write("\nCÓDIGO: ");
            string newCode;
            try
            {
                newCode = Console.ReadLine();
                Client tempClient1 = new Client();

                tempClient1 = (from b in bd.Clients
                               where b.ClientCode == newCode
                               select b).FirstOrDefault();

                if (tempClient1 is Client)
                {
                    Console.Clear();
                    Console.WriteLine($"\n\n----------------   >>> CÓDIGO '{newCode}' JÁ EXISTENTE!  <<<   ----------------\n\n\n");
                    ShowClientInfo(tempClient1);
                    Console.WriteLine($"\nDigite um código até 4 caracteres (máx.) diferente para o seu novo cliente.\nPrima [ENTER] para tentar novamente.");
                    Console.ReadLine();
                    NewClient();
                }

                else if (newCode.Length > 4)
                {
                    e.StringLimit(4);
                    NewClient();
                }

                else
                {
                    tempClient.ClientCode = newCode;
                    AddClientInfo();
                    bd.Clients.Add(tempClient);
                    bd.SaveChanges();
                    m.bdUpdatedBackENTER("Novo Cliente Adicionado");
                }
            }
            catch (System.FormatException)
            {
                e.FormatNumbers();
                NewClient();
            }
        }

        /// <summary>
        /// Método que recolhe a informação de um cliente para a variavel tempClient (editar ou criar Cliente).
        /// </summary>
        public void AddClientInfo()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            Console.Write("\n\nDigite a informação do Cliente\n");

            Console.Write("\nNOME: ");
            tempClient.ClientName = Console.ReadLine();
            if (tempClient.ClientName.Length > 50)
            {
                e.StringLimit(50);
                AddClientInfo();
            }
            bd.Clients.Add(tempClient);
        }

        /// <summary>
        /// Recolhe a informação de um cliente para a variavel tempClient (editar ou criar Cliente)
        /// </summary>
        public void UpdateClientInfo()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            Console.Write("\n\nDigite a informação do Cliente\n");

            Console.Write("\nNOME: ");
            tempClient.ClientName = Console.ReadLine();
            if (tempClient.ClientName.Length > 50)
            {
                e.StringLimit(50);
                UpdateClientInfo();
            }
            bd.Clients.Update(tempClient);
            bd.SaveChanges();
        }
    }
}
