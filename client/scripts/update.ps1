cd ./client/Lykke.Pay.Service.Wallets.Client/
iwr http://localhost:4566/swagger/v1/swagger.json -o Service.Assets.json
autorest --input-file=Service.Assets.json --csharp --namespace=Lykke.Pay.Service.Wallets.Client --output-folder=./