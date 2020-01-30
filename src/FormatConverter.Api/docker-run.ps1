dotnet publish
docker build -t format-converter . 
docker run -p 80:80 format-converter 