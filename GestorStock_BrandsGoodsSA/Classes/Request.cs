using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestorStock_BrandsGoodsSA.Models
{
    /// <summary>
    /// Classe com todos os métodos relativos à criação de Pedidos de Clientes na base de dados através da aplicação.
    /// É a mesma classe da tabela Request na Base de Dados. Interage com a classe da tabela RequestDetail para criar os pedidos.
    /// </summary>
    public partial class Request
    {
        //instâncias de outras classes para chamar métodos aí presentes.
        Client c = new Client();
        Menu m = new Menu();
        Error e = new Error();
        Article a = new Article();

        //instâncias de outras classes para guardar objectos temporários de cada uma.
        Client tempClient = new Client();
        Article tempArticle = new Article();
        RequestDetail tempRequestDetail = new RequestDetail();

        /// <summary>
        /// Variavel de tipo Request para guardar objectos Request (pedido de cliente)
        /// </summary>
        Request tempRequest;


        /// <summary>
        /// Método que recolhe informações do detalhe do pedido (Código de artigo, nome, preço, quantidade e total) presentes na Base de Dados, através de query das informações.
        /// </summary>
        /// <param name="selectedRequest"></param>
        public void RequestInfo(int selectedRequest)
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            var client = from c in bd.Clients
                         join r in bd.Requests on c.ClientId equals r.ClientId
                         where r.RequestId == selectedRequest
                         select c.ClientName;


            var viewRequest = (from rd in bd.RequestDetails
                               join a in bd.Articles on rd.ArticleId equals a.ArticleId
                               where rd.RequestId == selectedRequest
                               select new { a.ArticleCode, a.ArticleName, a.ArticlePrice, rd.ArticleQuantity }).ToList();

            if (viewRequest.Count < 1)
            {
                e.NoOption();
                ShowRequests();
            }
            else
            {
                foreach (var item in client)
                {
                    Console.WriteLine("\n---------------------------------------------------------------");
                    Console.WriteLine($"PPEDIDO Nº. {selectedRequest} - Cliente {item.ToUpper()}");
                    Console.WriteLine("---------------------------------------------------------------");
                }

                Console.WriteLine("\nCÓDIGO\t| ARTIGO");
                double soma = 0;
                Console.OutputEncoding = Encoding.Default;
                foreach (var item in viewRequest)
                {
                    Console.WriteLine($"{item.ArticleCode}\t| {item.ArticleName} ---> PREÇO UNITÁRIO: {item.ArticlePrice}€ X QUANTIDADE: {item.ArticleQuantity} unidades = {item.ArticlePrice * item.ArticleQuantity}€");

                    soma += item.ArticlePrice * item.ArticleQuantity;
                }
                Console.WriteLine($"\nTOTAL: {soma}€");
                Console.WriteLine("---------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Método que apresenta a lista de pedidos de cliente por ordem alfabética (cliente).
        /// </summary>
        public void ShowRequests()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            Console.Clear();

            var requestList = (from r in bd.Requests
                               join c in bd.Clients on r.ClientId equals c.ClientId
                               orderby c.ClientName
                               select new { r.RequestId, c.ClientName }).ToList();

            if (requestList.Count < 1)
            {
                Console.WriteLine("A lista de pedidos está vazia. Crie um ou mais pedidos para poderem ser consultados.");
            }
            else
            {
                Console.WriteLine("-- LISTA DE PEDIDOS --\n");
                Console.WriteLine("CÓDIGO__| NOME DO CLIENTE_________");
                foreach (var item in requestList)
                {
                    Console.WriteLine($"{item.RequestId}\t| {item.ClientName}");
                }

                Console.Write("\nDigite o código do Pedido e prima [ENTER] para o visualizar.\nCÓDIGO: ");
                try
                {
                    int selectedRequest = Convert.ToInt32(Console.ReadLine());
                    RequestInfo(selectedRequest);

                }
                catch (System.FormatException)
                {
                    e.FormatNumbers();
                    ShowRequests();
                }
            }
            m.BackENTER();
            m.ManageClientRequests();
        }


        /// <summary>
        /// Método que permite adicionar artigos e a informação da quantidade ao pedido de cliente.
        /// </summary>
        public void AddRequestDetail()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            tempRequestDetail = new RequestDetail();

            //Guardar valor requestID na linha que se está a editar
            tempRequestDetail.RequestId = tempRequest.RequestId;

            bool validation = true;
            bool control = true;

            Console.WriteLine("\nDigite o código do artigo para adicionar ao pedido:");

            do
            {
                validation = true;
                try
                {
                    //Guardar valor articleID na linha que se está a editar
                    Console.Write("\nCódigo: ");
                    int selectedArticleCode = Convert.ToInt32(Console.ReadLine());
                    tempArticle = (from a in bd.Articles
                                   where a.ArticleCode == selectedArticleCode
                                   select a).FirstOrDefault();

                    if (tempArticle is Article && tempArticle.ArticleState == true)
                    {
                        tempRequestDetail.ArticleId = tempArticle.ArticleId;
                        validation = true;
                    }
                    else if (tempArticle is null || tempArticle.ArticleState == false)
                    {
                        e.UnavailableOrInexistent();
                        validation = false;
                        a.ArticleList();
                    }

                }
                catch (System.FormatException)
                {
                    e.NoOption();
                    validation = false;
                    a.ArticleList();
                }
                catch (OverflowException)
                {
                    e.NoOption();
                    validation = false;
                    a.ArticleList();
                }
            }
            while (validation == false);

            do
            {
                Console.Write("Quantidade: ");
                try
                {
                    tempRequestDetail.ArticleQuantity = Convert.ToInt32(Console.ReadLine());

                    if (tempRequestDetail.ArticleQuantity < 0)
                    {
                        e.NumberRequirement("positivo");
                        control = false;
                        Console.WriteLine($"Artigo: {tempArticle.ArticleName}");
                    }
                    else
                    {
                        control = true;
                    }
                }

                catch (System.FormatException)
                {
                    e.FormatNumbersInt();
                    control = false;
                    Console.WriteLine($"Artigo: {tempArticle.ArticleName}");
                }
                catch (OverflowException)
                {
                    e.NoOption();
                    control = false;
                    Console.WriteLine($"Artigo: {tempArticle.ArticleName}");
                }
            }
            while (control == false);

            bd.RequestDetails.Add(tempRequestDetail);
            tempArticle.ArticleAmount -= Convert.ToInt32(tempRequestDetail.ArticleQuantity);
            bd.SaveChanges();
        }


        /// <summary>
        /// Método para escolher o cliente e guardar o pedido na tabela Request.
        /// </summary>
        public void AddRequestClient()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            tempRequest = new Request();

            var clientList = (from tempClient in bd.Clients
                              select tempClient).ToList();

            var articleList = (from tempArticle in bd.Articles
                               where tempArticle.ArticleState == true
                              select tempArticle).ToList();

            if (clientList.Count < 1 || articleList.Count<1)
            {
                Console.WriteLine("A lista de clientes está vazia ou não existem de artigos em estado 'Disponível'.\nCrie um ou mais clientes e/ou artigos para poder criar Pedidos de Cliente.\nOs artigos deverão ter o seu estado 'Disponível' para poderem ser seleccionados na criação de pedidos de cliente.");
                m.BackENTER();
                m.ManageClientRequests();
            }
            else
            {
                c.ClientList();
                Console.WriteLine("\nDigite o código do cliente que faz o pedido.");
                Console.WriteLine("OU");
                m.BackXENTER();
                string selectedCODE = Console.ReadLine();

                if (selectedCODE == "x" || selectedCODE == "X")
                {
                    m.ChooseMainMenu();
                }
                else
                {
                    try
                    {
                        tempClient = (from b in bd.Clients
                                      where b.ClientCode == selectedCODE
                                      select b).FirstOrDefault();

                        if (tempClient is Client)
                        {
                            Console.WriteLine($"\n-- Registar pedido do cliente: {tempClient.ClientName.ToUpper()} --");

                            //Associar Cliente ao pedido
                            tempRequest.ClientId = tempClient.ClientId;
                            /////////////////
                            bd.Requests.Add(tempRequest);
                            bd.SaveChanges();
                        }

                        else
                        {
                            Error e = new Error();
                            e.NoOption();
                            AddRequestClient();
                        }
                    }
                    catch (System.FormatException)
                    {
                        e.ErrorFormatLetters();
                        AddRequestClient();
                    }
                }
            }
        }


        /// <summary>
        /// Método que regista 1 novo pedido de cliente alterando a quantidade de stock dos artigos na BD
        /// </summary>
        public void NewClientRequest()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            bool endRequest = false;

            Console.Clear();
            Console.WriteLine("-- CRIAR NOVO PEDIDO DE CLIENTE --\n");

            AddRequestClient();

            Console.Clear();
            Console.WriteLine($"\n-- PEDIDO N.º {tempRequest.RequestId} para o cliente {tempClient.ClientName.ToUpper()} --\n");

            //Para que a.ArticleList(); mostre apenas os artigos disponíveis.
            a.showUnavailable = false;
            a.ArticleList();

            AddRequestDetail();

            do
            {
                Console.Write("\nPrima [Enter] para adicionar + 1 artigo ao pedido OU [ESC] para finalizar.\n");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    endRequest = true;
                    bd.SaveChanges();
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    AddRequestDetail();
                    endRequest = false;
                    
                }
            }
            while (endRequest == false);

            RequestInfo(tempRequestDetail.RequestId);
            m.bdUpdatedBackENTER("PEDIDO REGISTADO COM SUCESSO! ");
        }
    }
}
