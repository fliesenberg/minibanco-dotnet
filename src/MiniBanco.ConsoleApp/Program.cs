internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("MiniBanco.ConsoleApp: Motor de Gestão de Contas Bancárias");

        int opcao = 0;
        bool opcaoValida = false;

        do
        {
            Console.WriteLine("1 - Abrir nova conta");
            Console.WriteLine("2 - Registrar movimentação");
            Console.WriteLine("3 - Aplicar fechamento mensal");
            Console.WriteLine("4 - Exibir relatório das contas");
            Console.WriteLine("0 - Sair");

            Console.WriteLine("Digite a opção desejada: ");

            string strOpcao = Console.ReadLine() ?? string.Empty;

            opcaoValida = int.TryParse(strOpcao, out opcao);
            
            if (!opcaoValida)
            {
                Console.WriteLine("Opção inválida!");
            }

        } while (opcao != 0 || !opcaoValida);
    }
}