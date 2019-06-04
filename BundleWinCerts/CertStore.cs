using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BundleWinCerts
{
    public class CertStore
    {
        //read root and intermediate cert stores
        public static void Read(Dictionary<string, X509Certificate2> certs)
        {
            X509Store storeRoot = new X509Store(StoreName.Root, StoreLocation.LocalMachine);

            storeRoot.Open(OpenFlags.ReadOnly);

            foreach (X509Certificate2 cert in storeRoot.Certificates)
            {
                if (!certs.ContainsKey(cert.Thumbprint))
                    certs.Add(cert.Thumbprint, cert);
            }

            storeRoot.Close();

            X509Store storeInt = new X509Store(StoreName.CertificateAuthority, StoreLocation.LocalMachine);

            storeInt.Open(OpenFlags.ReadOnly);

            foreach (X509Certificate2 cert in storeInt.Certificates)
            {
                if (!certs.ContainsKey(cert.Thumbprint))
                    certs.Add(cert.Thumbprint, cert);
            }

            storeInt.Close();
        }
    }
}
