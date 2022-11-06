using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turismo.Models
{
    public class Dados
    {
        public static Usuario usuario {get;set;}
        public static PacotesTuristicos turismo {get;set;}
        public static FaleConosco fale {get;set;}


        static Dados(){
            usuario = new Usuario();
            turismo = new PacotesTuristicos();
            fale = new FaleConosco();
        }

    }
}