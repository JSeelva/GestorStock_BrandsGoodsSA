using System;
using System.Linq;
using System.Text;

namespace GestorStock_BrandsGoodsSA.Models
{
    /// <summary>
    /// Classe com todos os métodos relativos à gestão atualização de Artigos na base de dados através da aplicação.
    /// É a mesma classe da tabela Article na Base de Dados.
    /// </summary>
    public partial class Article
    {
        /// <summary>
        /// Variável de tipo artigo que guarda as informações do artigo temporário a editar/criar.
        /// </summary>
        static Article tempArticle;

        /// <summary>
        /// Enumerado com opções do menu para escolher o tipo de informação acerca do artigo a atualizar.
        /// </summary>
        enum ArticleInfo { Name = 1, Price, Stock, State }

        /// <summary>
        /// Variável que guarda a informação (true/false) se a lista de artigos mostra os artigos indiponíveis ou não, conforme a pertinência do contexto
        /// </summary>
        public bool showUnavailable;

        /// <summary>
        /// Variável que guarda a informação (true/false) se a lista de artigos está vazia ou não.
        /// </summary>
        private bool listEmpty;

        //instâncias de outras classes
        Error e = new Error();


        /// <summary>
        /// Escreve na Consola as informações de um ARTIGO. Utiliza a váriavel tempArticle para definir o artigo.
        /// </summary>
        /// <param name="tempArticle"></param>
        /// 
        public void ShowArticleInfo(Article tempArticle)
        {
            Console.OutputEncoding = Encoding.UTF8;

            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            Console.WriteLine("_____________");
            Console.Write($"{tempArticle.ArticleCode} >> { tempArticle.ArticleName} | Preço: {tempArticle.ArticlePrice}€ | Stock: {tempArticle.ArticleAmount}");

            if (tempArticle.ArticleState == true)
            {
                Console.WriteLine(" | Estado: Disponível");
            }
            else if (tempArticle.ArticleState == false)
            {
                Console.WriteLine(" | Estado: Indisponível");
            }
        }

        /// <summary>
        /// Método com a query à base de dados para consulta da lista de artigos.
        /// </summary>
        public void ArticleList()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            var articleList = (from tempArticle in bd.Articles
                              select tempArticle).ToList();

            if (articleList.Count < 1)
            {
                listEmpty = true;
                Console.WriteLine("A lista de artigos está vazia.");
            }
            else
            {
                if (showUnavailable == true)
                {
                    var artList = from tempArticle in bd.Articles
                                  select tempArticle;
                    foreach (Article tempArticle in artList)
                    {
                        ShowArticleInfo(tempArticle);
                    }
                }

                else if (showUnavailable == false)
                {
                    var artList = from tempArticle in bd.Articles
                                  where tempArticle.ArticleState == true
                                  select tempArticle;
                    foreach (Article tempArticle in artList)
                    {
                        ShowArticleInfo(tempArticle);
                    }
                }
            }
        }

        /// <summary>
        /// Método que mostra a lista de Artigos.
        /// </summary>
        public void ShowArticleList()
        {
            Menu m = new Menu();
            Console.Clear();
            Console.WriteLine("-- LISTA DE ARTIGOS --\n");
            showUnavailable = true;
            ArticleList();
            m.BackENTER();
        }

        /// <summary>
        /// Método para atualizar os dados do artigo: nome, preço, stock e estado.
        /// </summary>
        public void UpdateArticle()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            Menu m = new Menu();
            Console.Clear();
            Console.WriteLine("-- ATUALIZAR ARTIGO --\n");
            showUnavailable = true;
            ArticleList();
            if (listEmpty == true)
            {
                m.BackENTER();
            }
            else
            {
                Console.WriteLine("\nDigite o código do artigo que pretende actualizar.");
                Console.WriteLine("OU");
                m.BackXENTER();
                string exit = Console.ReadLine();
                int selectedCODE = 0;

                if (exit == "x" || exit == "X")
                {
                    m.ManageArticles();
                }
                else
                {
                    try
                    {
                        selectedCODE = Convert.ToInt32(exit);
                        tempArticle = (from b in bd.Articles
                                       where b.ArticleCode == selectedCODE
                                       select b).FirstOrDefault();

                        if (tempArticle is Article)
                        {
                            Console.Clear();
                            Console.WriteLine($"\n-- Actualizar dados do artigo: {tempArticle.ArticleName.ToUpper()} --");
                            Console.WriteLine("\n\nDados atuais do artigo:\n");
                            ShowArticleInfo(tempArticle);
                            ChooseInfoUpdate(tempArticle.ArticleName);
                            bd.SaveChanges();
                            m.bdUpdatedBackENTER("artigo atualizado com sucesso!");
                        }

                        else
                        {
                            Error e = new Error();
                            e.NoOption();
                            UpdateArticle();
                        }
                    }
                    catch (System.FormatException)
                    {
                        e.FormatNumbers();
                        UpdateArticle();
                    }
                }
            }
            
        }

        /// <summary>
        /// Método para criar um novo artigo.
        /// </summary>
        public void NewArticle()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            Console.Clear();
            Console.WriteLine("-- Criar Novo Artigo --\n");
            tempArticle = new Article();

            Console.WriteLine("--------------------------------");
            Console.WriteLine("--> Lista de Artigos Existentes");
            Console.WriteLine("--------------------------------\n");
            showUnavailable = true;
            ArticleList();
            Console.WriteLine("\nDigite um código numérico para o seu novo artigo");
            Console.Write("\nCÓDIGO: ");
            int newCODE = 0;
            try
            {
                newCODE = Convert.ToInt32(Console.ReadLine());
                Article tempArticle1 = new Article();

                tempArticle1 = (from b in bd.Articles
                                where b.ArticleCode == newCODE
                                select b).FirstOrDefault();

                if (tempArticle1 is Article)
                {
                    Console.Clear();
                    Console.WriteLine($"\n\n----------------   >>> CÓDIGO '{newCODE}' JÁ EXISTENTE!  <<<   ----------------\n\n\n");
                    ShowArticleInfo(tempArticle1);
                    Console.WriteLine($"\nDigite um código numérico diferente para o seu novo artigo.\nPrima [ENTER] para tentar novamente.");
                    Console.ReadLine();
                    NewArticle();
                }

                else
                {
                    Menu m = new Menu();
                    tempArticle.ArticleCode = newCODE;
                    CollectNewArticleInfo();
                    bd.Articles.Add(tempArticle);
                    bd.SaveChanges();
                    m.bdUpdatedBackENTER("novo artigo adicionado com sucesso!");
                }
            }
            catch (System.FormatException)
            {
                e.FormatNumbers();
                NewArticle();
            }
        }



        /// <summary>
        /// Método para atualizar o NOME do Artigo.
        /// </summary>
        public void UpdateName()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            Console.Write("\nNOME: ");
                tempArticle.ArticleName = Console.ReadLine();
            
            if (tempArticle.ArticleName.Length > 50)
            {
                e.StringLimit(50);
                UpdateName();
            }
            bd.Articles.Update(tempArticle);
            bd.SaveChanges();
        }

        /// <summary>
        /// Método para atualizar o PREÇO do Artigo.
        /// </summary>
        public void UpdatePrice()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            Console.Write("\nPREÇO: ");
            try
            {
                tempArticle.ArticlePrice = Math.Round(Convert.ToSingle(Console.ReadLine()), 2);

                if (tempArticle.ArticlePrice <= 0.004)
                {
                    e.NumberRequirement("POSITIVO com 2 CASAS DECIMAIS, no máximo");
                    UpdatePrice();
                }
            }
            catch (System.FormatException)
            {
                e.FormatNumbers();
                UpdatePrice();
            }
            bd.Articles.Update(tempArticle);
            bd.SaveChanges();
        }

        /// <summary>
        /// Método para atualizar o STOCK do Artigo.
        /// </summary>
        public void UpdateStock()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            Console.Write("\nSTOCK: ");
            try
            {
                tempArticle.ArticleAmount = Convert.ToInt32(Console.ReadLine());  
            }
            catch (System.FormatException)
            {
                e.NumberRequirement("positivo e inteiro");
                UpdateStock();
            }
            if (tempArticle.ArticleAmount < 0)
            {
                e.NumberRequirement("positivo e inteiro");
                UpdateStock();
            }
            bd.Articles.Update(tempArticle);
            bd.SaveChanges();
        }


        /// <summary>
        /// Método para atualizar o ESTADO do Artigo.
        /// </summary>
        public void UpdateState()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            short state;
            Console.Write("\nESTADO ( 0-Indisponível | 1-Disponível ): ");
            try
            {
                state = Convert.ToInt16(Console.ReadLine());
            }
            catch
            {
                e.FormatNumbers();
                state = -1;
            }
            if (state == 1 || state == 0)
            {
                tempArticle.ArticleState = Convert.ToBoolean(state);
            }
            else
            {
                Console.WriteLine("Inserção Inválida -> Deve apenas digitar: '0'- Indisponível   OU   '1'- Disponível");
                UpdateState();
            }
            bd.Articles.Update(tempArticle);
            bd.SaveChanges();
        }

        /// <summary>
        /// Método para abrir MENU de escolha de informações do artigo a actualizar.
        /// </summary>
        public void ChooseInfoUpdate(string articleName)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Error e = new();
            bool exiT = false;

            while (exiT == false)
            {
                Article a = new Article();
                Console.Clear();
                Console.WriteLine($"-- ESCOLHER DADOS PARA ATUALIZAR:--\n");
                Console.Write($"{tempArticle.ToString()}");
                string state = tempArticle.ArticleState == true ? "Disponível" : "Indisponível";
                Console.WriteLine($", Estado: {state}\n");

                Console.WriteLine("1-Nome\n2-Preço\n3-Stock\n4-Estado(disponível | indisponível)");
                Console.WriteLine("\nDigite um algarismo de 1 a 4 para escolher a informação a actualizar\nOU\n'X', para voltar ao Menu Anterior.");

                string exit = Console.ReadLine();
                int option = 0;

                if (exit == "x" || exit == "X")
                {
                    UpdateArticle();
                }
                else
                {
                    try
                    {
                        option = Convert.ToInt32(exit);
                        if (option < 1 || option > 4)
                        {
                            e.ErrorMenu();
                        }
                    }
                    catch (System.FormatException)
                    {
                        e.ErrorMenu();
                    }
                    ArticleInfo choice = (ArticleInfo)option;
                    switch (choice)
                    {
                        case ArticleInfo.Name:
                            a.UpdateName();
                            break;
                        case ArticleInfo.Price:
                            a.UpdatePrice();
                            break;
                        case ArticleInfo.Stock:
                            a.UpdateStock();
                            break;
                        case ArticleInfo.State:
                            a.UpdateState();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Método para recolher os dados do novo artigo: nome, preço, stock e estado.
        /// </summary>
        public void CollectNewArticleInfo()
        {
            Console.Write("\n\nDigite as informações do artigo.");

            NewName();
            NewPrice();
            NewStock();
            NewState();
        }

        /// <summary>
        /// Método para gravar o NOME do novo Artigo na variável tempArticle
        /// </summary>
        public void NewName()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            Console.Write("\nNOME: ");
            tempArticle.ArticleName = Console.ReadLine();

            if (tempArticle.ArticleName.Length > 50)
            {
                e.StringLimit(50);
                NewName();
            }
        }

        /// <summary>
        /// Método para gravar o PREÇO do novo Artigo na variável tempArticle
        /// </summary>
        public void NewPrice()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            Console.Write("\nPREÇO: ");
            try
            {
                tempArticle.ArticlePrice = Math.Round(Convert.ToSingle(Console.ReadLine()), 2);

                if (tempArticle.ArticlePrice <= 0.004)
                {
                    e.NumberRequirement("POSITIVO com 2 CASAS DECIMAIS, no máximo.");
                    NewPrice();
                }
            }
            catch (System.FormatException)
            {
                e.FormatNumbers();
                NewPrice();
            }
 
        }
        /// <summary>
        /// Método para gravar o STOCK do novo Artigo na variável tempArticle
        /// </summary>
        public void NewStock()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            Console.Write("\nSTOCK: ");
            try
            {
                tempArticle.ArticleAmount = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                e.NumberRequirement("positivo e inteiro");
                NewStock();
            }
            if (tempArticle.ArticleAmount < 0)
            {
                e.NumberRequirement("positivo e inteiro");
                NewStock();
            }
        }

        /// <summary>
        /// Método para gravar o ESTADO do novo Artigo na variável tempArticle
        /// </summary>
        public void NewState()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();

            short state;
            Console.Write("\nESTADO ( 0-Indisponível | 1-Disponível ): ");
            try
            {
                state = Convert.ToInt16(Console.ReadLine());
            }
            catch
            {
                e.FormatNumbers();
                state = -1;
            }
            if (state == 1 || state == 0)
            {
                tempArticle.ArticleState = Convert.ToBoolean(state);
            }
            else
            {
                Console.WriteLine("Inserção Inválida -> Deve apenas digitar: '0'- Indisponível   OU   '1'- Disponível");
                NewState();
            }
        }
    }
}
