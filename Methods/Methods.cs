using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Dapper;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using System.Globalization;
using CadastroFilmes;

namespace Methods{
        
    public static class methods{

        public static void Menu(SqlConnection connection){

            Console.Clear();

            System.Console.WriteLine("Seja Bem-Vindo ao Cadastro de Filmes");
            System.Console.WriteLine("-------------------------------------");
            System.Console.WriteLine("");

            System.Console.WriteLine("1 - Visualizar todos os filmes");
            System.Console.WriteLine("2 - Visualizar apenas um filme");
            System.Console.WriteLine("3 - Inserir um filme");
            System.Console.WriteLine("4 - Atualizar um filme");
            System.Console.WriteLine("5 - Excluir um filme");
            System.Console.WriteLine("0 - Sair");
            System.Console.WriteLine("");

            try{
                System.Console.Write("Escolha uma opção: ");
                var opcao = int.Parse(Console.ReadLine());

                switch(opcao){
                    case 1: ReadAllFilmes(connection); break;
                    case 2: ReadOneFilme(connection); break;
                    case 3: InsertFilme(connection); break;
                    case 4: UpdateFilme(connection); break;
                    case 5: DeleteFilme(connection); break;
                    case 0: Environment.Exit(0); break;
                    default: Menu(connection); break;
                }
            }catch(Exception){
                System.Console.WriteLine("Opção inválida");
                Console.ReadKey();
            }
        }
        
        static void ReadAllFilmes(SqlConnection connection)
        {
            Console.Clear();
            var filmes = connection.Query<Filmes>("SELECT [Id], [Nome] FROM Filmes");

            System.Console.WriteLine("FILMES");
            System.Console.WriteLine("---------------------");
            foreach(var item in filmes){
                System.Console.WriteLine($"#ID: {item.Id} - {item.Nome}");
            }

            try{
                System.Console.WriteLine("");
                System.Console.WriteLine("Digite 1 para voltar para o menu");
                System.Console.WriteLine("Digite 0 para sair do programa");
                System.Console.Write("Escolha a sua opção: ");
                var opcao2 = int.Parse(Console.ReadLine());

                switch(opcao2){
                    case 1: Menu(connection); break;
                    case 0: Environment.Exit(0); break;
                    default: Menu(connection); break;
                }
            }catch(Exception){
                System.Console.WriteLine("Opção inválida");
                Console.ReadKey();
            }
        }
        static void ReadOneFilme(SqlConnection connection)
        {
            Console.Clear();

            System.Console.WriteLine("VISUALIZAR FILME");
            System.Console.WriteLine("-----------------");
            System.Console.WriteLine("");

            System.Console.Write("Digite o ID do filme: ");
            var Id = int.Parse(Console.ReadLine());

            var filme = connection.Query<Filmes>
            ("SELECT * FROM [Filmes] WHERE [Id] = @Id", new{id=Id});

             Console.Clear();
            System.Console.WriteLine("FILME");
            System.Console.WriteLine("---------------------");
            System.Console.WriteLine("");
     
            foreach(var item in filme){
                System.Console.WriteLine($"ID: {item.Id}");
                System.Console.WriteLine($"NOME: {item.Nome}");
                System.Console.WriteLine($"GÊNERO: {item.Genero}");
                System.Console.WriteLine($"DATA DE LANÇAMENTO: {item.DataDeLancamento}");
                System.Console.WriteLine($"DESCRIÇÃO: {item.Descricao}");
            }

            try{
                System.Console.WriteLine("");
                System.Console.WriteLine("Digite 1 para voltar para o menu");
                System.Console.WriteLine("Digite 0 para sair do programa");
                System.Console.Write("Escolha a sua opção: ");
                var opcao2 = int.Parse(Console.ReadLine());

                switch(opcao2){
                    case 1: Menu(connection); break;
                    case 0: Environment.Exit(0); break;
                    default: Menu(connection); break;
                }
                }catch(Exception){
                    System.Console.WriteLine("Opção inválida");
                    Console.ReadKey();
                }       
        }

        static void InsertFilme(SqlConnection connection){

            Console.Clear();

            System.Console.WriteLine("INSERIR FILME");
            System.Console.WriteLine("--------------");
            System.Console.WriteLine("");

            System.Console.Write("Digite o nome do filme: ");
            string nome = Console.ReadLine();

            System.Console.Write("Digite o gênero do filme: ");
            string genero = Console.ReadLine();

            //System.Console.Write("Digite a data de lançamento do filme: ");
            //DateTime data = DateTime.Parse(Console.ReadLine().ToString());
            //DateTime date = DateTime.ParseExact(data, @"dd/MM/yyyy", CultureInfo.InvariantCulture);
            //data.ToString("dd-MM-yyyy");

            System.Console.Write("Digite a descrição do filme: ");
            string descricao = Console.ReadLine();

            var newFilme = new Filmes(){
                Nome = nome,
                Genero = genero,
                //DataDeLancamento = data,
                Descricao = descricao
            };

            var InsertSql = @"INSERT INTO [Filmes]
                            ([Nome], [Genero], [Descricao]) VALUES(
                              @nome,
                              @genero,
                              @descricao      
                            )";

            var rows = connection.Execute(InsertSql, new{
                newFilme.Nome,
                newFilme.Genero,
                newFilme.DataDeLancamento,
                newFilme.Descricao
            });
            System.Console.WriteLine($"{rows} filmes inseridos");
            System.Console.WriteLine("");
            try{
                System.Console.WriteLine("");
                System.Console.WriteLine("Digite 1 para voltar para o menu");
                System.Console.WriteLine("Digite 0 para sair do programa");
                System.Console.Write("Escolha a sua opção: ");
                var opcao2 = int.Parse(Console.ReadLine());

                switch(opcao2){
                    case 1: Menu(connection); break;
                    case 0: Environment.Exit(0); break;
                    default: Menu(connection); break;
                }
            }catch(Exception){
                System.Console.WriteLine("Opção inválida");
                Console.ReadKey();
            }

        }
    
        static void UpdateFilme(SqlConnection connection){
            Console.Clear();

            System.Console.WriteLine("ATUALIZAR FILME");
            System.Console.WriteLine("-----------------");
            System.Console.WriteLine("");

            System.Console.Write("Digite o ID do Filme: ");
            var id = int.Parse(Console.ReadLine());

            System.Console.Write("Digite o nome do filme: ");
            string nome = Console.ReadLine();

            System.Console.Write("Digite o gênero do filme: ");
            string genero = Console.ReadLine();

            System.Console.WriteLine("Digite a descrição do filme: ");
            string descricao = Console.ReadLine();            

            var filme = new Filmes(){
                Id = id,
                Nome = nome,
                Genero = genero,
                Descricao = descricao
            };

            var UpdateSql = @"UPDATE [Filmes] SET [Nome] = @nome,
                            [Genero] = @genero,
                            [Descricao] = @descricao 
                            WHERE [Id] = @id";
            
            var rows = connection.Execute(UpdateSql, new{
                filme.Id,
                filme.Nome,
                filme.Genero,
                filme.Descricao
            });
            System.Console.WriteLine($"{rows} filmes atualizados");

            try{
                System.Console.WriteLine("");
                System.Console.WriteLine("Digite 1 para voltar para o menu");
                System.Console.WriteLine("Digite 0 para sair do programa");
                System.Console.Write("Escolha a sua opção: ");
                var opcao2 = int.Parse(Console.ReadLine());

                switch(opcao2){
                    case 1: Menu(connection); break;
                    case 0: Environment.Exit(0); break;
                    default: Menu(connection); break;
                }
            }catch(Exception){
                System.Console.WriteLine("Opção inválida");
                Console.ReadKey();
            }
        }
    
        static void DeleteFilme(SqlConnection connection){
            Console.Clear();

            System.Console.WriteLine("EXCLUIR FILME");
            System.Console.WriteLine("--------------");
            System.Console.WriteLine("");

            System.Console.Write("Digite o ID do filme: ");
            var id = int.Parse(Console.ReadLine());

            var filme = new Filmes(){
                Id = id
            };

            var DeleteSql = @"DELETE FROM [Filmes]
                            WHERE [Id] = @id";

            var rows = connection.Execute(DeleteSql, new{
                filme.Id
            });                
            System.Console.WriteLine($"{rows} filme excluido");

            try{
                System.Console.WriteLine("");
                System.Console.WriteLine("Digite 1 para voltar para o menu");
                System.Console.WriteLine("Digite 0 para sair do programa");
                System.Console.Write("Escolha a sua opção: ");
                var opcao2 = int.Parse(Console.ReadLine());

                switch(opcao2){
                    case 1: Menu(connection); break;
                    case 0: Environment.Exit(0); break;
                    default: Menu(connection); break;
                }
            }catch(Exception){
                System.Console.WriteLine("Opção inválida");
                Console.ReadKey();
            }  
        }
    
        

    }
}
