using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApartmentDataAccess;

namespace ApartmentWebApi.Controllers
{
    public class CityController : ApiController
    {
        /// <summary>
        /// To get all the cities in India. 
        /// </summary>
        /// <returns>A list of cities in India.</returns>
        [HttpGet]
        public HttpResponseMessage GetCities()
        {
            try
            {
                using (ApartmentsEntities entities = new ApartmentsEntities())
                {
                    var result = entities.CityMasters.ToList();
                    if (result != null)
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Result Found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error Occured!");
            }


        }

        /// <summary>
        /// Get City by its name;
        /// </summary>
        /// <param name="name">Take Citi name as the parameter.</param>
        /// <returns>Returns the city if present or return no content</returns>

        [HttpGet]
        [Route("api/city/{name}")]
        public HttpResponseMessage GetCityByCityName(string name)
        {
            try
            {
                using (ApartmentsEntities entities = new ApartmentsEntities())
                {
                    var result = entities.CityMasters.FirstOrDefault(x => x.CityName == name);

                    if (result != null)
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "City " + name + "not found in database"); ;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error Occured!");
            }
        }

        /// <summary>
        /// Get all the cities present in a state
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Return all the city present in the state.</returns>

        [HttpGet]
        [Route("api/state/{name}")]

        public HttpResponseMessage GetCityByState(string name)
        {
            try
            {
                using (ApartmentsEntities entities = new ApartmentsEntities())
                {
                    var result = entities.CityMasters.Where(t => t.CityState == name);
                    if (result != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);

                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No city found for state: " + name);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error Occured!");
            }
        }


        /// <summary>
        ///     Get the city by Id
        /// </summary>
        /// <param name="id">CityId</param>
        /// <returns>Return the city name</returns>
        [HttpGet]
        public HttpResponseMessage GetCityById(int id)
        {
            try
            {
                using (ApartmentsEntities entities = new ApartmentsEntities())
                {
                    var result = entities.CityMasters.FirstOrDefault(x => x.CityId == id);
                    if (result != null)
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No city found for id: " + id);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error Occured!");
            }
        }

        /// <summary>
        /// Update the City Details
        /// </summary>
        /// <param name="CityName"></param>
        /// <param name="cityMaster"></param>

        [HttpPut]
        [Route("api/city/{name}")]
        public HttpResponseMessage UpdateCityName(string name, [FromBody] CityMaster cityMaster)
        {
            try
            {
                using (ApartmentsEntities entities = new ApartmentsEntities())
                {
                    var result = entities.CityMasters.FirstOrDefault(x => x.CityName == name);
                    if (result != null)
                    {
                        result.CityName = cityMaster.CityName;
                        result.CityState = cityMaster.CityState;
                        result.CityId = cityMaster.CityId;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Updated the city " + name + "to " + cityMaster.CityName);
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No city found with name : " + name);
                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error Occured!");
            }
        }
    }
}
