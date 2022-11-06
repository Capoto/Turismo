using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Turismo.Models;
using Microsoft.AspNetCore.Http;
using MySqlConnector;
using Microsoft.AspNetCore.Session;

namespace Turismo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

    public IActionResult Registrar()
    {

        return View();
    }

    public IActionResult Index()
    {

        return View();
    }


    public IActionResult ConfirmaCadastro()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
            return View();
    }


    public IActionResult MessageConfirma()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
            return View();
    }

    [HttpPost]
        public IActionResult Registrar(Usuario newuser)
    {

        Dados.usuario.AdicionaUsuario(newuser); 
        return View("ConfirmaRegistro");
    }

    public IActionResult DestinoCerto()
    {
        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        return View();
    }

    public IActionResult DestinoCertos()
    {
        
        return View();
    }

    public IActionResult DescricaoPacotes()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");

        return View();
    }

     public IActionResult DescricaoPacotes2()
    {

        
        return View();
    }


    public IActionResult LocaisInclusos()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        return View();
    }

    public IActionResult LocaisInclusos2()
    {

      
        return View();
    }

    public IActionResult Pacotes()
    {

        List<PacotesTuristicos> x = Dados.turismo.Listar();
        return View(x);
    }

    public IActionResult ConfirmaRegistro()
    {

        
        return View();
    }

    public IActionResult EditaPacotes()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        List<PacotesTuristicos> x = Dados.turismo.Listar();
        
        return View(x);
    }

    public IActionResult EditaPacotes2()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        List<PacotesTuristicos> x = Dados.turismo.Listar();
        
        return View(x);
    }

    [HttpPost]
    public IActionResult MudaPacotes(int id)
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
       
        
        return View("MudaPacotes");
    }
    
    [HttpGet]
    public IActionResult MudaPacotes(int id,PacotesTuristicos pacotes)
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
       
        Dados.turismo.Editar(id,pacotes);
        
        return RedirectToAction("EditaPacotes2");
    }



    public IActionResult ExcluirPacotes()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        List<PacotesTuristicos> x = Dados.turismo.Listar();
        
        return View(x);
    }

    [HttpPost]
    public IActionResult ExcluirPacotes(int id)
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
       
        Dados.turismo.Excluir(id);
        return RedirectToAction("ExcluirPacotes");
    }

    public IActionResult ExcluirPacotes2()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        List<PacotesTuristicos> x = Dados.turismo.Listar();
        
        return View(x);
    }

    [HttpPost]
    public IActionResult ExcluirPacotes2(int id)
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
       
        Dados.turismo.Excluir(id);
        return RedirectToAction("ExcluirPacotes");
    }


    public IActionResult MenssagensRecebidas()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        List<FaleConosco> x = Dados.fale.Listar();
        
        return View(x);
    }

    [HttpPost] 
    public IActionResult MenssagensRecebidas(int Id)
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        
        Dados.fale.Deletar(Id);   
        
        return RedirectToAction("MenssagensRecebidas");
    }


    public IActionResult FaleConosco()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        
        return View();
    }

    [HttpPost]
    public IActionResult FaleConosco(FaleConosco falei)
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        falei.UsuarioId = HttpContext.Session.GetInt32("Id");
        Dados.fale.Adicionar(falei);
        return View("MessageConfirma");
    }


    public IActionResult AdminIndex()
    {
        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        return View();
    }
    
    public IActionResult PacotesCadastrados()
    {

        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");

        List<PacotesTuristicos> z = Dados.turismo.Listar();    
        return View(z);
    }
    

    public IActionResult Login()
    {
       
        return View();
    }

    [HttpPost]
    public IActionResult Login(Usuario user)
    {

        

            string stringconexao = "Database=turismo;Data Source=localhost;User Id=root;Allow User Variables=True";

            MySqlConnection conexao = new MySqlConnection(stringconexao);

            conexao.Open();

            string query = "Select * from Usuario where Login = @Login and Senha =  @Senha";
            
            MySqlCommand myCommand = new MySqlCommand(query);
            myCommand.Connection = conexao;
            myCommand.Parameters.AddWithValue("@Senha", user.Senha);
            myCommand.Parameters.AddWithValue("@Login", user.Login);


            if( user.Login=="Admin" && user.Senha=="Admin" ){
                HttpContext.Session.SetString("Login", user.Login);
                return Redirect("AdminIndex");     
            }
            else{

            MySqlDataReader reader = myCommand.ExecuteReader();

             if (reader.Read()){
                HttpContext.Session.SetString("Login", user.Login);
                HttpContext.Session.SetInt32("Id", reader.GetInt32("Id"));
                
                return Redirect("DestinoCerto");
                
                }
            else{
                ViewBag.Mensagem = "Falha no login";
                conexao.Close();
                }

            }
        


        return View();
    }


    public IActionResult CadastraPacotes()
    {
        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        return View();
    }

    [HttpPost]
    public IActionResult CadastraPacotes(PacotesTuristicos pacotes)
    {
        if(HttpContext.Session.GetString("Login") == null)
            return RedirectToAction("Login");
        pacotes.Usuario =   HttpContext.Session.GetInt32("Id");
        Dados.turismo.Cadastrar(pacotes);
        return View("ConfirmaCadastro");
    }
    public IActionResult Privacy()
    {
        List<Usuario> x = Dados.usuario.Listar();   
        return View(x);
    }


        

        
    }
}
