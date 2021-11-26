using Microsoft.VisualStudio.TestTools.UnitTesting;
using COJ.Web.Infraestructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Infraestructure.Services.Tests
{
    [TestClass()]
    public class HashServiceTests
    {
        private  HashService hashService;

        public HashServiceTests()
        {
            this.hashService = new HashService();
        }

        [TestMethod()]
        public void HashService()
        {
            var input = "CubaVive2021*";
            var hashed = hashService.ComputeHash(input);
            Assert.IsNotNull(hashed);
            var result = hashService.VerifyHash(hashed, input);
            Assert.AreEqual(result, Domain.Values.HashVerificationResult.Success);

            this.hashService = new HashService();
            var result2 = hashService.VerifyHash(hashed, input);
            Assert.AreEqual(result2, Domain.Values.HashVerificationResult.Success);
        }
    }
}