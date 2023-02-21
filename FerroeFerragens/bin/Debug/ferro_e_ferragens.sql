create database FerroeFerragens;
use FerroeFerragens;

create table tabela_prod(id_prod int not null auto_increment primary key,
nome_prod varchar(75) not null, descr_prod varchar(350) not null,valor_unit decimal(7,2) not null,qtde_estoque int(5) not null,fornecedor varchar(75) not null);

INSERT INTO `ferroeferragens`.`tabela_prod` (`nome_prod`, `descr_prod`, `valor_unit`, `qtde_estoque`, `fornecedor`) VALUES ('Rolha', 'Tampa buraco', '12.50', '16', 'RolhasEmaisRolhas');
INSERT INTO tabela_prod(nome_prod,descr_prod,valor_unit,qtde_estoque,fornecedor) VALUES 
('Borca', 'Firma coisas', '0.75', '25', 'BorcasEparafusos'),
('Parafusos', 'prende coisas', '1.25', '65', 'BorcasEparafusos');


select * from tabela_prod;