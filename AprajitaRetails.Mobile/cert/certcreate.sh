
 openssl req -x509 -nodes -days 3650 -newkey rsa:2048 \
-keyout akssever.com.key -out akssever.com.crt \
-subj "/C=IN/ST=JH/L=DUMKA/O=aksserver.com/CN=akssever.com"

cat  akssever.com.crt akssever.com.key  > akssever.com.pem

openssl pkcs12 -export -out akssever.com.pfx -inkey akssever.com.key -in akssever.com.pem

sudo cp * /etc/encryption

