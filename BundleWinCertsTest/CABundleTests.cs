using Microsoft.VisualStudio.TestTools.UnitTesting;
using BundleWinCerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace BundleWinCerts.Tests
{
    [TestClass()]
    public class CABundleTests
    {
        [TestMethod()]
        [DeploymentItem("CABundle-CRLF.txt")]
        [DeploymentItem("CABundle-LF.txt")]
        public void ReadTest()
        {
            Dictionary<string, X509Certificate2> certs = new Dictionary<string, X509Certificate2>();
            string crlfpath = Path.GetFullPath("CABundle-CRLF.txt");
            CABundle.Read(crlfpath, certs);
            Assert.IsTrue(certs.Count == 2, "Did not read 2 certs from CRLF bundle");

            certs.Clear();
            string lfpath = Path.GetFullPath("CABundle-LF.txt");
            CABundle.Read(lfpath, certs);
            Assert.IsTrue(certs.Count == 2, "Did not read 2 certs from LF bundle");
        }

        [TestMethod()]
        [DeploymentItem("CABundle-CRLF.txt")]
        public void WriteTest()
        {
            Dictionary<string, X509Certificate2> certs = new Dictionary<string, X509Certificate2>();
            string crlfpath = Path.GetFullPath("CABundle-CRLF.txt");
            CABundle.Read(crlfpath, certs);

            string newpath = Directory.GetParent(crlfpath).FullName;
            string newbundle = Path.Combine(newpath, "CABundle-new.txt");

            CABundle.Write(newbundle, certs);

            certs.Clear();
            CABundle.Read(newbundle, certs);
            Assert.IsTrue(certs.Count == 2, "Did not read 2 certs from new bundle");
        }
    }
}