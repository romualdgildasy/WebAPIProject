using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Dtos.Stockage;
using WebAPIProject.Helpers;
using WebAPIProject.Models;

namespace WebAPIProject.Interfaces
{
    /*Une interface définit un ensemble de méthodes, propriétés, 
    événements ou indexeurs sans implémentation. 
    Elle agit comme un contrat que les classes ou structures implémentant cette 
    interface doivent respecter. Cela garantit que ces classes/structures 
    fournissent des implémentations concrètes des membres définis dans l'interface.*/
    public interface IStockageRepository
    {
        Task <List<Stockage>> GetAllAsync(QueryObject query);

        Task <Stockage ?> GetByIdAsync(int id);

        Task <Stockage> CreateAsync(Stockage stockageModel);

        Task <Stockage ?> UpdateAsync(int id, UpdateStockageRequestDto stockageDto);

        Task <Stockage ?> DeleteAsync(int id);

        /*methode de verification si un stock existe*/
        Task <bool> StockExist(int id);


    }
}