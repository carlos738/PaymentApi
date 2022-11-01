using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment_Teste_Api.Models;
using Payment_Teste_Api.Context;

namespace Payment_Teste_Api.Context
{
    public class VendaContext : DbContext
    {
        public VendaContext(DbContextOptions<VendaContext> options) : base(options)
        {
            
        }
        public DbSet<Venda> Vendas{get; set; }
    }
}