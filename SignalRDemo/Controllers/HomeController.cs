using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SignalRDemo.Models;
using SpotifyAPI.Web.Models;

namespace SignalRDemo.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            using (var db = new MusicContext())
            {
                var savedTrack = new SavedTrack();

                db.SavedTrack.Add(savedTrack);
                db.SaveChanges();

                // Display all Blogs from the database 
                var query = db.Track.First();

            }

            return View();
        }

        public class MusicContext : DbContext
        {
            /// <summary>
            /// 
            /// </summary>
            public DbSet<SavedTrack> SavedTrack { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<AudioFeatures> AudioFeatures { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<AvailabeDevices> AvailabeDevices { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<BasicModel> BasicModel { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<Category> Category { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<CategoryPlaylist> CategoryPlaylist { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<Device> Device { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<FullAlbum> FullAlbum { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<FullTrack> FullTrack { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<FullArtist> FullArtist { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<FullPlaylist> FullPlaylist { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<SimpleArtist> SimpleArtist { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<SimpleTrack> SimpleTracks { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DbSet<SimpleAlbum> SimpleAlbum { get; set; }
        }
    }
}
