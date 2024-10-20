using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject.Helpers
{
    /*ici nous allons parametrer nos requetes de données de telle maniere 
    que lorsqu'on envoit les données ça arrive sous de requetes
    */
    /*il permet aussi de faire un filtrage lorsu'une rece=herche par exemple
    ici nous pouvons rechercher a traver le symble et le nom de la comapgnie*/
    public class QueryObject
    {
       public string ? Symbole {get;set;} = null;
       public string? NomCompagnie{get;set;} = null; 

       /*ici nous allons faire un tri*/
       public string? SortBy{get;set;}= null;

       /*faire un tri croissant et decroissant*/
       public bool IsDecsending{get;set;}= false;
       public int PageNumber {get;set;} =1;
       public int PageSize {get;set;}=20;
    }
}