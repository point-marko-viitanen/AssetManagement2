using AssetManagement2.Models;
using AssetManagement2.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetManagement2.Database;
using System.Globalization;

namespace AssetManagement2.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        
        public ActionResult List()
        {
            List<LocatedAssetsViewModel> model = new List<LocatedAssetsViewModel>();

            AssetLocationEntities entities = new AssetLocationEntities();
            try
            {
                List<Assetlocation1> assets = entities.Assetlocations1.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                CultureInfo fiFi = new CultureInfo("fi-Fi");
                foreach (Assetlocation1 asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.Id;
                    view.LocationCode = asset.AssetLocation.Code;
                    view.LocationName = asset.AssetLocation.Name;
                    view.AssetCode = asset.Asset.Code;
                    view.AssetName = asset.Asset.Type + " : "+asset.Asset.Model;
                    view.LastSeen = asset.LastSeen.Value.ToString(fiFi);

                    model.Add(view);

                }
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }

        public ActionResult ListJson()
        {
            List<LocatedAssetsViewModel> model = new List<LocatedAssetsViewModel>();

            AssetLocationEntities entities = new AssetLocationEntities();
            try
            {
                List<Assetlocation1> assets = entities.Assetlocations1.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                CultureInfo fiFi = new CultureInfo("fi-Fi");
                foreach (Assetlocation1 asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.Id;
                    view.LocationCode = asset.AssetLocation.Code;
                    view.LocationName = asset.AssetLocation.Name;
                    view.AssetCode = asset.Asset.Code;
                    view.AssetName = asset.Asset.Type + " : " + asset.Asset.Model;
                    view.LastSeen = asset.LastSeen.Value.ToString(fiFi);

                    model.Add(view);

                }
            }
            finally
            {
                entities.Dispose();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AssignLocation()
        {
            string json = Request.InputStream.ReadToEnd();
            AssignLocationModel inputData =
                JsonConvert.DeserializeObject<AssignLocationModel>(json);

            bool success = false;
            string error = "";

            AssetLocationEntities entities = new AssetLocationEntities();
            try
            {
                // Haetaan ensin paikan id-numero koodin perusteella.
                int locationId = (from l in entities.AssetLocations
                                  where l.Code == inputData.LocationCode
                                  select l.Id).FirstOrDefault();

                // Haetaan laitteen id-numero koodin perusteella.
                int assetId = (from a in entities.Assets
                               where a.Code == inputData.AssetCode
                               select a.Id).FirstOrDefault();

                if ((locationId > 0) && (assetId > 0))
                {
                    // Tallennetaan uusi rivi aikaleiman kanssa kantaan
                    Assetlocation1 newEntry = new Assetlocation1();
                    newEntry.LocationId = locationId;
                    newEntry.AssetId = assetId;
                    newEntry.LastSeen = DateTime.Now;

                    entities.Assetlocations1.Add(newEntry);
                    entities.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                error = error.GetType().Name + ": " + ex.Message;
            }
            finally
            {
                entities.Dispose();
            }

            //Palautetaan JSON-muotoinen tulos kutsujalle
            var result = new { success = success, error = error };
            return Json(result);
        }
    }
}