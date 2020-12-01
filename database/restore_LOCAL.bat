@echo off
psql --dbname=postgres://postgres:postgres@localhost --command="DROP DATABASE bookclub" --command="CREATE DATABASE bookclub"
pause
psql --dbname=postgres://postgres:postgres@localhost/bookclub --file=data.sql --log-file=log.log --output=output.log
pause