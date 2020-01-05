﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eRestaurant.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eRestaurant.Controllers
{
    [Route("api/org")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public OrganizationController(IUnitOfWork unitOfWork) => _uow = unitOfWork;

        [HttpGet("types")]
        public IActionResult GetDishTypes()
        {
            var types = _uow.DishTypes.GetAll().Select(dt => new { Id = dt.Id.ToString(), dt.Name });
            return Ok(types);
        }

        [HttpGet("units")]
        public IActionResult GetUnits()
        {
            var units = _uow.Units.GetAll().Select(u => new { Id = u.Id.ToString(), u.Name });
            return Ok(units);
        }

        [HttpGet("statuses")]
        public IActionResult GetOrderStatuses()
        {
            var statuses = _uow.OrderStatuses.GetAll().Select(os => new { os = os.Id.ToString(), os.Name });
            return Ok(statuses);
        }
    }
}