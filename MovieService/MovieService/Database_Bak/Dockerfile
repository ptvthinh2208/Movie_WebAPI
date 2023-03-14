FROM mcr.microsoft.com/mssql/server:2019-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=passWord@123
ENV MSSQL_PID=Developer
ENV MSSQL_TCP_PORT=1433
        
WORKDIR /src
        
COPY MovieService/Database_Bak/MovieServiceDB.bak /dbbackups/

