using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace Turismo.Models
{
    public class Usuario
    {
        
        //Variavéis com nome das colunas do bancos de dados
        public int Id {get;set;}
        public string Nome {get;set;}

        public string Login {get;set;}

        public string Senha {get;set;}

        public string DataNascimento {get;set;}

        public void AdicionaUsuario(Usuario user){

            //define a string de conexão
                          
            string stringconexao = "Database=turismo;Data Source=localhost;User Id=root";
            
            MySqlConnection conexao = new MySqlConnection(stringconexao);

            //abrir a conexão
            conexao.Open();

            //string de query

            string query = "Insert into Usuario(Nome,Login,Senha,DataNascimento) values('"+user.Nome+"','"+user.Login+"','"+user.Senha+"','"+user.DataNascimento+"' )";


            MySqlCommand comando = new MySqlCommand(query, conexao);
            comando.ExecuteNonQuery();

            //fechar a conexão
            conexao.Close();
        }

        public List<Usuario> Listar() {

            //lista para amarzenar os Usuários
            List<Usuario> Users = new  List<Usuario>();
            
            //String de conexão com banco de dados
            string stringconexao = "Database=turismo;Data Source=localhost;User id=root";


            MySqlConnection conexao = new MySqlConnection(stringconexao);

            //abre a conexão
            conexao.Open();

            //Query que seleciona todos os usuários cadastrados
            string query = "Select * from Usuario";

            MySqlCommand comando = new MySqlCommand(query,conexao);

            //Lê os dados da busca
            MySqlDataReader resultado = comando.ExecuteReader();

          

             //Adicona o termos na lista
             while(resultado.Read()){

                Usuario us = new Usuario();

                us.Id  = resultado.GetInt32("Id");      
                us.Nome = resultado.GetString("Nome");
                us.Login = resultado.GetString("Login");
                us.Senha = resultado.GetString("Senha");
                us.DataNascimento = Convert.ToDateTime(resultado["DataNascimento"]).ToString("dd/MM/yyyy");

                Users.Add(us);


             }

            //Fecha a conexao
            resultado.Close();
            conexao.Close();
              
            //retorna a lista
            return Users;
        }

        

    }
}