using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApartmentDataAccess;

namespace ApartmentWebApi.Controllers
{
    public class ApartmentController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetApartments()
        {
            using (ApartmentsEntities entities = new ApartmentsEntities())
            {
                var result = entities.ApartmentDetails.ToList();
                var test = entities.getApartmentDetails().ToList();
                if (result == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Data Found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage GetApartmentById(int id)
        {
            using (ApartmentsEntities entities = new ApartmentsEntities())
            {
                try
                {
                    var result = entities.ApartmentDetails.FirstOrDefault(x => x.ApartmentId == id);
                    if (result == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Result found for the same Id");
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error Occered");
                }

            }


        }

        [HttpPost]
        public HttpResponseMessage SaveNewApartmentDetail([FromBody] ApartmentDetail apartmentDetail)
        {
            try
            {
                using (ApartmentsEntities entities = new ApartmentsEntities())
                {
                    entities.ApartmentDetails.Add(apartmentDetail);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, "Item Created");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateApartmentDetail(int id, [FromBody] ApartmentDetail apartmentDetail)
        {
            try
            {
                using (ApartmentsEntities entities = new ApartmentsEntities())
                {
                    var result = entities.ApartmentDetails.FirstOrDefault(x => x.ApartmentId == id);
                    if (result != null)
                    {
                        result.ApartmentId = apartmentDetail.ApartmentId;
                        result.ApartmentName = apartmentDetail.ApartmentName;
                        result.NoOfFloors = apartmentDetail.NoOfFloors;
                        result.Address1 = apartmentDetail.Address1;
                        result.Address2 = apartmentDetail.Address2;
                        result.City = apartmentDetail.City;
                        result.State = apartmentDetail.State;
                        result.PinCode = apartmentDetail.PinCode;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Item Updated!");
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error Occured");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Error Occured");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteApartmentDetail(int id)
        {
            try
            {
                using (ApartmentsEntities entities = new ApartmentsEntities())
                {
                    var result = entities.ApartmentDetails.FirstOrDefault(x => x.ApartmentId == id);
                    if (result != null)
                    {
                        entities.ApartmentDetails.Remove(result);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Item Removed Successfully!");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent, "Item not found!");
                    }

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error Occured!");
            }
        }
    }
}
