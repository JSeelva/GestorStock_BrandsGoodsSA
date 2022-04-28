using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace GestorStock_BrandsGoodsSA.Models
{
    /// <summary>
    /// Classe com métodos especificos deste tipo de user (ExportJson(); e ImportJson_AddOrUpdate();
    /// </summary>
    class UserAdmin : User, IUser
     {
        /// <summary>
        /// Enumerado dos items do Menu apresentado a um utilizador Admin
        /// </summary>
        enum MenuAdmin { ManageClientRequests = 1, ManageArticles, ManageClients, ImportStock, ExportStock,Login, Exit }


        /// <summary>
        /// Método que exporta o ficheiro exportedArticles.json com a informação dos artigos na base de dados.
        /// </summary>
        private void ExportJson()
        {
            Menu m = new Menu();
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            Console.WriteLine("-- Exportar Stock --");
            List<Article> articles = new List<Article>();

            foreach (Article tempArticle in bd.Articles)
            {
                articles.Add(tempArticle);
            }

            string ficheiroJson = @"..\..\..\exportedArticles.json";
            // Serializar JSON
            DataContractJsonSerializer jsonSerializerWriter = new DataContractJsonSerializer(articles.GetType());
            FileStream streamWriter = File.Create(ficheiroJson);
            jsonSerializerWriter.WriteObject(streamWriter, articles);
            streamWriter.Close();

            m.bdUpdatedBackENTER("Informações exportadas com sucesso!");
        }


        /// <summary>
        /// Método que importa o ficheiro importArticles.json adicionando ou actualizando os artigos para a base de dados
        /// </summary>  
        private void ImportJson_AddOrUpdate()
        {
            using BrandsGoods_StockContext bd = new BrandsGoods_StockContext();
            Menu m = new Menu();
            Console.WriteLine("-- Importar Stock --");
            List<Article> articles = new List<Article>();
            string ficheiroJson = @"..\..\..\importArticles.json";

            // Desserializar JSON
            DataContractJsonSerializer jsonSerializerReader = new DataContractJsonSerializer(articles.GetType());
            try
            {
                FileStream streamReader = File.OpenRead(ficheiroJson);
                var obj = jsonSerializerReader.ReadObject(streamReader);
                List<Article> newArticles = (List<Article>)obj;
                streamReader.Close();
                Article tempArticle = new Article();
                // Passar resultados para a BD
                foreach (Article article in newArticles)
                {
                    tempArticle = (from b in bd.Articles
                                   where b.ArticleCode == article.ArticleCode
                                   select b).FirstOrDefault();
                    if (tempArticle is Article)
                    {
                        tempArticle.ArticleName = article.ArticleName;
                        tempArticle.ArticlePrice = article.ArticlePrice;
                        tempArticle.ArticleAmount = article.ArticleAmount;
                        tempArticle.ArticleState = true;
                        bd.Articles.Update(tempArticle);
                    }
                    else
                    {
                        bd.Articles.Add(article);
                        article.ArticleState = true;
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("\nAcesso ao ficheiro de importação perdido.\nVerifique se o mesmo existe com o nome exportedArticles.json na devida localização.");
                m.BackENTER();
                m.ChooseMainMenu();
            }
               
            bd.SaveChanges();
            m.bdUpdatedBackENTER("Informações importadas com sucesso!");
        }

        /// <summary>
        /// Apresenta o Menu com acesso a todas as funções, incluindo Importar e Exportar Stock.
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
                Console.WriteLine("1-Gerir Pedidos de Cliente\n2-Gerir Artigos\n3-Gerir Clientes\n4-Importar Stock\n5-Exportar Stock\n6-Log Out\n7-Sair da Aplicação");
                Console.WriteLine("\nDigite um algarismo de 1 a 7 para escolher uma opção, por favor.");

                int option = 0;
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                    if (option < 1 || option > 7)
                    {
                        format.ErrorMenu();
                    }
                }
                catch (System.FormatException)
                {
                    format.ErrorMenu();
                }
                MenuAdmin choice = (MenuAdmin)option;

                switch (choice)
                {
                    case MenuAdmin.ManageClientRequests:
                        m.ManageClientRequests();
                        break;
                    case MenuAdmin.ManageArticles:
                        m.ManageArticles();
                        break;
                    case MenuAdmin.ManageClients:
                        m.ManageClients();
                        break;
                    case MenuAdmin.ImportStock:
                        ImportJson_AddOrUpdate();
                        break;
                    case MenuAdmin.ExportStock:
                        ExportJson();
                        break;
                    case MenuAdmin.Login:
                        User u = new User();
                        u.Login();
                        break;
                    case MenuAdmin.Exit:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
