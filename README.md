# BundleWinCerts
Extract the root and intermediate certs from the windows cert store and combine them with the unix-style cert bundle for use by tools such as git

This was originally written to bundle certs from the windows cert store into the git ca-bundle file (for example to put corporate certs into the bundle for enterprise github usage"

Usage:
BundleWinCerts &lt;existing cert bundle path&gt; &lt;new bundle path&gt;

&lt;existing cert bundle path&gt; must exist and cannot be overwritten
&lt;new bundle path&gt; must be in a directory that exists and will be overwritten silently

example:
BundleWinCerts "C:\Program Files\Git\mingw64\ssl\certs\ca-bundle.crt" "C:\Program Files\Git\mingw64\ssl\certs\ca-bundle-plusWinRoot.crt"
