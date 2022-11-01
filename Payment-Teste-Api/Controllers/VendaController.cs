using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment_Teste_Api.Context;
using Payment_Teste_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Payment_Teste_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
       private readonly VendaContext _context;
       public VendaController(VendaContext context)
       {
        _context = context;
       }

        [HttpPost]
        public IActionResult Create (Venda venda)
        {
         venda.StatusVenda = 0;
         _context.Add(venda);
         _context.SaveChanges();

         return Ok(venda);
        }
        [HttpGet("Procurar{id}")]
        public ActionResult<Venda> Procurar(int id)
        {
            var venda = _context.Vendas
            .Include(x => x.vendedor)
            .Where(x => x.vendedor.Id == id)
            .Include(x => x.ListaItens)
            .Where(x => x.Id == id)
            .FirstOrDefault();

            return Ok(venda);
        }
        [HttpPut("Atualizar{id}")]
        public ActionResult<Venda> Atualizar(int id, EnumStatusVenda status)
        {
            var venda = _context.Vendas
            .Include(x => x.vendedor)
            .Where(x => x.vendedor.Id == id)
            .Include(x => x.ListaItens)
            .Where(x => x.Id == id)
            .FirstOrDefault();

            if (venda.StatusVenda == EnumStatusVenda.AguardandoPagamento && (status == EnumStatusVenda.PagamentoAprovado || status == EnumStatusVenda.Cancelada))
            {
                venda.StatusVenda = status;
            }
            else if (venda.StatusVenda == EnumStatusVenda.PagamentoAprovado && (status == EnumStatusVenda.EnviadoParaTransportadora || status == EnumStatusVenda.Cancelada))
            {
                venda.StatusVenda = status;
            }
            else if(venda.StatusVenda == EnumStatusVenda.EnviadoParaTransportadora &&(status == EnumStatusVenda.Entregue ))
            {
                venda.StatusVenda = status;
            }
            else
            {
                return BadRequest(new {Erro = "Transação não permitida, verifique quais transações de status não são permitidas e tente nvamente"});
            }

            _context.Vendas.Update(venda);
            _context.SaveChanges();

            return Ok(venda);
        }


    }
   
}
