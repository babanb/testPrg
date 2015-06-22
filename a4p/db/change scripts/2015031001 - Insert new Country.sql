--USE [AdoPets_Redesign]
GO
INSERT INTO [dbo].[Country]([Id],[Name],[Code])
  VALUES
(4,'ANDORRA','AD'),
(5,'UNITED ARAB EMIRATES','AE'),
(6,'AFGHANISTAN','AF'),
(7,'ANTIGUA AND BARBUDA','AG'),
(8,'ANGUILLA','AI'),
(9,'ALBANIA','AL'),
(10,'ARMENIA','AM'),
(11,'ANGOLA','AO'),
(12,'ANTARCTICA','AQ'),
(13,'ARGENTINA','AR'),
(14,'AMERICAN SAMOA','AS'),
(15,'AUSTRIA','AT'),
(16,'AUSTRALIA','AU'),
(17,'ARUBA','AW'),
(18,'ALAND ISLANDS','AX'),
(19,'AZERBAIJAN','AZ'),
(20,'BOSNIA AND HERZEGOVINA','BA'),
(21,'BARBADOS','BB'),
(22,'BANGLADESH','BD'),
(23,'BELGIUM','BE'),
(24,'BURKINA FASO','BF'),
(25,'BULGARIA','BG'),
(26,'BAHRAIN','BH'),
(27,'BURUNDI','BI'),
(28,'BENIN','BJ'),
(29,'SAINT BARTHELEMY','BL'),
(30,'BERMUDA','BM'),
(31,'BRUNEI DARUSSALAM','BN'),
(32,'BOLIVIA, PLURINATIONAL STATE OF','BO'),
(33,'BONAIRE, SAINT EUSTATIUS AND SABA','BQ'),
(34,'BRAZIL','BR'),
(35,'BAHAMAS','BS'),
(36,'BHUTAN','BT'),
(37,'BOUVET ISLAND','BV'),
(38,'BOTSWANA','BW'),
(39,'BELARUS','BY'),
(40,'BELIZE','BZ'),
(41,'CANADA','CA'),
(42,'COCOS (''KEELING'') ISLANDS','CC'),
(43,'CONGO, THE DEMOCRATIC REPUBLIC OF THE','CD'),
(44,'CENTRAL AFRICAN REPUBLIC','CF'),
(45,'CONGO','CG'),
(46,'SWITZERLAND','CH'),
(47,'COTE D''IVOIRE','CI'),
(48,'COOK ISLANDS','CK'),
(49,'CHILE','CL'),
(50,'CAMEROON','CM'),
(51,'CHINA','CN'),
(52,'COLOMBIA','CO'),
(53,'COSTA RICA','CR'),
(54,'CUBA','CU'),
(55,'CAPE VERDE','CV'),
(56,'CURACAO ','CW'),
(57,'CHRISTMAS ISLAND','CX'),
(58,'CYPRUS','CY'),
(59,'CZECH REPUBLIC','CZ'),
(60,'GERMANY','DE'),
(61,'DJIBOUTI','DJ'),
(62,'DENMARK','DK'),
(63,'DOMINICA','DM'),
(64,'DOMINICAN REPUBLIC','DO'),
(65,'ALGERIA ','DZ'),
(66,'ECUADOR','EC'),
(67,'ESTONIA','EE'),
(68,'EGYPT','EG'),
(69,'SAHARA OCCIDENTAL','EH'),
(70,'ERITREA','ER'),
(71,'SPAIN','ES'),
(72,'ETHIOPIA','ET'),
(73,'FINLAND','FI'),
(74,'FIJI','FJ'),
(75,'FALKLAND ISLANDS (''MALVINAS'')','FK'),
(76,'MICRONESIA, FEDERATED STATES OF','FM'),
(77,'FAROE ISLANDS','FO'),
(78,'GABON','GA'),
(79,'UNITED KINGDOM','GB'),
(80,'GRENADA','GD'),
(81,'GEORGIA','GE'),
(82,'FRENCH GUIANA','GF'),
(83,'GUERNSEY','GG'),
(84,'GHANA','GH'),
(85,'GIBRALTAR','GI'),
(86,'GREENLAND','GL'),
(87,'GAMBIA','GM'),
(88,'GUINEA','GN'),
(89,'GUADELOUPE','GP'),
(90,'EQUATORIAL GUINEA','GQ'),
(91,'GREECE','GR'),
(92,'SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS ','GS'),
(93,'GUATEMALA','GT'),
(94,'GUAM','GU'),
(95,'GUINEA-BISSAU','GW'),
(96,'GUYANA','GY'),
(97,'HONG KONG','HK'),
(98,'HEARD ISLAND AND MCDONALD ISLANDS','HM'),
(99,'HONDURAS','HN'),
(100,'CROATIA','HR'),
(101,'HAITI','HT'),
(102,'HUNGARY','HU'),
(103,'INDONESIA','ID'),
(104,'IRELAND','IE'),
(105,'ISRAEL','IL'),
(106,'ISLE OF MAN','IM'),
(107,'BRITISH INDIAN OCEAN TERRITORY','IO'),
(108,'IRAQ','IQ'),
(109,'IRAN, ISLAMIC REPUBLIC OF','IR'),
(110,'ICELAND','IS'),
(111,'ITALY','IT'),
(112,'JERSEY','JE'),
(113,'JAMAICA','JM'),
(114,'JORDAN','JO'),
(115,'JAPAN','JP'),
(116,'KENYA','KE'),
(117,'KYRGYZSTAN','KG'),
(118,'CAMBODIA','KH'),
(119,'KIRIBATI','KI'),
(120,'COMOROS','KM'),
(121,'SAINT KITTS AND NEVIS','KN'),
(122,'KOREA, DEMOCRATIC PEOPLE''S REPUBLIC OF','KP'),
(123,'KOREA, REPUBLIC OF','KR'),
(124,'KUWAIT','KW'),
(125,'CAYMAN ISLANDS','KY'),
(126,'KAZAKHSTAN','KZ'),
(127,'LAO PEOPLE''S DEMOCRATIC REPUBLIC','LA'),
(128,'LEBANON','LB'),
(129,'SAINT LUCIA','LC'),
(130,'LIECHTENSTEIN','LI'),
(131,'SRI LANKA','LK'),
(132,'LIBERIA','LR'),
(133,'LESOTHO','LS'),
(134,'LITHUANIA','LT'),
(135,'LUXEMBOURG','LU'),
(136,'LATVIA','LV'),
(137,'LIBYAN ARAB JAMAHIRIYA','LY'),
(138,'MOROCCO','MA'),
(139,'MONACO','MC'),
(140,'MOLDOVA, REPUBLIC OF','MD'),
(141,'MONTENEGRO','ME'),
(142,'SAINT MARTIN (''FRENCH PART'')','MF'),
(143,'MADAGASCAR','MG'),
(144,'MARSHALL ISLANDS','MH'),
(145,'MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF','MK'),
(146,'MALI','ML'),
(147,'MYANMAR','MM'),
(148,'MONGOLIA','MN'),
(149,'MACAO','MO'),
(150,'NORTHERN MARIANA ISLANDS','MP'),
(151,'MARTINIQUE','MQ'),
(152,'MAURITANIA','MR'),
(153,'MONTSERRAT','MS'),
(154,'MALTA','MT'),
(155,'MAURITIUS','MU'),
(156,'MALDIVES','MV'),
(157,'MALAWI','MW'),
(158,'MEXICO','MX'),
(159,'MALAYSIA','MY'),
(160,'MOZAMBIQUE','MZ'),
(161,'NAMIBIA','NA'),
(162,'NEW CALEDONIA','NC'),
(163,'NIGER','NE'),
(164,'NORFOLK ISLAND','NF'),
(165,'NIGERIA','NG'),
(166,'NICARAGUA','NI'),
(167,'NETHERLANDS','NL'),
(168,'NORWAY','NO'),
(169,'NEPAL','NP'),
(170,'NAURU','NR'),
(171,'NIUE','NU'),
(172,'NEW ZEALAND','NZ'),
(173,'OMAN','OM'),
(174,'PANAMA','PA'),
(175,'PERU','PE'),
(176,'FRENCH POLYNESIA','PF'),
(177,'PAPUA NEW GUINEA','PG'),
(178,'PHILIPPINES','PH'),
(179,'PAKISTAN','PK'),
(180,'POLAND','PL'),
(181,'SAINT PIERRE AND MIQUELON','PM'),
(182,'PITCAIRN','PN'),
(183,'PUERTO RICO','PR'),
(184,'PALESTINIAN TERRITORY, OCCUPIED','PS'),
(185,'PORTUGAL','PT'),
(186,'PALAU','PW'),
(187,'PARAGUAY','PY'),
(188,'QATAR','QA'),
(189,'REUNION','RE'),
(190,'ROMANIA','RO'),
(191,'SERBIA','RS'),
(192,'RUSSIAN FEDERATION','RU'),
(193,'RWANDA','RW'),
(194,'SAUDI ARABIA','SA'),
(195,'SOLOMON ISLANDS','SB'),
(196,'SEYCHELLES','SC'),
(197,'SUDAN','SD'),
(198,'SWEDEN','SE'),
(199,'SINGAPORE','SG'),
(200,'SAINT HELENA, ASCENSION AND TRISTAN DA CUNHA','SH'),
(201,'SLOVENIA','SI'),
(202,'SVALBARD AND JAN MAYEN','SJ'),
(203,'SLOVAKIA','SK'),
(204,'SIERRA LEONE','SL'),
(205,'SAN MARINO','SM'),
(206,'SENEGAL','SN'),
(207,'SOMALIA','SO'),
(208,'SURINAME','SR'),
(209,'SAO TOME AND PRINCIPE','ST'),
(210,'EL SALVADOR','SV'),
(211,'SINT MAARTEN (''DUTCH PART'')','SX'),
(212,'SYRIAN ARAB REPUBLIC','SY'),
(213,'SWAZILAND','SZ'),
(214,'TURKS AND CAICOS ISLANDS','TC'),
(215,'CHAD','TD'),
(216,'FRENCH SOUTHERN TERRITORIES','TF'),
(217,'TOGO','TG'),
(218,'THAILAND','TH'),
(219,'TAJIKISTAN','TJ'),
(220,'TOKELAU','TK'),
(221,'TIMOR-LESTE','TL'),
(222,'TURKMENISTAN','TM'),
(223,'TUNISIA','TN'),
(224,'TONGA','TO'),
(225,'TURKEY','TR'),
(226,'TRINIDAD AND TOBAGO','TT'),
(227,'TUVALU','TV'),
(228,'TAIWAN, PROVINCE OF CHINA','TW'),
(229,'TANZANIA, UNITED REPUBLIC OF','TZ'),
(230,'UKRAINE','UA'),
(231,'UGANDA','UG'),
(232,'UNITED STATES MINOR OUTLYING ISLANDS','UM'),
(233,'URUGUAY','UY'),
(234,'UZBEKISTAN','UZ'),
(235,'HOLY SEE (''VATICAN CITY STATE'')','VA'),
(236,'SAINT VINCENT AND THE GRENADINES','VC'),
(237,'VENEZUELA, BOLIVARIAN REPUBLIC OF','VE'),
(238,'VIRGIN ISLANDS, BRITISH','VG'),
(239,'VIRGIN ISLANDS, U.S.','VI'),
(240,'VIET NAM','VN'),
(241,'VANUATU','VU'),
(242,'WALLIS AND FUTUNA','WF'),
(243,'SAMOA','WS'),
(244,'YEMEN','YE'),
(245,'MAYOTTE','YT'),
(246,'SOUTH AFRICA','ZA'),
(247,'ZAMBIA','ZM'),
(248,'ZIMBABWE','ZW')


GO
