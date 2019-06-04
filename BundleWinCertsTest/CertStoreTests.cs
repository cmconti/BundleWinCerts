using Microsoft.VisualStudio.TestTools.UnitTesting;
using BundleWinCerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace BundleWinCerts.Tests
{
    [TestClass()]
    public class CertStoreTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            Dictionary<string, X509Certificate2> certs = new Dictionary<string, X509Certificate2>();

            CertStore.Read(certs);

            //assume COMODO RSA Certification Authority root cert is on all windows machines
            Assert.IsTrue(certs.ContainsKey("AFE5D244A8D1194230FF479FE2F897BBCD7A8CB4"));

            //assume  COMODO RSA Certification Authority intermediate cert is on all windows machines
            Assert.IsTrue(certs.ContainsKey("B69E752BBE88B4458200A7C0F4F5B3CCE6F35B47"));
        }
    }
}