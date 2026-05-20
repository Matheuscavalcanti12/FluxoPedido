# 🚀 OrderFlow API

API REST para gerenciamento de pedidos com autenticação JWT e controle de acesso por roles. Senha com Hash, projeto EDUCATIVO full stack  em C#, mySql, .Net, Tsx

---

## 🧠 Projeto FullStack

* .NET (ASP.NET Core)
* MySQL
* JWT (Auth)
* Typescript  

---

## ⚙️ Funcionalidades

* Login com geração de token
* Senha com Hash
* Controle de acesso (`admin` / `user`)
* CRUD de produtos (admin)
* Criação e gerenciamento de pedidos

---

## 🔐 Autenticação

JWT com claims:

* `email`
* `role`

Exemplo:

```csharp
[Authorize(Roles = "admin")]
```

---

## 🗄️ Modelos

* `usuario`
* `produto`
* `pedido`
* `pedido_item`

---

## ▶️ Execução

```bash
git clone <repo>
git clone <repo Site_Lavanda>
rode primeiro dotnet run para iniciar o back end
depois rode o projeto Site_Lavanda com npm run dev para subir o projeto
```

---

## 🎯 Objetivo

Projeto educativo. Prática de:

* autenticação e autorização
* modelagem relacional
* construção de API REST
* Criação de Full stack 
