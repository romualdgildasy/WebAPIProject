using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Models;

namespace WebAPIProject.Interfaces
{
    public interface IFMPService
    {
        Task<Stockage> FindStockageBySymboleAsync(string symbole);
    }
}