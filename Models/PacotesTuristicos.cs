using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace Turismo.Models
{
    public class PacotesTuristicos
    {
        

        public int Id {get;set;}
        public string Nome {get;set;}

        public string Origem {get;set;}

        public string Destino {get;set;}

        public string Atrativos {get;set;}

        public string Saida  {get;set;}

        public string Retorno {get;set;}


        public int? Usuario {get;set;}


        public List<PacotesTuristicos> Listar(){

            string stringconexao = "Database=turismo;Data Source=localhost;User Id=root";

            MySqlConnection conexao = new MySqlConnection(stringconexao);

            conexao.Open();

            string query = "Select * from PacotesTuristicos";

            MySqlCommand comando = new MySqlCommand(query,conexao);

            MySqlDataReader resultado = comando.ExecuteReader();

            List<PacotesTuristicos> Pacotes = new  List<PacotesTuristicos>();

            while(resultado.Read()){

                PacotesTuristicos turismo = new PacotesTuristicos();

                turismo.Id = resultado.GetInt32("Id");
                if(!resultado.IsDBNull(resultado.GetOrdinal("Nome")))    
                    turismo.Nome = resultado.GetString("Nome");
                if(!resultado.IsDBNull(resultado.GetOrdinal("Origem")))
                    turismo.Origem = resultado.GetString("Origem");
                if(!resultado.IsDBNull(resultado.GetOrdinal("Destino")))    
                    turismo.Destino = resultado.GetString("Destino");
                if(!resultado.IsDBNull(resultado.GetOrdinal("Atrativos")))
                    turismo.Atrativos = resultado.GetString("Atrativos");
                if(!resultado.IsDBNull(resultado.GetOrdinal("Saida")))    
                    turismo.Saida = Convert.ToDateTime(resultado["Saida"]).ToString("dd/MM/yyyy");
                if(!resultado.IsDBNull(resultado.GetOrdinal("Retorno")))
                    turismo.Retorno = Convert.ToDateTime(resultado["Retorno"]).ToString("dd/MM/yyyy");
                if(!resultado.IsDBNull(resultado.GetOrdinal("Usuario")))
                    turismo.Usuario = resultado.GetInt32("Usuario");

                Pacotes.Add(turismo);
            }    

            return Pacotes;
        }
        public void Cadastrar(PacotesTuristicos turismo){

            //Define a conexão do banco de dados
            string stringconexao = "Database=turismo;Data Source=localhost;User Id=root";

           
            MySqlConnection conexao = new MySqlConnection(stringconexao);
            

            //abrir a conexao

            conexao.Open();


            string query = "Insert into PacotesTuristicos(Nome,Origem,Destino,Saida,Retorno,Usuario,Atrativos) values('" + turismo.Nome + "' ,'"  + turismo.Origem+"' , '" +turismo.Destino + "' , '" + turismo.Saida +"' , '" + turismo.Retorno +"' , '" + turismo.Usuario +"', '"+turismo.Atrativos+ "' )";

            MySqlCommand comando = new MySqlCommand(query,conexao);

            comando.ExecuteNonQuery();

            conexao.Close(); 

        }

        public void Editar(int id,PacotesTuristicos pacotes){

            string stringconexao = "Database=turismo;Data Source=localhost;User Id=root";

            string query = "Update PacotesTuristicos set  Nome =@Nome ,Origem =@Origem ,Destino =@Destino ,Saida =@Saida ,Retorno =@Retorno  ,Atrativos=@Atrativos where Id=@Id";
            
            MySqlConnection conexao = new MySqlConnection(stringconexao);
            conexao.Open();
            MySqlCommand comando = new MySqlCommand(query,conexao);

            comando.Parameters.AddWithValue("@Id", id);
            comando.Parameters.AddWithValue("@Nome",pacotes.Nome);
            comando.Parameters.AddWithValue("@Origem", pacotes.Origem);
            comando.Parameters.AddWithValue("@Destino", pacotes.Destino);
            comando.Parameters.AddWithValue("@Saida", pacotes.Saida);
            comando.Parameters.AddWithValue("@Retorno", pacotes.Retorno);
            comando.Parameters.AddWithValue("@Atrativos", pacotes.Atrativos);

            comando.ExecuteNonQuery();

            //fechar a conexão
            conexao.Close();

        }

        public void Excluir(int pacotes){


             string stringconexao = "Database=turismo;Data Source=localhost;User Id=root";

            MySqlConnection conexao = new MySqlConnection(stringconexao);

            conexao.Open();
           
            string query = "DELETE from PacotesTuristicos where Id = @Id";

          
            MySqlCommand comando = new MySqlCommand(query,conexao);  
            
            comando.Parameters.AddWithValue("@Id", pacotes);
            
           
           
            comando.ExecuteNonQuery();

            //fechar a conexão
            conexao.Close();


        }
    }
}