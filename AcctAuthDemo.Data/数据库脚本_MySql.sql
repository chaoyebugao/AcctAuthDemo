
CREATE DATABASE `acct_auth_demo` /*!40100 DEFAULT CHARACTER SET latin1 */;
;

USE `acct_auth_demo`
;

CREATE TABLE `Device` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `IsDeleted` tinyint(4) NOT NULL COMMENT '创建',
  `CreateAt` datetime NOT NULL COMMENT '创建时间',
  `UpdateAt` datetime NOT NULL COMMENT '更新时间',
  `DeviceId` varchar(45) NOT NULL COMMENT '设备Id',
  `DeviceName` varchar(45) NOT NULL COMMENT '设备名',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COMMENT='设备';
;

CREATE TABLE `LoginToken` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `IsDeleted` tinyint(4) NOT NULL COMMENT '是否已删除',
  `CreateAt` datetime NOT NULL COMMENT '创建时间',
  `UpdateAt` datetime NOT NULL COMMENT '更新时间',
  `UserId` bigint(20) NOT NULL COMMENT '用户Id',
  `DeviceId` varchar(70) NOT NULL COMMENT '设备Id',
  `Token` varchar(300) NOT NULL COMMENT '令牌',
  `ExpireAt` datetime NOT NULL COMMENT '过期时间',
  `IP` varchar(45) DEFAULT NULL COMMENT 'IP',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1 COMMENT='登录令牌';
;

CREATE TABLE `User` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `IsDeleted` tinyint(4) NOT NULL COMMENT '是否是删除的',
  `CreateAt` datetime NOT NULL COMMENT '创建时间',
  `UpdateAt` datetime NOT NULL COMMENT '更新时间',
  `Name` varchar(45) CHARACTER SET utf8 NOT NULL COMMENT '用户名',
  `Password` varchar(3000) DEFAULT NULL COMMENT '密码',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1 COMMENT='用户';
;

