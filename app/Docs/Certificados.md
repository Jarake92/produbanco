## CA
```shell
openssl genrsa -out ./myCA/rootCA/private/ca.key.pem 4096
chmod 400 ./myCA/rootCA/private/ca.key.pem
openssl rsa -noout -text -in ./myCA/rootCA/private/ca.key.pem

openssl req -config ./myCA/openssl_root.cnf -key myCA/rootCA/private/ca.key.pem -new -x509 -days 720 -sha256 -extensions v3_ca -out ./myCA/rootCA/certs/ca.cert.pem -subj "/C=EC/ST=Pichincha/L=Quito/O=ARQCorp/OU=ARQ Department/CN=Root CA"

chmod 444 ./myCA/rootCA/certs/ca.cert.pem
openssl x509 -noout -text -in ./myCA/rootCA/certs/ca.cert.pem
```


## INTERMEDIATE
```shell
openssl genrsa -out ./myCA/intermediateCA/private/intermediate.key.pem 4096
chmod 400 ./myCA/intermediateCA/private/intermediate.key.pem

openssl req -config ./myCA/openssl_intermediate.cnf -key ./myCA/intermediateCA/private/intermediate.key.pem -new -sha256 -out ./myCA/intermediateCA/certs/intermediate.csr.pem -subj "/C=EC/ST=Pichincha/L=Quito/O=ARQCorp/OU=ARQ Department/CN=Intermediate CA"

openssl ca -config ./myCA/openssl_root.cnf -extensions v3_intermediate_ca -days 700 -notext -md sha256 -in ./myCA/intermediateCA/certs/intermediate.csr.pem -out ./myCA/intermediateCA/certs/intermediate.cert.pem
chmod 444 ./myCA/intermediateCA/certs/intermediate.cert.pem
openssl x509 -noout -text -in ./myCA/intermediateCA/certs/intermediate.cert.pem
Verificar contra el CA
openssl verify -CAfile ./myCA/rootCA/certs/ca.cert.pem ./myCA/intermediateCA/certs/intermediate.cert.pem
```


## CREATE BUNDLE INTERMEDIATE AND CA
```shell
cat ./myCA/intermediateCA/certs/intermediate.cert.pem ./myCA/rootCA/certs/ca.cert.pem > ./myCA/intermediateCA/certs/ca-chain.cert.pem
openssl verify -CAfile ./myCA/intermediateCA/certs/ca-chain.cert.pem ./myCA/intermediateCA/certs/intermediate.cert.pem
```


## Generate and sign server certificate using Intermediate CA
### - ORQUESTADOR
```shell
openssl genpkey -algorithm RSA -out ./myCA/intermediateCA/private/orquestador.arquitectura.com.key.pem
chmod 400 ./myCA/intermediateCA/private/orquestador.arquitectura.com.key.pem
openssl req -config ./myCA/openssl_intermediate.cnf -key ./myCA/intermediateCA/private/orquestador.arquitectura.com.key.pem -new -sha256 -out ./myCA/intermediateCA/csr/orquestador.arquitectura.com.csr.pem -subj "/C=EC/ST=Pichincha/L=Quito/O=ARQCorp/OU=ARQ Department/CN=orquestador.arquitectura.com"
openssl ca -config ./myCA/openssl_intermediate.cnf -extensions server_cert -days 375 -notext -md sha256 -in ./myCA/intermediateCA/csr/orquestador.arquitectura.com.csr.pem -out ./myCA/intermediateCA/certs/orquestador.arquitectura.com.cert.pem
openssl x509 -noout -text -in ./myCA/intermediateCA/certs/orquestador.arquitectura.com.cert.pem
```
### - CLIENTE
```shell
openssl genpkey -algorithm RSA -out ./myCA/intermediateCA/private/cliente.arquitectura.com.key.pem
chmod 400 ./myCA/intermediateCA/private/cliente.arquitectura.com.key.pem
openssl req -config ./myCA/openssl_intermediate.cnf -key ./myCA/intermediateCA/private/cliente.arquitectura.com.key.pem -new -sha256 -out ./myCA/intermediateCA/csr/cliente.arquitectura.com.csr.pem -subj "/C=EC/ST=Pichincha/L=Quito/O=ARQCorp/OU=ARQ Department/CN=cliente.arquitectura.com"
openssl ca -config ./myCA/openssl_intermediate.cnf -extensions server_cert -days 375 -notext -md sha256 -in ./myCA/intermediateCA/csr/cliente.arquitectura.com.csr.pem -out ./myCA/intermediateCA/certs/cliente.arquitectura.com.cert.pem
openssl x509 -noout -text -in ./myCA/intermediateCA/certs/cliente.arquitectura.com.cert.pem
```

### - DIRECCION
```shell
openssl genpkey -algorithm RSA -out ./myCA/intermediateCA/private/direccion.arquitectura.com.key.pem
chmod 400 ./myCA/intermediateCA/private/direccion.arquitectura.com.key.pem
openssl req -config ./myCA/openssl_intermediate.cnf -key ./myCA/intermediateCA/private/direccion.arquitectura.com.key.pem -new -sha256 -out ./myCA/intermediateCA/csr/direccion.arquitectura.com.csr.pem -subj "/C=EC/ST=Pichincha/L=Quito/O=ARQCorp/OU=ARQ Department/CN=direccion.arquitectura.com"
openssl ca -config ./myCA/openssl_intermediate.cnf -extensions server_cert -days 375 -notext -md sha256 -in ./myCA/intermediateCA/csr/direccion.arquitectura.com.csr.pem -out ./myCA/intermediateCA/certs/direccion.arquitectura.com.cert.pem
openssl x509 -noout -text -in ./myCA/intermediateCA/certs/direccion.arquitectura.com.cert.pem
```

### - TELEFONO
```shell
openssl genpkey -algorithm RSA -out ./myCA/intermediateCA/private/telefono.arquitectura.com.key.pem
chmod 400 ./myCA/intermediateCA/private/telefono.arquitectura.com.key.pem
openssl req -config ./myCA/openssl_intermediate.cnf -key ./myCA/intermediateCA/private/telefono.arquitectura.com.key.pem -new -sha256 -out ./myCA/intermediateCA/csr/telefono.arquitectura.com.csr.pem -subj "/C=EC/ST=Pichincha/L=Quito/O=ARQCorp/OU=ARQ Department/CN=telefono.arquitectura.com"
openssl ca -config ./myCA/openssl_intermediate.cnf -extensions server_cert -days 375 -notext -md sha256 -in ./myCA/intermediateCA/csr/telefono.arquitectura.com.csr.pem -out ./myCA/intermediateCA/certs/telefono.arquitectura.com.cert.pem
openssl x509 -noout -text -in ./myCA/intermediateCA/certs/telefono.arquitectura.com.cert.pem
```

### - IAM
```shell
openssl genpkey -algorithm RSA -out ./myCA/intermediateCA/private/iam.arquitectura.com.key.pem
chmod 400 ./myCA/intermediateCA/private/iam.arquitectura.com.key.pem
openssl req -config ./myCA/openssl_intermediate.cnf -key ./myCA/intermediateCA/private/iam.arquitectura.com.key.pem -new -sha256 -out ./myCA/intermediateCA/csr/iam.arquitectura.com.csr.pem -subj "/C=EC/ST=Pichincha/L=Quito/O=ARQCorp/OU=ARQ Department/CN=iam.arquitectura.com"
openssl ca -config ./myCA/openssl_intermediate.cnf -extensions server_cert -days 375 -notext -md sha256 -in ./myCA/intermediateCA/csr/iam.arquitectura.com.csr.pem -out ./myCA/intermediateCA/certs/iam.arquitectura.com.cert.pem
openssl x509 -noout -text -in ./myCA/intermediateCA/certs/iam.arquitectura.com.cert.pem
```