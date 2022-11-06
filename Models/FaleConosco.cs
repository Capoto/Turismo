using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;


namespace Turismo.Models
{
    public class FaleConosco
    {
        public int Id {get;set;}

        public string Nome {get;set;}

        public string Mensagem {get;set;}

        public string Email {get;set;}

        public int? UsuarioId {get;set;}

       
        public void Adicionar(FaleConosco fale){

            string stringconexao = "Database=turismo;Data Source=localhost;User Id=root";

            MySqlConnection conexao = new MySqlConnection(stringconexao);

            conexao.Open();

            string query = "Insert into FaleConosco(Nome,Mensagem,Email,UsuarioId) values(@Nome,@Mensagem,@Email,@UsuarioId)";

          
            MySqlCommand comando = new MySqlCommand(query,conexao);  
            
            comando.Parameters.AddWithValue("@Nome", fale.Nome);
            comando.Parameters.AddWithValue("@Mensagem", fale.Mensagem);
            comando.Parameters.AddWithValue("@Email", fale.Email);
            comando.Parameters.AddWithValue("@UsuarioId", fale.UsuarioId );
           
           
            comando.ExecuteNonQuery();

            //fechar a conexão
            conexao.Close();

        }


        public void Deletar(int fale){

            string stringconexao = "Database=turismo;Data Source=localhost;User Id=root";

            MySqlConnection conexao = new MySqlConnection(stringconexao);

            conexao.Open();
           
            string query = "DELETE from FaleConosco where Id = @Id";

          
            MySqlCommand comando = new MySqlCommand(query,conexao);  
            
            comando.Parameters.AddWithValue("@Id", fale);
            
           
           
            comando.ExecuteNonQuery();

            //fechar a conexão
            conexao.Close();

        }

        public List<FaleConosco> Listar(){

            List<FaleConosco>  falar = new  List<FaleConosco>();

            string stringconexao = "Database=turismo;Data Source=localhost;User Id=root";

            string query = "Select * from Faleconosco";

            MySqlConnection conexao = new MySqlConnection(stringconexao);

            conexao.Open();

            MySqlCommand comando = new MySqlCommand(query,conexao);

            MySqlDataReader  resultado = comando.ExecuteReader();

            while(resultado.Read()){

                FaleConosco fale = new FaleConosco();


                fale.Id = resultado.GetInt32("Id");                  
                fale.Nome = resultado.GetString("Nome");
                fale.Mensagem = resultado.GetString("Mensagem");
                fale.Email = resultado.GetString("Email");
                fale.UsuarioId = resultado.GetInt32("UsuarioId");

                falar.Add(fale);
            }

            resultado.Close();
            conexao.Close();

            return falar;        
        }


    }
}