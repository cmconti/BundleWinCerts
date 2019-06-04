using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace BundleWinCerts
{
    class Program
    {
        static Dictionary<string, X509Certificate2> certs = new Dictionary<string, X509Certificate2>();

        static int Main(string[] args)
        {
            if (2 != args.Length)
            {
                Usage();
                return 1;
            }
            string ca_bundlePath = Path.GetFullPath(args[0]);
            string newca_bundlePath = Path.GetFullPath(args[1]);
            if (!File.Exists(ca_bundlePath) ||
                !Directory.Exists(Path.GetDirectoryName(newca_bundlePath)) ||
                (0 == String.Compare(ca_bundlePath, newca_bundlePath, StringComparison.InvariantCultureIgnoreCase)))
            {
                Usage();
                return 1;
            }

            //string ca_bundlePath =@"C:\Program Files\Git\mingw64\ssl\certs\ca-bundle.crt";
            CABundle.Read(ca_bundlePath, certs);

            CertStore.Read(certs);

            //string newca_bundlePath = @"C:\Program Files\Git\mingw64\ssl\certs\ca-bundle-plusWinRoot.crt";
            CABundle.Write(newca_bundlePath, certs);
            return 0;
        }

        static void Usage()
        {
            Console.WriteLine("BundleWinCerts");
            Console.WriteLine("Usage:");
            Console.WriteLine("BundleWinCerts <existing cert bundle path> <new bundle path>");
            Console.WriteLine();
            Console.WriteLine("<existing cert bundle path> must exist and cannot be overwritten");
            Console.WriteLine("<new bundle path> must be in a directory that exists and will be overwritten silently");
            Console.WriteLine();
            Console.WriteLine("example:");
            Console.WriteLine(@"BundleWinCerts ""C:\Program Files\Git\mingw64\ssl\certs\ca-bundle.crt"" ""C:\Program Files\Git\mingw64\ssl\certs\ca-bundle-plusWinRoot.crt""");
        }
    }
}
