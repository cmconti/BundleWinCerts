using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace BundleWinCerts
{
    class Program
    {
        static Dictionary<string, X509Certificate2> certs = new Dictionary<string, X509Certificate2>();

        static void Main(string[] args)
        {
            //string ca_bundlePath = @"C:\Users\ChrisC\AppData\Local\GitHub\PortableGit_f02737a78695063deace08e96d5042710d3e32db\mingw32\ssl\certs\ca-bundle.crt";
            string ca_bundlePath = @"C:\Program Files\Git\mingw64\ssl\certs\ca-bundle.crt";
            CABundle.Read(ca_bundlePath, certs);

            CertStore.Read(certs);

            string newca_bundlePath = @"C:\Program Files\Git\mingw64\ssl\certs\ca-bundle-plusWinRoot.crt";
            CABundle.Write(newca_bundlePath, certs);
        }
    }
}
