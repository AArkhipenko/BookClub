@echo off
pg_dump --dbname="postgres://postgres:postgres@localhost/bookclub" --schema=public --create --clean --if-exists --no-owner --no-acl --file=data.sql
pause