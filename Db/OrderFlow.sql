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


INSERT INTO pedido (id_pedido)
VALUES (4);

select * from pedido;
select * from usuario;
select * from produto;

Alter table produto add column marca varchar (200);
ALTER TABLE produto MODIFY COLUMN imagem LONGTEXT;