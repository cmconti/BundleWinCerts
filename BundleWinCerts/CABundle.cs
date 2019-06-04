﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace BundleWinCerts
{
    public class CABundle
    {
        public static void Read(string ca_bundlePath, Dictionary<string, X509Certificate2> certs)
        {
            string[] lines = File.ReadAllLines(ca_bundlePath);

            string b64 = String.Empty;
            bool readingCert = false;

            foreach (string line in lines)
            {
                if (!readingCert)
                {
                    //RFC7468 requireseol after he pre and post encpsulating boundaries
                    //https://tools.ietf.org/html/rfc7468#section-3
                    if (line.Contains("BEGIN CERTIFICATE"))
                    {
                        readingCert = true;
                    }
                }
                else
                {
                    if (line.Contains("END CERTIFICATE"))
                    {
                        readingCert = false;
                        ProcessCert(b64, certs);
                        b64 = String.Empty;
                    }
                    else
                    {
                        b64 += (line);
                    }
                }
            }
        }

        public static void Write(string newca_bundlePath, Dictionary<string, X509Certificate2> certs)
        {
            var type = X509ContentType.Cert;

            var header = new string[] {
                "##",
                "## This file is similar to the one auto-generated by GitHub Desktop.",
                "## Any changes made will be overwritten",
                "##",
                "## We will export all trusted certificate",
                "## authorities from the Windows certificate store to this file",
                "## as well as the default trusted cURL certificate authorities.",
                "##",
                "## File was last updated on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:sszzz"),
                "##",
            };

            File.WriteAllLines(newca_bundlePath, header);

            foreach (var cert in certs)
            {
                var data = new string[6];
                data[0] = String.Empty;
                data[1] = cert.Value.Subject;
                data[2] = new String('=', data[1].Length);
                data[3] = "-----BEGIN CERTIFICATE-----";
                //data[4] = Convert.ToBase64String(cert.Value.Export(type), Base64FormattingOptions.InsertLineBreaks);
                data[4] = Convert.ToBase64String(cert.Value.Export(type));
                data[4] = Regex.Replace(data[4], "(.{64})", "$1" + Environment.NewLine); ;
                data[5] = "-----END CERTIFICATE-----";

                File.AppendAllLines(newca_bundlePath, data);
            }
        }

        private static void ProcessCert(string b64, Dictionary<string, X509Certificate2> certs)
        {
            byte[] raw = Convert.FromBase64String(b64);
            X509Certificate2 cert = new X509Certificate2();
            cert.Import(raw);
            //Console.WriteLine(cert.Subject);

            if (!certs.ContainsKey(cert.Thumbprint))
                certs.Add(cert.Thumbprint, cert);
        }
    }
}
