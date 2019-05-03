# BundleWinCerts
Extract the root and intermediate certs from the windows cert store and combine them with the unix-style cert bundle for use by tools such as git


This first release assumes that you are bundling windows certs into C:\Program Files\Git\mingw64\ssl\certs\ca-bundle.crt, and creates the file C:\Program Files\Git\mingw64\ssl\certs\ca-bundle-plusWinRoot.crt as its output.  When run, it will proceed without prompting as this was written for use by gitInstConf.cmd in https://github.com/cmconti/DevEnvInit

TODO: parameterize program to work with any CA bundle