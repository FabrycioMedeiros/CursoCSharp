using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CadastroDeClientes
{
    class Program
    {
        static Dictionary<int, string> _cadastro = new Dictionary<int, string>();
        static string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"clientes.txt");

        static void Main(string[] args)
        {
            int opcao = 0;
            LerArquivo();
            while (opcao != 10)
            {
                Cabecalho("Menu Principal");
                Menu();
                Console.Write("Digite um opção: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Opção Inválida, digite novamente!");                    
                    Console.ReadKey();                    
                }
                SelecionarOpcaoDoMenu(opcao);
            }            
        }

        /// <summary>
        /// Mostrar o Cabelho do Menu principal ou da opção selecionada
        /// </summary>
        /// <param name="titulo">Titulo atual da ação</param>
        static void Cabecalho(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("==================================================");
            Console.WriteLine("= "+titulo);
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        /// <summary>
        /// Mostra as opções do Menu
        /// </summary>
        static void Menu()
        {            
            Console.WriteLine(" 1 - Cadastrar Cliente");
            Console.WriteLine(" 2 - Alterar dados de um cliente");
            Console.WriteLine(" 3 - Excluir dados de um cliente");
            Console.WriteLine(" 4 - Listar todos os clientes");
            Console.WriteLine(" 5 - Consultar todos os clientes ativos");
            Console.WriteLine(" 6 - Informar a média da renda anual dos clientes ativos");
            Console.WriteLine(" 7 - Informar os aniversariantes do dia");
            Console.WriteLine(" 8 - Pesquisar um cliente pelo código");
            Console.WriteLine(" 9 - Pesquisar um cliente pelo nome");
            Console.WriteLine(" 10 - Sair");
            Console.WriteLine();
        }
        /// <summary>
        /// Chama a função escolhida pelo usuário
        /// </summary>
        /// <param name="opcao">Opção digitada pelo usuário</param>
        static void SelecionarOpcaoDoMenu ( int opcao )
        {
            int codigo;
            switch (opcao)
            {
                case 1:
                    CriarNovoCliente();
                    break;
                case 2:
                    AlterarCliente();
                    break;             
                case 3:                  
                    inicioCase3:
                    Console.Write("Informe o código do cilente : ");
                    try{
                        codigo = int.Parse(Console.ReadLine());      
                    }
                    catch(Exception){
                        Console.WriteLine("Erro na leitura do código. Favor inserir apenas números. Tente novamente");
                        Console.WriteLine("");
                        goto inicioCase3;
                    }
                    ExcluirCliente(codigo);                   
                    break;
                case 4:
                    ConsultarTodosClientes();
                    break;
                case 5:
                    ConsultarTodosClientesAtivos();
                    break;
                case 6:
                    InformarRendaMedia();
                    break;
                case 7:
                    InformarAniversarios();
                    break;
                case 8:
                    inicioCase8:
                    Console.WriteLine("Qual o código do cliente desejado?");
                    try{
                        codigo = int.Parse(Console.ReadLine());      
                    }
                    catch(Exception){
                        Console.WriteLine("Erro na leitura do código. Favor inserir apenas números. Tente novamente");
                        Console.WriteLine("");
                        goto inicioCase8;
                    }
                    ConsultarClienteCodigo(codigo);
                    break;
                case 9:
                    Console.WriteLine("Qual o nome do cliente desejado?");
                    string nome = Console.ReadLine();                
                    ConsultarClienteNome(nome);
                    break;
                case 10:
                    break;
                default:
                    Console.WriteLine("Opção fora do intervalor de 1 até 10, por favor, digite novamente!!!");
                    Console.ReadKey();
                    break;
            }
        }

        static void CriarNovoCliente()
        {
            Cabecalho("Inserir um novo cliente");

            //NOME
            Console.WriteLine("Nome..........: ");
            string nome = Console.ReadLine();

            //CELULAR
            inicioCel:
            Console.WriteLine("Celular.......: (utilize o formato ddd+número digitando apenas números");
            string celular = Console.ReadLine();

            long teste;
            try{
                teste = long.Parse(celular);
            }
            catch(Exception){
                Console.WriteLine("Número inserido incorretamente. Por favor, tente novamente");
                Console.WriteLine("Exemplo: 22999999999");
                goto inicioCel;
            }

            //EMAIL
            inicioEmail:
            Console.WriteLine("e-mail........: ");
            string email = Console.ReadLine();

            try{
                string[] teste2 = email.Split("@");
                if (teste2.Length > 0 && teste2[0].Length > 0 && teste2[1].Length > 0){
                    string[] teste3 = teste2[1].Split(".");
                    if (teste3[0].Length == 0 || teste3[1].Length == 0){
                        throw new Exception();
                    }
                }
            }
            catch{
                Console.WriteLine("Email inserido incorretamente. Por favor, tente novamente");
                Console.WriteLine("Exemplo: email@dominio.com");
                goto inicioEmail;                                   
            }

            //DATA
            inicioData:
            Console.WriteLine("Dta Nascimento: (utilize o formato DD/MM/AAAA");
            string dtaNascimento = Console.ReadLine();

            try{
                string[] testeData = dtaNascimento.Split("/");
                if (testeData.Length != 3 || testeData[0].Length != 2 || testeData[1].Length != 2 || testeData[2].Length != 4){
                    throw new Exception();
                }
            }
            catch(Exception){
                Console.WriteLine("Data inserida incorretamente. Por favor, tente novamente");
                Console.WriteLine("Exemplo: 01/12/1950");
                goto inicioData;                                  

            }

            //RENDA
            inicioRenda:
            Console.WriteLine("Renda Anual...: ");
            float rendaAnual;
            try{
                rendaAnual = float.Parse(Console.ReadLine());
            }
            catch(Exception){
                Console.WriteLine("Valor inserido incorretamente. Por favor, tente novamente");
                Console.WriteLine("Exemplo: 2000,50");
                goto inicioRenda;
            }

            //ATIVO
            inicioAtivo:
            Console.WriteLine("Ativo.........: (0 para inativo e 1 para ativo)");
            
            int ativo;
            try{
                ativo = int.Parse(Console.ReadLine());
                if (ativo != 1 && ativo != 0){
                    throw new Exception();
                }
            }
            catch(Exception){
                Console.WriteLine("Valor inserido incorretamente. Por favor, tente novamente");
                Console.WriteLine("Formato: 0 para inativo e 1 para ativo");
                Console.ReadKey();
                goto inicioAtivo;
            }

            //-----------------------------------------
            // Obter um codigo disponivel na lista
            //-----------------------------------------
            int codigo = ObterNovoCodigoCliente();
            //-----------------------------------------
            string linhaCadastro = $"{codigo};{nome};{celular};{email};{dtaNascimento};{rendaAnual};{ativo}";
            _cadastro.Add(codigo, linhaCadastro);
            GravarDadosArquivo(linhaCadastro);
        }

        static void AlterarCliente()
        {
            

        }

        static void ExcluirCliente(int codigo)
        {
            Cabecalho("Excluir os clientes");
            {
                try
                {
                    foreach (KeyValuePair<int, string> linha in _cadastro)
                    {
                        string[] vetor = linha.Value.Split(";");                                 
                        if (linha.Key == codigo)
                        {
                            _cadastro.Remove(codigo);
                            System.IO.File.WriteAllText(_fileName, "");
                            Console.WriteLine("Excluído com sucesso! ");
                        }
                    }
                    foreach (KeyValuePair<int, string> linha in _cadastro)
                    {
                        string[] vetor = linha.Value.Split(";");
                        string linhaCadastro = vetor[0] + ";" + vetor[1] + ";" + vetor[2] + ";" + vetor[3] + ";" + vetor[4] + ";" + vetor[5] + ";" + vetor[6] + ";";
                        GravarDadosArquivo(linhaCadastro); 
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Excluiu erro: {0}", e.Message);
                }
                Console.ReadKey();
            }
        }


        static void InformarRendaMedia()
        {
            Cabecalho("Informar Clientes com Renda Média de ");
            Console.WriteLine("Codigo\t\tNome");
            Console.WriteLine("================================");
            var media = 0;
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {

                string[] vetor = linha.Value.Split(";");
                if (vetor[6] == "1"){
                    media += int.Parse(vetor[5]);
                }
            }
            media = media / _cadastro.Count;
            Console.WriteLine("Renda Média dos Clientes é de " + media);
            Console.ReadKey();
        }

        static void ConsultarTodosClientes()
        {
            Cabecalho("Consultar todos os clientes");
            Console.WriteLine("Codigo\t\tNome");
            Console.WriteLine("================================");
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                string[] vetor = linha.Value.Split(";");
                Console.WriteLine("{0}\t\t{1}", linha.Key, vetor[1]);
            }
            Console.ReadKey();
        }

        static void ConsultarTodosClientesAtivos()
        {
            Cabecalho("Consultar todos os clientes ativos");
            Console.WriteLine("Codigo\t\tNome");
            Console.WriteLine("================================");
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                
                string[] vetor = linha.Value.Split(";");
                if (vetor[6] == "1"){
                    Console.WriteLine("{0}\t\t{1}", linha.Key, vetor[1]);
                }
            }
            Console.ReadKey();
        }

        static int ConsultarClienteCodigo(int codigo)
        {
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                if (linha.Key == codigo){
                    string[] vetor = linha.Value.Split(";");
                    Cabecalho("Consulta de cliente por código");
                    Console.WriteLine("Codigo\t\tNome");
                    Console.WriteLine("================================");
                    Console.WriteLine("{0}\t\t{1}", linha.Key, vetor[1]);
                    Console.ReadKey();
                    return 0;
                }
            }
            Console.WriteLine("Cliente não encontrado");
            Console.ReadKey();
            return 0;
        }

        static int ConsultarClienteNome(string nome)
        {

            Cabecalho("Consulta de cliente por nome");
            Console.WriteLine("Codigo\t\tNome");
            Console.WriteLine("================================");
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                string[] vetor = linha.Value.Split(";");
                if (vetor[1] == nome){
                    Console.WriteLine("{0}\t\t{1}", linha.Key, vetor[1]);
                    Console.ReadKey();
                    return 0;
                }
            }
            Console.WriteLine("Cliente não encontrado");
            Console.ReadKey();
            return 0;
        }

        static void GravarDadosArquivo(string linhaCadastro)
        
        {
            using (StreamWriter outputFile = new StreamWriter(_fileName, true))
            {
                outputFile.WriteLine(linhaCadastro);
            }
        }

        static void LerArquivo()
        {
            foreach (string line in System.IO.File.ReadLines(_fileName))
            {                
                string[] campos = line.Split(";");
                try { 
                _cadastro.Add(int.Parse(campos[0]), line);
                    }
                catch (Exception e)
                {
                    Console.WriteLine("Excluiu erro: {0}", e.Message);
                }
            }
            

        }

        static void InformarAniversarios()
        {            
            string hoje = DateTime.Now.ToShortDateString();
            Console.WriteLine(hoje);
            Cabecalho(" Aniversariantes do dia de hoje:" + hoje);
            Console.WriteLine("Codigo\t\tNome");
            Console.WriteLine("================================");

            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                // Console.WriteLine(hoje);                
                string[] vetor = linha.Value.Split(";");
                try{
                    if (vetor[4].Substring(0,5) == hoje.Substring(0,5)){
                        Console.WriteLine("{0}\t\t{1}", linha.Key, vetor[1]);
                    }
                }
                catch(Exception){
                    //faz nada, só ignora enão mostra
                }
            }
            Console.ReadKey();
        }

        static int ObterNovoCodigoCliente()
        {
            int codigo = 0;
            int codigoDB;
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                codigoDB = linha.Key;
                if (codigoDB > codigo)
                    codigo = codigoDB;
            }
            return ++codigo;
        }


    }
}
