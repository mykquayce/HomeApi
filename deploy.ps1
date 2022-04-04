docker pull eassbhhtgu/homeapi:latest
if (!$?) { return; }

docker stack deploy --compose-file .\docker-compose.yml home
