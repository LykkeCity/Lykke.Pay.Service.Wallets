cd ../Lykke.Signing.Api/
iwr http://13.93.116.252:5566/swagger/v1/swagger.json -o Lykke.Signing.Api.json
autorest --input-file=Lykke.Signing.Api.json --csharp --namespace=Lykke.Signing.Api --output-folder=./