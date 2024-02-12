-- Categories
insert into Category (Id, Name)
values (NEWID(), 'Plushies');
insert into Category (Id, Name)
values (NEWID(), 'Bags');
insert into Category (Id, Name)
values (NEWID(), 'Wallets');
insert into Category (Id, Name)
values (NEWID(), 'Figures');
-- Roles
insert into AspNetRoles (Id, Name, NormalizedName)
values (NEWID(), 'Admin', 'ADMIN');
insert into AspNetRoles (Id, Name, NormalizedName)
values (NEWID(), 'Client', 'CLIENT');
-- Products
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Vaporeon Mimikiu', 50.00, 50, (select Id from Category where Name = 'Plushies'),
        'https://pokemon-faction.com/cdn/shop/products/product-image-1714578092_480x.jpg?v=1626812080');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Flareon Mimikiu', 40.00, 50, (select Id from Category where Name = 'Plushies'),
        'https://pokemon-faction.com/cdn/shop/products/product-image-1804715633_480x.jpg?v=1627213674');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Jolteon Mimikiu', 60.00, 50, (select Id from Category where Name = 'Plushies'),
        'https://pokemon-faction.com/cdn/shop/products/product-image-1804718336_480x.jpg?v=1626812077');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Leafeon Mimikiu', 70.00, 50, (select Id from Category where Name = 'Plushies'),
        'https://pokemon-faction.com/cdn/shop/products/product-image-1714578097_480x.jpg?v=1627213876');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Glaceon Mimikiu', 30.00, 50, (select Id from Category where Name = 'Plushies'),
        'https://pokemon-faction.com/cdn/shop/products/product-image-1804715638_480x.jpg?v=1626812076');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Umbreon Mimikiu', 80.00, 50, (select Id from Category where Name = 'Plushies'),
        'https://pokemon-faction.com/cdn/shop/products/product-image-1804715637_480x.jpg?v=1626812080');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Espeon Mimikiu', 90.00, 50, (select Id from Category where Name = 'Plushies'),
        'https://pokemon-faction.com/cdn/shop/products/product-image-1714578094_480x.jpg?v=1626812078');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Sylveon Mimikiu', 10.00, 50, (select Id from Category where Name = 'Plushies'),
        'https://pokemon-faction.com/cdn/shop/products/product-image-1804697401_480x.jpg?v=1626812079');

insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Pokeball Bag', 75.00, 40, (select Id from Category where Name = 'Bags'),
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT3yNLmeHVBEDrlsRFn0WXA0OFmFou8WBkULc3INApw0SPFj4-O4_5-wwRWCFmorGOYAc8&usqp=CAU');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Pokeball Bag', 22.00, 20, (select Id from Category where Name = 'Bags'),
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQnHo86MIiby9KnrfXeT75TIiui9TYYsBGNTaI80ZuAJmXup8qrJGAyTNUCqTHcQg36hzM&usqp=CAU');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'Pokeball Bag', 124.00, 50, (select Id from Category where Name = 'Bags'),
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSxmk_skPglNXsONKVIgVfeZgqsfshoyEPGoFuo5Pm0JtEtodj-R1YaYmx9UTihhuzT2Vk&usqp=CAU');

insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'AA Wallet', 50.00, 50, (select Id from Category where Name = 'Wallets'),
        'https://www.pokemoncenter.com/images/DAMRoot/Full-Size/10000/P8557_710-95821_01.jpg');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'HHH Wallet', 80.00, 50, (select Id from Category where Name = 'Wallets'),
        'https://i5.walmartimages.com/asr/cef81da1-9108-4fa6-b6c8-c5c6b29eb80c.283f48d4bbf1eaa7ff16a90ed00da983.jpeg?odnHeight=612&odnWidth=612&odnBg=FFFFFF');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'ZZZ Wallet', 40.00, 50, (select Id from Category where Name = 'Wallets'),
        'https://target.scene7.com/is/image/Target/GUEST_649bd33d-1108-4aad-8ab4-35b0a94ce8d7?wid=800&hei=800&qlt=80&fmt=webp');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'BB Wallet', 150.00, 50, (select Id from Category where Name = 'Wallets'),
        'https://target.scene7.com/is/image/Target/GUEST_d341fb62-90eb-4d73-afdd-889cb14489b5?wid=800&hei=800&qlt=80&fmt=webp');

insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'SSS Figure', 200.00, 100, (select Id from Category where Name = 'Figures'),
        'https://www.pokemoncenter.com/images/DAMRoot/Full-Size/10000/P9073_710-96728_01.jpg');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'FFF Figure', 300.00, 100, (select Id from Category where Name = 'Figures'),
        'https://www.pokemoncenter.com/images/DAMRoot/Full-Size/10000/P9206_703-97872_01.jpg');
insert into Product (Id, Name, Price, Stock, CategoryId, Photo)
values (NEWID(), 'YYY Figure', 700.00, 100, (select Id from Category where Name = 'Figures'),
        'https://www.pokemoncenter.com/images/DAMRoot/Full-Size/10000/P6259_703-05967_01.jpg');



