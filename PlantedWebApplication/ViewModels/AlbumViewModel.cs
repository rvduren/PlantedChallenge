using System.Collections.Generic;
using PlantedWebApplication.Models;

namespace PlantedWebApplication.ViewModels
{
    public class AlbumViewModel
    {
        public string AlbumName { get; set; }
        public List<AlbumPrice> Prices { get; set; }
        
        public List<Album> Albums { get; set; }
    }
}