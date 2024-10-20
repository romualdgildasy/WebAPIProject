using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Dtos.Stockage;
using WebAPIProject.Models;

namespace WebAPIProject.Mappers
{
    public static class StockageMappers
    {
        public static StockageDto ToStockageDto(this Stockage stockageModel)
        {
            return new StockageDto
            {
                Id = stockageModel.Id,
                Symbole = stockageModel.Symbole,
                NomCompagnie = stockageModel.NomCompagnie,
                Acheter = stockageModel.Acheter,
                DerniereDividente = stockageModel.DerniereDividente,
                Industrie = stockageModel.Industrie,
                CapitalisationBoursiere = stockageModel.CapitalisationBoursiere,
                Comments = stockageModel.Comments.Select(c => c.ToCommentsDto()).ToList()
            };
        }

        public static Stockage ToStockageFromCreateDto(this CreateStockageRequestDto stockageDto)
        {
            return new Stockage
            {
                Symbole = stockageDto.Symbole,
                NomCompagnie= stockageDto.NomCompagnie,
                Acheter = stockageDto.Acheter,
                DerniereDividente = stockageDto.DerniereDividente,
                Industrie = stockageDto.Industrie,
                CapitalisationBoursiere = stockageDto.CapitalisationBoursiere,
            };
        }
    }
} 