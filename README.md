# Movie_WebAPI: How to run API locally?
To run the API locally, please doing step by step: 
1> Open terminal in VS and use command 'docker-comopose build' like bellow 
![image](https://user-images.githubusercontent.com/126745003/225191708-d44dd24d-c5c6-439e-aa8e-c71e86ae4127.png)
2> When build complete, open Docker-Desktop you can see two images created from Dockerfile inside project
![image](https://user-images.githubusercontent.com/126745003/225192445-72ca49ae-aae7-4690-b482-184e89ff474b.png)
3> Now, back to terminal in VS and use command 'docker-compose up' to build Containers from two images created before
![image](https://user-images.githubusercontent.com/126745003/225192843-9a327147-bb2f-4793-a899-de00c501fab9.png)
4> When containers is built, please visit localhost:8080 to run WebAPI
![image](https://user-images.githubusercontent.com/126745003/225193110-97068d13-2e30-4e58-af04-0074a30553c4.png)
# Movie_WebAPI: How to Restore a SQL Server database in a Linux container?
To restore a database in Linux container, please doing step by step:
1> Open Docker-Desktop => tab Containers => you will see Container is running have image Movie_DB
![image](https://user-images.githubusercontent.com/126745003/225193722-53bf7244-7248-40c3-909d-4f7e245cec7d.png)
2> Open image Movie_DB => Move in tab Terminal
![image](https://user-images.githubusercontent.com/126745003/225193903-2afa262b-1916-4967-8e1a-7a978a814a72.png)
3> Run the command 'cd /dbbackups' to connect folder dbbackups
![image](https://user-images.githubusercontent.com/126745003/225194076-c4819d93-57b1-4457-a856-8b91023605de.png)
4> Run the command 'cp MovieServiceDB.bak /var/opt/mssql/data' to coppy file Backup to folder data
![image](https://user-images.githubusercontent.com/126745003/225194245-1d6ce8c5-87e5-402e-b1cb-f853cd7c49e2.png)
5> Run the command '/opt/mssql-tools/bin/sqlcmd -U sa -P passWord@123' to connect Sql Server and use sqlcmd
![image](https://user-images.githubusercontent.com/126745003/225194733-2b7ce3e8-4f6f-4bae-83ac-34b3f19bf941.png)
6> Run the command 
1>RESTORE DATABASE MovieServiceDB FROM DISK ="/var/opt/mssql/data/MovieServiceDB.bak" WITH MOVE "MovieServiceDB" TO "/var/opt/mssql/data/MovieServiceDB.ldf", MOVE "MovieServiceDB_log" TO "/var/opt/mssql/data/MovieServiceDB_log.ldf"
2>GO
![image](https://user-images.githubusercontent.com/126745003/225195072-48cbb00f-03ce-43af-a63a-0b8e95e48885.png)
 
