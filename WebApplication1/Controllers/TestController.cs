﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antila.Core;
using Antila.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TestController : ControllerBase
    {
        private readonly ITestData testData;

        public IEnumerable<Test> Test { get; set; }

        public TestController(ITestData testData)
        {
            this.testData = testData;
        }

        [HttpGet]
        public async Task<IEnumerable<Test>> GetTests()
        {
            return Test = await testData.GetTest();
        }

        [HttpPost]
        public void PostTests([FromBody] Test test)
        {
            
            testData.CalculateNumberOfPoints(test.Id, test.Question.Answers.Select(x => x.Id).FirstOrDefault());
            
        }

        [HttpGet("summary")]
        public string GetSummary()
        { 
            string summary  = "Odpowiedziałeś poprawnie na " + testData.PointsCount()
                + " z " +testData.QuestionsCount() + " pytań.";
            testData.ErasePointsCount();
            return summary;
        }
    }
}