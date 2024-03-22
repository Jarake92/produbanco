echo on
docker build -t pruebasregistro.azurecr.io/demo-cliente:v5 -f.\Dockerfile-cliente .
docker build -t pruebasregistro.azurecr.io/demo-direccion:v5 -f.\Dockerfile-direccion .
docker build -t pruebasregistro.azurecr.io/demo-telefono:v5 -f.\Dockerfile-telefono .
docker build -t pruebasregistro.azurecr.io/demo-orquestador:v5 -f.\Dockerfile-orquestador .
ECHO ' '
echo 'FIN'
