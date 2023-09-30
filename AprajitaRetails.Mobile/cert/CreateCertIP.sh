openssl req -x509 -nodes -days 3650 -newkey rsa:2048 \
-keyout 152.67.78.183.key -out 152.67.78.183.crt \
-subj "/C=IN/ST=JH/L=DUMKA/O=152.67.78.183/CN=152.67.78.183"

cat  152.67.78.183.crt 152.67.78.183.key  > 152.67.78.183.pem

openssl pkcs12 -export -out 152.67.78.183.pfx -inkey 152.67.78.183.key -in 152.67.78.183.pem

sudo cp * /etc/encryption

