using System.Web.Mvc;
using PlantedWebApplication.Repository;
using PlantedWebApplication.ViewModels;

namespace PlantedWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly AlbumRepository _albumRepository;

        public HomeController()
        {
            _albumRepository = new AlbumRepository();
        }

        public ActionResult Index(int discogsId = 0)
        {
            AlbumViewModel result = null;
            var allAlbums = _albumRepository.GetAlbums();

            result = discogsId == 0 ? GetListWithAlbumsToChooseFrom() : GetSelectedAlbum(discogsId);

            return View(result);
        }

        private AlbumViewModel GetListWithAlbumsToChooseFrom()
        {
            var result = new AlbumViewModel
            {
                Albums = _albumRepository.GetAlbums()
            };
            return result;
        }

        private AlbumViewModel GetSelectedAlbum(int discogsId)
        {
            var result = new AlbumViewModel
            {
                AlbumName = _albumRepository.GetAlbumName(discogsId),
                Prices = _albumRepository.GetAlbumPrices(discogsId),
                Albums = _albumRepository.GetAlbums()
            };
            return result;
        }
    }
}