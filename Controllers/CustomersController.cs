﻿using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class CustomersController : ApiController
    {
        private NORTHWNDEntities _db;

        public CustomersController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/customers/city/London
        [HttpGet, Route("api/customers/city/{city}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAll(string city)
        {
            var resultSet = from customer in _db.Customers
                            where customer.City == "London"
                            select customer;

            return Ok(resultSet);

           //  throw new NotImplementedException("Write a query to return all customers in the given city");
        }

        // GET: api/customers/mexicoSwedenGermany
        [HttpGet, Route("api/customers/mexicoSwedenGermany"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAllFromMexicoSwedenGermany()
        {
            var resultSet = from customer in _db.Customers
                            where customer.Country == "Mexico" || customer.Country == "Sweden" || customer.Country == "Germany"
                            select customer;

            return Ok(resultSet);

            // throw new NotImplementedException("Write a query to return all customers from Mexico, Sweden and Germany.");
        }

        // GET: api/customers/shippedUsing/Speedy Express
        [HttpGet, Route("api/customers/shippedUsing/{shipperName}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersThatShipWith(string shipperName)
        {
            var innerJoinQuery = from customer in _db.Customers
                                 join order in _db.Orders on customer.CustomerID equals order.CustomerID
                                 join shipper in _db.Shippers on order.ShipVia equals shipper.ShipperID
                                 where shipper.CompanyName == "Speedy Express"
                                 select customer;
     
            return Ok(innerJoinQuery);
            // throw new NotImplementedException("Write a query to return all customers with orders that shipped using the given shipperName.");
        }

        // GET: api/customers/withoutOrders
        [HttpGet, Route("api/customers/withoutOrders"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersWithoutOrders()
        {
            var resultSet = from customer in _db.Customers
                            where !customer.Orders.Any()
                            select customer;

            return Ok(resultSet);

            // throw new NotImplementedException("Write a query to return all customers with no orders in the Orders table.");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
