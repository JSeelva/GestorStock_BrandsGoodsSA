using System;

namespace GestorStock_BrandsGoodsSA.Models
{
    /// <summary>
    /// Classe com métodos para tratamento de erros.
    /// </summary>
    class Error
    {
        /// <summary>
        /// Método construtor da classe Error
        /// </summary>
        public Error()
        {
        }

        public void ErrorMenu()
        {
            Console.Clear();
            Console.WriteLine($"\nDeve digitar um dos algarismos apresentados ou 'X' para fazer a sua escolha.\n\nPrima [ENTER] para escolher de novo.");
            Console.ReadLine();
        }
       
        public void FormatNumbers()
        {
            Console.Clear();
            Console.WriteLine($"\nSeleção incorrecta de digitos.\nDeve introduzir 1 ou mais algarismos.\n\nPrima [ENTER] para escolher de novo.");
            Console.ReadLine();
        }

        public void ErrorFormatLetters()
        {
            Console.Clear();
            Console.WriteLine($"\nSeleção incorrecta de digitos.\nDeve introduzir 4 letras.\n\nPrima [ENTER] para escolher de novo.");
            Console.ReadLine();
        }

        public void NoOption()
        {
            Console.Clear();
            Console.WriteLine($"\nOpção inexistente.\nPrima [ENTER] para escolher de novo.");
            Console.ReadLine();
        }

        public void UnavailableOrInexistent()
        {
            Console.Clear();
            Console.WriteLine($"\nArtigo INEXISTENTE ou em estado INDISPONÍVEL\nPrima [ENTER] para escolher de novo.");
            Console.ReadLine();
        }

        /// <summary>
        /// Método que trata erro de limite excedido. Recebe argumento limit.
        /// </summary>
        /// <param name="limit">Número máximo de caracteres a incluir na mensagem de tratamento de erro.</param>
        public void StringLimit(short limit)
        {
            Console.Clear();
            Console.WriteLine($"\nLimite de caractéres excedido. Máximo de {limit} caractéres.\n Prima [ENTER] para repetir.");
            Console.ReadLine();
        }

        /// <summary>
        /// Método que trata erro de formato. Recebe argumento requirement.
        /// </summary>
        /// <param name="requirement">Tipo de informação requerida para armazenamento em variável.</param>
        public void NumberRequirement(string requirement)
        {
            Console.Clear();
            Console.WriteLine($"\nEste tipo de informação requer um valor {requirement}. Apenas são aceites números.\n Prima [ENTER] para repetir.");
            Console.ReadLine();
        }

        public void FormatNumbersInt()
        {
            Console.Clear();
            Console.WriteLine($"\nSeleção incorrecta de digitos.\nDeve introduzir 1 número inteiro.\n\nPrima [ENTER] para escolher de novo.");
            Console.ReadLine();
        }
    }
}
