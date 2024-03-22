build:
	dotnet build  src/state.orquestation.net6.sln
build-cliente:
	dotnet clean src/comun/shared.comun.csproj
	dotnet cleanmake src/cliente/api.cliente/api.cliente.csproj
	dotnet build src/comun/shared.comun.csproj
	dotnet build src/cliente/api.cliente/api.cliente.csproj
build-telefono:
	dotnet build src/comun/shared.comun.csproj
	dotnet build src/telefono/api.telefono/api.telefono.csproj
build-direccion:
	dotnet build src/comun/shared.comun.csproj
	dotnet build src/direccion/api.direccion/api.direccion.csproj	
build-orquestador:
	dotnet build src/comun/shared.comun.csproj
	dotnet build src/orquestador/api.orquestador/api.orquestador.csproj

clean: 
	dotnet clean src/state.orquestation.net6.sln
restore:
	dotnet restore src/comun/shared.comun.csproj
	dotnet restore src/cliente/api.cliente/api.cliente.csproj
	dotnet restore src/telefono/api.telefono/api.telefono.csproj
	dotnet restore src/direccion/api.direccion/api.direccion.csproj
	dotnet restore src/orquestador/api.orquestador/api.orquestador.csproj

run-cliente:
	dotnet run --project ./src/cliente/api.cliente/api.cliente.csproj
run-telefono:
	dotnet run --project src/telefono/api.telefono/api.telefono.csproj
run-direccion:
	dotnet run --project src/direccion/api.direccion/api.direccion.csproj	
run-orquestador:
	dotnet run --project src/orquestador/api.orquestador/api.orquestador.csproj

docker-cliente: 
	sudo docker build -t azdevacrakspoc.azurecr.io/demo-cliente:v8 -f Dockerfile-cliente .
docker-direccion:
	sudo docker build -t azdevacrakspoc.azurecr.io/demo-direccion:v8 -f Dockerfile-direccion .
docker-telefono:
	sudo docker build -t azdevacrakspoc.azurecr.io/demo-telefono:v8 -f Dockerfile-telefono .
docker-orquestador: 
	sudo docker build -t azdevacrakspoc.azurecr.io/demo-orquestador:v8 -f Dockerfile-orquestador .

docker-push5:
	docker push azdevacrakspoc.azurecr.io/demo-cliente:v5
	docker push azdevacrakspoc.azurecr.io/demo-direccion:v5
	docker push azdevacrakspoc.azurecr.io/demo-telefono:v5
	docker push azdevacrakspoc.azurecr.io/demo-orquestador:v5
	docker push azdevacrakspoc.azurecr.io/demo-identidad:v5
	
docker-push8:
	docker push azdevacrakspoc.azurecr.io/demo-cliente:v8
	docker push azdevacrakspoc.azurecr.io/demo-direccion:v8
	docker push azdevacrakspoc.azurecr.io/demo-telefono:v8
	docker push azdevacrakspoc.azurecr.io/demo-orquestador:v8
	docker push azdevacrakspoc.azurecr.io/demo-identidad:v8

