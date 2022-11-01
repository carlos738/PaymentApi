using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Payment_Teste_Api.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public  Vendedor vendedor{ get; set; }
        public DateTime Data { get; set; }
        public List<Item> ListaItens { get; set; }
        public EnumStatusVenda StatusVenda { get; set; }
        
    }
}