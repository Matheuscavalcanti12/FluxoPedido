using MySql.Data.MySqlClient;
create database OrderFlow;
use OrderFlow;

/*Tabelas = usuario,produto,pedido,pedido_item*/
/*Se o MySql estiver com senha, as strings de conexão devem incluir a senha*/

create table usuario(id int primary key auto_increment not null,
email varchar(150),
senha varchar(150),
role varchar(50));

create table produto(id_produto int primary key auto_increment  not null,
desc_produto varchar(200),
valor decimal(10,2),
imagem varchar(200),
marca varchar(200)
);

create table pedido(id_pedido int primary key auto_increment not null,
status varchar (150),
id_usuario int,
foreign key(id_usuario) references usuario(id));

create table pedido_item(id_pedido_item int primary key auto_increment not null,
pedido_id int,
produto_id int,
quantidade int,
foreign key(produto_id) references produto (id_produto),
foreign key (pedido_id) references pedido(id_pedido));
select * from produto;

INSERT INTO produto
(id_produto, desc_produto, valor, imagem)
VALUES
(2, 'Vestido Floral Lilás', 189.9, 'product1.jpg');

INSERT INTO produto
(id_produto, desc_produto, valor, imagem)
VALUES
(3, 'Suéter Tricô Marrom', 149.9, 'product2.jpg');

INSERT INTO produto
(id_produto, desc_produto, valor, imagem)
VALUES
(4, 'Calça Linho Bege', 129.9, 'product3.jpg');

INSERT INTO produto
(id_produto, desc_produto, valor, imagem)
VALUES
(5, 'Blusa Seda Lavanda', 169.9, 'product4.jpg');

INSERT INTO produto
(id_produto, desc_produto, valor, imagem)
VALUES
(6, 'Jaqueta Couro Caramelo', 259.9, 'product5.jpg');

INSERT INTO produto
(id_produto, desc_produto, valor, imagem)
VALUES
(7, 'Cachecol Cashmere Lilás', 89.9, 'product6.jpg');

INSERT INTO pedido (id_pedido)
VALUES (4);

select * from pedido;
select * from usuario;
select * from produto;

Alter table produto add column marca varchar (200);