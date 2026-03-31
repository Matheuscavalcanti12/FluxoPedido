using MySql.Data.MySqlClient;
create database OrderFlow;
use OrderFlow;

/*Tabelas = usuario,produto,pedido,pedido_item*/

create table usuario(id int primary key auto_increment not null,
email varchar(150),
senha varchar(150),
role varchar(50));

create table produto(id_produto int primary key auto_increment  not null,
desc_produto varchar(200),
valor decimal(10,2)
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