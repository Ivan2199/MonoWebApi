using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Xml.Linq;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class VehicleController : ApiController
    {


        private static List<Vehicle> Vehicles = new List<Vehicle>();
        // GET api/<controller>
        [HttpGet]
        public HttpResponseMessage Get(string vehicleType = null)
        {
            if (!string.IsNullOrEmpty(vehicleType))
            {
                List<Vehicle> filteredVehicles = Vehicles
                    .Where(v => v.VehicleType != null && v.VehicleType.Equals(vehicleType, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (filteredVehicles.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, $"No vehicles found with the specified VehicleType: {vehicleType}");
                }

                return Request.CreateResponse(HttpStatusCode.OK, filteredVehicles);
            }

            if (Vehicles.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "List is empty");
            }

            return Request.CreateResponse(HttpStatusCode.OK, Vehicles);
        }



        // GET api/<controller>/
        public HttpResponseMessage Get(int id)
        {
            Vehicle vehicle = Vehicles.Find(x => x.Id == id);
           
           
            if (vehicle == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No Vehicle with that Id"); 
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, vehicle);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No data has been entered");
            }

            int maxId = Vehicles.Any() ? Vehicles.Max(v =>v.Id) : 0;
            vehicle.Id = maxId + 1;
            
            Vehicles.Add(vehicle);
            return Request.CreateResponse(HttpStatusCode.OK, "Data has been entered successfully");
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody] Vehicle veihcleUpdated)
        {
            Vehicle vehicleCurrent = Vehicles.Find(x => x.Id == id);
            if (vehicleCurrent == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No vehicle with that id was found");
            }
            else
            {
                vehicleCurrent.VehicleOwner = veihcleUpdated.VehicleOwner;
                vehicleCurrent.VehicleMileage = veihcleUpdated.VehicleMileage;
                return Request.CreateResponse(HttpStatusCode.OK, "Data has been updated successfully");
            }
            
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            Vehicle vehicle = Vehicles.Find(x => x.Id == id);
            if (vehicle == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No vehicle with that id was found");
            }
            else
            {
                Vehicles.Remove(vehicle);
                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle has been deleted successfully");
            }
        }
    }
}