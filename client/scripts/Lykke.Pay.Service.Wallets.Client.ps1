cd ../Lykke.Pay.Service.Wallets.Client/
iwr http://localhost:4566/swagger/v1/swagger.json -o Lykke.Pay.Service.Wallets.Client.json
autorest --input-file=Lykke.Pay.Service.Wallets.Client.json --csharp --namespace=Lykke.Pay.Service.Wallets.Client --output-folder=./