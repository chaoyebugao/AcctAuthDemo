
CREATE DATABASE `acct_auth_demo` /*!40100 DEFAULT CHARACTER SET latin1 */;
;

USE `acct_auth_demo`
;

CREATE TABLE `Device` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '����',
  `IsDeleted` tinyint(4) NOT NULL COMMENT '����',
  `CreateAt` datetime NOT NULL COMMENT '����ʱ��',
  `UpdateAt` datetime NOT NULL COMMENT '����ʱ��',
  `DeviceId` varchar(45) NOT NULL COMMENT '�豸Id',
  `DeviceName` varchar(45) NOT NULL COMMENT '�豸��',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COMMENT='�豸';
;

CREATE TABLE `LoginToken` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '����',
  `IsDeleted` tinyint(4) NOT NULL COMMENT '�Ƿ���ɾ��',
  `CreateAt` datetime NOT NULL COMMENT '����ʱ��',
  `UpdateAt` datetime NOT NULL COMMENT '����ʱ��',
  `UserId` bigint(20) NOT NULL COMMENT '�û�Id',
  `DeviceId` varchar(70) NOT NULL COMMENT '�豸Id',
  `Token` varchar(300) NOT NULL COMMENT '����',
  `ExpireAt` datetime NOT NULL COMMENT '����ʱ��',
  `IP` varchar(45) DEFAULT NULL COMMENT 'IP',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1 COMMENT='��¼����';
;

CREATE TABLE `User` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '����',
  `IsDeleted` tinyint(4) NOT NULL COMMENT '�Ƿ���ɾ����',
  `CreateAt` datetime NOT NULL COMMENT '����ʱ��',
  `UpdateAt` datetime NOT NULL COMMENT '����ʱ��',
  `Name` varchar(45) CHARACTER SET utf8 NOT NULL COMMENT '�û���',
  `Password` varchar(3000) DEFAULT NULL COMMENT '����',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1 COMMENT='�û�';
;

