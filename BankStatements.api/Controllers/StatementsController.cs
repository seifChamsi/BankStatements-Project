using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BankStatements.api.Entities;
using BankStatements.api.Models;
using BankStatements.api.Services;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankStatements.api.Controllers
{
    [EnableCors("myPolicy")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class StatementsController : ControllerBase
    {
        
        // POST: api/Statements
        [HttpPost]
        public async Task<IActionResult>  PostStatement(IFormFile excelFile,[FromForm]StatementEntity newStatement )
        {
            var statement =  dataService.TreatStatement(excelFile, newStatement.nom, newStatement.rib, newStatement.mois , newStatement.annee);
            return Ok(statement);
        }
    }
}       