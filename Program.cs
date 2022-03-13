using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Dapper;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using System.Globalization;
using Methods;

namespace CadastroFilmes
{
    class Program
    {
        public const string connectionstring = @"Server=DESKTOP-H7797U1\SQLEXPRESS;
                                                Database=FilmesOnline;
                                                Integrated Security=True;
                                                Encrypt=False";
        static void Main(string[] args)
        {
           using(var connection = new SqlConnection(connectionstring)){ 
            
                methods.Menu(connection);

           }
        }
    }
}

