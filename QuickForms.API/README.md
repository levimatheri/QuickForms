# How to run, backup and restore mongodb database

## Run container
- `cd` into the `data` directory
- Run `docker run -d -p 27017:27017 --name test-mongo -v backup:/backup mongo:latest`

## Backup 
- `cd` into the `data` directory
- Run `docker exec -t <container-id> mongodump --gzip --db QuickForms --archive=/backup/quickforms.gz`

## Restore
- `cd` into the `data` directory
- Run `docker exec -t <container-id> mongorestore --gzip --archive=/backup/quickforms.gz`