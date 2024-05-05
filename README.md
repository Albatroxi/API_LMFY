#### Criação de usuário

Ao registrar um usuário.

```http
  POST /api/users/register
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `nome`           | `string`   | **Obrigatório**. Informar o nome do usuário               |
| `email`          | `string`   | **Obrigatório**. Informar o email - _É chave primária_    |
| `pssW`           | `string`   | **Obrigatório**. Informar a senha - _Mínimo 6 dígitos_    |
| `attribute`      | `integer`  | **Não Obrigatório**. Flag de definição do tipo de usuário |

Parâmetro - attribute - é definido o tipo de usuário para referencias as permições de ações de cada usuário:

* 0 - Alunos
* 1 - Professores
* 2 - Administradores
* 3 - Desenvolvedores

# Documentação da API

#### Breve descrição das bibliotecas utilizadas

| Informação                                 | Descrição                                                 |
| :------------------------------------------| :-------------------------------------------------------- |
| `Microsoft.EntityFrameworkCore`            | **Descrição 1**.                                          |
| `Microsoft.EntityFrameworkCore.Tools`      | **Descrição 2**.                                          |
| `Microsoft.EntityFrameworkCore.Design`     | **Descrição 3**.                                          |
| `Pomelo.EntityFrameworkCore.Mysql`         | **Descrição 4**.                                          |
| `System.Net.Mail`                          | **Descrição 5**.                                          |


