using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Models;
using NSE.Core.DomainObjects.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientesContext _context;

        public ClienteRepository(ClientesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async void Adicionar(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        public async Task<Cliente> ObterPorCpf(string cpf)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
