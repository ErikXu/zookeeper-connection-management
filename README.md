# zookeeper-connection-management
Use ZooKeeper to Manage Database Connection Configuration

## Install ZooKeeper with Docker  
```
docker pull zookeeper:3.4.13
docker run --name zookeeper -d -p 2181:2181 zookeeper:3.4.13
```

## Install Mysql with Docker
```
docker pull mysql:3.4.5.7
docker run --name mysql -e MYSQL_ROOT_PASSWORD=123456 -p 3306:3306 -d mysql:5.7 --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci
docker run --name mysql2 -e MYSQL_ROOT_PASSWORD=123456 -p 3307:3306 -d mysql:5.7 --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci
docker run --name mysql3 -e MYSQL_ROOT_PASSWORD=123456 -p 3308:3306 -d mysql:5.7 --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci
```

## Init Database
``` sql
CREATE DATABASE test;
USE test;
CREATE TABLE `table` (
  `id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
);
```

### mysql
``` sql
USE test;
INSERT INTO `table` (id, name) VALUES (1, 'A1');
INSERT INTO `table` (id, name) VALUES (2, 'B1');
INSERT INTO `table` (id, name) VALUES (3, 'C1');
```

### mysql2
``` sql
USE test;
INSERT INTO `table` (id, name) VALUES (1, 'A2');
INSERT INTO `table` (id, name) VALUES (2, 'B2');
INSERT INTO `table` (id, name) VALUES (3, 'C2');
```

### mysql3
``` sql
USE test;
INSERT INTO `table` (id, name) VALUES (1, 'A3');
INSERT INTO `table` (id, name) VALUES (2, 'B3');
INSERT INTO `table` (id, name) VALUES (3, 'C3');
```
