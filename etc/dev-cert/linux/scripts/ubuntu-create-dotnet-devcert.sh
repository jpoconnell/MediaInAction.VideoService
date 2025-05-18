#!/bin/sh
. "$(dirname "$0")/common.sh"

$SUDO rm /etc/ssl/certs/dotnet-devcert.pem
$SUDO cp $CRTFILE "/usr/local/share/ca-certificates"

openssl pkcs12 -export -out mediainaction-authserver.pfx -in $CRTFILE
$SUDO update-ca-certificates


#cleanup
